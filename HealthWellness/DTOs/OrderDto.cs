namespace HealthWellness.DTOs
{
  // What the Angular frontend sends after Razorpay payment succeeds
  public class PlaceOrderDto
  {
    public string BuyerName { get; set; } = string.Empty;
    public string BuyerEmail { get; set; } = string.Empty;
    public string BuyerPhone { get; set; } = string.Empty;
    public string BuyerAddress { get; set; } = string.Empty;

    public string ProductName { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public decimal AmountPaid { get; set; }

    public string RazorpayPaymentId { get; set; } = string.Empty;
  }
}
