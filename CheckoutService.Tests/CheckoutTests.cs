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


    }
}
