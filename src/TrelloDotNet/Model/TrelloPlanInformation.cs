using System.Collections.Generic;
using System.Text.Json.Serialization;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Contains what Trello Plan a Board/Workspace is using (Free, Standard, Premium, Enterprise) and thereby what features are supported
    /// </summary>
    public class TrelloPlanInformation
    {
        /// <summary>
        /// Id of the Board/Workspace
        /// </summary>
        [JsonPropertyName("id")]
        [JsonInclude]
        public string Id { get; set; }

        /// <summary>
        /// Name of the Board/Workspace
        /// </summary>
        [JsonPropertyName("name")]
        [JsonInclude]
        public string Name { get; set; }

        /// <summary>
        /// What subscription plan (Free, Standard, Premium or Enterprise)
        /// </summary>
        public TrelloPlan Plan
        {
            get
            {
                if (Features == null || Features.Count == 0)
                {
                    return TrelloPlan.Unknown;
                }

                if (Features.Contains("isEnterprise"))
                {
                    return TrelloPlan.Enterprise;
                }

                if (Features.Contains("isPremium"))
                {
                    return TrelloPlan.Premium;
                }

                if (Features.Contains("isStandard"))
                {
                    return TrelloPlan.Standard;
                }

                return TrelloPlan.Free;
            }
        }

        /// <summary>
        /// List of Features for the Board/Workspace
        /// </summary>
        [JsonPropertyName("premiumFeatures")]
        [JsonInclude]
        public List<string> Features { get; private set; }

        /// <summary>
        /// If this board support Custom Fields (Only populated if GetBoardOptions.CardFields include 'premiumFeatures')
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public bool IsCustomFieldsSupported => Plan == TrelloPlan.Standard || Plan == TrelloPlan.Premium || Plan == TrelloPlan.Enterprise;

        /// <summary>
        /// If this board support Advanced Checklists (Only populated if GetBoardOptions.CardFields include 'premiumFeatures')
        /// </summary>
        // ReSharper disable once UnusedMember.Global
        public bool IsAdvancedChecklistsSupported => Plan == TrelloPlan.Standard || Plan == TrelloPlan.Premium || Plan == TrelloPlan.Enterprise;

        /// <summary>
        /// If this board support List Colors (Only populated if GetBoardOptions.CardFields include 'premiumFeatures')
        /// </summary>
        public bool IsListColorsSupported => Plan == TrelloPlan.Standard || Plan == TrelloPlan.Premium || Plan == TrelloPlan.Enterprise;
    }
}