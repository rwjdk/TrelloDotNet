using System;
using System.Collections.Generic;
using System.Diagnostics;
using TrelloDotNet.AutomationEngine.Interface;

namespace TrelloDotNet.AutomationEngine.Model
{
    /// <summary>
    /// A representation of a Webhook Automation
    /// </summary>
    [DebuggerDisplay("{Name}")]
    public class Automation
    {
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Automation (as such not used for anything other than identification and error messages)</param>
        /// <param name="trigger">The Trigger of the automation (aka the first, light, initial condition of what you wish the automation to react to)</param>
        /// <param name="conditions">The additional conditions of the automation</param>
        /// <param name="actions">The action(s) that should be taken should trigger + all conditions be met</param>
        public Automation(string name, IAutomationTrigger trigger, List<IAutomationCondition> conditions, List<IAutomationAction> actions)
        {
            Name = name;
#pragma warning disable CS0618 // Type or member is obsolete
            Trigger = trigger ?? throw new ArgumentNullException(nameof(trigger), "Trigger can't be null");
#pragma warning restore CS0618 // Type or member is obsolete
            Triggers = new List<IAutomationTrigger> { trigger };
            Conditions = conditions;

            if (actions == null)
            {
                throw new ArgumentNullException(nameof(actions), "Actions can't be null");
            }

            if (actions.Count == 0)
            {
                throw new ArgumentException("You need at least one action", nameof(actions));
            }

            Actions = actions;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name of the Automation (as such not used for anything other than identification and error messages)</param>
        /// <param name="triggers">A List of possible triggers of the automation (aka the first, light, initial condition of what you wish the automation to react to). These are evaluated as 'OR' (aka proceed if any of the triggers are met)</param>
        /// <param name="conditions">The additional conditions of the automation</param>
        /// <param name="actions">The action(s) that should be taken should trigger + all conditions be met</param>
        public Automation(string name, List<IAutomationTrigger> triggers, List<IAutomationCondition> conditions, List<IAutomationAction> actions)
        {
            Name = name;
            Triggers = triggers ?? throw new ArgumentNullException(nameof(triggers), "Trigger can't be null");
            if (triggers.Count == 0)
            {
                throw new ArgumentException("You need at least one Trigger", nameof(actions));
            }

            Conditions = conditions;

            if (actions == null)
            {
                throw new ArgumentNullException(nameof(actions), "Actions can't be null");
            }

            if (actions.Count == 0)
            {
                throw new ArgumentException("You need at least one action", nameof(actions));
            }

            Actions = actions;
        }

        /// <summary>
        /// Name of the Automation
        /// </summary>
        public string Name { get; }

        /// <summary>
        /// The Trigger of the automation (aka the first, light, initial condition of what you wish the automation to react to)
        /// </summary>
        [Obsolete("This property is Deprecated as system now support multiple Triggers in Automations. Please use Triggers.First() Instead [Will be removed in v2.0 of this nuGet Package]")]
        public IAutomationTrigger Trigger { get; }

        /// <summary>
        /// A List of Triggers of the automation (aka the first, light, initial condition of what you wish the automation to react to). Will proceed if any of the triggers are met
        /// </summary>
        public List<IAutomationTrigger> Triggers { get; }

        /// <summary>
        /// The additional conditions of the automation
        /// </summary>
        public List<IAutomationCondition> Conditions { get; }

        /// <summary>
        /// The action(s) that should be taken should trigger + all conditions be met
        /// </summary>
        public List<IAutomationAction> Actions { get; }
    }
}