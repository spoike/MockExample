namespace MockExample.BL
{
    /// <summary>
    /// Business logic class that handles orders
    /// </summary>
    public class OrderHandler
    {
        public IOrderRepository OrderRepository { get; set; }

        public IOrderPrinter OrderPrinter { get; set; }

        public IOrderView OrderView { get; set; }

        public Order NewOrder()
        {
            return new Order();
        }

        public void Submit(Order order)
        {
            if (0 != order.OrderRows.Count)
            {
                OrderRepository.Store(order);
                OrderPrinter.Print(order);
                OrderView.ShowReceipt(order);
            } else
            {
                OrderView.AlertIsEmpty();
            }
        }
    }
}
