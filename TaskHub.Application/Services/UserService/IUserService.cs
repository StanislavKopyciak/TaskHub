using TaskHub.Application.DTO.User;

namespace TaskHub.Application.Services.UserService
{
    public interface IUserService
    {
        Task<UserDTO> GetByIdAsync(Guid id);
        Task<UserDTO> AddAsync(UserSignUpDTO user);
        Task<int> UpdateAsync(Guid id, UserSignUpDTO user);
        Task<int> DeleteAsync(Guid id);
    }
}
