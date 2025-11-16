using Application.DTOs;
using Application.Services;
using Domain;
using Microsoft.EntityFrameworkCore;

public class GroupDrawService : IGroupDrawService
{
    private readonly AdessoWorldLeagueDbContext _context;
    private readonly Random _random = new Random();

    public GroupDrawService(AdessoWorldLeagueDbContext context)
    {
        _context = context;
    }

    public async Task<DrawResultDto> RunDrawAsync(DrawRequestDto request)
    {
        // ---------------------------
        // 1) Validation
        // ---------------------------
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.GroupCount != 4 && request.GroupCount != 8)
            throw new ArgumentException("Group count must be 4 or 8.");

        // ---------------------------
        // 2) Load teams with countries
        // ---------------------------
        var teams = await _context.Teams
            .Include(t => t.Country)
            .ToListAsync();

        if (!teams.Any())
            throw new InvalidOperationException("No teams found in the database.");

        // Fast lookup dictionary for later DTO build
        var teamsById = teams.ToDictionary(t => t.Id);

        // ---------------------------
        // 3) Create Draw + Groups
        // ---------------------------
        var draw = new Draw
        {
            DrawerName = request.DrawerName,
            CreatedAt = DateTime.Now,
            Groups = new List<Group>()
        };

        for (int i = 0; i < request.GroupCount; i++)
        {
            char letter = (char)('A' + i);

            draw.Groups.Add(new Group
            {
                GroupName = letter.ToString(),
                GroupTeams = new List<GroupTeam>()
            });
        }

        var groups = draw.Groups.ToList();

        // ---------------------------
        // 4) GLOBAL team pool 
        //    (A selected team is removed from the entire tournament)
        // ---------------------------
        var rng = new Random();
        var globalPool = teams.OrderBy(t => rng.Next()).ToList();

        // ---------------------------
        // 5) Per-group country filters
        //    (Each group cannot pick same country twice)
        // ---------------------------
        var usedCountriesPerGroup = groups.ToDictionary(
            g => g,
            g => new HashSet<int>() // country IDs
        );

        // Teams per group = 32 / n
        int teamsPerGroup = teams.Count / request.GroupCount;

        // ---------------------------
        // 6) ROUND-BASED DRAW
        //    Round 1: A,B,C,D each take 1 team
        //    Round 2: A,B,C,D each take 2nd team 
        // ---------------------------
        for (int round = 0; round < teamsPerGroup; round++)
        {
            foreach (var group in groups)
            {
                // 6.1 Filter global pool by country rule
                var filtered = globalPool
                    .Where(t => !usedCountriesPerGroup[group].Contains(t.CountryId))
                    .ToList();

                // 6.2 If no team satisfies country rule -> fallback (must pick someone)
                if (!filtered.Any())
                    filtered = globalPool;

                // 6.3 Pick a random team
                var selected = filtered[rng.Next(filtered.Count)];

                // 6.4 Add to group
                group.GroupTeams.Add(new GroupTeam
                {
                    TeamId = selected.Id
                });

                // 6.5 Mark this country as used in this group
                usedCountriesPerGroup[group].Add(selected.CountryId);

                // 6.6 Remove team globally -> CANNOT appear in ANY other group
                globalPool.Remove(selected);
            }
        }

        // ---------------------------
        // 7) Save to database
        // ---------------------------
        await _context.Draws.AddAsync(draw);
        await _context.SaveChangesAsync();

        // ---------------------------
        // 8) Build clean response DTO
        // ---------------------------
        return new DrawResultDto
        {
            Drawer = draw.DrawerName,
            Date = draw.CreatedAt,
            Groups = draw.Groups.Select(g => new GroupDto
            {
                GroupName = g.GroupName,
                Teams = g.GroupTeams.Select(gt =>
                {
                    var t = teamsById[gt.TeamId];
                    return new TeamDto
                    {
                        Id = t.Id,
                        Name = t.Name,
                        CountryName = t.Country.Name
                    };
                }).ToList()
            }).ToList()
        };
    }

}
