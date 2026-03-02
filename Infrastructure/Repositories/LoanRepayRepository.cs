using Application.Interfaces;
using Domain.Entities;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using Application.DTO;

namespace Infrastructure.Repositories
{
    public class LoanRepayRepository : ILoanRepayment
    {
        private readonly ApplicationDbContext _context;

        public LoanRepayRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<LoanRepaymentCreateDTO> CreateLoanRepaymentAsync(LoanRepaymentCreateDTO loanRepaymentDto)
        {
            var loanRepayment = new LoanRepayment
            {
                LoanId = loanRepaymentDto.LoanId,
                Amount = loanRepaymentDto.Amount,
                RepaymentDate = loanRepaymentDto.RepaymentDate ?? DateTime.Now,
                Note = loanRepaymentDto.Note ?? string.Empty,
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            _context.LoanRepayments.Add(loanRepayment);
            await _context.SaveChangesAsync();

            loanRepaymentDto.Id = loanRepayment.Id;
            return loanRepaymentDto;
        }

        public async Task<LoanRepaymentUpdateDTO?> UpdateLoanRepaymentAsync(LoanRepaymentUpdateDTO loanRepaymentDto)
        {
            var loanRepayment = await _context.LoanRepayments.FindAsync(loanRepaymentDto.Id);
            if (loanRepayment == null)
            {
                return null;
            }

            loanRepayment.LoanId = loanRepaymentDto.LoanId;
            loanRepayment.Amount = loanRepaymentDto.Amount;
            loanRepayment.RepaymentDate = loanRepaymentDto.RepaymentDate ?? DateTime.Now;
            loanRepayment.Note = loanRepaymentDto.Note ?? string.Empty;
            loanRepayment.UpdatedAt = DateTime.Now;

            _context.LoanRepayments.Update(loanRepayment);
            await _context.SaveChangesAsync();

            return loanRepaymentDto;
        }

        public async Task<LoanRepaymentDeleteDTO?> DeleteLoanRepaymentAsync(LoanRepaymentDeleteDTO loanRepaymentDto)
        {
            var loanRepayment = await _context.LoanRepayments.FindAsync(loanRepaymentDto.Id);
            if (loanRepayment == null)
            {
                return null;
            }

            _context.LoanRepayments.Remove(loanRepayment);
            await _context.SaveChangesAsync();

            return loanRepaymentDto;
        }

        public async Task<LoanRepaymentCreateDTO?> GetLoanRepaymentByIdAsync(int id)
        {
            var lr = await _context.LoanRepayments.FindAsync(id);
            if (lr == null) return null;

            return new LoanRepaymentCreateDTO
            {
                Id = lr.Id,
                LoanId = lr.LoanId,
                Amount = lr.Amount,
                RepaymentDate = lr.RepaymentDate,
                Note = lr.Note
            };
        }

        public async Task<List<LoanRepaymentCreateDTO>> GetAllLoanRepaymentsAsync()
        {
            return await _context.LoanRepayments
                .Select(lr => new LoanRepaymentCreateDTO
                {
                    Id = lr.Id,
                    LoanId = lr.LoanId,
                    Amount = lr.Amount,
                    RepaymentDate = lr.RepaymentDate,
                    Note = lr.Note
                }).ToListAsync();
        }
    }
}
