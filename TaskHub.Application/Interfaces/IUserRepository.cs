using TaskHub.Core.Entities;

namespace TaskHub.Application.Interfaces
{
    public interface IUserRepository : IRepository<User>
    {
        Task<User> GetByEmailAsync(string email, CancellationToken ct);
    }
}
