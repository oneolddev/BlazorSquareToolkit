using Simple;
using BlazorSquareToolkit.WebPayments;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddScoped<InputSquarePaymentOptions>(
    sp =>
    {
        var options = new InputSquarePaymentOptions();
        builder.Configuration.GetSection(InputSquarePaymentOptions.Square).Bind(options);
        return options;
    });

await builder.Build().RunAsync();
