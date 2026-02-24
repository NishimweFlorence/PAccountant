using Application.DTO;
using Application.Interfaces;
using Domain.Entities;

namespace Application.Services.Loans
{
    public class LoanService : ILoanService
    {
        private readonly ILoan _loan;
        
        //Constructor
        public LoanService(ILoan loan)
        {
            _loan = loan;
        }

        public Task<LoanCreateDTO> CreateLoanAsync(LoanCreateDTO loan)
        {
            return _loan.CreateLoanAsync(loan);
        }

        public Task<LoanUpdateDTO> UpdateLoanAsync(LoanUpdateDTO loan)
        {
            return _loan.UpdateLoanAsync(loan);
        }

        public Task<LoanDeleteDTO> DeleteLoanAsync(LoanDeleteDTO loan)
        {
            return _loan.DeleteLoanAsync(loan);
        }

        public Task<LoanCreateDTO> GetLoanByIdAsync(int id)
        {
            return _loan.GetLoanByIdAsync(id);
        }

        public Task<List<LoanCreateDTO>> GetAllLoansAsync()
        {
            return _loan.GetAllLoansAsync();
        }
    }
}