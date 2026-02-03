using TaskHub.Core.Entities;

namespace TaskHub.Core.Interfaces
{
    public interface IUserRepository<T> : IRepository<T> where T : User
    {
        Task<T> GetByEmailAndPasswordAsync(string email, string password);
        Task<T> GetByEmailAsync(string email);
    }
}
