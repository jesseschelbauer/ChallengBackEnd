using SimpleWebAplication.EndpointsDefinitions;
using SimpleWebAplication.Models;

namespace SimpleWebAplication.Services
{
    public interface IOrderService
    {
        Task<ServiceResult<UserAsset>> Create(OrderRequest request, CancellationToken ct);
    }
}