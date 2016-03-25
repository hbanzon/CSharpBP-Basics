using Acme.Common;
using static Acme.Common.LoggingService;  // this is a C# 6 feature
using System;

namespace Acme.Biz
{
  /// <summary>
  /// Manages products carried in inventory.
  /// </summary>
  public class Product
  {
    public Product()
    {
      Console.WriteLine("Product instance has been created");
    }

    public Product(int productId, string productName, string description) : this()
    {
      this.ProductName = productName;
      this.ProductId = productId;
      this.Description = description;
      Console.WriteLine("Product instance has a name: " + this.ProductName);
    }

    private string productName;

    public string ProductName
    {
      get { return productName; }
      set { productName = value; }
    }

    public decimal Cost { get; set; }

    private string description;

    public string Description
    {
      get { return description; }
      set { description = value; }
    }

    private int productId;

    public int ProductId
    {
      get { return productId; }
      set { productId = value; }
    }

    private Vendor productVendor;

    public Vendor ProductVendor
    {
      get {
        if (productVendor == null)
          productVendor = new Vendor();
        return productVendor;
      }

      set { productVendor = value; }
    }

    public string SayHello()
    {
      //var vendor = new Vendor();
      //vendor.SendWelcomeEmail("Message from Product");

      var emailService = new EmailService();
      var confirmation = emailService.SendMessage("New Product", this.ProductName, "sales@mtroapp.com");

      var result = LogAction("Say Hello");

      return "Hello " + ProductName
          + " (" + ProductId + "): "
          + Description;
    }

    /// <summary>
    /// Calculate the Suggested Price.  Example of an Expression-Bodied Method
    /// </summary>
    /// <param name="markupPercent"></param>
    /// <returns></returns>
    public decimal CalculateSuggestedPrice(decimal markupPercent) 
      => this.Cost + (this.Cost * markupPercent/100);

    public override string ToString() => $"{ProductName}-{ProductId}";
   
  }
}
