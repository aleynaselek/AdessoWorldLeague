namespace Application.DTOs
{
    public class DrawResultDto
    {
        public string Drawer { get; set; }
        public DateTime Date { get; set; }
        public List<GroupDto> Groups { get; set; } = new();
    }

}
