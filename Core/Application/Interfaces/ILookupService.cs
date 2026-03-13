using System.Collections.Generic;
using System.Threading.Tasks;
using Application.DTO;

namespace Application.Interfaces
{
    public interface ILookupService
    {
        /// <summary>
        /// Gets merged list of built-in (default) types and custom types.
        /// </summary>
        Task<List<string>> GetTypesAsync(int userId, string category, List<string> defaultTypes);

        /// <summary>
        /// Gets all custom lookups for a specific user.
        /// </summary>
        Task<List<CustomLookupDTO>> GetAllCustomLookupsAsync(int userId);

        Task<CustomLookupDTO> CreateCustomLookupAsync(int userId, string category, string name);
        Task UpdateCustomLookupAsync(int id, string newName);
        Task DeleteCustomLookupAsync(int id);
    }
}
