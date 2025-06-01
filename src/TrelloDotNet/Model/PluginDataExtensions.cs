using System.Linq;
using System.Text.Json;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Extensions for PluginData
    /// </summary>
    public static class PluginDataExtensions
    {
        /// <summary>
        /// Cast the Value of PluginData to a custom PluginData Model
        /// </summary>
        /// <typeparam name="T">The Custom Model with JsonPropertyName() on each Property</typeparam>
        /// <param name="pluginData">The generic PluginData</param>
        /// <param name="pluginId">The Id of the Plugin</param>
        /// <returns></returns>
        public static T Cast<T>(this System.Collections.Generic.List<PluginData> pluginData, string pluginId)
        {
            PluginData firstOrDefault = pluginData.FirstOrDefault(x => x.PluginId == pluginId);
            if (firstOrDefault == null)
            {
                return default;
            }

            return Cast<T>(firstOrDefault);
        }

        /// <summary>
        /// Cast the Value of PluginData to a custom PluginData Model
        /// </summary>
        /// <param name="pluginData">The generic PluginData</param>
        /// <typeparam name="T">The Custom Model with JsonPropertyName() on each Property</typeparam>
        /// <returns></returns>
        public static T Cast<T>(this PluginData pluginData)
        {
            if (pluginData == null)
            {
                return default;
            }

            return JsonSerializer.Deserialize<T>(pluginData.Value);
        }
    }
}