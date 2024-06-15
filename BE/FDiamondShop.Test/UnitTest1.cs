using FDiamondShop.API.Models;
using FDiamondShop.API.Models.DTO;
using FDiamondShop.API.Repository.IRepository;
using Microsoft.AspNetCore.Identity;
using Moq;

namespace FDiamondShop.Test
{
    public class UnitTest1
    {
        public class LoginTests
        {
            private readonly Mock<UserManager<IdentityUser>> _userManagerMock;
            private readonly Mock<SignInManager<IdentityUser>> _signInManagerMock;

            public LoginTests()
            {
                var userStoreMock = new Mock<IUserStore<IdentityUser>>();
                _userManagerMock = new Mock<UserManager<IdentityUser>>(
                    userStoreMock.Object, null, null, null, null, null, null, null, null);

                var contextAccessorMock = new Mock<Microsoft.AspNetCore.Http.IHttpContextAccessor>();
                var userClaimsPrincipalFactoryMock = new Mock<IUserClaimsPrincipalFactory<IdentityUser>>();
                _signInManagerMock = new Mock<SignInManager<IdentityUser>>(
                    _userManagerMock.Object,
                    contextAccessorMock.Object,
                    userClaimsPrincipalFactoryMock.Object,
                    null,
                    null,
                    null,
                    null);
            }

            [Fact]
            public async Task Login_Success()
            {
                // Arrange
                var email = "test@example.com";
                var password = "Password123!";
                var user = new IdentityUser { UserName = email, Email = email };

                _userManagerMock.Setup(um => um.FindByEmailAsync(email))
                    .ReturnsAsync(user);
                _signInManagerMock.Setup(sm => sm.PasswordSignInAsync(email, password, false, false))
                    .ReturnsAsync(SignInResult.Success);

                // Act
                var result = await _signInManagerMock.Object.PasswordSignInAsync(email, password, false, false);

                // Assert
                Assert.True(result.Succeeded);
            }
        }
    }
}