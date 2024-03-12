using LavaKassa.BLL.Services;
using LavaKassa.DAL.Configs;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace LavaKassa
{
    public class Program
    {
        private readonly IServiceProvider _service;

        private readonly AppSettings _appsettings;

        public Program()
        {
            var appsettingsJson = File.ReadAllText("appsettings.json");

            _appsettings = JsonConvert.DeserializeObject<AppSettings>(appsettingsJson) ?? throw new InvalidOperationException("Failed to load configuration.");

            _service = new ServiceCollection()
                .AddSingleton(_appsettings)
                /// Services
                .AddSingleton<LavaService>()
                .AddSingleton<HashService>()
                /// Other
                .AddSingleton<SignatureService>()
               .BuildServiceProvider();
        }

        static void Main(string[] args)
           => new Program().RunAsync().GetAwaiter().GetResult();

        private async Task RunAsync()
        {
            var lavaService = _service.GetService<LavaService>();

            Console.WriteLine("Введите сумму для создания платежа: ");
            while (true)
            {
                try
                {
                    var sum = int.Parse(Console.ReadLine());

                    var paymentResponse = await lavaService.GeneratePayment(price: sum);

                    Console.WriteLine($"Url для оплаты: {paymentResponse.Data.Url}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"Произошла ошибка: {ex.Message}");
                }
            }
        }
    }
}
