using Application.Queries;
using Domain.Models;
using Infrastructure.Contracts;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Application.Handlers
{
    public class GetTopSpendingUsersHandler : IRequestHandler<GetTopSpendingUsersQuery, List<TopSpendingUsersModel>>
    {
        private readonly IUserRepository _repository;
        private readonly ILogger<GetTopSpendingUsersHandler> _logger;

        public GetTopSpendingUsersHandler(IUserRepository repository, ILogger<GetTopSpendingUsersHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }


        public async Task<List<TopSpendingUsersModel>> Handle(GetTopSpendingUsersQuery request, CancellationToken cancellationToken)
        {
            try
            {
                if (request == null)
                    throw new ArgumentNullException(nameof(request), "The request cannot be null.");

                return await _repository.GetTopSpendingUsers();
            }
            catch (ArgumentNullException ex)
            {
                _logger.LogWarning(ex, "Received null request in GetTopSpendingUsersQuery handler");
                throw;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error generating top spending user report");
                throw new ApplicationException("Could not generate top spending users report.", ex);
            }
        }

    }

}
