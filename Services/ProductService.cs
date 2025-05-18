using AutoMapper;
using Microsoft.EntityFrameworkCore;
using MyProject.Data;
using MyProject.DTOs;
using MyProject.Models;
using MyProject.Repositories;

namespace MyProject.Services
{
    public class ProductService : IProductService
    {
        private readonly IProductRepository _repo;
        private readonly ICategoryRepository _categoryRepo;
        private readonly ILogger<ProductService> _logger;
        private readonly AppDbContext _context;
        private readonly IMapper _mapper;

        public ProductService(IProductRepository repo, ICategoryRepository categoryRepo, IMapper mapper, ILogger<ProductService> logger, AppDbContext context)
        {
            _repo = repo;
            _categoryRepo = categoryRepo;
            _mapper = mapper;
            _logger = logger;
            _context = context;
        }

        public async Task<IEnumerable<ProductReadDTO>> GetAllAsync(int pageNumber, int pageSize)
        {
            
            _logger.LogInformation("Fetching products...");
            var products = await _repo.GetAllAsync(pageNumber, pageSize);
            return _mapper.Map<IEnumerable<ProductReadDTO>>(products);
        }

        public async Task<ProductReadDTO?> GetByIdAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return null;
            return _mapper.Map<ProductReadDTO>(product);
        }

        public async Task<ProductReadDTO> CreateAsync(ProductCreateDTO dto)
        {
            var product = _mapper.Map<Product>(dto);

            // Link categories
            product.ProductCategories = new List<ProductCategory>();
            foreach (var catId in dto.CategoryIds.Distinct())
            {
                var category = await _categoryRepo.GetByIdAsync(catId);
                if (category == null)
                    throw new KeyNotFoundException($"Category with id {catId} not found.");

                product.ProductCategories.Add(new ProductCategory
                {
                    CategoryId = catId,
                    Product = product
                });
            }

            await _repo.AddAsync(product);
            await _repo.SaveChangesAsync();

            return _mapper.Map<ProductReadDTO>(product);
        }

        public async Task<bool> UpdateAsync(int id, ProductUpdateDTO dto)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return false;

            _mapper.Map(dto, product);

            // Update categories
            product.ProductCategories.Clear();

            foreach (var catId in dto.CategoryIds.Distinct())
            {
                var category = await _categoryRepo.GetByIdAsync(catId);
                if (category == null)
                    throw new KeyNotFoundException($"Category with id {catId} not found.");

                product.ProductCategories.Add(new ProductCategory
                {
                    CategoryId = catId,
                    ProductId = id,
                    Product = product
                });
            }

            _repo.Update(product);
            return await _repo.SaveChangesAsync();
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var product = await _repo.GetByIdAsync(id);
            if (product == null) return false;

            _repo.Delete(product);
            return await _repo.SaveChangesAsync();
        }
        public async Task<PagedResult<ProductReadDTO>> GetProductsAsync(int pageNumber, int pageSize)
        {
            var query = _context.Products
                .Include(p => p.ProductCategories)
                .ThenInclude(pc => pc.Category)
                .AsNoTracking();

            var totalCount = await query.CountAsync();

            var products = await query
                .OrderBy(p => p.Name)
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize)
                .Select(p => new ProductReadDTO
                {
                    Id = p.Id,
                    Name = p.Name,
                    Categories = p.ProductCategories
                        .Select(pc => new CategoryReadDTO
                        {
                            Id = pc.Category.Id,
                            Name = pc.Category.Name
                        }).ToList()
                })
                .ToListAsync();

            return new PagedResult<ProductReadDTO>
            {
                Items = products,
                TotalCount = totalCount,
                PageNumber = pageNumber,
                PageSize = pageSize
            };
        }
        public async Task<bool> UpdateProductAsync(int productId, ProductUpdateDTO dto)
        {
            var product = await _context.Products
                .Include(p => p.ProductCategories)
                .FirstOrDefaultAsync(p => p.Id == productId);

            if (product == null) return false;

            product.Name = dto.Name;
            
            _context.ProductCategories.RemoveRange(product.ProductCategories);
            
            product.ProductCategories = dto.CategoryIds
                .Select(cid => new ProductCategory
                {
                    ProductId = product.Id,
                    CategoryId = cid
                }).ToList();

            await _context.SaveChangesAsync();
            return true;
        }


    }
}
