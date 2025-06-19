using Application.Handlers;
using Application.Queries;
using Domain.Models;
using Infrastructure.Contracts;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace Tests
{
    public class GetTopSpendingUsersHandlerTests : IDisposable
    {
        private readonly Mock<IUserRepository> _mock;
        private readonly Mock<ILogger<GetTopSpendingUsersHandler>> _loggerMock; 
        private readonly GetTopSpendingUsersHandler _handler;

        public GetTopSpendingUsersHandlerTests()
        {
            _mock = new Mock<IUserRepository>();
            _loggerMock = new Mock<ILogger<GetTopSpendingUsersHandler>>(); 
            _handler = new GetTopSpendingUsersHandler(_mock.Object, _loggerMock.Object); 
        }

        public void Dispose()
        {
            _mock.Reset();
            _loggerMock.Reset(); 
            _handler.Equals(null);
        }

        [Test]
        public async Task Handle_ShouldReturnTopSpendingUsers_WhenRequestIsValid()
        {
            // Arrange  
            var expectedUsers = new List<TopSpendingUsersModel>
            {
                new TopSpendingUsersModel { Id = 1, Name = " ", TotalSpent = 1000 },
                new TopSpendingUsersModel { Id = 2, Name = " ", TotalSpent = 800 }
            };

            _mock.Setup(repo => repo.GetTopSpendingUsers()).ReturnsAsync(expectedUsers);

            // Act  
            var result = await _handler.Handle(new GetTopSpendingUsersQuery(), CancellationToken.None);

            // Assert  
            NUnit.Framework.Assert.That(result, Is.Not.Null);
            NUnit.Framework.Assert.That(result.Count, Is.EqualTo(expectedUsers.Count));
            _mock.Verify(repo => repo.GetTopSpendingUsers(), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowArgumentNullException_WhenRequestIsNull()
        {
            // Arrange  
            GetTopSpendingUsersQuery nullRequest = null;
            // Act & Assert  
            NUnit.Framework.Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(nullRequest, CancellationToken.None));
        }
    }
}
