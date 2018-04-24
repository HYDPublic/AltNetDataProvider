using AltNetDataProvider.Domain;
using FluentAssertions;
using NUnit.Framework;

namespace AltNetDataProvider.Tests
{
    [TestFixture]
    public class PurchaseOrderTestsWithDataProvider
    {
        [Test]
        public void Create_PurchaseOrder()
        {
            var po = DataProvider.Get<PurchaseOrder>();
            var item = DataProvider.Get<Item>();
            po.Should().NotBeNull();
            item.Should().NotBeNull();
        }
    }
}