using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Business.Interface;
using VGProducts.Entities.DTOs;
using VGProducts.Service.Interface;

namespace VGProducts.Service.Implementation
{
    public class StateServices : IStateServices
    {
        private readonly IStateBusiness stateBusiness;

        public StateServices(IStateBusiness stateBusiness) 
        {
            this.stateBusiness = stateBusiness;
        }
        public async Task<AddStateDto> AddStateAsync(AddStateDto addStateDto)
        {
            return await stateBusiness.AddStateAsync(addStateDto);
        }

        public async Task<string> DeleteStateAsync(Guid id)
        {
            return await stateBusiness.DeleteStateAsync(id);
        }

        public async Task<List<StateDto>> GetAllStateAsync()
        {
            return await stateBusiness.GetAllStateAsync();
        }

        public async Task<StateDto> GetStateByIdAsync(Guid Countryid)
        {
            return await stateBusiness.GetStateByIdAsync(Countryid);
        }
    }
}
