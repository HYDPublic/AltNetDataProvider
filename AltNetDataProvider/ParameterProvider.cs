using System.Collections.Generic;
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

        public object GetValue(ParameterInfo parameterInfo)
        {
            var parameterName = parameterInfo.Name;
            var parameterType = parameterInfo.ParameterType;

            return _argumentSetter.ContainsKey(parameterName)
                ? _argumentSetter[parameterName]
                : DataProvider.Get(parameterType);
        }
    }
}