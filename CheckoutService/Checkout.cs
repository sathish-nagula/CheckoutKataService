using CheckoutService.DomainModels;

namespace CheckoutService
{
    public class Checkout : ICheckout
    {
        private List<Item> items;
        private List<DiscountRule> discountRules;
        public Dictionary<string, int> ScannedItems;
        private decimal total;

        public Checkout(List<Item> items, List<DiscountRule> discountRules)
        {
            this.items = items;
            this.discountRules = discountRules;
            ScannedItems = new Dictionary<string, int>();
            total = 0;
        }

        public int GetTotalPrice()
        {
            throw new NotImplementedException();
        }

        public void Scan(string sku)
        {
            if (items.Exists(i => i.SKU == sku))
            {
                if (ScannedItems.ContainsKey(sku))
                {
                    ScannedItems[sku]++;
                }
                else
                {
                    ScannedItems[sku] = 1;
                }
            }
        }
    }
}
