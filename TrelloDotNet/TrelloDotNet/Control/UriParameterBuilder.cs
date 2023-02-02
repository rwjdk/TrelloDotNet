using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json.Serialization;
using TrelloDotNet.Model;

namespace TrelloDotNet.Control
{
    internal class UriParameterBuilder
    {
        internal List<UriParameter> GetViaUriParameterAttribute<T>(T instance)
        {
            var type = instance.GetType();
            var propertyInfos = type.GetProperties();
            List<UriParameter> parameters = new List<UriParameter>();
            foreach (var updateableProperty in propertyInfos)
            {
                var updateableAttributes = updateableProperty.GetCustomAttributes(typeof(UriParameterAttribute), true);
                if (!updateableAttributes.Any())
                {
                    continue;
                }

                var jsonPropertyNameAttributes = updateableProperty.GetCustomAttributes(typeof(JsonPropertyNameAttribute), true);
                if (!jsonPropertyNameAttributes.Any())
                {
                    continue;
                }
                var jsonPropertyName = (JsonPropertyNameAttribute)jsonPropertyNameAttributes.First();
                var updateableAttribute = (UriParameterAttribute)updateableAttributes.First();
                var updateablePropertyType = updateableProperty.PropertyType;

                var rawValue = updateableProperty.GetValue(instance);
                if (rawValue == null && !updateableAttribute.IncludeIfNull)
                {
                    continue;
                }

                if (updateablePropertyType == typeof(string))
                {
                    parameters.Add(new UriParameter(jsonPropertyName.Name, (string)rawValue));
                }
                else if (updateablePropertyType == typeof(int) || updateablePropertyType == typeof(int?))
                {
                    parameters.Add(new UriParameter(jsonPropertyName.Name, (int?)rawValue));
                }
                else if (updateablePropertyType == typeof(DateTimeOffset) || updateablePropertyType == typeof(DateTimeOffset?))
                {
                    parameters.Add(new UriParameter(jsonPropertyName.Name, (DateTimeOffset?)rawValue));
                }
                else if (updateablePropertyType == typeof(bool))
                {
                    parameters.Add(new UriParameter(jsonPropertyName.Name, (bool?)rawValue));
                }
                else if (updateablePropertyType == typeof(List<string>))
                {
                    var list = (List<string>)rawValue;
                    parameters.Add(list == null ? new UriParameter(jsonPropertyName.Name, string.Empty) : new UriParameter(jsonPropertyName.Name, string.Join(",", list)));
                }
                else
                {
                    throw new Exception($"Unsupported type of Property: {jsonPropertyName.Name} - Type: {updateablePropertyType}");
                }
            }

            return parameters;
        }
    }
}