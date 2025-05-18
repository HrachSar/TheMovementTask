using System.Collections.Generic;
using System.Threading.Tasks;
using MyProject.DTOs;

namespace MyProject.Services
{
    public interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDTO>> GetAllAsync(int pageNumber, int pageSize);
        Task<CategoryReadDTO?> GetByIdAsync(int id);
        Task<CategoryReadDTO> CreateAsync(CategoryCreateDTO dto);
        Task<bool> UpdateAsync(int id, CategoryUpdateDTO dto);
        Task<bool> DeleteAsync(int id);
        Task<PagedResult<CategoryReadDTO>> GetCategoriesAsync(int pageNumber, int pageSize);
    }
}