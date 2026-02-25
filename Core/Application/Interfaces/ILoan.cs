using Application.DTO;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Interfaces
{
    public interface ILoan
    {
        Task<LoanCreateDTO> CreateLoanAsync(LoanCreateDTO loan);
        Task<LoanUpdateDTO> UpdateLoanAsync(LoanUpdateDTO loan);
        Task<LoanDeleteDTO> DeleteLoanAsync(LoanDeleteDTO loan);
        Task<LoanCreateDTO> GetLoanByIdAsync(int id);
        Task<List<LoanCreateDTO>> GetAllLoansAsync();
    }
}