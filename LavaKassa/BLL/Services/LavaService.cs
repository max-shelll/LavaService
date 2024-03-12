using LavaKassa.DAL.Models.Response;
using System.Net;
using System.Text;

namespace LavaKassa.BLL.Services
{
    public class LavaService
    {
        private readonly SignatureService _signatureService;

        public LavaService(SignatureService signatureService)
        {
            _signatureService = signatureService;
        }

        public async Task<Payment> GeneratePayment(int price)
        {
            try
            {
                var lavaSignature = _signatureService.GetPaymentSignature(price);

                var httpClient = new HttpClient();
                var request = new HttpRequestMessage(HttpMethod.Post, "https://api.lava.ru/business/invoice/create");

                request.Headers.Add("Accept", "application/json");
                request.Headers.Add("Signature", lavaSignature.Signature);

                request.Content = new StringContent(lavaSignature.RequestData, Encoding.UTF8, "application/json");

                var response = await httpClient.SendAsync(request);

                if (response.StatusCode != HttpStatusCode.OK)
                {
                    string str = await response.Content.ReadAsStringAsync();
                    throw new Exception("Response is " + str + "\r\n" + "Code is " + response.StatusCode);
                }

                var responseBody = await response.Content.ReadAsStringAsync();

                return System.Text.Json.JsonSerializer.Deserialize<Payment>(responseBody);
            }
            catch (Exception ex)
            {
                throw new Exception($"Ошибка при генерации Payment\nMessage: {ex.Message}");
            }
        }
    }
}
