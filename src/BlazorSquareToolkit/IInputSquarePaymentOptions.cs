namespace BlazorSquareToolkit;

public class InputSquarePaymentOptions
{
    public const string Square = "Square";
    public string Environment { get; set; } = string.Empty;
    public string ApplicationId { get; set; } = string.Empty;
    public string LocationId { get; set; } = string.Empty;
}