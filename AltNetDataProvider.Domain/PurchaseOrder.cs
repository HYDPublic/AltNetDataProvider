using System;
using System.Collections.Generic;
using System.Linq;

namespace AltNetDataProvider.Domain
{
    public class PurchaseOrder
    {
        public PurchaseOrder(Customer customer, 
            string orderNumber, 
            DateTime? requiredDeliveryDate,
            IEnumerable<PurchaseOrderLine> lines)
        {
            if (string.IsNullOrWhiteSpace(orderNumber))
                throw new ArgumentException("Value cannot be null or whitespace.", nameof(orderNumber));
            Customer = customer ?? throw new ArgumentNullException(nameof(customer));
            OrderNumber = orderNumber;

            if (customer.Category == CustomerCategory.Normal && requiredDeliveryDate < DateTime.Today)
            {
                throw new ArgumentException("Cannot request delivery in the past", nameof(requiredDeliveryDate));
            }

            RequiredDeliveryDate = requiredDeliveryDate;
            Lines = lines?.ToArray() ?? throw new ArgumentNullException(nameof(lines));
            CreatedTimestamp = DateTime.UtcNow;
        }

        public Customer Customer { get; }
        public string OrderNumber { get; }
        public DateTime? RequiredDeliveryDate { get; }
        public DateTime CreatedTimestamp { get; }
        public PurchaseOrderLine[] Lines { get; }
    }
}