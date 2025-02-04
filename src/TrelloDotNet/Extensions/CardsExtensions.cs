using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TrelloDotNet.Model;

namespace TrelloDotNet.Extensions
{
    public static class CardsExtensions
    {
        public static List<Card> Filter(this IEnumerable<Card> cards, List<CardsFilterCondition> conditions, List<CustomField> customFields)
        {
            foreach (CardsFilterCondition entry in conditions)
            {
                switch (entry.Field)
                {
                    case CardsConditionField.Name:
                        cards = FilterName(cards, entry);
                        break;
                    case CardsConditionField.List:
                        cards = FilterList(cards, entry);
                        break;
                    case CardsConditionField.Label:
                        cards = FilterLabel(cards, entry);
                        break;
                    case CardsConditionField.Member:
                        cards = FilterMember(cards, entry);
                        break;
                    case CardsConditionField.Description:
                        cards = FilterDescription(cards, entry);
                        break;
                    case CardsConditionField.DueDate:
                        cards = FilterDueDate(cards, entry);
                        break;
                    case CardsConditionField.StartDate:
                        cards = FilterStartDate(cards, entry);
                        break;
                    case CardsConditionField.CreateDate:
                        cards = FilterCreateDate(cards, entry);
                        break;
                    case CardsConditionField.CustomField:
                        cards = FilterCustomFields(cards, entry, customFields);
                        break;
                }
            }

            return cards.ToList();
        }

