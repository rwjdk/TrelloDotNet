using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// Add a Comment to the Card
    /// </summary>
    public class AddCommentToCardAction : IAutomationAction
    {
        /// <summary>
        /// Comment to add
        /// </summary>
        public string Comment { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="comment">Comment to add</param>
        /// <remarks>TIP: Use keyword '@card' to at-mention owner's of the card</remarks>
        public AddCommentToCardAction(string comment)
        {
            Comment = comment;
        }

        /// <summary>
        /// The method called when an automation should be performed
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to the Execution. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <param name="processingResult">An object you can use to report back to the user if the action was performed and details about it</param>
        /// <returns>Void</returns>
        public async Task PerformActionAsync(WebhookAction webhookAction, ProcessingResult processingResult)
        {
            if (webhookAction.Data?.Card == null)
            {
                throw new AutomationException("Could not perform AddCommentToCardAction as WebhookAction did not involve a Card");
            }

            await webhookAction.TrelloClient.AddCommentAsync(webhookAction.Data.Card.Id, new Comment(Comment), CancellationToken.None);
        }
    }
}