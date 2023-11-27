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

        public decimal GetTotalPrice()
        {
            total = 0;
            foreach (var item in ScannedItems)
            {
                var itemInfo = items.Find(i => i.SKU == item.Key);
                if (itemInfo != null)
                {
                    total += itemInfo.Price;    
                }

            }
            return total;
        }

        public void Scan(string sku)
        {
            if (!items.Exists(i => i.SKU == sku))
            {
                return;
            }
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
