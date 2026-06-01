using AutoMapper;
using TaskHub.Application.DTO.TaskItem;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Services.TaskService
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository<TaskItem> _taskHubRepository;
        private readonly IMapper _mapper;

        public TaskService(
            ITaskRepository<TaskItem> taskRepository,
            IMapper mapper)
        {
            _taskHubRepository = taskRepository;
            _mapper = mapper;
        }

        public async Task<TaskItemDTO> AddAsync(Guid userId, TaskCreateDTO item)
        {
            var entity = _mapper.Map<TaskItem>(item);
            entity.UserId = userId;

            var added = await _taskHubRepository.AddAsync(userId, entity);

            return _mapper.Map<TaskItemDTO>(added);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await _taskHubRepository.DeleteAsync(id);
        }

        public async Task<IEnumerable<TaskItemDTO>> GetAllByUserIdAsync(Guid userId)
        {
            var tasks = await _taskHubRepository.GetAllByUserIdAsync(userId);

            return _mapper.Map<IEnumerable<TaskItemDTO>>(tasks);
        }

        public async Task<TaskItemDTO> GetByIdAsync(Guid id)
        {
            var task = await _taskHubRepository.GetByIdAsync(id);
            return _mapper.Map<TaskItemDTO>(task);
        }

        public async Task<int> UpdateAsync(Guid id, TaskCreateDTO item)
        {
            var updateEntity = _mapper.Map<TaskItem>(item);
            return await _taskHubRepository.UpdateAsync(id, updateEntity);
        }
    }
}
