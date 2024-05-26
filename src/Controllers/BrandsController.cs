using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamwork.Controllers
{
    public class BrandsController : BaseController
    {
        private IBrandService _brandService;

        public BrandsController(IBrandService brandService)
        {
            _brandService = brandService;
        }

        [HttpGet]

        [ProducesResponseType(StatusCodes.Status200OK)]
        public ActionResult<IEnumerable<BrandReadDto>> FindMany([FromQuery(Name = "limit")] int limit, [FromQuery(Name = "offset")] int offset)
        {
            return Ok(_brandService.FindMany(limit, offset));
        }

        [HttpGet("{brandId}")]

        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BrandReadDto>> FindOne(Guid brandId)
        {
            BrandReadDto? brand = await _brandService.FindOne(brandId);
            if (brand is not null)
            {
                return Ok(brand);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<BrandReadDto>> CreateOne(BrandCreateDto newBrand)
        {
            return Ok(await _brandService.CreateOne(newBrand));
        }

        [HttpPut("{brandId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BrandReadDto?>> UpdateOne(Guid brandId, BrandUpdateDto updateBrand)
        {
            BrandReadDto? targetBrand = await _brandService.UpdateOne(brandId, updateBrand);
            if (targetBrand is not null)
            {
                return Ok(targetBrand);
            }
            return NotFound();
        }

        [HttpDelete("{brandId}")]
        [Authorize(Roles = "Admin")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<BrandReadDto>> DeleteOne(Guid brandId)
        {
            BrandReadDto? deletedBrand = await _brandService.DeleteOne(brandId);
            if (deletedBrand is not null)
            {
                return Ok(deletedBrand);
            }
            return NotFound();
        }
    }
}