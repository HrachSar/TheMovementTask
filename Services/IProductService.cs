using MyProject.DTOs;

namespace MyProject.Services
{
    public interface IProductService
    {
        Task<IEnumerable<ProductReadDTO>> GetAllAsync(int pageNumber, int pageSize);
        Task<ProductReadDTO?> GetByIdAsync(int id);
        Task<ProductReadDTO> CreateAsync(ProductCreateDTO dto);
        Task<bool> UpdateAsync(int id, ProductUpdateDTO dto);
        Task<PagedResult<ProductReadDTO>> GetProductsAsync(int pageNumber, int pageSize);
        Task<bool> UpdateProductAsync(int productId, ProductUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
    }
}