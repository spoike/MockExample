namespace MockExample.BL
{
    /// <summary>
    /// Business logic class that handles orders
    /// </summary>
    public class OrderHandler
    {
        public IOrderRepository OrderRepository { get; set; }

        public Order NewOrder()
        {
            return new Order();
        }

        public void Submit(Order order)
        {
            OrderRepository.Store(order);
        }
    }
}
