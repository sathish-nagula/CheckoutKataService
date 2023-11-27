using CheckoutService.DomainModels;

namespace CheckoutService.Tests
{
    public class CheckoutTests
    {
        private Checkout checkout;
        private List<Item> items;
        private List<DiscountRule> discountRules;

        public CheckoutTests()
        {
            items = new List<Item>()
            {
                new Item() { SKU = "A", Price = 50},
                new Item() { SKU = "B", Price = 30},
                new Item() { SKU = "C", Price = 20},
                new Item() { SKU = "D", Price = 15}
            };

            discountRules = new List<DiscountRule>()
            {
                new DiscountRule() {SKU = "A", Quantity = 3, Discount = 20},
                new DiscountRule() {SKU = "B", Quantity = 2, Discount = 15}
            };

            checkout = new Checkout(items, discountRules);
        }

        [Fact]
        public void Scan_ValidSKU_ShouldAddItemToScannedItems()
        {
            string sku = "A";
            checkout.Scan(sku);

            Assert.True(checkout.ScannedItems.ContainsKey(sku));
            Assert.Equal(1, checkout.ScannedItems[sku]);

        }

        [Fact]
        public void Scan_InvalidSKU_ShouldNotAddItemToScannedItems()
        {
            string sku = "X";
            checkout.Scan(sku);
            Assert.False(checkout.ScannedItems.ContainsKey(sku));
        }
        
        [Fact]
        public void GetTotalPrice_NoItemsScanned_ShouldReturnZero()
        {
            decimal expected = 0;
            decimal actual = checkout.GetTotalPrice();
            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotalPrice_OneItemScanned_ShouldReturnItemPrice()
        {
            string sku = "A";
            decimal expected = items.Find(i => i.SKU == sku).Price;
            
            checkout.Scan(sku);
            decimal actual = checkout.GetTotalPrice();

            Assert.Equal(expected, actual);
        }

        [Fact]

        public void GetTotalPrice_MultipleItemsScanned_ShouldReturnTotalPrice()
        {
            string[] skus = new string[] { "A", "B", "C", "D" };
            decimal expected = 0;
            foreach (var sku in skus)
            {
                expected += items.Find(i => i.SKU == sku).Price;
            }

            foreach (var sku in skus)
            {
                checkout.Scan(sku);
            }
            decimal actual = checkout.GetTotalPrice();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotalPrice_MultipleItemsScannedWithDiscounts_ShouldReturnTotalPriceWithDiscounts()
        {
            string[] skus = new string[] { "A", "A", "A", "B", "B", "C", "D" };
            decimal expected = 210;

            foreach (var sku in skus)
            {
                checkout.Scan(sku);
            }
            decimal actual = checkout.GetTotalPrice();

            Assert.Equal(expected, actual);
        }

        [Fact]
        public void GetTotalPrice_MultipleItemsScannedWithDiscounts_ExceededQuantity_ShouldReturnTotalPriceWithDiscountsOnlyForAllowedQuantity()
        {
            string[] skus = new string[] { "A", "A", "A", "A", "B", "B", "B", "C", "D", "D" };
            decimal expected = 305;

            foreach (var sku in skus)
            {
                checkout.Scan(sku);
            }
            decimal actual = checkout.GetTotalPrice();

            Assert.Equal(expected, actual);
        }

    }
}
