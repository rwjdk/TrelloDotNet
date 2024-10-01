using System;

namespace TrelloDotNet.AutomationEngine.Model
{
    /// <summary>
    /// The Configuration of the Automation Engine
    /// </summary>
    public class Configuration
    {
        /// <summary>
        /// An instance of the TrelloClient, connected with same APIKey/token as the Webhook was configured with
        /// </summary>
        public TrelloClient TrelloClient { get; }
        /// <summary>
        /// One or more Automations the Automation Engine should consider when running
        /// </summary>
        public Automation[] Automations { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="trelloClient">An instance of the TrelloClient, connected with same APIKey/token as the Webhook was configured with</param>
        /// <param name="automations">One or more Automations the Automation Engine should consider when running</param>
        public Configuration(TrelloClient trelloClient, params Automation[] automations)
        {
            TrelloClient = trelloClient ?? throw new ArgumentNullException(nameof(trelloClient), "TrelloClient can't be null");
            if (automations.Length == 0)
            {
                throw new ArgumentException("You need to provide at least one Automation", nameof(automations));
            }

            Automations = automations;
        }
    }
}