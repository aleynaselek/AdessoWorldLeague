namespace Domain
{
    public class Group
    {
        public int Id { get; set; }
        public string GroupName { get; set; }

        public ICollection<GroupTeam> GroupTeams { get; set; }
    }


}
