using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ILiabilityService
    {
        List<Liability> GetAllLiabilities();
        Liability GetLiabilityById(int id);
        void CreateLiability(CreateLiabilityDTO LiabilityDTO);
        void UpdateLiability(int Id, UpdateLiabilityDTO LiabilityDTO);
    }
}