using System;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Exception from calling the Trello API
    /// </summary>
    public class TrelloApiException : Exception
    {
        /// <summary>
        /// What URL was sent to Trello (good to debug with in Postman and similar tools) //todo - will someone see this as a security risk? (setting is anything here or not of perhaps filter out token and api key)
        /// </summary>
        public string DataSentToTrello { get; }

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