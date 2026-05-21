using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Repository.Interface;

namespace VGProducts.Business.Implementation
{
    public class AddressBusiness : IAddressBusiness
    {
        private readonly IAddressRepository addressRepository;

        public AddressBusiness(IAddressRepository addressRepository)
        {
            this.addressRepository = addressRepository;
        }

        public async Task<AddressDto> AddAddressAsync(AddAddressDto addAddressDto)
        {
            return await addressRepository.AddAddressAsync(addAddressDto);
        }

        public async Task<string> DeleteAddressAsync(Guid id, Guid userId)
        {
            return await addressRepository.DeleteAddressAsync(id, userId);
        }

        public async Task<AddressWithUserDto> GetAddressByIdAsync(Guid addressId, Guid userId)
        {
            return await addressRepository.GetAddressByIdAsync(addressId, userId);
        }

        public async Task<List<AddressWithUserDto>> GetAllAddressAsync(Guid userId)
        {
            return await addressRepository.GetAllAddressAsync(userId);
        }

        public async Task<AddressDto> UpdateAddressAsync(Guid addressId, UpdateAddressDto updateAddressDto, Guid userId)
        {
            return await addressRepository.UpdateAddressAsync(addressId, updateAddressDto, userId);
        }
    }
}
