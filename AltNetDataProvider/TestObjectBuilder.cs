using System;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;

namespace AltNetDataProvider
{
    // Give it a type, and it will build an instance of that type, using the greedy constructor algorithm and
    // building parameters using the DataProvider.
    public class TestObjectBuilder<T>
    {
        private Type Type => typeof(T);
        private ParameterProvider ParameterProvider { get; }
        private ParameterInfo[] ParameterInfos { get; }

        public TestObjectBuilder()
        {
            ParameterProvider = new ParameterProvider();
            ParameterInfos = Type.GetConstructorWithMostParameters().GetParameters().ToArray();
        }

        public ParameterInfo[] ConstructorParameters => ParameterInfos;

        public TestObjectBuilder<T> SetArgument(string arg, object value)
        {
            if (!ParameterInfos.Select(x => x.Name).Contains(arg))
            {
                throw new Exception($"The constructor with the most parameters for type {Type.Name} does not have a a parameter named {arg}.");
            }

            ParameterProvider.SetArgument(arg, value);
            return this;
        }

        public TestObjectBuilder<T> Clone(T entity)
        {
            ParameterInfos.ToList()
                .ForEach(paramInfo =>
                {
                    var paramName = paramInfo.Name;
                    var property =
                        Type.GetProperty(paramInfo.Name,
                            BindingFlags.NonPublic | BindingFlags.Instance | BindingFlags.Public |
                            BindingFlags.IgnoreCase);
                    if (property == null)
                    {
                        throw new Exception(
                            "Clone will not work on this entity. For clone to work, each constructor parameter must have a correspondingly named property (case doesn't matter)");
                    }
                    var entityParamValue =
                        property.GetValue(entity, null);
                    SetArgument(paramName,
                        entityParamValue);
                });
            return this;
        }


        /// <summary>
        /// Sets an argument based on an expression to the Property name being set.
        /// Depends on the argument to the constructor being named consistently
        /// eg. string MyProperty { get; set; } => public MyClass(string myProperty)
        /// MyProperty => myProperty
        /// </summary>
        public TestObjectBuilder<T> SetArgument<TParam>(Expression<Func<T, TParam>> expr, TParam value)
        {
            var propertyName = MemberUtility.GetMemberInfo(expr).Name;
            var paramName = string.Format("{0}{1}",
                Char.ToLower(propertyName[0]),
                propertyName.Substring(1));

            return SetArgument(paramName, value);
        }

        public T Build()
        {
            var parameters = ParameterInfos
                .Select(p => ParameterProvider.GetValue(p))
                .ToArray();

            var constr = Type.GetConstructorWithMostParameters();
            try
            {
                return (T) constr.Invoke(parameters);
            }
            catch (TargetInvocationException e)
            {
                Console.WriteLine(e);
                throw e.InnerException;
            }
        }
    }
}