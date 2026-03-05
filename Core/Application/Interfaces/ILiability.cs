using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILiability
    {
        Task<List<Liability>> GetAllLiabilitiesAsync();
        Task<Liability?> GetLiabilityByIdAsync(int id);
        Task CreateLiabilityAsync(CreateLiabilityDTO LiabilityDTO);
        Task UpdateLiabilityAsync(int Id, UpdateLiabilityDTO LiabilityDTO);
    }
}
