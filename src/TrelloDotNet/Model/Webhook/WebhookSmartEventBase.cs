using System;

namespace TrelloDotNet.Model.Webhook
{
    /// <summary>
    /// Base-class of a SmartEvent Argument
    /// </summary>
    public class WebhookSmartEventBase
    {
        /// <summary>
        /// ID of the Board
        /// </summary>
        public string BoardId { get; }

        /// <summary>
        /// Name of the Board
        /// </summary>
        public string BoardName { get; }

        /// <summary>
        /// Member that initiated the event
        /// </summary>
        public Member MemberCreator { get; }

        /// <summary>
        /// The Time of the Event in UTC Time
        /// </summary>
        public DateTimeOffset TimeOfEvent { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="action">The WebhookAction</param>
        protected WebhookSmartEventBase(WebhookAction action)
        {
            BoardId = action.Data.Board.Id;
            BoardName = action.Data.Board.Name;
            MemberCreator = action.MemberCreator;
            TimeOfEvent = action.Date;
        }
    }
}