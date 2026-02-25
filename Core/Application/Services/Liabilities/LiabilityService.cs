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
        public List<Liability> GetAllLiabilities()
        {
            List<Liability> _liabilities = _liability.GetAllLiabilities();
            return _liabilities;
        }

        public Liability GetLiabilityById(int id)
        {
            return _liability.GetLiabilityById(id);
        }
        public void CreateLiability(CreateLiabilityDTO LiabilityDTO)
        {
            _liability.CreateLiability(LiabilityDTO);
        }
        public void UpdateLiability(int Id, UpdateLiabilityDTO LiabilityDTO)
        {
            _liability.UpdateLiability(Id, LiabilityDTO);
        }
    }
}