using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;
using CobrArWeb.Controllers;
using CobrArWeb.Data;
using CobrArWeb.Services.Interfaces;
using CobrArWeb.Models.Views.Home;

namespace CobrArWeb.Tests
{
    [TestClass]
    public class HomeControllerTests
    {
        private readonly HomeController _controller;
        private readonly Mock<ILogger<HomeController>> _loggerMock;
        private readonly Mock<IAuthenticationService> _authenticationServiceMock;
        private readonly Mock<CobrArWebContext> _contextMock;

        public HomeControllerTests()
        {
            _loggerMock = new Mock<ILogger<HomeController>>();
            _authenticationServiceMock = new Mock<IAuthenticationService>();
            _contextMock = new Mock<CobrArWebContext>();

            _controller = new HomeController(_loggerMock.Object, _contextMock.Object, _authenticationServiceMock.Object);

            // Setup session
            var httpContext = new DefaultHttpContext();
            httpContext.Session = new TestSession();
            _controller.ControllerContext.HttpContext = httpContext;
        }

        [TestMethod]
        public void Login_SuccessfulAuthentication_RedirectsToIndex()
        {
            // Arrange
            var loginVM = new LoginVM { Name = "testuser", Password = "testpassword" };
            var authenticatedUser = new User { Email = "testuser@example.com", UserRole = UserRole.Admin };
            _authenticationServiceMock.Setup(x => x.AuthenticateUser(loginVM.Name, loginVM.Password)).Returns(authenticatedUser);

            // Act
            var result = _controller.Login(loginVM) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
        }

        [TestMethod]
        public void Login_UnsuccessfulAuthentication_RedirectsToIndexWithError()
        {
            // Arrange
            var loginVM = new LoginVM { Name = "testuser", Password = "wrongpassword" };
            _authenticationServiceMock.Setup(x => x.AuthenticateUser(loginVM.Name, loginVM.Password)).Returns((User)null);

            // Act
            var result = _controller.Login(loginVM) as RedirectToActionResult;

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual("Index", result.ActionName);
            Assert.AreEqual("ERR_LOGIN", result.RouteValues["ErrorTag"]);
            Assert.AreEqual("Nom d'utilisateur ou mot de passe incorrect", result.RouteValues["ErrorText"]);
        }
    }

    public class TestSession : ISession
    {
        private readonly Dictionary<string, object> _sessionStorage = new Dictionary<string, object>();

        public string Id => throw new NotImplementedException();

        public bool IsAvailable => throw new NotImplementedException();

        public IEnumerable<string> Keys => _sessionStorage.Keys;

        public void Clear()
        {
            _sessionStorage.Clear();
        }

        public Task CommitAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public Task LoadAsync(CancellationToken cancellationToken = default)
        {
            throw new NotImplementedException();
        }

        public void Remove(string key)
        {
            _sessionStorage.Remove(key);
        }

        public void Set(string key, byte[] value)
        {
            _sessionStorage[key] = value;
        }

        public bool TryGetValue(string key, out byte[] value)
        {
            if (_sessionStorage.TryGetValue(key, out object storedValue))
            {
                value = (byte[])storedValue;
                return true;
            }
            else
            {
                value = null;
                return false;
            }
        }
    }
}
