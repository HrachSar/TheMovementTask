using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.DTOs;
using MyProject.Models;
using MyProject.Repositories;

namespace MyProject.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _repo;
        private readonly IMapper _mapper;
        private readonly AppDbContext _context;

        public CategoryService(ICategoryRepository repo, IMapper mapper, AppDbContext context)
        {
            _repo = repo;
            _mapper = mapper;
            _context = context;
        }

        public async Task<IEnumerable<CategoryReadDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            var categories = await _repo.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<CategoryReadDTO>>(categories);
        }

        public async Task<CategoryReadDTO?> GetByIdAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return null;
            return _mapper.Map<CategoryReadDTO>(category);
        }

        public async Task<CategoryReadDTO> CreateAsync(CategoryCreateDTO dto)
        {
            var category = _mapper.Map<Category>(dto);
            await _repo.AddAsync(category);
            await _repo.SaveChangesAsync();
            return _mapper.Map<CategoryReadDTO>(category);
        }

        public async Task<bool> UpdateAsync(int id, CategoryUpdateDTO dto)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return false;

            _mapper.Map(dto, category);
            _repo.Update(category);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var category = await _repo.GetByIdAsync(id);
            if (category == null) return false;

            _repo.Delete(category);
            return await _repo.SaveChangesAsync();
        }
        public async Task<PagedResult<CategoryReadDTO>> GetCategoriesAsync(int pageNumber, int pageSize)
        {

            var query = _context.Categories.AsNoTracking();

            var totalCount = await query.CountAsync();

            var categories = await query
                .OrderBy(c => c.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(c => new CategoryReadDTO()
                {
                    Id = c.Id,
                    Name = c.Name
                })
                .ToListAsync();

            return new PagedResult<CategoryReadDTO>
            {
                Items = categories,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
    }
        
}