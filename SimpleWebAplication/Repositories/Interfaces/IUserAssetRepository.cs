using SimpleWebAplication.Models;

public interface IUserAssetRepository
{
    Task<UserAsset> Create(UserAsset userAsset, CancellationToken ct);

    Task<IEnumerable<UserAsset>> List(int userId);
}