using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace BackendTeamwork.Controllers
{
    public class AddressesController : BaseController
    {
        private IAddressService _AddressService;
        public AddressesController(IAddressService AddressService)
        {
            _AddressService = AddressService;
        }

        [HttpGet("{addressId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AddressReadDto>> FindOne(Guid addressId)
        {
            AddressReadDto? address = await _AddressService.FindOne(addressId);
            if (address is not null)
            {
                return Ok(address);
            }
            return NotFound();
        }

        [HttpPost]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        public async Task<ActionResult<AddressReadDto>> CreateOne([FromBody] AddressCreateDto newAddress)
        {
            return Ok(await _AddressService.CreateOne(newAddress));
        }

        [HttpPut("{addressId}")]
        [Authorize(Roles = "Admin, Customer")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<AddressReadDto>> UpdateOne(Guid addressId, AddressUpdateDto updatedAddress)
        {
            AddressReadDto? updated = await _AddressService.UpdateOne(addressId, updatedAddress);
            if (updated is not null)
            {
                return Ok(updated);
            }
            return NotFound();
        }
    }
}