using Application.DTO;
using Domain.Entities;
using Application.Interfaces;

namespace Application.Services.LoanRepayments
{
    public class LoanRepaymentService : ILoanRepaymentService
    {
        private readonly ILoanRepayment _loanRepayment;

        //constructor
        public LoanRepaymentService(ILoanRepayment loanRepayment)
        {
            _loanRepayment = loanRepayment;
        }

        public Task<LoanRepaymentCreateDTO> CreateLoanRepaymentAsync(LoanRepaymentCreateDTO loanRepayment)
        {
            return _loanRepayment.CreateLoanRepaymentAsync(loanRepayment);
        }

        public Task<LoanRepaymentUpdateDTO> UpdateLoanRepaymentAsync(LoanRepaymentUpdateDTO loanRepayment)
        {
            return _loanRepayment.UpdateLoanRepaymentAsync(loanRepayment);
        }

        public Task<LoanRepaymentDeleteDTO> DeleteLoanRepaymentAsync(LoanRepaymentDeleteDTO loanRepayment)
        {
            return _loanRepayment.DeleteLoanRepaymentAsync(loanRepayment);
        }

        public Task<LoanRepaymentCreateDTO> GetLoanRepaymentByIdAsync(int id)
        {
            return _loanRepayment.GetLoanRepaymentByIdAsync(id);
        }

        public Task<List<LoanRepaymentCreateDTO>> GetAllLoanRepaymentsAsync()
        {
            return _loanRepayment.GetAllLoanRepaymentsAsync();
        }
    }
}
