using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Repository.Interface
{
    public interface IAddressRepository
    {
        Task<AddressDto> AddAddressAsync(AddAddressDto addAddressDto);
        Task<List<AddressWithUserDto>> GetAllAddressAsync(Guid userId);
        Task<string> DeleteAddressAsync(Guid id, Guid userId);
        Task<AddressWithUserDto> GetAddressByIdAsync(Guid addressId, Guid userId);
        Task<AddressDto> UpdateAddressAsync(Guid addressId, UpdateAddressDto updateAddressDto, Guid userId);
    }
}
