using Application.DTO;
using Domain.Entities;


namespace Application.Interfaces
{
    public interface ILoanRepayment
    {
        Task<LoanRepaymentCreateDTO> CreateLoanRepaymentAsync(LoanRepaymentCreateDTO loanRepayment);
        Task<LoanRepaymentUpdateDTO> UpdateLoanRepaymentAsync(LoanRepaymentUpdateDTO loanRepayment);
        Task<LoanRepaymentDeleteDTO> DeleteLoanRepaymentAsync(LoanRepaymentDeleteDTO loanRepayment);
        Task<LoanRepaymentCreateDTO> GetLoanRepaymentByIdAsync(int id);
        Task<List<LoanRepaymentCreateDTO>> GetAllLoanRepaymentsAsync();
    }
}