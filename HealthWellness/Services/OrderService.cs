using HealthWellness.Data;
using HealthWellness.DTOs;
using HealthWellness.Interfaces;
using HealthWellness.Models;
using System.Net;
using System.Net.Mail;

namespace HealthWellness.Services
{
  public class OrderService : IOrderService
  {
    private readonly WellnessDbContext _db;
    private readonly IConfiguration _config;

    public OrderService(WellnessDbContext db, IConfiguration config)
    {
      _db = db;
      _config = config;
    }

    public async Task<Order> PlaceOrderAsync(PlaceOrderDto dto)
    {
      // 1️⃣  Save order to database
      var order = new Order
      {
        BuyerName = dto.BuyerName,
        BuyerEmail = dto.BuyerEmail,
        BuyerPhone = dto.BuyerPhone,
        BuyerAddress = dto.BuyerAddress,
        ProductName = dto.ProductName,
        ProductId = dto.ProductId,
        AmountPaid = dto.AmountPaid,
        RazorpayPaymentId = dto.RazorpayPaymentId,
        Status = "Confirmed",
        CreatedAt = DateTime.UtcNow
      };

      _db.Orders.Add(order);
      await _db.SaveChangesAsync();

      // 2️⃣  Send confirmation email to buyer
      await SendBuyerEmailAsync(order);

      // 3️⃣  Send notification email to you (admin)
      await SendAdminEmailAsync(order);

      return order;
    }

    public IEnumerable<Order> GetAll()
        => _db.Orders.OrderByDescending(o => o.CreatedAt).ToList();

    // ── Email to the customer ──────────────────────────────────────────
    private async Task SendBuyerEmailAsync(Order order)
    {
      var subject = $"✅ Order Confirmed — {order.ProductName} | Satvik Sanctuary";
      var body = $@"
<div style='font-family:Georgia,serif;max-width:560px;margin:0 auto;color:#2C1810'>
  <div style='background:#2C1810;padding:28px 32px;text-align:center'>
    <h1 style='color:#D4A017;font-size:26px;margin:0;letter-spacing:1px'>Satvik Sanctuary</h1>
    <p style='color:#F5EDD0;margin:6px 0 0;font-size:13px'>Ancient wisdom for modern living</p>
  </div>

  <div style='padding:36px 32px;background:#FAF6EE;border:1px solid #E8DCC8'>
    <h2 style='color:#2C1810;font-size:22px;margin:0 0 6px'>Your order is confirmed 🌿</h2>
    <p style='color:#6B4C3B;font-size:14px;margin:0 0 28px'>
      Thank you, {order.BuyerName}! We've received your order and are preparing it with care.
    </p>

    <div style='background:#FFF8EE;border:1px solid rgba(184,134,11,0.2);border-radius:4px;padding:20px 24px;margin-bottom:24px'>
      <table style='width:100%;font-size:14px'>
        <tr><td style='color:#9B7B6A;padding:6px 0'>Product</td>   <td style='color:#2C1810;font-weight:500;text-align:right'>{order.ProductName}</td></tr>
        <tr><td style='color:#9B7B6A;padding:6px 0'>Amount Paid</td><td style='color:#2C1810;font-weight:500;text-align:right'>₹{order.AmountPaid:F0}</td></tr>
        <tr><td style='color:#9B7B6A;padding:6px 0'>Payment ID</td> <td style='color:#2C1810;font-size:12px;text-align:right'>{order.RazorpayPaymentId}</td></tr>
        <tr><td style='color:#9B7B6A;padding:6px 0'>Order Date</td> <td style='color:#2C1810;text-align:right'>{order.CreatedAt:dd MMM yyyy}</td></tr>
        <tr><td style='color:#9B7B6A;padding:6px 0'>Status</td>     <td style='text-align:right'><span style='background:#D4EDDA;color:#155724;padding:2px 10px;border-radius:12px;font-size:12px'>Confirmed</span></td></tr>
      </table>
    </div>

    <p style='color:#6B4C3B;font-size:13px;line-height:1.7'>
      We'll dispatch your order within <strong>2–3 business days</strong> and send you tracking details at this email.
      For any questions, reply to this email or reach us at
      <a href='mailto:sattviksanctuary@gmail.com' style='color:#B8860B'>sattviksanctuary@gmail.com</a>.
    </p>
  </div>

  <div style='padding:18px 32px;background:#2C1810;text-align:center'>
    <p style='color:rgba(245,237,208,0.5);font-size:11px;margin:0'>
      © 2025 Satvik Sanctuary · Bhopal, Madhya Pradesh<br>
      <a href='https://www.instagram.com/sattviksanctuary/' style='color:#D4A017'>@sattviksanctuary</a>
    </p>
  </div>
</div>";

      await SendEmailAsync(order.BuyerEmail, subject, body);
    }

    // ── Alert email to you (admin) ─────────────────────────────────────
    private async Task SendAdminEmailAsync(Order order)
    {
      var adminEmail = _config["Email:AdminEmail"]!;
      var subject = $"🛒 New Order #{order.Id} — {order.ProductName}";
      var body = $@"
<div style='font-family:Arial,sans-serif;max-width:480px'>
  <h2 style='color:#2C1810'>New Order Received</h2>
  <table style='width:100%;font-size:14px;border-collapse:collapse'>
    <tr><td style='padding:8px;background:#FAF6EE;color:#9B7B6A;width:40%'>Order ID</td>     <td style='padding:8px'>#{order.Id}</td></tr>
    <tr><td style='padding:8px;color:#9B7B6A'>Buyer Name</td>    <td style='padding:8px'>{order.BuyerName}</td></tr>
    <tr><td style='padding:8px;background:#FAF6EE;color:#9B7B6A'>Email</td>         <td style='padding:8px'>{order.BuyerEmail}</td></tr>
    <tr><td style='padding:8px;color:#9B7B6A'>Phone</td>         <td style='padding:8px'>{order.BuyerPhone}</td></tr>
    <tr><td style='padding:8px;background:#FAF6EE;color:#9B7B6A'>Address</td>       <td style='padding:8px'>{order.BuyerAddress}</td></tr>
    <tr><td style='padding:8px;color:#9B7B6A'>Product</td>       <td style='padding:8px'>{order.ProductName}</td></tr>
    <tr><td style='padding:8px;background:#FAF6EE;color:#9B7B6A'>Amount</td>        <td style='padding:8px'><strong>₹{order.AmountPaid:F0}</strong></td></tr>
    <tr><td style='padding:8px;color:#9B7B6A'>Payment ID</td>    <td style='padding:8px'>{order.RazorpayPaymentId}</td></tr>
    <tr><td style='padding:8px;background:#FAF6EE;color:#9B7B6A'>Ordered At</td>    <td style='padding:8px'>{order.CreatedAt:dd MMM yyyy, hh:mm tt} UTC</td></tr>
  </table>
</div>";

      await SendEmailAsync(adminEmail, subject, body);
    }

    // ── Core SMTP send ─────────────────────────────────────────────────
    private async Task SendEmailAsync(string toEmail, string subject, string htmlBody)
    {
      var fromEmail = _config["Email:From"]!;
      var password = _config["Email:Password"]!;

      using var client = new SmtpClient("smtp.gmail.com", 587)
      {
        EnableSsl = true,
        Credentials = new NetworkCredential(fromEmail, password)
      };

      var mail = new MailMessage
      {
        From = new MailAddress(fromEmail, "Satvik Sanctuary"),
        Subject = subject,
        Body = htmlBody,
        IsBodyHtml = true
      };
      mail.To.Add(toEmail);

      await client.SendMailAsync(mail);
    }
  }
}
