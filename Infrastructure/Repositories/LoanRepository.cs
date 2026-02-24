using Application.Interfaces;
using Domain.Entities;
using Application.DTO;
using Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Repositories
{
    public class LoanRepository : ILoan
    {
        private readonly ApplicationDbContext _dbContext;

        public LoanRepository(ApplicationDbContext context)
        {
            _dbContext = context;
        }

        public async Task<List<LoanCreateDTO>> GetAllLoansAsync()
        {
            return await _dbContext.Loans
                .Select(l => new LoanCreateDTO
                {
                    Id = l.Id,
                    AccountId = l.AccountId,
                    Borrower = l.Borrower,
                    Amount = l.Amount,
                    InterestRate = l.InterestRate,
                    AmountToPay = l.AmountToPay,
                    Currency = l.Currency,
                    LoanDate = l.LoanDate,
                    Description = l.Description,
                    Status = l.Status
                }).ToListAsync();
        }

        public async Task<LoanCreateDTO> GetLoanByIdAsync(int id)
        {
            var l = await _dbContext.Loans.FindAsync(id);
            if (l == null) return null;

            return new LoanCreateDTO
            {
                Id = l.Id,
                AccountId = l.AccountId,
                Borrower = l.Borrower,
                Amount = l.Amount,
                InterestRate = l.InterestRate,
                AmountToPay = l.AmountToPay,
                Currency = l.Currency,
                LoanDate = l.LoanDate,
                Description = l.Description,
                Status = l.Status
            };
        }

        public async Task<LoanCreateDTO> CreateLoanAsync(LoanCreateDTO loanDTO)
        {
            var loan = new Loan
            {
                AccountId = loanDTO.AccountId,
                Borrower = loanDTO.Borrower,
                Amount = loanDTO.Amount,
                InterestRate = loanDTO.InterestRate,
                AmountToPay = loanDTO.AmountToPay,
                Currency = loanDTO.Currency,
                LoanDate = loanDTO.LoanDate ?? DateTime.Now,
                Description = loanDTO.Description ?? "",
                Status = loanDTO.Status ?? "Active",
                CreatedAt = DateTime.Now,
                UpdatedAt = DateTime.Now
            };

            await _dbContext.Loans.AddAsync(loan);
            await _dbContext.SaveChangesAsync();
            
            loanDTO.Id = loan.Id;
            return loanDTO;
        }

        public async Task<LoanUpdateDTO> UpdateLoanAsync(LoanUpdateDTO loanDTO)
        {
            var loan = await _dbContext.Loans.FindAsync(loanDTO.Id);
            if (loan == null) return null;

            loan.AccountId = loanDTO.AccountId;
            loan.Borrower = loanDTO.Borrower;
            loan.Amount = loanDTO.Amount;
            loan.InterestRate = loanDTO.InterestRate;
            loan.AmountToPay = loanDTO.AmountToPay;
            loan.Currency = loanDTO.Currency;
            loan.LoanDate = loanDTO.LoanDate ?? DateTime.Now;
            loan.Description = loanDTO.Description ?? "";
            loan.Status = loanDTO.Status ?? "Active";
            loan.UpdatedAt = DateTime.Now;

            _dbContext.Loans.Update(loan);
            await _dbContext.SaveChangesAsync();
            return loanDTO;
        }

        public async Task<LoanDeleteDTO> DeleteLoanAsync(LoanDeleteDTO loanDTO)
        {
            var loan = await _dbContext.Loans.FindAsync(loanDTO.Id);
            if (loan != null)
            {
                _dbContext.Loans.Remove(loan);
                await _dbContext.SaveChangesAsync();
            }
            return loanDTO;
        }
    }
}
