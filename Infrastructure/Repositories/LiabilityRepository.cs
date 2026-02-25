using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Application.DTO;

namespace Infrastructure.Repositories
{
    public class LiabilityRepository : ILiability  
    {
        private readonly ApplicationDbContext _context;

        public LiabilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Liability GetLiabilityById(int Id)
        {
            return _context.Liabilities.Find(Id);
        }

        public List<Liability> GetAllLiabilities()
        {
            return _context.Liabilities.ToList();
        }

        public void CreateLiability(CreateLiabilityDTO LiabilityDTO)
        {
            var liability = new Liability
            {
                Type = LiabilityDTO.Type,
                LenderName = LiabilityDTO.LenderName,
                OriginalAmount = LiabilityDTO.OriginalAmount,
                CurrentAmount = LiabilityDTO.CurrentAmount,
                DueDate = LiabilityDTO.DueDate,
                CreatedAt = LiabilityDTO.CreatedAt,
                Currency      = LiabilityDTO.Currency
            };
            _context.Liabilities.Add(liability);
            _context.SaveChanges();
        }

        public void UpdateLiability(int Id, UpdateLiabilityDTO LiabilityDTO)
        {
            var liability = _context.Liabilities.Find(Id);
            if (liability != null)
            {
                liability.Type = LiabilityDTO.Type;
                liability.OriginalAmount = LiabilityDTO.OriginalAmount;
                liability.CurrentAmount = LiabilityDTO.CurrentAmount;
                liability.DueDate = LiabilityDTO.DueDate;
                liability.Currency       = LiabilityDTO.Currency;
                _context.SaveChanges();
            }
        }
    }
    
}