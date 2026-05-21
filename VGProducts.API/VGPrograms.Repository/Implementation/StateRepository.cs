using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;
using VGProducts.Entities.Enums;
using VGProducts.Entities.Model;
using VGProducts.Repository.DataAccess;
using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class StateRepository : IStateRepository
    {
        private readonly ApplicationDbContext applicationDbContext;

        public StateRepository(ApplicationDbContext applicationDbContext) 
        {
            this.applicationDbContext = applicationDbContext;
        }

        public async Task<AddStateDto> AddStateAsync(AddStateDto addStateDto)
        {
            var data = new State
            {
                StateName = addStateDto.StateName,
                CountryId = addStateDto.CountryId,
                IsActive = IsActive.Active,
                CreatedAt = DateTime.UtcNow,
                IsDeleted = false,
            };
            await applicationDbContext.State.AddAsync(data);
            await applicationDbContext.SaveChangesAsync();
            return new AddStateDto
            {
                StateName = data.StateName,
                CountryId = data.CountryId
            };
        }

        public async Task<string> DeleteStateAsync(Guid id)
        {
            var state = await applicationDbContext.State.FirstOrDefaultAsync(s => s.StateId == id);
            if (state == null)
            {
                return "State Not Found";
            }
            state.IsActive = IsActive.Inactive;
            state.UpdatedAt = DateTime.UtcNow;
            state.IsDeleted = true;

            await applicationDbContext.SaveChangesAsync();
            return "State deleted successfully";
        }

        public async Task<List<StateDto>> GetAllStateAsync()
        {
            var state = await applicationDbContext.State.ToListAsync();
            return state.Select(s => new StateDto
            {
                StateId = s.StateId,
                StateName = s.StateName,
                CountryId = s.CountryId,
                IsActive = s.IsActive.ToString(),
                CreatedAt = s.CreatedAt
            }).ToList();
        }

        public async Task<StateDto> GetStateByIdAsync(Guid Countryid)
        {
            var state = await applicationDbContext.State.Where(s => s.CountryId == Countryid).FirstOrDefaultAsync();
            return new StateDto
            {
                StateId = state.StateId,
                StateName = state.StateName,
                CountryId = state.CountryId,
                IsActive = state.IsActive.ToString(),
                CreatedAt = state.CreatedAt
            };
        }
    }
}
