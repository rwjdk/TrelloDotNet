using System;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace TrelloDotNet.Control
{
    /// <summary>
    /// Enum vis JSON Property Converter
    /// </summary>
    /// <typeparam name="T"></typeparam>
    internal class EnumViaJsonPropertyConverter<T> : JsonConverter<T> where T : Enum
    {
        /// <inheritdoc />
        public override T Read(ref Utf8JsonReader reader, Type typeToConvert, JsonSerializerOptions options)
        {
            var stringValue = reader.GetString();
            if (string.IsNullOrWhiteSpace(stringValue))
            {
                return default;
            }

            var members = typeToConvert.GetFields();
            foreach (var memberInfo in members)
            {
                var customAttributes = memberInfo.GetCustomAttributes(typeof(JsonPropertyNameAttribute), false);
                if (customAttributes.Length > 0 && ((JsonPropertyNameAttribute)customAttributes[0]).Name == stringValue)
                {
                    return (T)memberInfo.GetValue(null);
                }
            }

            throw new Exception($"Could not covert string value '{stringValue}' to Enum of type '{typeToConvert}'");
        }

        /// <inheritdoc />
        public override void Write(Utf8JsonWriter writer, T value, JsonSerializerOptions options)
        {
            writer.WriteStringValue(value.GetJsonPropertyName());
        }
    }
}