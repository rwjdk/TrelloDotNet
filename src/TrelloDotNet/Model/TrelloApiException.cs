using System;
using System.Net;

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
        /// The underlying HTTP Status code
        /// </summary>
        public HttpStatusCode? ErrorCode { get; }

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
        /// <param name="errorCode">Underlying HTTP Status code</param>
        public TrelloApiException(string message, string dataSentToTrello, HttpStatusCode? errorCode = null) : base(message)
        {
            DataSentToTrello = dataSentToTrello;
            ErrorCode = errorCode;
        }
    }
}