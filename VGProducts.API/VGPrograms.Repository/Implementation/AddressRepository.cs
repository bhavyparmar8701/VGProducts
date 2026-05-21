using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Entities.Model;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class AddressRepository : IAddressRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public AddressRepository(ApplicationDbContext applicationDbContext) 
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<AddressDto> AddAddressAsync(AddAddressDto addAddressDto)
        {
            var user = await applicationDbContext.Users.FirstOrDefaultAsync(u => u.Id == addAddressDto.UserId);

            if (user == null)
                return null;

            var data = new Address
            {
                UserId = addAddressDto.UserId,
                LandMark = addAddressDto.LandMark,
                AddressLine1 = addAddressDto.AddressLine1,
                AddressLine2 = addAddressDto.AddressLine2,
                CityId = addAddressDto.CityId,
                StateId = addAddressDto.StateId,
                CountryId = addAddressDto.CountryId,
                Pincode = addAddressDto.Pincode,
                SaveAs = addAddressDto.SaveAs,
                IsActive = IsActive.Active,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false
            };

            await applicationDbContext.Address.AddAsync(data);
            await applicationDbContext.SaveChangesAsync();

            return new AddressDto
            {
                AddressId = data.AddressId,
                UserId = data.UserId,
                LandMark = data.LandMark,
                AddressLine1 = data.AddressLine1,
                AddressLine2 = data.AddressLine2,
                CityId = data.CityId,
                StateId = data.StateId,
                CountryId = data.CountryId,
                Pincode = data.Pincode,
                SaveAs = data.SaveAs,
                IsActive = IsActive.Active,
                CreatedAt = DateTime.UtcNow
            };
        }

        public async Task<string> DeleteAddressAsync(Guid addressId,Guid userId)
        {
            var address = await applicationDbContext.Address.FirstOrDefaultAsync(a => a.AddressId == addressId && a.UserId == userId && a.IsActive == IsActive.Active);
            if (address == null)
            {
                return "Address Not Found";
            }
            address.IsActive = IsActive.Inactive;
            address.UpdatedAt = DateTime.UtcNow;
            address.IsDeleted = true;

            await applicationDbContext.SaveChangesAsync();
            return "Address deleted successfully";
        }

        public async Task<List<AddressWithUserDto>> GetAllAddressAsync(Guid userId)
        {
            var address = await applicationDbContext.Address
                .Where(a => a.UserId == userId && a.IsActive == IsActive.Active)
                .Include(a => a.City)
                .Include(a => a.State)
                .Include(a => a.Country)
                .ToListAsync();

            return address.Select(a => new AddressWithUserDto
            {
                AddressId = a.AddressId,
                UserId = a.UserId,
                LandMark = a.LandMark,
                AddressLine1 = a.AddressLine1,
                AddressLine2 = a.AddressLine2,

                CityId = a.CityId,
                CityName = a.City != null ? a.City.CityName : null,

                StateId = a.StateId,
                StateName = a.State != null ? a.State.StateName : null,

                CountryId = a.CountryId,
                CountryName = a.Country != null ? a.Country.CountryName : null,

                Pincode = a.Pincode,
                SaveAs = a.SaveAs,
                IsActive = a.IsActive.ToString(),
                CreatedAt = a.CreatedAt,
                UpdatedAt = a.UpdatedAt,
                IsDeleted = a.IsDeleted
            }).ToList();
        }

        public async Task<AddressWithUserDto> GetAddressByIdAsync(Guid addressId, Guid userId)
        {
            var address = await applicationDbContext.Address
                .Where(a => a.AddressId == addressId
                         && a.UserId == userId
                         && a.IsActive == IsActive.Active)
                .Include(a => a.City)
                .Include(a => a.State)
                .Include(a => a.Country)
                .FirstOrDefaultAsync();

            if (address == null)
            {
                return null; 
            }

            return new AddressWithUserDto
            {
                AddressId = address.AddressId,
                UserId = address.UserId,
                LandMark = address.LandMark,
                AddressLine1 = address.AddressLine1,
                AddressLine2 = address.AddressLine2,

                CityId = address.CityId,
                CityName = address.City != null ? address.City.CityName : null,

                StateId = address.StateId,
                StateName = address.State != null ? address.State.StateName : null,

                CountryId = address.CountryId,
                CountryName = address.Country != null ? address.Country.CountryName : null,

                Pincode = address.Pincode,
                SaveAs = address.SaveAs,
                IsActive = address.IsActive.ToString(),
                CreatedAt = address.CreatedAt,
                UpdatedAt = address.UpdatedAt,
                IsDeleted = address.IsDeleted
            };
        }


        public async Task<AddressDto> UpdateAddressAsync(Guid addressId, UpdateAddressDto updateAddressDto , Guid userId)
        {
            var existingAddress = await applicationDbContext.Address.FirstOrDefaultAsync(a => a.AddressId == addressId && a.UserId == userId);

            if (existingAddress == null)
                return null;

           
            existingAddress.LandMark = updateAddressDto.LandMark;
            existingAddress.AddressLine1 = updateAddressDto.AddressLine1;
            existingAddress.AddressLine2 = updateAddressDto.AddressLine2;
            existingAddress.CityId = updateAddressDto.CityId;
            existingAddress.StateId = updateAddressDto.StateId;
            existingAddress.CountryId = updateAddressDto.CountryId;
            existingAddress.Pincode = updateAddressDto.Pincode;
            existingAddress.SaveAs = updateAddressDto.SaveAs;
            existingAddress.UpdatedAt = DateTime.UtcNow;

            await applicationDbContext.SaveChangesAsync();

            return new AddressDto
            {
                AddressId = existingAddress.AddressId,
                UserId = existingAddress.UserId,
                LandMark = existingAddress.LandMark,
                AddressLine1 = existingAddress.AddressLine1,
                AddressLine2 = existingAddress.AddressLine2,
                CityId = existingAddress.CityId,
                StateId = existingAddress.StateId,
                CountryId = existingAddress.CountryId,
                Pincode = existingAddress.Pincode,
                SaveAs = existingAddress.SaveAs,
                IsActive = existingAddress.IsActive,
                CreatedAt = existingAddress.CreatedAt,
                UpdatedAt = existingAddress.UpdatedAt
            };
        }
    }
}
