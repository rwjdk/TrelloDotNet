using System;
using System.Diagnostics;

namespace TrelloDotNet.AutomationEngine.Model
{
    /// <summary>
    /// An Entry in the log of the Processing Result
    /// </summary>
    [DebuggerDisplay("{Timestamp} - {Message}")]
    public class ProcessingResultLogEntry
    {
        /// <summary>
        /// Timestamp of when log was created in UTC time
        /// </summary>
        public DateTimeOffset Timestamp { get; set; }

        /// <summary>
        /// Message of the Log
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message to log</param>
        public ProcessingResultLogEntry(string message)
        {
            Timestamp = DateTimeOffset.UtcNow;
            Message = message;
        }
    }
}