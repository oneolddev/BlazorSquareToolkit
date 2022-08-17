using BlazorSquareToolkit.WebPayments;
using Microsoft.Extensions.Options;
using System.Net.Http.Json;
using static System.Net.WebRequestMethods;

namespace FullWASMHosted.Client
{
    /// <summary>
    /// Implementaton of IInputSquarePaymentOptionsService
    /// 
    /// Retrieves InputSquarePaymentOptions from locally hosted api service.
    /// </summary>
    public class InputSquarePaymentOptionsService : IInputSquarePaymentOptionsService
    {
        private readonly HttpClient httpClient;

        public InputSquarePaymentOptionsService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }
        public async Task<InputSquarePaymentOptions> GetOptionsAsync()
        {
            var options = await httpClient.GetFromJsonAsync<InputSquarePaymentOptions>("api/SquarePaymentOptions");

            options ??= new InputSquarePaymentOptions();

            return await Task.FromResult(options);
        }
    }
}
