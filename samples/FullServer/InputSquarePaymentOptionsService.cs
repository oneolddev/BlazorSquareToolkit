using BlazorSquareToolkit.WebPayments;

namespace FullServer
{
    /// <summary>
    /// Implementaton of IInputSquarePaymentOptionsService
    /// 
    /// Retrieves InputSquarePaymentOptions from configuration.
    /// </summary>
    public class InputSquarePaymentOptionsService : IInputSquarePaymentOptionsService
    {
        private readonly IConfiguration config;

        public InputSquarePaymentOptionsService(IConfiguration config)
        {
            this.config = config;
        }

        public Task<InputSquarePaymentOptions> GetOptionsAsync()
        {
            var options = new InputSquarePaymentOptions();

            config.GetSection(InputSquarePaymentOptions.Square).Bind(options);

            return Task.FromResult(options);
        }
    }
}
