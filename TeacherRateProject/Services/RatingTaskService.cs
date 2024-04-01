using AutoMapper;
using TeacherRateProject.Data.UnitOfWork;
using TeacherRateProject.DTOs;
using TeacherRateProject.Models;
using TeacherRateProject.Services.Interfaces;

namespace TeacherRateProject.Services;

public class RatingTaskService : IRatingTaskService<RatingTaskDto, int>
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;

    public RatingTaskService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<RatingTaskDto> Add(RatingTaskDto task)
    {
        var taskFromDb = await _unitOfWork.RatingTask.Add(
            _mapper.Map<RatingTask>(task));
        _unitOfWork.Save();

        return _mapper.Map<RatingTaskDto>(taskFromDb);
    }

    public async Task<IEnumerable<RatingTaskDto>> GetAll()
    {
        return (await _unitOfWork.RatingTask.GetAll())
            .Select(_mapper.Map<RatingTaskDto>);
    }

    public async Task<RatingTaskDto> GetById(int id)
    {
        return _mapper.Map<RatingTaskDto>(await _unitOfWork.RatingTask.GetById(id));
    }
}
