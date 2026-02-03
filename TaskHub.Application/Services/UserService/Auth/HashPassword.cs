using Microsoft.Extensions.Options;
using System.Security.Cryptography;
using TaskHub.Core.Model;

namespace TaskHub.Application.Services.UserService.Auth
{
    public class PasswordHasher : IPasswordHasher
    {
        private readonly PBKDF2Section _settings;

        public PasswordHasher(IOptions<PBKDF2Section> settings)
        {
            _settings = settings.Value;
        }

        public string Hash(string password)
        {
            byte[] salt = RandomNumberGenerator.GetBytes(_settings.SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                _settings.IterationCount,
                HashAlgorithmName.SHA256);

            byte[] hash = pbkdf2.GetBytes(_settings.KeySize);

            byte[] hashBytes = new byte[_settings.SaltSize + _settings.KeySize];

            Array.Copy(salt, 0, hashBytes, 0, _settings.SaltSize);
            Array.Copy(hash, 0, hashBytes, _settings.SaltSize, _settings.KeySize);

            return Convert.ToBase64String(hashBytes);
        }

        public bool Verify(string password, string storedHash)
        {
            byte[] hashBytes = Convert.FromBase64String(storedHash);

            byte[] salt = new byte[_settings.SaltSize];
            Array.Copy(hashBytes, 0, salt, 0, _settings.SaltSize);

            var pbkdf2 = new Rfc2898DeriveBytes(
                password,
                salt,
                _settings.IterationCount,
                HashAlgorithmName.SHA256);

            byte[] hash = pbkdf2.GetBytes(_settings.KeySize);

            for (int i = 0; i < _settings.KeySize; i++)
            {
                if (hashBytes[i + _settings.SaltSize] != hash[i])
                    return false;
            }

            return true;
        }
    }
}
