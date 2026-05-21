using System.Security.Cryptography;
using System.Text;

namespace VGProducts.Unit
{
    public class EncryptDecrypt
    {
        private byte[] DeriveKeyFromPassword(string password)
        {
            var salt = Array.Empty<byte>(); // You can use a fixed salt or generate one dynamically
            var iterations = 10000; // Number of iterations for key derivation
            var derivedKeyLength = 16; // Desired key length in bytes (256 bits)
            var hashMethod = HashAlgorithmName.SHA256; // Hash algorithm for key derivation
            return Rfc2898DeriveBytes.Pbkdf2(
                Encoding.Unicode.GetBytes(password),
                salt,
                iterations,
                hashMethod,
                derivedKeyLength);
        }
        private byte[] iv = { 0x01, 0x02, 0x03, 0x04, 0x05, 0x06, 0x07, 0x08, 0x09, 0x10, 0x11, 0x12, 0x13, 0x14, 0x15, 0x16 };

        public async Task<string> EncryptAsync(string clearText, string passphrase)
        {
            try
            {
                if (string.IsNullOrEmpty(clearText))
                   return string.Empty;

                using (Aes aes = Aes.Create())
                {
                    aes.Padding = PaddingMode.PKCS7;
                    aes.Key = DeriveKeyFromPassword(passphrase);
                    aes.IV = this.iv;

                    using MemoryStream output = new();
                    using CryptoStream cryptoStream = new(output, aes.CreateEncryptor(), CryptoStreamMode.Write);

                    await cryptoStream.WriteAsync(Encoding.UTF8.GetBytes(clearText));
                    await cryptoStream.FlushFinalBlockAsync();

                    return Convert.ToBase64String(output.ToArray());
                }
            }
            catch (Exception e)
            {
                throw;
            }
        }
        public async Task<string> DecryptAsync(byte[] encrypted, string passphrase)
        {
            byte[] encryptedBytes = Convert.FromBase64String(Encoding.UTF8.GetString(encrypted));

            using (Aes aes = Aes.Create())
            {
                aes.Padding = PaddingMode.PKCS7;
                aes.Key = DeriveKeyFromPassword(passphrase);
                aes.IV = this.iv;

                using MemoryStream input = new(encrypted);
                using CryptoStream cryptoStream = new(input, aes.CreateDecryptor(), CryptoStreamMode.Read);

                using MemoryStream output = new();
                await cryptoStream.CopyToAsync(output);

                return Encoding.UTF8.GetString(output.ToArray());
            }
        }
    }
}
