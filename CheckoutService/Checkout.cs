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
                var discountRule = discountRules.Find(d => d.SKU == item.Key);
                if (itemInfo != null)
                {
                    if (discountRule != null && item.Value >= discountRule.Quantity)
                    {
                        int quotient = item.Value / discountRule.Quantity;
                        int remainder = item.Value % discountRule.Quantity;

                        total += quotient * (itemInfo.Price * discountRule.Quantity - discountRule.Discount) + remainder * itemInfo.Price;

                    }
                    else
                    {
                        total += item.Value * itemInfo.Price;
                    }
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
