using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AltNetDataProvider
{
    public class ParameterProvider
    {
        private readonly IDictionary<string, object> _argumentSetter = new Dictionary<string, object>();

        public ParameterProvider SetArgument(string arg, object value)
        {
            _argumentSetter[arg] = value;
            return this;
        }

        private readonly IDictionary<Type, Func<object>> _providerOverrides = new Dictionary<Type, Func<object>>();

        public ParameterProvider SetTypeProvider<T2>(Func<T2> provider)
        {
            _providerOverrides.Add(typeof(T2), ()=>provider());
            return this;
        }

        public ParameterProvider SetTypeProvider<T2>(T2 value)
        {
            _providerOverrides.Add(typeof(T2), () => value);
            return this;
        }


        public object GetValue(ParameterInfo parameterInfo)
        {
            var parameterName = parameterInfo.Name;
            var parameterType = parameterInfo.ParameterType;

            if (_argumentSetter.ContainsKey(parameterName))
            {
                return _argumentSetter[parameterName];
            }

            var specialProviders = _providerOverrides.Select(x => new ProviderOverride(x.Key, x.Value));
            return DataProvider.Get(parameterType, specialProviders);
        }
    }
}