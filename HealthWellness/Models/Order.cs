namespace HealthWellness.Models
{
  public class Order
  {
    public int Id { get; set; }

    // Buyer details (no login required)
    public string BuyerName { get; set; } = string.Empty;
    public string BuyerEmail { get; set; } = string.Empty;
    public string BuyerPhone { get; set; } = string.Empty;
    public string BuyerAddress { get; set; } = string.Empty;

    // Product details
    public string ProductName { get; set; } = string.Empty;
    public int ProductId { get; set; }
    public decimal AmountPaid { get; set; }   // in rupees (e.g. 149.00)

    // Razorpay proof
    public string RazorpayPaymentId { get; set; } = string.Empty;

    // Status tracking
    public string Status { get; set; } = "Confirmed"; // Confirmed, Shipped, Delivered
    public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

    // Optional: link to a registered user if they were logged in
    public int? UserId { get; set; }
    public User? User { get; set; }
  }
}
