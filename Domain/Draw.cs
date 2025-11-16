namespace Domain
{
    public class Draw
    {
        public int Id { get; set; }

        public string DrawerName { get; set; }
        public DateTime CreatedAt { get; set; }

        public ICollection<Group> Groups { get; set; }
    }


}
