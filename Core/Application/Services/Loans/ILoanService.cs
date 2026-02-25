using Application.DTO;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.Loans
{
    public interface ILoanService
    {
        Task<LoanCreateDTO> CreateLoanAsync(LoanCreateDTO loan);

        Task<LoanUpdateDTO> UpdateLoanAsync(LoanUpdateDTO loan);

        Task<LoanDeleteDTO> DeleteLoanAsync(LoanDeleteDTO loan);

        Task<LoanCreateDTO> GetLoanByIdAsync(int id);

        Task<List<LoanCreateDTO>> GetAllLoansAsync();
    }
}