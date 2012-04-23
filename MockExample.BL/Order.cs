using System.Collections.Generic;

namespace MockExample.BL
{
    public class Order
    {
        public IList<OrderRow> OrderRows { get; set; }

        public decimal OrderTotal { get; set; }

        public Order()
        {
            OrderRows = new List<OrderRow>();
        }
    }
}