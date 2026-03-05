using Domain.Entities;
using Application.DTO;
using Application.Interfaces;

namespace Application.Services.Liabilities
{
    public class LiabilityService : ILiabilityService
    {
        private readonly ILiability _liability;
        public LiabilityService(ILiability liability)
        {
            _liability = liability;
        }
        public async Task<List<Liability>> GetAllLiabilitiesAsync()
        {
            return await _liability.GetAllLiabilitiesAsync();
        }

        public async Task<Liability?> GetLiabilityByIdAsync(int id)
        {
            return await _liability.GetLiabilityByIdAsync(id);
        }
        public async Task CreateLiabilityAsync(CreateLiabilityDTO LiabilityDTO)
        {
            await _liability.CreateLiabilityAsync(LiabilityDTO);
        }
        public async Task UpdateLiabilityAsync(int Id, UpdateLiabilityDTO LiabilityDTO)
        {
            await _liability.UpdateLiabilityAsync(Id, LiabilityDTO);
        }
    }
}
