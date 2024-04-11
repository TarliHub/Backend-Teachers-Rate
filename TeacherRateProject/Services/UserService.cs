using AutoMapper;
using TeacherRateProject.Data.UnitOfWork;
using TeacherRateProject.DTOs;
using TeacherRateProject.Models;
using TeacherRateProject.Models.Paging;
using TeacherRateProject.Services.Interfaces;

namespace TeacherRateProject.Services
{
    public class UserService : IUserService<UserDto, int>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public UserService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<UserDto> Add(UserDto user)
        {
            if (!IsUserDtoValid(user))
                throw new ArgumentException(nameof(user), "invalid data in a model");

            var userFromDB = await _unitOfWork.User.Add(_mapper.Map<User>(user));
            _unitOfWork.Save();

            return _mapper.Map<UserDto>(userFromDB);
        }

        public async Task<CompletedTaskDto> AddTaskToUser(int userId, PostCompletedTaskDto task)
        {
            var user = await _unitOfWork.User.GetById(userId);
            var ratingTask = await _unitOfWork.RatingTask.GetById(task.TaskId);

            var completedTask = new CompletedTask()
            {
                Task = ratingTask,
                ActualRating = task.ActualRating,
            };

            await _unitOfWork.CompletedTask.Add(completedTask);

            user.Tasks.Add(completedTask);

            await _unitOfWork.User.Update(user.Id, user);
            _unitOfWork.Save();

            return _mapper.Map<CompletedTaskDto>(completedTask);
        }

        public async Task Delete(int id)
        {
            await _unitOfWork.User.Delete(id);
            _unitOfWork?.Save();
        }

        public async Task<IEnumerable<UserDto>> GetAll()
        {
            return (await _unitOfWork.User.GetAll())
                .Select(_mapper.Map<UserDto>);
        }

        public async Task<UserDto> GetById(int id)
        {
            return _mapper.Map<UserDto>(await _unitOfWork.User.GetById(id));
        }

        public async Task<PageList<UserDto>> GetPaged(int page, int pageSize)
        {
            var pageList = await _unitOfWork.User.GetPaged(page, pageSize);
            return new(){
                Items = new List<UserDto>(pageList.Items.Select(_mapper.Map<UserDto>)),
                PagesCount = pageList.PagesCount,
                PageIndex = pageList.PageIndex,
                PageSize = pageSize,
            };
        }

        public async Task<IEnumerable<CompletedTaskDto>> GetUserTasks(int userId)
        {
            var user = await _unitOfWork.User.GetById(userId);

            return user.Tasks.Select(_mapper.Map<CompletedTaskDto>);
        }

        public bool IsUserDtoValid(UserDto user)
        {
            return true;
        }

        public bool IsUserValid(User user)
        {
            return true;
        }

        public Task<IEnumerable<UserDto>> Query(Predicate<UserDto> predicate)
        {
            throw new NotImplementedException();
        }

        public async Task Update(int id, UserDto user)
        {
            await _unitOfWork.User.Update(id, _mapper.Map<User>(user));
        }
    }
}
