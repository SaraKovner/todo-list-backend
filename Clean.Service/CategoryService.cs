using AutoMapper;
using Clean.Core.DTOs;
using Clean.Core.Entities;
using Clean.Core.Repositories;
using Clean.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean.Service
{
    public class CategoryService : ICategoryService
    {
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public CategoryService(ICategoryRepository categoryRepository, IMapper mapper)
        {
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id, int userId)
        {
            var category = await _categoryRepository.GetByIdAsync(id);
            if (category == null || category.UserId != userId)
                return null;
            return _mapper.Map<CategoryDTO>(category);
        }

        public async Task<IEnumerable<CategoryDTO>> GetCategoriesByUserIdAsync(int userId)
        {
            var categories = await _categoryRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO categoryDto, int userId)
        {
            var category = _mapper.Map<Category>(categoryDto);
            category.UserId = userId;
            var createdCategory = await _categoryRepository.AddAsync(category);
            return _mapper.Map<CategoryDTO>(createdCategory);
        }

        public async Task<CategoryDTO> UpdateCategoryAsync(int id, CategoryDTO categoryDto, int userId)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null || existingCategory.UserId != userId)
                throw new UnauthorizedAccessException("Category not found or access denied");

            _mapper.Map(categoryDto, existingCategory);
            existingCategory.Id = id;
            existingCategory.UserId = userId;

            var updatedCategory = await _categoryRepository.UpdateAsync(existingCategory);
            return _mapper.Map<CategoryDTO>(updatedCategory);
        }

        public async Task DeleteCategoryAsync(int id, int userId)
        {
            var existingCategory = await _categoryRepository.GetByIdAsync(id);
            if (existingCategory == null || existingCategory.UserId != userId)
                throw new UnauthorizedAccessException("Category not found or access denied");

            await _categoryRepository.DeleteAsync(id);
        }
    }
}