namespace VGProducts.Repository.Interface
{
    public interface IPasswordServices
    {
        string HashPassword(string password);
        bool VerifyPassword(string Password, string hashedPassword);
    }
}
