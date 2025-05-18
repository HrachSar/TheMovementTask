
namespace MyProject.DTOs
{
    public class ProductCreateDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }

    public class ProductReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<CategoryReadDTO> Categories { get; set; } = new List<CategoryReadDTO>();
    }

    public class ProductUpdateDTO
    {
        public string Name { get; set; } = null!;
        public string? Description { get; set; }
        public List<int> CategoryIds { get; set; } = new List<int>();
    }
}