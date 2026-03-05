using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Application.DTO;
using Domain.ValueObjects;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LiabilityRepository : ILiability  
    {
        private readonly ApplicationDbContext _context;

        public LiabilityRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<Liability?> GetLiabilityByIdAsync(int Id)
        {
            return await _context.Liabilities.FirstOrDefaultAsync(l => l.Id == Id);
        }

        public async Task<List<Liability>> GetAllLiabilitiesAsync()
        {
            return await _context.Liabilities.ToListAsync();
        }

        public async Task CreateLiabilityAsync(CreateLiabilityDTO LiabilityDTO)
        {
            var liability = new Liability
            {
                Type = LiabilityDTO.Type,
                LenderName = LiabilityDTO.LenderName,
                OriginalAmount = LiabilityDTO.OriginalAmount,
                CurrentAmount = LiabilityDTO.CurrentAmount,
                DueDate = LiabilityDTO.DueDate,
                CreatedAt = LiabilityDTO.CreatedAt,
                Currency      = Currency.FromCode(LiabilityDTO.Currency)
            };
            _context.Liabilities.Add(liability);
            await _context.SaveChangesAsync();
        }

        public async Task UpdateLiabilityAsync(int Id, UpdateLiabilityDTO LiabilityDTO)
        {
            var liability = await _context.Liabilities.FindAsync(Id);
            if (liability != null)
            {
                liability.Type = LiabilityDTO.Type;
                liability.OriginalAmount = LiabilityDTO.OriginalAmount;
                liability.CurrentAmount = LiabilityDTO.CurrentAmount;
                liability.DueDate = LiabilityDTO.DueDate;
                liability.Currency       = Currency.FromCode(LiabilityDTO.Currency);
                await _context.SaveChangesAsync();
            }
        }
    }
}