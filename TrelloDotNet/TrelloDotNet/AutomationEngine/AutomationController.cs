using System;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Model;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine
{
    /// <summary>
    /// The Automation Controller that can be used to automate receiving of Webhooks events against a set of configured automations
    /// </summary>
    public class AutomationController
    {
        private readonly Automation[] _automations;
        private readonly WebhookDataReceiver _receiver;

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="configuration">Configuration of the Automation Engine</param>
        public AutomationController(Configuration configuration)
        {
            if (configuration == null)
            {
                throw new ArgumentNullException(nameof(configuration), "You did not provide a configuration");
            }

            var trelloClient = configuration.TrelloClient;
            _automations = configuration.Automations;
            _receiver = new WebhookDataReceiver(trelloClient);
        }

        /// <summary>
        /// Process raw JSON from a Trello Webhook against the provided set of Automations in the configuration
        /// </summary>
        /// <param name="request">The processing request (with the JSON)</param>
        /// <param name="cancellationToken">Cancellation Token</param>
        /// <returns></returns>
        public async Task<ProcessingResult> ProcessJsonFromWebhookAsync(ProcessingRequest request, CancellationToken cancellationToken = default)
        {
            cancellationToken.ThrowIfCancellationRequested();
            WebhookNotification data = _receiver.ConvertJsonToWebhookNotification(request.JsonFromWebhook);
            var webhookAction = data.Action;
            var result = new ProcessingResult();

            foreach (var automation in _automations)
            {
                try
                {
                    cancellationToken.ThrowIfCancellationRequested();
                    if (!await automation.Trigger.IsTriggerMetAsync(webhookAction))
                    {
                        result.AddToLog($"Automation '{automation.Name}' was not processed as trigger was not met");
                        result.AutomationsSkipped++;
                        continue; //Wrong Trigger
                    }
                }
                catch (Exception e)
                {
                    throw new AutomationException($"Error checking Trigger of type '{automation.Trigger.GetType()}' in automation '{automation.Name}'{AddErrorContext(data)}", e);
                }

                var conditionsMet = true;
                if (automation.Conditions != null)
                {
                    foreach (var x in automation.Conditions)
                    {
                        try
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            if (!await x.IsConditionMetAsync(webhookAction))
                            {
                                conditionsMet = false;
                                break;
                            }
                        }
                        catch (Exception e)
                        {
                            throw new AutomationException($"Error checking Condition of type '{x.GetType()}' in automation '{automation.Name}'{AddErrorContext(data)}", e);
                        }
                    }
                }

                if (conditionsMet)
                {
                    result.AutomationsProcessed++;
                    result.AddToLog($"Automation '{automation.Name}' trigger and condition was met - Executing {automation.Actions.Count} Action");
                    foreach (var actionAction in automation.Actions.Where(x => x != null))
                    {
                        try
                        {
                            cancellationToken.ThrowIfCancellationRequested();
                            await actionAction.PerformActionAsync(webhookAction, result);
                        }
                        catch(StopProcessingFurtherActionException)
                        {
                            return result;
                        }
                        catch (Exception e)
                        {
                            throw new AutomationException($"Error performing Action of type '{actionAction.GetType()}' in automation '{automation.Name}'{AddErrorContext(data)}", e);
                        }

                    }
                }
                else
                {
                    result.AddToLog($"Automation '{automation.Name}' was not processed as not all conditions where met was not met");
                    result.AutomationsSkipped++;
                }
            }
            return result;
        }

        private string AddErrorContext(WebhookNotification webhookNotification)
        {
            try
            {
                return webhookNotification.Action.SummarizeEvent();
            }
            catch
            {
                return string.Empty;
            }
        }
    }
}