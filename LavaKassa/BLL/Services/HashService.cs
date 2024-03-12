using System.Security.Cryptography;
using System.Text;

namespace LavaKassa.BLL.Services
{
    public class HashService
    {
        public string GenerateSha256Hash(string serializeData, string secretKey)
        {
            try
            {
                var data = Encoding.UTF8.GetBytes(serializeData);
                var key = Encoding.UTF8.GetBytes(secretKey);

                using (var hmac = new HMACSHA256(key))
                {
                    var hash = hmac.ComputeHash(data);

                    return BitConverter.ToString(hash).Replace("-", "").ToLower();
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при вычислении HMAC SHA256: {ex.Message}");
            }
        }
    }
}
