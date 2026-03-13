using System.Threading.Tasks;
using Domain.Entities;

namespace Application.Interfaces
{
    public interface ITransactionPdfService
    {
        Task<byte[]> GenerateTransactionPdfAsync(Transaction transaction, string signedBy);
    }
}
