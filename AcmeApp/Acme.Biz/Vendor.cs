using Acme.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz
{
  /// <summary>
  /// Manages the vendors from whom we purchase our inventory.
  /// </summary>
  public class Vendor
  {
    public int VendorId { get; set; }
    public string CompanyName { get; set; }
    public string Email { get; set; }

    /// <summary>
    /// Sends an email to welcome a new vendor.
    /// </summary>
    /// <returns></returns>
    public string SendWelcomeEmail(string message)
    {
      var emailService = new EmailService();
      var subject = ("Hello " + this.CompanyName).Trim();
      var confirmation = emailService.SendMessage(subject,
                                                  message,
                                                  this.Email);
      return confirmation;
    }

    /// <summary>
    /// Place an Order for a product with the specified quantity.
    /// </summary>
    /// <param name="product">The product</param>
    /// <param name="quantity">Quanity of Products to order</param>
    /// <returns></returns>
    public bool PlaceOrder(Product product, int quantity)
    {
      return PlaceOrder(product, quantity, null);
    }

    public bool PlaceOrder(Product product, int quantity, DateTimeOffset? deliverBy)
    {
      if (product == null)
        throw new ArgumentNullException(nameof(product));
      if (quantity <= 0)
        throw new ArgumentOutOfRangeException(nameof(quantity));

      var validDeliveryByDate = deliverBy != null ? deliverBy.Value.Ticks >= DateTime.UtcNow.AddDays(2).Ticks : true;
      return !product.ProductName.StartsWith("Test") && validDeliveryByDate;
    }
  }
}
