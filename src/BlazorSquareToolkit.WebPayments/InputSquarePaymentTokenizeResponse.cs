namespace BlazorSquareToolkit.WebPayments;

public class InputSquarePaymentTokenizeResponse
{
    public class FieldError
    {
        public string? Field { get; set; }
        public string? Message { get; set; }
        public string? Type { get; set; }
    }

    public class Card
    {
        public string? Brand { get; set; }
        public int? ExpMonth { get; set; }
        public int? ExpYear { get; set; }
        public string? Last4 { get; set; }
    }

    public class Billing
    {
        public string? PostalCode { get; set; }
    }

    public class CardDetails
    {
        public Card? Card { get; set; }
        public string? Method { get; set; }
        public Billing? Billing { get; set; }
    }

    public string Status { get; set; } = String.Empty;
    public CardDetails? Details { get; set; }
    public string Token { get; set; } = String.Empty;
    public List<FieldError>? Errors { get; set; }

    public bool IsValid()
    {
        return this.Status.Contains("OK");
    }
}
