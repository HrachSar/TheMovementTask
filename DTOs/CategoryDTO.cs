namespace MyProject.DTOs
{
    public class CategoryCreateDTO
    {
        public string Name { get; set; } = null!;
    }

    public class CategoryReadDTO
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }

    public class CategoryUpdateDTO
    {
        public string Name { get; set; } = null!;
    }
}