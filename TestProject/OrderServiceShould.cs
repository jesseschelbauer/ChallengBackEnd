using Moq;
using SimpleWebAplication.Models;
using SimpleWebAplication.Repositories;
using SimpleWebAplication.Services;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace TestProject
{
    public class OrderServiceShould
    {
        private const string AssetSymbol = "XXX";

        private IUserInfoService CreateUserInfoService()
        {
            Mock<IUserInfoService> userInfoService = new Mock<IUserInfoService>();
            userInfoService.Setup(i => i.GetAccountBalance(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult(1M));
            userInfoService.Setup(i => i.User).Returns(new User { Id = 1 });

            return userInfoService.Object;
        }

        private IAssetService CreateAssetService()         
        {
            Mock<IAssetService> assetService = new Mock<IAssetService>();
            assetService.Setup(i => i.Get(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((Asset?)new Asset { Id = 1, Price = 1, Symbol = AssetSymbol }));

            return assetService.Object;
        }

        [Fact]
        public async Task Create_successfully_WhenRequestIsValid()
        {
            var newUserMock = new Mock<User>();
            newUserMock.SetupGet(x => x.AccoutBalance).Returns(1);

            var user = newUserMock.Object;
            user.Username = Faker.Name.First();
            user.Cpf = "12345678910";
            user.Account = "123456";
            user.Name = Faker.Name.First();

            Mock<IUserRepository> userRepository = new Mock<IUserRepository>();
            userRepository.Setup(u => u.Get(It.IsAny<int>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((User?)user));

            var orderService = new OrderService(userRepository.Object, new UserAssetRepository(), CreateAssetService(), CreateUserInfoService());

            var orderRequest = new OrderRequest() { Symbol = AssetSymbol, Amount = 1 };

            var result = await orderService.Create(orderRequest, CancellationToken.None).ConfigureAwait(false);

            Assert.True(result.IsOk);
            Assert.NotNull(result.Result);
            Assert.Equal(orderRequest.Symbol, result.Result!.Symbol);
            Assert.Equal(orderRequest.Amount, result.Result.Amount);
            Assert.Equal(1, result.Result.CurrentPrice);
            Assert.Equal(orderRequest.Amount, result.Result.Amount);
        }

        [Fact]
        public async Task Returns409_WhenInvalidSymbolIsProvided()
        {
            Mock<IAssetService> assetService = new Mock<IAssetService>();
            assetService.Setup(i => i.Get(It.IsAny<string>(), It.IsAny<CancellationToken>())).Returns(Task.FromResult((Asset?)null));

            var orderService = new OrderService(new UserRepository(), new UserAssetRepository(), assetService.Object, CreateUserInfoService());

            var orderRequest = new OrderRequest() { Symbol = "JJJ", Amount = 1 };

            var result = await orderService.Create(orderRequest, CancellationToken.None).ConfigureAwait(false);

            Assert.False(result.IsOk);
            Assert.Null(result.Result);
            Assert.NotNull(result.ErrorResponse);
            Assert.IsType<ServiceResultResponse409Message>(result!.ErrorResponse);
        }

        [Fact]
        public async Task Returns409_WhenUserDontHaveBalance()
        {
            var orderService = new OrderService(new UserRepository(),new UserAssetRepository(), CreateAssetService(), CreateUserInfoService());

            var orderRequest = new OrderRequest() { Symbol = AssetSymbol, Amount = 10 };

            var result = await orderService.Create(orderRequest, CancellationToken.None).ConfigureAwait(false);

            Assert.False(result.IsOk);
            Assert.Null(result.Result);
            Assert.NotNull(result.ErrorResponse);
            Assert.IsType<ServiceResultResponse409Message>(result!.ErrorResponse);
        }
    }
}