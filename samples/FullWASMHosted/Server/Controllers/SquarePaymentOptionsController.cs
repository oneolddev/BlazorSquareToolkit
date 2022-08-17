using BlazorSquareToolkit.WebPayments;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace FullWASMHosted.Server.Controllers;

[Route("api/[controller]")]
[ApiController]
public class SquarePaymentOptionsController : ControllerBase
{
    private readonly ILogger<SquarePaymentOptionsController> logger;
    private readonly IConfiguration config;

    public SquarePaymentOptionsController(ILogger<SquarePaymentOptionsController> logger, IConfiguration config)
    {
        this.logger = logger;
        this.config = config;
    }

    [HttpGet]
    public InputSquarePaymentOptions Get()
    {
        var options = new InputSquarePaymentOptions();

        config.GetSection(InputSquarePaymentOptions.Square).Bind(options);

        return options;
    }
}
