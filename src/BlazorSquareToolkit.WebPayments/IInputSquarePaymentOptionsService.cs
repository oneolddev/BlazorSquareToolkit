namespace BlazorSquareToolkit.WebPayments;

/// <summary>
/// This interface decouples and allows for the asynchronous the retrieval of the InputSquarePaymentOptions
/// necessary to initialize the InputSquarePayments control.
/// </summary>
public interface IInputSquarePaymentOptionsService
{
    /// <summary>
    /// Asynchronous retrieval of options for InputSquarePayment control.
    /// </summary>
    /// <returns>InputSquarePaymentOptions</returns>
    Task<InputSquarePaymentOptions> GetOptionsAsync();
}
