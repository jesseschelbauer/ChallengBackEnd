using SimpleWebAplication.Models;
using SimpleWebAplication.Services;

namespace SimpleWebAplication.Context
{
    public static class DataContext
    {
        private static int Id = 1;
        public static int NewId { get { return Id++; } }
        static DataContext()
        {
            UserAssets.Add(new UserAsset() { Symbol = "PETR4", CurrentPrice = 28.44M });
            UserAssets.Add(new UserAsset() { Symbol = "MGLU3", CurrentPrice = 25.91M });
            UserAssets.Add(new UserAsset() { Symbol = "VVAR3", CurrentPrice = 25.91M });
            UserAssets.Add(new UserAsset() { Symbol = "SANB11", CurrentPrice = 40.77M });
            UserAssets.Add(new UserAsset() { Symbol = "TORO4", CurrentPrice = 115.98M });
        }

        public static List<UserAsset> UserAssets { get; set; } = new();

        public static List<Asset> Assets { get; } = new()
        {
            new Asset() { Id = NewId, Symbol = "PETR4", Price = 28.44M },
            new Asset() { Id = NewId, Symbol = "MGLU3", Price = 25.91M },
            new Asset() { Id = NewId, Symbol = "VVAR3", Price = 25.91M },
            new Asset() { Id = NewId, Symbol = "SANB11", Price = 40.77M },
            new Asset() { Id = NewId, Symbol = "TORO4", Price = 115.98M }
        };

        public static List<User> Users { get; } = new()
        {
            new User
            {
                Id = 1,
                PasswordHash = "$2a$11$xYqg7vpTbmo7rT1yujqKEu0bjydG.zzHfoStm9qb545mlJ5SFmP4W",
                Username = "jesse",
                Account = "123456",
                Name = "Jesse Schelbauer",
                Cpf = "05767299900",
            }
        };
    }
}
