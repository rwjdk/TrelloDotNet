using System;
using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Conditions
{
    /// <summary>
    /// Check if a Card have a certain value
    /// </summary>
    public class CardFieldCondition : IAutomationCondition
    {
        /// <summary>
        /// The Card Field to Check 
        /// </summary>
        public CardField FieldToCheck { get; }
        /// <summary>
        /// The constraint the field should be checked on
        /// </summary>
        public CardFieldConditionConstraint Constraint { get; }
        /// <summary>
        /// The Value of the Field (stored as an object)
        /// </summary>
        public object Value { get; }
        /// <summary>
        /// What String match-criteria should be used when Constraint is 'Value'
        /// </summary>
        public StringMatchCriteria StringValueMatchCriteria { get; }
        /// <summary>
        /// What DateTimeOffset match-criteria should be used when Constraint is 'Value'
        /// </summary>
        public DateTimeOffsetMatchCriteria DateTimeOffsetValueMatchCriteria { get; }

        /// <summary>
        /// When checking date fields (Start and Due) indicate that the matching should only happen on Date Level
        /// </summary>
        public bool MatchDateOnlyOnDateTimeOffsetFields { get; set; }

        /// <summary>
        /// Constructor (no value)
        /// </summary>
        /// <param name="fieldToCheck">The Card Field to Check</param>
        /// <param name="constraint">The constraint the field should be checked on</param>
        public CardFieldCondition(CardField fieldToCheck, CardFieldConditionConstraint constraint)
        {
            FieldToCheck = fieldToCheck;
            Constraint = constraint;
            Value = null;
            StringValueMatchCriteria = StringMatchCriteria.Equal;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldToCheck">The Card Field to Check </param>
        /// <param name="constraint">The constraint the field should be checked on</param>
        /// <param name="value">The value to check</param>
        /// <param name="valueMatchCriteria">The match-criteria</param>
        public CardFieldCondition(CardField fieldToCheck, CardFieldConditionConstraint constraint, DateTimeOffset value, DateTimeOffsetMatchCriteria valueMatchCriteria = DateTimeOffsetMatchCriteria.Equal)
        {
            FieldToCheck = fieldToCheck;
            Constraint = constraint;
            Value = value;
            DateTimeOffsetValueMatchCriteria = valueMatchCriteria;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="fieldToCheck">The Card Field to Check </param>
        /// <param name="constraint">The constraint the field should be checked on</param>
        /// <param name="value">The value to check</param>
        /// <param name="valueMatchCriteria">The match-criteria</param>
        public CardFieldCondition(CardField fieldToCheck, CardFieldConditionConstraint constraint, string value, StringMatchCriteria valueMatchCriteria = StringMatchCriteria.Equal)
        {
            FieldToCheck = fieldToCheck;
            Constraint = constraint;
            Value = value;
            StringValueMatchCriteria = valueMatchCriteria;
        }

        /// <summary>
        /// Method to check if the condition is met
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to check of the condition. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <returns>If condition is met or not</returns>
        public async Task<bool> IsConditionMetAsync(WebhookAction webhookAction)
        {
            if (webhookAction.Data?.Card == null)
            {
                throw new AutomationException("Could not perform CardFieldCondition.IsConditionMetAsync as WebhookAction did not involve a Card");
            }

            switch (FieldToCheck)
            {
                case CardField.Name:
                    return CheckName(webhookAction);
                case CardField.Description:
                    return await CheckDescription(webhookAction);
                case CardField.Start:
                    return await CheckStart(webhookAction);
                case CardField.Due:
                    return await CheckDue(webhookAction);
                case CardField.DueComplete:
                    return await CheckDueComplete(webhookAction);
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task<bool> CheckDueComplete(WebhookAction webhookAction)
        {
            var card = await webhookAction.Data.Card.GetAsync();
            return CheckBool(card.DueComplete);
        }

        private bool CheckBool(bool boolean)
        {
            switch (Constraint)
            {
                case CardFieldConditionConstraint.IsNotSet:
                    return !boolean;
                case CardFieldConditionConstraint.IsSet:
                    return boolean;
                case CardFieldConditionConstraint.Value:
                    var boolValue = (bool)Value;
                    switch (StringValueMatchCriteria)
                    {
                        case StringMatchCriteria.Equal:
                            return boolValue == boolean;
                        case StringMatchCriteria.StartsWith:
                        case StringMatchCriteria.EndsWith:
                        case StringMatchCriteria.Contains:
                            throw new AutomationException("Bool can't use MatchCriteria 'StartsWith', 'EndsWith' and 'Contains'");
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private bool CheckName(WebhookAction webhookAction)
        {
            var name = webhookAction.Data.Card.Name;
            return CheckString(name);
        }
        
        private async Task<bool> CheckDescription(WebhookAction webhookAction)
        {
            var card = await webhookAction.Data.Card.GetAsync();
            return CheckString(card.Description);
        }

        private bool CheckString(string stringValue)
        {
            switch (Constraint)
            {
                case CardFieldConditionConstraint.IsNotSet:
                    return string.IsNullOrWhiteSpace(stringValue);
                case CardFieldConditionConstraint.IsSet:
                    return !string.IsNullOrWhiteSpace(stringValue);
                case CardFieldConditionConstraint.Value:
                    var valueAsString = (string)Value;
                    switch (StringValueMatchCriteria)
                    {
                        case StringMatchCriteria.Equal:
                            return stringValue == valueAsString;
                        case StringMatchCriteria.StartsWith:
                            return stringValue.StartsWith(valueAsString);
                        case StringMatchCriteria.EndsWith:
                            return stringValue.EndsWith(valueAsString);
                        case StringMatchCriteria.Contains:
                            return stringValue.Contains(valueAsString);
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private async Task<bool> CheckStart(WebhookAction webhookAction)
        {
            var card = await webhookAction.Data.Card.GetAsync();
            return CheckDateTimeOffsetValue(card.Start);
        }
        
        private async Task<bool> CheckDue(WebhookAction webhookAction)
        {
            var card = await webhookAction.Data.Card.GetAsync();
            return CheckDateTimeOffsetValue(card.Due);
        }

        private bool CheckDateTimeOffsetValue(DateTimeOffset? valueOnCard)
        {
            switch (Constraint)
            {
                case CardFieldConditionConstraint.IsNotSet:
                    return !valueOnCard.HasValue;
                case CardFieldConditionConstraint.IsSet:
                    return valueOnCard.HasValue;
                case CardFieldConditionConstraint.Value:
                    var dateTimeOffsetValue = (DateTimeOffset)Value;
                    switch (DateTimeOffsetValueMatchCriteria)
                    {
                        case DateTimeOffsetMatchCriteria.Equal:
                            if (MatchDateOnlyOnDateTimeOffsetFields)
                            {
                                return valueOnCard.HasValue && valueOnCard.Value.Date == dateTimeOffsetValue.Date; //Check to date level
                            }
                            return valueOnCard?.ToString("yyyyMMdd_HHmm") == dateTimeOffsetValue.ToString("yyyyMMdd_HHmm"); //Check down to minute-level
                        case DateTimeOffsetMatchCriteria.After:
                            return valueOnCard.HasValue && valueOnCard.Value > dateTimeOffsetValue;
                        case DateTimeOffsetMatchCriteria.Before:
                            return valueOnCard.HasValue && valueOnCard.Value < dateTimeOffsetValue;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}