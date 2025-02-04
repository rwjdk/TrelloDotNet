using System;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Exception from calling the Trello API
    /// </summary>
    public class TrelloApiException : Exception
    {
        /// <summary>
        /// What URL was sent to Trello (good to debug with in Postman and similar tools)
        /// </summary>
        public string DataSentToTrello { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message to display to user</param>
        public TrelloApiException(string message) : base(message)
        {
            DataSentToTrello = string.Empty;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="message">Message to display to user</param>
        /// <param name="dataSentToTrello">The URI Payload used to call Trello</param>
        public TrelloApiException(string message, string dataSentToTrello) : base(message)
        {
            DataSentToTrello = dataSentToTrello;
        }
    }
}