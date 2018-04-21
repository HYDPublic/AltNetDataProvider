using System;

namespace AltNetDataProvider
{
    public static class ObjectPropertySetter
    {
        public static T CreateObjectWithAllPropertiesSet<T>()
        {
            return (T) CreateObjectWithAllPropertiesSet(typeof(T));
        }

        public static object CreateObjectWithAllPropertiesSet(Type type)
        {
            var entity = type.GetConstructor(new Type[0]).Invoke(null);

            var allProperties = type.GetProperties();

            foreach (var property in allProperties)
            {
                var propType = property.PropertyType;
                if (!propType.IsArray && property.GetSetMethod() != null)
                {
                    var value = DataProvider.Get(propType, true);
                    property.SetValue(entity, value, null);
                }
            }

            return entity;
        }
    }
}