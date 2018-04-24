using System;
using AltNetDataProvider.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace AltNetDataProvider.Tests
{
    [TestFixture]
    public class PurchaseOrderTestsWithTestObjectBuilder
    {
        [Test]
        public void SimpleConstruction()
        {
            var po = new TestObjectBuilder<PurchaseOrder>()
                .SetArgument(o => o.RequiredDeliveryDate, DateTime.Today.AddDays(7))
                .Build();
            po.Should().NotBeNull();
            po.RequiredDeliveryDate.Should().Be(DateTime.Today.AddDays(7));
        }

        [Test]
        public void ItemWeight_CannotBeZero()
        {
            Assert.Throws<ArgumentException>(() => new TestObjectBuilder<Item>()
                    .SetArgument(o => o.Weight, 0)
                    .Build())
                .ParamName.Should().Be("weight");
        }

        [Test]
        public void NormalCustomer_CannotRequestDeliveryInThePast()
        {
            var customer = new CustomerBuilder()
                .SetCategory(CustomerCategory.Normal)
                .Build();

            Assert.Throws<ArgumentException>(() => new TestObjectBuilder<PurchaseOrder>()
                .SetArgument(o => o.Customer, customer)
                .SetArgument(o => o.RequiredDeliveryDate, DateTime.Today.AddDays(-1))
                .Build());
        }

        [Test]
        public void VipCustomer_CanRequestDeliveryInThePast()
        {
            var customer = new CustomerBuilder()
                .SetCategory(CustomerCategory.Vip)
                .Build();

            Assert.DoesNotThrow(() => new TestObjectBuilder<PurchaseOrder>()
                .SetArgument(o => o.Customer, customer)
                .SetArgument(o => o.RequiredDeliveryDate, DateTime.Today.AddDays(-1))
                .Build());
        }

        [Test]
        public void Clone()
        {
            var po = new TestObjectBuilder<PurchaseOrder>()
                .SetArgument(o => o.RequiredDeliveryDate, DateTime.Today.AddDays(7))
                .Build();
            po.Should().NotBeNull();

            var po2 = new TestObjectBuilder<PurchaseOrder>().Clone(po)
                .SetArgument(o => o.RequiredDeliveryDate, po.RequiredDeliveryDate?.AddDays(-2))
                .Build();
            po2.Customer.Should().BeEquivalentTo(po.Customer);
            po2.OrderNumber.Should().Be(po.OrderNumber);
            po2.RequiredDeliveryDate.Should().Be(po.RequiredDeliveryDate?.AddDays(-2));
        }
    }
}