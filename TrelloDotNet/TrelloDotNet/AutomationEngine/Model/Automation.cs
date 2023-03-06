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
            Trigger = trigger ?? throw new ArgumentNullException(nameof(trigger), "Trigger can't be null");
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
        public IAutomationTrigger Trigger { get; }

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