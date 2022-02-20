using SimpleWebAplication.Models;
using SimpleWebAplication.Repositories;

namespace SimpleWebAplication.Services
{
    public interface ITransferService
    {
        Task<ServiceResult<TransferResponse>> Execute(TransferRequest request, CancellationToken ct);
    }
}