using System.Collections.Generic;

namespace TrelloDotNet.AutomationEngine.Model
{
    /// <summary>
    /// The result of the processing of the Webhook
    /// </summary>
    public class ProcessingResult
    {
        /// <summary>
        /// Log of events and actions performed
        /// </summary>
        public List<ProcessingResultLogEntry> Log { get; }

        /// <summary>
        /// The number of actions executed
        /// </summary>
        public int ActionsExecuted { get; set; }

        /// <summary>
        /// The Number of actions skipped (aka was requested to execute but did not need to as data was in already OK state)
        /// </summary>
        public int ActionsSkipped { get; set; }

        /// <summary>
        /// The number of automations processed
        /// </summary>
        public int AutomationsProcessed { get; set; }

        /// <summary>
        /// The number of automations skipped (due to their trigger or conditions not being met)
        /// </summary>
        public int AutomationsSkipped { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        public ProcessingResult()
        {
            Log = new List<ProcessingResultLogEntry>();
        }

        /// <summary>
        /// Add a message to the log
        /// </summary>
        /// <param name="message">Message to log</param>
        public void AddToLog(string message)
        {
            Log.Add(new ProcessingResultLogEntry(message));
        }
    }
}