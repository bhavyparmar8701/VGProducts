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
    public class StateBusiness : IStateBusiness
    {
        private readonly IStateRepository stateRepository;

        public StateBusiness(IStateRepository stateRepository) 
        {
            this.stateRepository = stateRepository;
        }

        public async Task<AddStateDto> AddStateAsync(AddStateDto addStateDto)
        {
            return await stateRepository.AddStateAsync(addStateDto);
        }

        public async Task<string> DeleteStateAsync(Guid id)
        {
            return await stateRepository.DeleteStateAsync(id);
        }

        public async Task<List<StateDto>> GetAllStateAsync()
        {
            return await stateRepository.GetAllStateAsync();
        }

        public async Task<StateDto> GetStateByIdAsync(Guid Countryid)
        {
            return await stateRepository.GetStateByIdAsync(Countryid);
        }
    }
}
