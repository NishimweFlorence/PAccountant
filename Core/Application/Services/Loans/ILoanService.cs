using Application.DTO;
using Domain.Entities;

namespace Application.Interfaces
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
