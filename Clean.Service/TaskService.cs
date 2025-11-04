using AutoMapper;
using Clean.Core.DTOs;
using Clean.Core.Entities;
using Clean.Core.Repositories;
using Clean.Core.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Clean.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository _taskRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IMapper _mapper;

        public TaskService(ITaskRepository taskRepository, ICategoryRepository categoryRepository, IMapper mapper)
        {
            _taskRepository = taskRepository;
            _categoryRepository = categoryRepository;
            _mapper = mapper;
        }

        public async Task<TaskDTO?> GetTaskByIdAsync(int id)
        {
            var task = await _taskRepository.GetByIdAsync(id);
            return task != null ? _mapper.Map<TaskDTO>(task) : null;
        }

        public async Task<IEnumerable<TaskDTO>> GetTasksByUserIdAsync(int userId)
        {
            var tasks = await _taskRepository.GetByUserIdAsync(userId);
            return _mapper.Map<IEnumerable<TaskDTO>>(tasks);
        }

        public async Task<TaskDTO> CreateTaskAsync(TaskInputDTO taskInputDto, int userId)
        {
            // בדיקה שהקטגוריה שייכת למשתמש
            var category = await _categoryRepository.GetByIdAsync(taskInputDto.CategoryId);
            if (category == null || category.UserId != userId)
                throw new UnauthorizedAccessException("Category not found or access denied");

            var task = _mapper.Map<TaskItem>(taskInputDto);
            task.UserId = userId;
            
            var createdTask = await _taskRepository.AddAsync(task);
            return _mapper.Map<TaskDTO>(createdTask);
        }

        public async Task<TaskDTO> UpdateTaskAsync(int id, TaskInputDTO taskInputDto, int userId)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null || existingTask.UserId != userId)
                throw new UnauthorizedAccessException("Task not found or access denied");

            // בדיקה שהקטגוריה החדשה שייכת למשתמש
            if (taskInputDto.CategoryId != existingTask.CategoryId)
            {
                var category = await _categoryRepository.GetByIdAsync(taskInputDto.CategoryId);
                if (category == null || category.UserId != userId)
                    throw new UnauthorizedAccessException("Category not found or access denied");
            }

            _mapper.Map(taskInputDto, existingTask);
            existingTask.Id = id;

            var updatedTask = await _taskRepository.UpdateAsync(existingTask);
            return _mapper.Map<TaskDTO>(updatedTask);
        }

        public async Task DeleteTaskAsync(int id, int userId)
        {
            var existingTask = await _taskRepository.GetByIdAsync(id);
            if (existingTask == null || existingTask.UserId != userId)
                throw new UnauthorizedAccessException("Task not found or access denied");

            await _taskRepository.DeleteAsync(id);
        }
    }
}