using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// Trigger for when checklist is removed from card
    /// </summary>
    public class RemoveChecklistFromCardTrigger : IAutomationTrigger
    {
        /// <inheritdoc />
        public async Task<bool> IsTriggerMetAsync(WebhookAction webhookAction)
        {
            await Task.CompletedTask;
            return webhookAction.Type == WebhookActionTypes.RemoveChecklistFromCard;
        }
    }
}