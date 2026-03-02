using Application.DTO;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services.LoanRepayments
{
    public interface ILoanRepaymentService
    {
        Task<LoanRepaymentCreateDTO> CreateLoanRepaymentAsync(LoanRepaymentCreateDTO loanRepayment);

        Task<LoanRepaymentUpdateDTO> UpdateLoanRepaymentAsync(LoanRepaymentUpdateDTO loanRepayment);

        Task<LoanRepaymentDeleteDTO> DeleteLoanRepaymentAsync(LoanRepaymentDeleteDTO loanRepayment);

        Task<LoanRepaymentCreateDTO> GetLoanRepaymentByIdAsync(int id);

        Task<List<LoanRepaymentCreateDTO>> GetAllLoanRepaymentsAsync();
    }
}
