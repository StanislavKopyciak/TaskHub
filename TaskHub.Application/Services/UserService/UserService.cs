using TaskHub.Application.DTO.User;
using TaskHub.Application.Interfaces;
using TaskHub.Core.Entities;

namespace TaskHub.Application.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly IUserRepository<User> _userRepository;

        public UserService (IUserRepository<User> userRepository)
        {
            _userRepository = userRepository;
        }


        public async Task<UserDTO> AddAsync(UserSignUpDTO user)
        {
            var entity = ToEntity(user);
            var added = await _userRepository.AddAsync(entity);
            return ToDto(added);
        }

        public async Task<int> DeleteAsync(Guid id)
        {
            return await _userRepository.DeleteAsync(id);
        }

        public async Task<UserDTO> GetByIdAsync(Guid id)
        {
            var user = await _userRepository.GetByIdAsync(id);
            return ToDto(user);
        }

        public async Task<int> UpdateAsync(Guid id, UserSignUpDTO user)
        {
            var entity = ToEntity(user);
            return await _userRepository.UpdateAsync(id, entity);
        }

        private UserDTO ToDto(User user)
        {
            return new UserDTO
            {
                UserId = user.UserId,
                Name = user.Name,
                Email = user.Email
            };
        }

        private User ToEntity(UserSignUpDTO dto)
        {
            return new User
            {
                Name = dto.Name,
                Email = dto.Email,
                Password = dto.Password
            };
        }

    }
}
