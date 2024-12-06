using ExpenseTracker.Data;
using ExpenseTracker.Server.DTOs;
using ExpenseTracker.Server.Models;
using AutoMapper;
using Microsoft.EntityFrameworkCore;

namespace ExpenseTracker.Server.Services
{

    public interface ICategoryService
    {
        Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync(int userId);  
        Task<CategoryReadDto> GetCategoryByIdAsync(int id, int userId); 
        Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto createDto, int userId); 
        Task<CategoryReadDto> UpdateCategoryAsync(int id, CategoryUpdateDto updateDto, int userId); 
        Task DeleteCategoryAsync(int id, int userId); 
    }
    public class CategoryService : ICategoryService
    {
        private readonly ExpenseTrackerDbContext _context;
        private readonly IMapper _mapper;

        public CategoryService(ExpenseTrackerDbContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<CategoryReadDto>> GetAllCategoriesAsync(int userId)
        {
            var categories = await _context.Categories
                .Where(c => c.UserId == userId)
                .ToListAsync();

            return _mapper.Map<IEnumerable<CategoryReadDto>>(categories);
        }

        public async Task<CategoryReadDto> GetCategoryByIdAsync(int id, int userId)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id && c.UserId == userId)
                .FirstOrDefaultAsync();

            if (category == null) throw new KeyNotFoundException("Category not found.");

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto> CreateCategoryAsync(CategoryCreateDto createDto, int userId)
        {
            var category = _mapper.Map<Category>(createDto);
            category.UserId = userId;  

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task<CategoryReadDto> UpdateCategoryAsync(int id, CategoryUpdateDto updateDto, int userId)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id && c.UserId == userId)
                .FirstOrDefaultAsync();

            if (category == null) throw new KeyNotFoundException("Category not found.");

            _mapper.Map(updateDto, category);

            _context.Categories.Update(category);
            await _context.SaveChangesAsync();

            return _mapper.Map<CategoryReadDto>(category);
        }

        public async Task DeleteCategoryAsync(int id, int userId)
        {
            var category = await _context.Categories
                .Where(c => c.Id == id && c.UserId == userId)
                .FirstOrDefaultAsync();

            if (category == null) throw new KeyNotFoundException("Category not found.");

            _context.Categories.Remove(category);
            await _context.SaveChangesAsync();
        }
    }
}
