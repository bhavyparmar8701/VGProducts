using StackExchange.Redis;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class AddressServices : IAddressServices
    {
        private readonly IAddressBusiness addressBusiness;

        public AddressServices(IAddressBusiness addressBusiness)
        {
            this.addressBusiness = addressBusiness;
        }

        public async Task<AddressDto> AddAddressAsync(AddAddressDto addAddressDto)
        {
            return await addressBusiness.AddAddressAsync(addAddressDto);
        }

        public async Task<string> DeleteAddressAsync(Guid id, Guid userId)
        {
            return await addressBusiness.DeleteAddressAsync(id, userId);
        }

        public async Task<AddressWithUserDto> GetAddressByIdAsync(Guid addressId, Guid userId)
        {
            return await addressBusiness.GetAddressByIdAsync(addressId, userId);
        }

        public async Task<List<AddressWithUserDto>> GetAllAddressAsync(Guid userId)
        {
            return await addressBusiness.GetAllAddressAsync(userId);
        }

        public async Task<AddressDto> UpdateAddressAsync(Guid addressId, UpdateAddressDto updateAddressDto , Guid userId)
        {
            return await addressBusiness.UpdateAddressAsync(addressId, updateAddressDto, userId);
        }
    }
}