        private static IEnumerable<Card> FilterCustomFields(IEnumerable<Card> cards, CardsFilterCondition entry, List<CustomField> customFields)
        {
            if (entry.CustomFieldId == null)
            {
                throw new TrelloApiException("CustomFieldId was not provided");
            }

            CustomField customFieldDefinition = customFields.FirstOrDefault(x => x.Id == entry.CustomFieldId);
            if (customFieldDefinition == null)
            {
                throw new TrelloApiException($"CustomField with id {entry.CustomFieldId} was not found");
            }

            cards = cards.Where(x =>
            {
                CustomFieldItem customFieldItem = x.CustomFieldItems.FirstOrDefault(y => y.CustomFieldId == entry.CustomFieldId);
                switch (entry.Condition)
                {
                    case CardsCondition.HasAnyValue:
                        return customFieldItem != null;
                    case CardsCondition.DoNotHaveAnyValue:
                        return customFieldItem == null;
                }

                switch (customFieldDefinition.Type)
                {
                    case CustomFieldType.Checkbox:
                        var @checked = customFieldItem?.Value.Checked ?? false;
                        switch (entry.Condition)
                        {
                            case CardsCondition.Equal:
                                return @checked == entry.ValueAsBoolean;
                            case CardsCondition.NotEqual:
                                return @checked != entry.ValueAsBoolean;
                            default:
                                throw new TrelloApiException($"Custom Field of Type Checkbox can't use Condition '{entry.Condition}'");
                        }
                    case CustomFieldType.Date:
                        var date = customFieldItem?.Value.Date;
                        switch (entry.Condition)
                        {
                            case CardsCondition.Equal:
                                return date.HasValue && date.Value == entry.ValueAsDateTimeOffset;
                            case CardsCondition.NotEqual:
                                return !date.HasValue || date.Value != entry.ValueAsDateTimeOffset;
                            case CardsCondition.GreaterThan:
                                return date.HasValue && date.Value > entry.ValueAsDateTimeOffset;
                            case CardsCondition.LessThan:
                                return date.HasValue && date.Value < entry.ValueAsDateTimeOffset;
                            case CardsCondition.GreaterThanOrEqual:
                                return date.HasValue && date.Value >= entry.ValueAsDateTimeOffset;
                            case CardsCondition.LessThanOrEqual:
                                return date.HasValue && date.Value <= entry.ValueAsDateTimeOffset;
                            case CardsCondition.AnyOfThese:
                                return date.HasValue && entry.ValueAsDateTimeOffsets.Any(y => y == date.Value);
                            case CardsCondition.NoneOfThese:
                                return date.HasValue && entry.ValueAsDateTimeOffsets.All(y => y != date.Value);
                            case CardsCondition.Contains:
                            case CardsCondition.DoNotContains:
                            case CardsCondition.AllOfThese:
                            case CardsCondition.RegEx:
                            case CardsCondition.StartsWith:
                            case CardsCondition.EndsWith:
                            case CardsCondition.DoNotStartWith:
                            case CardsCondition.DoNotEndWith:
                            default:
                                throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a CustomField of Type Date");
                        }
                    case CustomFieldType.List:
                        string listValue = customFieldDefinition.Options.FirstOrDefault(y => y.Id == customFieldItem?.ValueId)?.Value.Text ?? string.Empty;
                        switch (entry.Condition)
                        {
                            case CardsCondition.Equal:
                                return listValue.Equals(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase);
                            case CardsCondition.NotEqual:
                                return listValue != entry.ValueAsString;
                            case CardsCondition.GreaterThan:
                                return listValue.Length > entry.ValueAsInteger;
                            case CardsCondition.LessThan:
                                return listValue.Length < entry.ValueAsInteger;
                            case CardsCondition.GreaterThanOrEqual:
                                return listValue.Length >= entry.ValueAsInteger;
                            case CardsCondition.LessThanOrEqual:
                                return listValue.Length <= entry.ValueAsInteger;
                            case CardsCondition.Contains:
                                return listValue.Contains(entry.ValueAsString);
                            case CardsCondition.DoNotContains:
                                return !listValue.Contains(entry.ValueAsString);
                            case CardsCondition.AnyOfThese:
                                return entry.ValueAsStrings?.Any(y => y.Equals(listValue, StringComparison.InvariantCultureIgnoreCase)) ?? false;
                            case CardsCondition.NoneOfThese:
                                return entry.ValueAsStrings?.All(y => y != listValue) ?? false;
                            case CardsCondition.RegEx:
                                return Regex.IsMatch(listValue, entry.ValueAsString, RegexOptions.IgnoreCase);
                            case CardsCondition.StartsWith:
                                return listValue.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase);
                            case CardsCondition.EndsWith:
                                return listValue.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase);
                            case CardsCondition.DoNotStartWith:
                                return !listValue.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase);
                            case CardsCondition.DoNotEndWith:
                                return !listValue.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase);
                            case CardsCondition.AllOfThese:
                            default:
                                throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a CustomField of Type List");
                        }
                    case CustomFieldType.Number:
                        var number = customFieldItem?.Value.Number ?? decimal.MinValue;
                        switch (entry.Condition)
                        {
                            case CardsCondition.Equal:
                                return number == entry.ValueAsDecimal;
                            case CardsCondition.NotEqual:
                                return number != entry.ValueAsDecimal;
                            case CardsCondition.GreaterThan:
                                return number > entry.ValueAsDecimal;
                            case CardsCondition.LessThan:
                                return number < entry.ValueAsDecimal;
                            case CardsCondition.GreaterThanOrEqual:
                                return number >= entry.ValueAsDecimal;
                            case CardsCondition.LessThanOrEqual:
                                return number <= entry.ValueAsDecimal;
                            case CardsCondition.AnyOfThese:
                                return entry.ValueAsDecimals.Any(y => y == number);
                            case CardsCondition.NoneOfThese:
                                return entry.ValueAsDecimals.All(y => y != number);
                            case CardsCondition.AllOfThese:
                            case CardsCondition.RegEx:
                            case CardsCondition.StartsWith:
                            case CardsCondition.EndsWith:
                            case CardsCondition.DoNotStartWith:
                            case CardsCondition.DoNotEndWith:
                            default:
                                throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a CustomField of Type Number");
                        }
                    case CustomFieldType.Text:
                    default:
                        string textValue = customFieldItem?.Value.Text ?? string.Empty;
                        switch (entry.Condition)
                        {
                            case CardsCondition.Equal:
                                return textValue.Equals(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase);
                            case CardsCondition.NotEqual:
                                return textValue != entry.ValueAsString;
                            case CardsCondition.GreaterThan:
                                return textValue.Length > entry.ValueAsInteger;
                            case CardsCondition.LessThan:
                                return textValue.Length < entry.ValueAsInteger;
                            case CardsCondition.GreaterThanOrEqual:
                                return textValue.Length >= entry.ValueAsInteger;
                            case CardsCondition.LessThanOrEqual:
                                return textValue.Length <= entry.ValueAsInteger;
                            case CardsCondition.Contains:
                                return textValue.Contains(entry.ValueAsString);
                            case CardsCondition.DoNotContains:
                                return !textValue.Contains(entry.ValueAsString);
                            case CardsCondition.AnyOfThese:
                                return entry.ValueAsStrings?.Any(y => y.Equals(textValue, StringComparison.InvariantCultureIgnoreCase)) ?? false;
                            case CardsCondition.NoneOfThese:
                                return entry.ValueAsStrings?.All(y => y != textValue) ?? false;
                            case CardsCondition.RegEx:
                                return Regex.IsMatch(textValue, entry.ValueAsString, RegexOptions.IgnoreCase);
                            case CardsCondition.StartsWith:
                                return textValue.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase);
                            case CardsCondition.EndsWith:
                                return textValue.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase);
                            case CardsCondition.DoNotStartWith:
                                return !textValue.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase);
                            case CardsCondition.DoNotEndWith:
                                return !textValue.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase);
                            case CardsCondition.AllOfThese:
                            default:
                                throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a CustomField of Type Text");
                        }
                }
            });

            return cards;
        }

        private static IEnumerable<Card> FilterDueDate(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.Equal:
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due == entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.NotEqual:
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due != entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.GreaterThan:
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due > entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.LessThan:
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due < entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.GreaterThanOrEqual:
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due >= entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.LessThanOrEqual:
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due <= entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.HasAnyValue:
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue);
                    break;
                case CardsCondition.DoNotHaveAnyValue:
                    cards = cards.Where(x => !x.Due.HasValue);
                    break;
                case CardsCondition.AnyOfThese:
                    List<DateTimeOffset> anyOfThese = entry.ValueAsDateTimeOffsets;
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && anyOfThese.Any(y => y == x.Due));
                    break;
                case CardsCondition.NoneOfThese:
                    List<DateTimeOffset> noneOfThese = entry.ValueAsDateTimeOffsets;
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && noneOfThese.All(y => y != x.Due));
                    break;
                case CardsCondition.Contains:
                case CardsCondition.DoNotContains:
                case CardsCondition.AllOfThese:
                case CardsCondition.RegEx:
                case CardsCondition.StartsWith:
                case CardsCondition.EndsWith:
                case CardsCondition.DoNotStartWith:
                case CardsCondition.DoNotEndWith:
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a FilterDueDate");
            }

            return cards;
        }

        private static IEnumerable<Card> FilterCreateDate(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.Equal:
                    cards = cards.Where(x => x.Created.HasValue && x.Created == entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.NotEqual:
                    cards = cards.Where(x => x.Created.HasValue && x.Created != entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.GreaterThan:
                    cards = cards.Where(x => x.Created.HasValue && x.Created > entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.LessThan:
                    cards = cards.Where(x => x.Created.HasValue && x.Created < entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.GreaterThanOrEqual:
                    cards = cards.Where(x => x.Created.HasValue && x.Created >= entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.LessThanOrEqual:
                    cards = cards.Where(x => x.Created.HasValue && x.Created <= entry.ValueAsDateTimeOffset);
                    break;
                case CardsCondition.HasAnyValue:
                    cards = cards.Where(x => x.Created.HasValue);
                    break;
                case CardsCondition.DoNotHaveAnyValue:
                    cards = cards.Where(x => !x.Created.HasValue);
                    break;
                case CardsCondition.AnyOfThese:
                    List<DateTimeOffset> anyOfThese = entry.ValueAsDateTimeOffsets;
                    cards = cards.Where(x => anyOfThese.Any(y => x.Created != null && y == x.Created.Value));
                    break;
                case CardsCondition.NoneOfThese:
                    List<DateTimeOffset> noneOfThese = entry.ValueAsDateTimeOffsets;
                    cards = cards.Where(x => noneOfThese.All(y => x.Created != null && y != x.Created.Value));
                    break;
                case CardsCondition.Contains:
                case CardsCondition.DoNotContains:
                case CardsCondition.AllOfThese:
                case CardsCondition.RegEx:
                case CardsCondition.StartsWith:
                case CardsCondition.EndsWith:
                case CardsCondition.DoNotStartWith:
                case CardsCondition.DoNotEndWith:
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a FilterCreateDate");
            }

            return cards;
        }

        private static IEnumerable<Card> FilterStartDate(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.Equal:
                    cards = cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date == entry.ValueAsDateTimeOffset.Value.Date);
                    break;
                case CardsCondition.NotEqual:
                    cards = cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date != entry.ValueAsDateTimeOffset.Value.Date);
                    break;
                case CardsCondition.GreaterThan:
                    cards = cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date > entry.ValueAsDateTimeOffset.Value.Date);
                    break;
                case CardsCondition.LessThan:
                    cards = cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date < entry.ValueAsDateTimeOffset.Value.Date);
                    break;
                case CardsCondition.GreaterThanOrEqual:
                    cards = cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date >= entry.ValueAsDateTimeOffset.Value.Date);
                    break;
                case CardsCondition.LessThanOrEqual:
                    cards = cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date <= entry.ValueAsDateTimeOffset.Value.Date);
                    break;
                case CardsCondition.HasAnyValue:
                    cards = cards.Where(x => x.Start.HasValue);
                    break;
                case CardsCondition.DoNotHaveAnyValue:
                    cards = cards.Where(x => !x.Start.HasValue);
                    break;
                case CardsCondition.AnyOfThese:
                    List<DateTimeOffset> anyOfThese = entry.ValueAsDateTimeOffsets;
                    cards = cards.Where(x => x.Start.HasValue && anyOfThese.Any(y => y.Date == x.Start.Value.Date));
                    break;
                case CardsCondition.NoneOfThese:
                    List<DateTimeOffset> noneOfThese = entry.ValueAsDateTimeOffsets;
                    cards = cards.Where(x => x.Start.HasValue && noneOfThese.All(y => y.Date != x.Start.Value.Date));
                    break;
                case CardsCondition.Contains:
                case CardsCondition.DoNotContains:
                case CardsCondition.AllOfThese:
                case CardsCondition.RegEx:
                case CardsCondition.StartsWith:
                case CardsCondition.EndsWith:
                case CardsCondition.DoNotStartWith:
                case CardsCondition.DoNotEndWith:
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a FilterStartDate");
            }

            return cards;
        }

        private static IEnumerable<Card> FilterList(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.AnyOfThese:
                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => y == x.ListId));
                    }
                    else
                    {
                        cards = cards.Where(x => x.ListId == entry.ValueAsString);
                    }

                    break;

                case CardsCondition.NoneOfThese:
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.All(y => y != x.ListId));
                    }
                    else
                    {
                        cards = cards.Where(x => x.ListId != entry.ValueAsString);
                    }

                    break;
                case CardsCondition.HasAnyValue:
                case CardsCondition.DoNotContains:
                case CardsCondition.Contains:
                case CardsCondition.GreaterThan:
                case CardsCondition.LessThan:
                case CardsCondition.GreaterThanOrEqual:
                case CardsCondition.LessThanOrEqual:
                case CardsCondition.DoNotHaveAnyValue:
                case CardsCondition.AllOfThese:
                case CardsCondition.RegEx:
                case CardsCondition.StartsWith:
                case CardsCondition.EndsWith:
                case CardsCondition.DoNotStartWith:
                case CardsCondition.DoNotEndWith:
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a List Condition");
            }

            return cards;
        }

        private static IEnumerable<Card> FilterLabel(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.AllOfThese:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => { return entry.ValueAsStrings.All(y => x.LabelIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }
                    else
                    {
                        cards = cards.Where(x => x.LabelIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => { return x.LabelIds.Count == entry.ValueAsStrings.Count && entry.ValueAsStrings.All(y => x.LabelIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }
                    else
                    {
                        cards = cards.Where(x => x.LabelIds.Count == 1 && x.LabelIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.AnyOfThese:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => { return entry.ValueAsStrings.Any(y => x.LabelIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }
                    else
                    {
                        cards = cards.Where(x => x.LabelIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.NoneOfThese:
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => { return entry.ValueAsStrings.All(y => !x.LabelIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }
                    else
                    {
                        cards = cards.Where(x => !x.LabelIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.DoNotHaveAnyValue:
                    cards = cards.Where(x => x.LabelIds.Count == 0);
                    break;
                case CardsCondition.HasAnyValue:
                    cards = cards.Where(x => x.LabelIds.Count != 0);
                    break;
                case CardsCondition.GreaterThan:
                    cards = cards.Where(x => x.LabelIds.Count > entry.ValueAsInteger);
                    break;
                case CardsCondition.LessThan:
                    cards = cards.Where(x => x.LabelIds.Count < entry.ValueAsInteger);
                    break;
                case CardsCondition.GreaterThanOrEqual:
                    cards = cards.Where(x => x.LabelIds.Count >= entry.ValueAsInteger);
                    break;
                case CardsCondition.LessThanOrEqual:
                    cards = cards.Where(x => x.LabelIds.Count <= entry.ValueAsInteger);
                    break;
                case CardsCondition.DoNotContains:
                case CardsCondition.Contains:
                case CardsCondition.RegEx:
                case CardsCondition.StartsWith:
                case CardsCondition.EndsWith:
                case CardsCondition.DoNotStartWith:
                case CardsCondition.DoNotEndWith:
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a Label Condition");
            }

            return cards;
        }

        private static IEnumerable<Card> FilterMember(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.AllOfThese:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => { return entry.ValueAsStrings.All(y => x.MemberIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }
                    else
                    {
                        cards = cards.Where(x => x.MemberIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => { return x.MemberIds.Count == entry.ValueAsStrings.Count && entry.ValueAsStrings.All(y => x.MemberIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }
                    else
                    {
                        cards = cards.Where(x => x.MemberIds.Count == 1 && x.MemberIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.AnyOfThese:

                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => { return entry.ValueAsStrings.Any(y => x.MemberIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }
                    else
                    {
                        cards = cards.Where(x => x.MemberIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.NoneOfThese:
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => { return entry.ValueAsStrings.All(y => !x.MemberIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }
                    else
                    {
                        cards = cards.Where(x => !x.MemberIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.DoNotHaveAnyValue:
                    cards = cards.Where(x => x.MemberIds.Count == 0);
                    break;
                case CardsCondition.HasAnyValue:
                    cards = cards.Where(x => x.MemberIds.Count != 0);
                    break;
                case CardsCondition.GreaterThan:
                    cards = cards.Where(x => x.MemberIds.Count > entry.ValueAsInteger);
                    break;
                case CardsCondition.LessThan:
                    cards = cards.Where(x => x.MemberIds.Count < entry.ValueAsInteger);
                    break;
                case CardsCondition.GreaterThanOrEqual:
                    cards = cards.Where(x => x.MemberIds.Count >= entry.ValueAsInteger);
                    break;
                case CardsCondition.LessThanOrEqual:
                    cards = cards.Where(x => x.MemberIds.Count <= entry.ValueAsInteger);
                    break;
                case CardsCondition.Contains:
                case CardsCondition.DoNotContains:
                case CardsCondition.RegEx:
                case CardsCondition.StartsWith:
                case CardsCondition.EndsWith:
                case CardsCondition.DoNotStartWith:
                case CardsCondition.DoNotEndWith:
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a Member Condition");
            }

            return cards;
        }

        private static IEnumerable<Card> FilterName(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Contains(x.Name, StringComparer.InvariantCultureIgnoreCase));
                    }
                    else if (entry.ValueAsString != null)
                    {
                        cards = cards.Where(x => entry.ValueAsString.Equals(x.Name, StringComparison.CurrentCultureIgnoreCase));
                    }
                    else if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Name.Length == entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => !entry.ValueAsStrings.Contains(x.Name, StringComparer.InvariantCultureIgnoreCase));
                    }
                    else if (entry.ValueAsString != null)
                    {
                        cards = cards.Where(x => !entry.ValueAsString.Equals(x.Name));
                    }
                    else if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Name.Length != entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.GreaterThan:
                    //Assume this means length of name
                    if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Name.Length > entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.LessThan:
                    //Assume this means length of name
                    if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Name.Length < entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.GreaterThanOrEqual:
                    //Assume this means length of name
                    if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Name.Length >= entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.LessThanOrEqual:
                    //Assume this means length of name
                    if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Name.Length <= entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.HasAnyValue:
                    cards = cards.Where(x => !string.IsNullOrWhiteSpace(x.Name));
                    break;
                case CardsCondition.DoNotHaveAnyValue:
                    cards = cards.Where(x => string.IsNullOrWhiteSpace(x.Name));
                    break;
                case CardsCondition.Contains:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => x.Name.Contains(y)));
                    }
                    else
                    {
                        cards = cards.Where(x => x.Name.Contains(entry.ValueAsString));
                    }

                    break;
                case CardsCondition.DoNotContains:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => !x.Name.Contains(y)));
                    }
                    else
                    {
                        cards = cards.Where(x => !x.Name.Contains(entry.ValueAsString));
                    }

                    break;
                case CardsCondition.AnyOfThese:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => y.Contains(x.Name)));
                    }
                    else
                    {
                        cards = cards.Where(x => entry.ValueAsString.Equals(x.Name, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.AllOfThese:
                    throw new TrelloApiException("AllOfThese on Name Filter does not make sense");
                case CardsCondition.NoneOfThese:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => !entry.ValueAsStrings.Contains(x.Name, StringComparer.InvariantCultureIgnoreCase));
                    }
                    else
                    {
                        cards = cards.Where(x => !entry.ValueAsString.Equals(x.Name, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.RegEx:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.All(y => Regex.IsMatch(x.Name, y, RegexOptions.IgnoreCase)));
                    }
                    else
                    {
                        cards = cards.Where(x => Regex.IsMatch(x.Name, entry.ValueAsString, RegexOptions.IgnoreCase));
                    }

                    break;
                case CardsCondition.StartsWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => x.Name.StartsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }
                    else
                    {
                        cards = cards.Where(x => x.Name.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.EndsWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => x.Name.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }
                    else
                    {
                        cards = cards.Where(x => x.Name.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;

                case CardsCondition.DoNotStartWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.All(y => !x.Name.StartsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }
                    else
                    {
                        cards = cards.Where(x => !x.Name.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;

                case CardsCondition.DoNotEndWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.All(y => !x.Name.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }
                    else
                    {
                        cards = cards.Where(x => !x.Name.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a Name Condition");
            }

            return cards;
        }

        private static IEnumerable<Card> FilterDescription(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.Equal:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Contains(x.Description, StringComparer.InvariantCultureIgnoreCase));
                    }
                    else if (entry.ValueAsString != null)
                    {
                        cards = cards.Where(x => entry.ValueAsString.Equals(x.Description, StringComparison.CurrentCultureIgnoreCase));
                    }
                    else if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Description.Length == entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.NotEqual:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => !entry.ValueAsStrings.Contains(x.Description, StringComparer.InvariantCultureIgnoreCase));
                    }
                    else if (entry.ValueAsString != null)
                    {
                        cards = cards.Where(x => !entry.ValueAsString.Equals(x.Description, StringComparison.CurrentCultureIgnoreCase));
                    }
                    else if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Description.Length != entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.GreaterThan:
                    //Assume this means length of Description
                    if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Description.Length > entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.LessThan:
                    //Assume this means length of Description
                    if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Description.Length < entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.GreaterThanOrEqual:
                    //Assume this means length of Description
                    if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Description.Length >= entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.LessThanOrEqual:
                    //Assume this means length of Description
                    if (entry.ValueAsInteger.HasValue)
                    {
                        cards = cards.Where(x => x.Description.Length <= entry.ValueAsInteger);
                    }

                    break;
                case CardsCondition.HasAnyValue:
                    cards = cards.Where(x => !string.IsNullOrWhiteSpace(x.Description));
                    break;
                case CardsCondition.DoNotHaveAnyValue:
                    cards = cards.Where(x => string.IsNullOrWhiteSpace(x.Description));
                    break;
                case CardsCondition.Contains:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => x.Description.Contains(y)));
                    }
                    else
                    {
                        cards = cards.Where(x => x.Description.Contains(entry.ValueAsString));
                    }

                    break;
                case CardsCondition.DoNotContains:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => !x.Description.Contains(y)));
                    }
                    else
                    {
                        cards = cards.Where(x => !x.Description.Contains(entry.ValueAsString));
                    }

                    break;
                case CardsCondition.AnyOfThese:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => y.Contains(x.Description)));
                    }
                    else
                    {
                        cards = cards.Where(x => entry.ValueAsString.Equals(x.Description, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.AllOfThese:
                {
                    throw new TrelloApiException("AllOfThese on Description Filter does not make sense");
                }
                case CardsCondition.NoneOfThese:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => !entry.ValueAsStrings.Contains(x.Description, StringComparer.InvariantCultureIgnoreCase));
                    }
                    else
                    {
                        cards = cards.Where(x => !entry.ValueAsString.Equals(x.Description, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.RegEx:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.All(y => Regex.IsMatch(x.Description, y, RegexOptions.IgnoreCase)));
                    }
                    else
                    {
                        cards = cards.Where(x => Regex.IsMatch(x.Description, entry.ValueAsString, RegexOptions.IgnoreCase));
                    }

                    break;
                case CardsCondition.StartsWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => x.Description.StartsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }
                    else
                    {
                        cards = cards.Where(x => x.Description.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;
                case CardsCondition.EndsWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => x.Description.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }
                    else
                    {
                        cards = cards.Where(x => x.Description.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;

                case CardsCondition.DoNotStartWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => !x.Description.StartsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }
                    else
                    {
                        cards = cards.Where(x => !x.Description.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;

                case CardsCondition.DoNotEndWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        cards = cards.Where(x => entry.ValueAsStrings.Any(y => !x.Description.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }
                    else
                    {
                        cards = cards.Where(x => !x.Description.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));
                    }

                    break;
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a Description Condition");
            }

            return cards;
        }
    }
}