using SimpleWebAplication.Context;
using SimpleWebAplication.Models;

public class UserAssetRepository : IUserAssetRepository
{
    public UserAssetRepository()
    {        
    }

    public async Task<UserAsset> Create(UserAsset userAsset, CancellationToken ct)
    {
        DataContext.UserAssets.Add(userAsset);
        userAsset.Id = DataContext.NewId;
        return await Task.FromResult(userAsset).ConfigureAwait(false);
    }

    public async Task<IEnumerable<UserAsset>> List(int userId)
    {
        return await Task.FromResult(DataContext.UserAssets.Where(a => a.UserId == userId)).ConfigureAwait(false);
    }
}
