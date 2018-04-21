namespace AltNetDataProvider.Domain
{
    public class PurchaseOrderLine
    {
        public PurchaseOrderLine(Item item, int quantity)
        {
            Item = item;
            Quantity = quantity;
        }

        public Item Item { get;}
        public int Quantity { get; }
    }
}