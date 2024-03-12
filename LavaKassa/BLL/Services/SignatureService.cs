using LavaKassa.DAL.Configs;
using LavaKassa.DAL.Models.Request;
using Newtonsoft.Json;

namespace LavaKassa.BLL.Services
{
    public class SignatureService
    {
        private readonly HashService _hashService;

        private readonly AppSettings _appsettings;

        public SignatureService(HashService hashService, AppSettings appsettings)
        {
            _hashService = hashService;
            _appsettings = appsettings;
        }

        public LavaSignature GetPaymentSignature(int price)
        {
            try
            {
                var requestData = new
                {
                    comment = "Покупка товара на Royal Shop",
                    orderId = Guid.NewGuid().ToString(),
                    shopId = _appsettings.ShopId,
                    sum = price
                };

                var jsonRequest = JsonConvert.SerializeObject(requestData);

                var signature = _hashService.GenerateSha256Hash(jsonRequest, _appsettings.SecretKey);

                return new LavaSignature() { RequestData = jsonRequest, Signature = signature };
            }
            catch (Exception ex)
            {
                throw new Exception("Ошибка при генерации Signature", ex);
            }
        }
    }
}
