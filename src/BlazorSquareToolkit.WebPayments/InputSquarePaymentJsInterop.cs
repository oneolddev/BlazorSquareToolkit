using Microsoft.JSInterop;
using System.Text.Json;

namespace BlazorSquareToolkit.WebPayments;

internal class InputSquarePaymentJsInterop : IAsyncDisposable
{
    private readonly Lazy<Task<IJSObjectReference>> moduleTask;
    private readonly DotNetObjectReference<InputSquarePayment> dotnetRef;
    private IJSObjectReference module = default!;

    public InputSquarePaymentJsInterop(IJSRuntime jsRuntime, DotNetObjectReference<InputSquarePayment> dotnet)
    {
        moduleTask = new(() => jsRuntime.InvokeAsync<IJSObjectReference>(
            "import", "./_content/BlazorSquareToolkit.WebPayments/InputSquarePayment.razor.js").AsTask());

        this.dotnetRef = dotnet;
    }
    public async ValueTask DisposeAsync()
    {
        if (moduleTask.IsValueCreated)
        {
            await this.module.DisposeAsync();
            //
            dotnetRef.Dispose();
        }
    }
    public async ValueTask Initialize(InputSquarePaymentOptions paymentOptions)
    {
        this.module = await moduleTask.Value;
        await this.module.InvokeVoidAsync(
            "initialize",
            paymentOptions.Environment,
            paymentOptions.ApplicationId,
            paymentOptions.LocationId,
            dotnetRef);
    }

    public async ValueTask<InputSquarePaymentTokenizeResponse> Tokenize()
    {
        var resp = await this.module.InvokeAsync<string>("tokenize");
        var tokenizeResponse = JsonSerializer
            .Deserialize<InputSquarePaymentTokenizeResponse>(
            resp,
            new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

        return tokenizeResponse ??= new InputSquarePaymentTokenizeResponse();
    }
}
