using Microsoft.VisualStudio.TestTools.UnitTesting;
using Acme.Biz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Acme.Biz.Tests
{
  [TestClass()]
  public class VendorTests
  {
    [TestMethod()]
    public void SendWelcomeEmail_ValidCompany_Success()
    {
      // Arrange
      var vendor = new Vendor();
      vendor.CompanyName = "ABC Corp";
      var expected = "Message sent: Hello ABC Corp";

      // Act
      var actual = vendor.SendWelcomeEmail("Test Message");

      // Assert
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void SendWelcomeEmail_EmptyCompany_Success()
    {
      // Arrange
      var vendor = new Vendor();
      vendor.CompanyName = "";
      var expected = "Message sent: Hello";

      // Act
      var actual = vendor.SendWelcomeEmail("Test Message");

      // Assert
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void SendWelcomeEmail_NullCompany_Success()
    {
      // Arrange
      var vendor = new Vendor();
      vendor.CompanyName = null;
      var expected = "Message sent: Hello";

      // Act
      var actual = vendor.SendWelcomeEmail("Test Message");

      // Assert
      Assert.AreEqual(expected, actual);
    }

    [TestMethod()]
    public void PlaceOrderTest_Success()
    {
      var vendor = new Vendor();
      var product = new Product(1, "Baby Q", "blah");
      var success = vendor.PlaceOrder(product, 1) && vendor.PlaceOrder(product, 1, new DateTimeOffset(DateTime.UtcNow.AddDays(3)));
      Assert.IsTrue(success);
    }

    [TestMethod()]
    public void PlaceOrderTest_UnsuccessfulProduct()
    {
      var vendor = new Vendor();
      var product = new Product(1, "Test", "blah");
      var success = vendor.PlaceOrder(product, 1);
      Assert.IsFalse(success);
    }

    [TestMethod()]
    public void PlaceOrderTest_UnsuccessfulDeliveryDate()
    {
      var vendor = new Vendor();
      var product = new Product(1, "Test", "blah");
      var success = vendor.PlaceOrder(product, 1, new DateTimeOffset(DateTime.UtcNow));
      Assert.IsFalse(success);
    }

    [TestMethod()]
    [ExpectedException(typeof(ArgumentNullException))]
    public void PlaceOrderTest_InvalidArguments()
    {
      var vendor = new Vendor();
      vendor.PlaceOrder(null, 1);
    }

    [TestMethod()]
    public void CalculateSuggestedPrice()
    {
      // Arrange
      var product = new Product(1, "Baby Q", "blah");
      product.Cost = 50m;
      var expectedSuggestedPrice = 55m;

      // Act
      var actualSuggestedPrice = product.CalculateSuggestedPrice(10m);

      // Assert
      Assert.AreEqual(expectedSuggestedPrice, actualSuggestedPrice);
    }

    [TestMethod()]
    public void ToStringTest()
    {
      // Arrange
      var product = new Product(1, "Baby Q", "blah");
      var expectedString = "Baby Q-1";

      // Act
      var actualString = product.ToString();

      // Assert
      Assert.AreEqual(expectedString, actualString);
    }
  }
}