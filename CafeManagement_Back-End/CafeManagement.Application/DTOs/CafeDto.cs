namespace CafeManagement.Application.DTOs
{
    public class CafeDto
    {
        public Guid? Id { get; set; } = null;
        public string Name { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Logo { get; set; }
        public int? Employees { get; set; } = 0;
    }
}
