using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;

namespace SupermarketRegister
{
   [TestFixture]
   public class PurchaseFixture
   {
      [Test]
      public void TestPurchase()
      {
         var receipt = new Receipt();
         receipt.AddItem(1, "Candy Bar", 0.50);
         receipt.AddItem(2, "Soda", 1);
         var expected =
            @"1 Candy Bar @ $0.50 = $0.50
2 Soda @ $1.00 = $2.00
	SubTotal = $2.50
	Tax (10%) = $0.25
	Total = $2.75";
         Assert.AreEqual(expected, receipt.ToString());
      }
   }

   public class Receipt
   {
      readonly List<LineItem> lineItems = new List<LineItem>();

      public void AddItem(int qty, string item, double price)
      {
         lineItems.Add(new LineItem {Qty = qty, Item = item, Price = price});
      }

      double SubTotal
      {
         get { return lineItems.Sum(x => x.Total); }
      }

      double Tax
      {
         get { return SubTotal*.1; }
      }

      double Total
      {
         get { return SubTotal + Tax; }
      }

      public override string ToString()
      {
         var sb = new StringBuilder();
         foreach (var lineItem in lineItems)
         {
            sb.AppendLine(lineItem.ToString());
         }
         sb.AppendLine(string.Format("	SubTotal = {0}", SubTotal.ToString("C")));
         sb.AppendLine(string.Format("	Tax (10%) = {0}", Tax.ToString("C")));
         sb.Append(string.Format("	Total = {0}", Total.ToString("C")));
         return sb.ToString();
      }
   }

   public class LineItem
   {
      public int Qty { get; set; }
      public string Item { get; set; }
      public double Price { get; set; }

      public double Total
      {
         get { return Qty*Price; }
      }

      public override string ToString()
      {
         return string.Format("{0} {1} @ {2} = {3}", Qty, Item, Price.ToString("C"), Total.ToString("C"));
      }
   }
}