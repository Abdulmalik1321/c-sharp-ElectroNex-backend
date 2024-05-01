using BackendTeamwork.Entities;

namespace BackendTeamwork.Abstractions
{
    public interface IAddressRepository
    {

        public Task<Address?> FindOne(Guid addressId);
        public Task<Address> CreateOne(Address newAddress);
        public Task<Address> UpdateOne(Guid addressId, Address updatedAddress);

    }
}