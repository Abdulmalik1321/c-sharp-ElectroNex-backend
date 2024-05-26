using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamwork.Controllers
{
    public class CategoriesController : BaseController
    {
        private ICategoryService _categoryService;

        public CategoriesController(ICategoryService categoryService)
        {
            _categoryService = categoryService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<CategoryReadDto>> FindMany([FromQuery(Name = "limit")] int limit, [FromQuery(Name = "offset")] int offset)
        {
            return Ok(_categoryService.FindMany(limit, offset));
        }

        [HttpGet("{categoryId}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryReadDto>> FindOne(Guid categoryId)
        {
            CategoryReadDto? category = await _categoryService.FindOne(categoryId);
            if (category is not null)
            {
                return Ok(category);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<CategoryReadDto>> CreateOne(CategoryCreateDto newCategory)
        {
            return Ok(await _categoryService.CreateOne(newCategory));
        }

        [HttpPut("{categoryId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryReadDto?>> UpdateOne(Guid categoryId, CategoryUpdateDto updateCategory)
        {
            CategoryReadDto? targetCategory = await _categoryService.UpdateOne(categoryId, updateCategory);
            if (targetCategory is not null)
            {
                return Ok(targetCategory);
            }
            return NotFound();
        }

        [HttpDelete("{categoryId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<CategoryReadDto>> DeleteOne(Guid categoryId)
        {
            CategoryReadDto? deletedCategory = await _categoryService.DeleteOne(categoryId);
            if (deletedCategory is not null)
            {
                return Ok(deletedCategory);
            }
            return NotFound();
        }
    }
}