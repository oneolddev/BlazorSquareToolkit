using Microsoft.AspNetCore.Components;
using Microsoft.JSInterop;
using System.Diagnostics.CodeAnalysis;

namespace BlazorSquareToolkit.WebPayments
{
    public partial class InputSquarePayment
    {
        [Parameter]
        public string Nonce { get; set; } = default !;
        ElementReference InputReference;
        [Inject]
        InputSquarePaymentOptions options { get; set; } = default !;
        [Inject]
        IJSRuntime JsRuntime { get; set; } = default !;
        InputSquarePaymentJsInterop JsInterop { get; set; } = default !;
        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (firstRender)
            {
                JsInterop = new InputSquarePaymentJsInterop(JsRuntime, DotNetObjectReference.Create(this));
                await JsInterop.Initialize(options);
            }
        }

        /// <summary>
        /// Return a TokenizeResponse object
        /// </summary>
        /// <returns>TokenizeResponse</returns>
        public async Task<InputSquarePaymentTokenizeResponse> Tokenize()
        {
            var tokenizeResponse = await JsInterop.Tokenize();
            CurrentValue = tokenizeResponse.Token;
            return tokenizeResponse;
        }

        /// <summary>
        /// Called by javascript when credit card field loses focus.
        /// </summary>
        /// <param name = "status">true only if all credit card input fields are valid</param>
        /// <returns></returns>
        [JSInvokable]
        public Task OnChange(bool status)
        {
            CurrentValue = status ? "cnon:" : string.Empty;
            return Task.CompletedTask;
        }

        protected override bool TryParseValueFromString(string? value, [MaybeNullWhen(false)] out string result, [NotNullWhen(false)] out string? validationErrorMessage)
        {
            throw new NotImplementedException();
        }
    }
}