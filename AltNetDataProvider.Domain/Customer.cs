namespace AltNetDataProvider.Domain
{
    public class Customer
    {
        public Customer(string id, string name, CustomerCategory category)
        {
            Id = id;
            Name = name;
            Category = category;
        }

        public string Id { get; }
        public string Name { get; }
        public CustomerCategory Category { get; }
    }

    public enum CustomerCategory
    {
        Normal = 0,
        Vip = 1
    }
}