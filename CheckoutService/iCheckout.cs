namespace CheckoutService
{
    public interface ICheckout
    {
        void Scan(string item);
        decimal GetTotalPrice();
    }
}
