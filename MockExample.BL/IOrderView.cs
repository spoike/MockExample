namespace MockExample.BL
{
    public interface IOrderView
    {
        void ShowReceipt(Order o);
        void AlertIsEmpty();
    }
}