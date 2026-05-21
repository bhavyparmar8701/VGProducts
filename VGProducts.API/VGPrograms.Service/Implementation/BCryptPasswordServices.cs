using VGProducts.Repository.Interface;

namespace VGProducts.Repository.Implementation
{
    public class BCryptPasswordServices : IPasswordServices
    {
        private const int WorkFatory = 12;

        public string HashPassword(string password)
        {
            return BCrypt.Net.BCrypt.HashPassword(password);
        }

        public bool VerifyPassword(string hashedPassword, string Password)
        {
            return BCrypt.Net.BCrypt.Verify(Password, hashedPassword);
        }
    }
}
