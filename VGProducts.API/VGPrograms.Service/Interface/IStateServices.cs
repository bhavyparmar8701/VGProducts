using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VGProducts.Entities.DTOs;

namespace VGProducts.Service.Interface
{
    public interface IStateServices
    {
        Task<AddStateDto> AddStateAsync(AddStateDto addStateDto);
        Task<List<StateDto>> GetAllStateAsync();
        Task<string> DeleteStateAsync(Guid id);
        Task<StateDto> GetStateByIdAsync(Guid Countryid);
    }
}
