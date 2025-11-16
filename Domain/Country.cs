namespace Domain
{
    public class Country
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public ICollection<Team> Teams { get; set; }
    }
}
