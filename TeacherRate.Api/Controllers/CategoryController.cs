using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using TeacherRate.Api.DTOs;
using TeacherRate.Api.Models.Paging;
using TeacherRate.Api.Models.Requests;
using TeacherRate.Api.Services;
using TeacherRate.Domain.Interfaces;
using TeacherRate.Domain.Models;

namespace TeacherRate.Api.Controllers;

[Route("api/category")]
[ApiController]
public class CategoryController : ControllerBase
{
    private readonly ICategoryService _categoryService;
    private readonly IMapper _mapper;

    public CategoryController(ICategoryService categoryService, IMapper mapper)
    {
        _categoryService = categoryService;
        _mapper = mapper;
    }

    [HttpGet]
    public async Task<ActionResult<PagedList<TaskCategoryDTO>>> GetCategories(
        [FromQuery] PageRequest pageRequest)
    {
        var categories = _categoryService.GetCategories();

        var page = categories.ToPagedList(pageRequest.Page, pageRequest.Size);

        return Ok(page.Map<TaskCategoryDTO>(_mapper));
    }

    [HttpGet("{id}")]
    public async Task<ActionResult<TaskCategoryDTO>> GetCategoryById(int id)
    {
        var category = await _categoryService.GetCategoryById(id);

        if(category == null)
            return NotFound(nameof(category));

        return Ok(_mapper.Map<TaskCategoryDTO>(category));
    }

    [HttpPost, Authorize(Roles = "Admin")]
    public async Task<ActionResult<TaskCategoryDTO>> AddCategory(CreateCategoryRequest request)
    {
        var category = new TaskCategory() { Name = request.Name };
        var categoryFromDb = await _categoryService.AddCategory(category);
        return Ok(_mapper.Map<TaskCategoryDTO>(categoryFromDb));
    }
}
