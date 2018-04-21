using AltNetDataProvider.Domain;

namespace AltNetDataProvider.Tests
{
    public class CustomerBuilder
    {
        private CustomerCategory _category = CustomerCategory.Normal;

        public CustomerBuilder SetCategory(CustomerCategory category)
        {
            _category = category;
            return this;
        }

        public Customer Build()
        {
            return new TestObjectBuilder<Customer>()
                .SetArgument(o => o.Category, _category)
                .Build();
        }
    }
}