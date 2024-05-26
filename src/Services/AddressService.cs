using AutoMapper;
using BackendTeamwork.Abstractions;
using BackendTeamwork.DTOs;
using BackendTeamwork.Entities;

namespace BackendTeamwork.Services
{
    public class AddressService : IAddressService
    {
        private IAddressRepository _addressRepository;
        private IMapper _mapper;

        public AddressService(IAddressRepository addressRepository, IMapper mapper)
        {
            _addressRepository = addressRepository;
            _mapper = mapper;
        }

        public async Task<AddressReadDto?> FindOne(Guid addressId)
        {
            return _mapper.Map<AddressReadDto>(await _addressRepository.FindOne(addressId));
        }

        public async Task<AddressReadDto> CreateOne(AddressCreateDto newAddress)
        {
            return _mapper.Map<AddressReadDto>(await _addressRepository.CreateOne(_mapper.Map<Address>(newAddress)));
        }

        public async Task<AddressReadDto?> UpdateOne(Guid addressId, AddressUpdateDto updatedAddress)
        {
            Address? oldAddress = await _addressRepository.FindOneById(addressId);
            if (oldAddress is null)
            {
                return null;
            }
            Address address = _mapper.Map<Address>(updatedAddress);
            address.Id = addressId;
            return _mapper.Map<AddressReadDto>(await _addressRepository.UpdateOne(address));
        }


    }
}