using System;
using AltNetDataProvider.Domain;

namespace AltNetDataProvider.Tests
{
    public class PurchaseOrderBuilder
    {
        private string _orderNumber = "P0333";

        public PurchaseOrderBuilder SetOrderNumber(string orderNumber)
        {
            _orderNumber = orderNumber;
            return this;
        }

        private CustomerCategory _category = CustomerCategory.Normal;

        public PurchaseOrderBuilder SetCustomerCategory(CustomerCategory category)
        {
            _category = category;
            return this;
        }

        private DateTime _requiredDeliveryDate = DateTime.Today.AddDays(7);

        public PurchaseOrderBuilder SetRequiredDeliveryDate(DateTime requiredDeliveryDate)
        {
            _requiredDeliveryDate = requiredDeliveryDate;
            return this;
        }

        public PurchaseOrder Build()
        {
            return new PurchaseOrder(
                new Customer("123", "Celia Smith", _category),
                _orderNumber,
                _requiredDeliveryDate,
                new[]
                {
                    new PurchaseOrderLine(
                        new Item("Item123", "Chair", 123, 456, new Money("USD", 100.0m)),
                        10)
                });
        }
    }
}