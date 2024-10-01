using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// Trigger for when checklist item is deleted
    /// </summary>
    public class DeleteCheckItemTrigger : IAutomationTrigger
    {
        /// <inheritdoc />
        public async Task<bool> IsTriggerMetAsync(WebhookAction webhookAction)
        {
            await Task.CompletedTask;
            return webhookAction.Type == WebhookActionTypes.DeleteCheckItem;
        }
    }
}