using System;
using System.Collections.Generic;
using AltNetDataProvider.Domain;
using NUnit.Framework;

namespace AltNetDataProvider.Tests
{
    [SetUpFixture]
    public class CustomDataProviders
    {
        [OneTimeSetUp]
        public void TestFixtureSetUp()
        {
            DataProvider.RegisterCustomProviders(CustomProviders);
        }

        private static IDictionary<Type, Func<object>> CustomProviders => new Dictionary<Type, Func<object>>
        {
            [typeof(PurchaseOrder)] = GetPurchaseOrder
        };

        private static object GetPurchaseOrder()
        {
            return new TestObjectBuilder<PurchaseOrder>()
                .SetArgument(o => o.RequiredDeliveryDate, DateTime.Today.AddDays(DataProvider.Get<int>()))
                .Build();
        }
    }
}