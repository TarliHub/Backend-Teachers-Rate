using AutoMapper;
using TeacherRateProject.Data.Repository.Interfaces;
using TeacherRateProject.Data.UnitOfWork;
using TeacherRateProject.DTOs;
using TeacherRateProject.Models;
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

        public Task<IEnumerable<UserDto>> GetPaged(int page, int pageSize)
        {
            throw new NotImplementedException();
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

        public Task Update(int id, UserDto user)
        {
            throw new NotImplementedException();
        }
    }
}
