using System;
using System.Runtime.Serialization;

namespace TrelloDotNet.AutomationEngine.Model
{
    /// <summary>
    /// Exception directly from the Automation Engine
    /// </summary>
    public class AutomationException : Exception
    {
        /// <inheritdoc />
        public AutomationException()
        {
        }

        /// <inheritdoc />
        protected AutomationException(SerializationInfo info, StreamingContext context) : base(info, context)
        {
        }

        /// <inheritdoc />
        public AutomationException(string message) : base(message)
        {
        }

        /// <inheritdoc />
        public AutomationException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}