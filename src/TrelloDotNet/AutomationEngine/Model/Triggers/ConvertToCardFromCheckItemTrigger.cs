﻿using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

// ReSharper disable UnusedMember.Global

namespace TrelloDotNet.AutomationEngine.Model.Triggers
{
    /// <summary>
    /// Trigger when a Checklist Item is converted to a card
    /// </summary>
    public class ConvertToCardFromCheckItemTrigger : IAutomationTrigger
    {
        /// <summary>
        /// If the Trigger is met
        /// </summary>
        /// <remarks>
        /// While this is built to support async execution, It is best practice to keep a Trigger as light as possible only checking values against the webhook Action and not make any API Calls. The reason for this is that Triggers are called quite often, so it is better to make simple triggers and supplement with Conditions
        /// </remarks>
        /// <param name="webhookAction">The Webhook Action that led to the check of the trigger. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <returns>If trigger is met or not</returns>
        public Task<bool> IsTriggerMetAsync(WebhookAction webhookAction)
        {
            return Task.FromResult(webhookAction.Type == WebhookActionTypes.ConvertToCardFromCheckItem);
        }
    }
}