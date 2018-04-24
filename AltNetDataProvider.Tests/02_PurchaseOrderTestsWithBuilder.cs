using System;
using AltNetDataProvider.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace AltNetDataProvider.Tests
{
    [TestFixture]
    public class PurchaseOrderTestsWithBuilder
    {
        [Test]
        public void SimpleConstruction()
        {
            var po = new PurchaseOrderBuilder().Build();
            po.Should().NotBeNull();
        }
        
        [Test]
        public void Cannot_RequestDeliveryInThePast()
        {
            Assert.Throws<ArgumentException>(() => new PurchaseOrderBuilder()
                .SetCustomerCategory(CustomerCategory.Normal)
                .SetRequiredDeliveryDate(DateTime.Today.AddDays(-7))
                .Build());
        }

        [Test]
        public void VipCustomer_CanRequestDeliveryInThePast()
        {
            Assert.DoesNotThrow(() => new PurchaseOrderBuilder()
                .SetCustomerCategory(CustomerCategory.Vip)
                .SetRequiredDeliveryDate(DateTime.Today.AddDays(-7))
                .Build());
        }
    }
}