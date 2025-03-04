using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using TrelloDotNet.Model;

namespace TrelloDotNet.Extensions
{
    /// <summary>
    /// Extensions for list of Cards
    /// </summary>
    public static class CardsExtensions
    {
        /// <summary>
        /// Filter list of cards based on a set of conditions
        /// </summary>
        /// <param name="cards">The list of cards</param>
        /// <param name="conditions">The conditions to apply</param>
        /// <returns></returns>
        public static List<Card> Filter(this IEnumerable<Card> cards, params CardsFilterCondition[] conditions)
        {
            foreach (CardsFilterCondition entry in conditions)
            {
                switch (entry.Field)
                {
                    case CardsConditionField.Name:
                        cards = FilterName(cards, entry);
                        break;
                    case CardsConditionField.ListId:
                        cards = FilterListId(cards, entry);
                        break;
                    case CardsConditionField.ListName:
                        cards = FilterListName(cards, entry);
                        break;
                    case CardsConditionField.LabelId:
                        cards = FilterLabelId(cards, entry);
                        break;
                    case CardsConditionField.LabelName:
                        cards = FilterLabelName(cards, entry);
                        break;
                    case CardsConditionField.MemberId:
                        cards = FilterMemberId(cards, entry);
                        break;
                    case CardsConditionField.MemberName:
                        cards = FilterMemberName(cards, entry);
                        break;
                    case CardsConditionField.Description:
                        cards = FilterDescription(cards, entry);
                        break;
                    case CardsConditionField.DueWithNoDueComplete:
                        cards = FilterDueDateNoDueComplete(cards, entry);
                        break;
                    case CardsConditionField.Due:
                        cards = FilterDueDate(cards, entry);
                        break;
                    case CardsConditionField.Start:
                        cards = FilterStartDate(cards, entry);
                        break;
                    case CardsConditionField.Created:
                        cards = FilterCreateDate(cards, entry);
                        break;
                    case CardsConditionField.CustomField:
                        cards = FilterCustomField(cards, entry);
                        break;
                    case CardsConditionField.DueComplete:
                        cards = FilterDueComplete(cards, entry);
                        break;
                }
            }

            return cards.ToList();
        }

        private static IEnumerable<Card> FilterLabelName(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.AllOfThese:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => x.Labels.Any(z => z.Name.Equals(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Labels.Any(y => y.Name.Equals(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));

                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return x.LabelIds.Count == entry.ValueAsStrings.Count && entry.ValueAsStrings.All(y => x.Labels.Any(z => z.Name.Equals(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    // ReSharper disable once ConvertIfStatementToReturnStatement
                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.LabelIds.Count == entry.ValueAsNumber.Value);
                    }

                    return cards.Where(x => x.LabelIds.Count == 1 && x.Labels.Any(z => z.Name.Equals(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));

                case CardsCondition.AnyOfThese:

                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.Labels.Any(z => z.Name.Equals(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Labels.Any(z => z.Name.Equals(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));

                case CardsCondition.NoneOfThese:
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => !x.Labels.Any(z => z.Name.Equals(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    // ReSharper disable once ConvertIfStatementToReturnStatement
                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.LabelIds.Count != entry.ValueAsNumber.Value);
                    }

                    return cards.Where(x => !x.Labels.Any(z => z.Name.Equals(entry.ValueAsString)));

                case CardsCondition.DoNotHaveAnyValue:
                    return cards.Where(x => x.LabelIds.Count == 0);
                case CardsCondition.HasAnyValue:
                    return cards.Where(x => x.LabelIds.Count != 0);
                case CardsCondition.GreaterThan:
                    return cards.Where(x => x.LabelIds.Count > entry.ValueAsNumber);
                case CardsCondition.LessThan:
                    return cards.Where(x => x.LabelIds.Count < entry.ValueAsNumber);
                case CardsCondition.GreaterThanOrEqual:
                    return cards.Where(x => x.LabelIds.Count >= entry.ValueAsNumber);
                case CardsCondition.LessThanOrEqual:
                    return cards.Where(x => x.LabelIds.Count <= entry.ValueAsNumber);
                case CardsCondition.Between:
                {
                    if (entry.ValueAsNumbers?.Count != 2)
                    {
                        throw new TrelloApiException("Between Condition for Labels need 2 and only 2 Numbers");
                    }

                    decimal from = entry.ValueAsNumbers.First();
                    decimal to = entry.ValueAsNumbers.Last();
                    cards = cards.Where(x => x.LabelIds.Count >= from);
                    cards = cards.Where(x => x.LabelIds.Count <= to);
                    return cards;
                }
                case CardsCondition.NotBetween:
                {
                    if (entry.ValueAsNumbers?.Count != 2)
                    {
                        throw new TrelloApiException("NotBetween Condition for Labels need 2 and only 2 Numbers");
                    }

                    decimal from = entry.ValueAsNumbers.First();
                    decimal to = entry.ValueAsNumbers.Last();
                    cards = cards.Where(x => x.LabelIds.Count > to || x.LabelIds.Count < from);
                    return cards;
                }
                case CardsCondition.Contains:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.Labels.Any(z => z.Name.Contains(y))); });
                    }

                    return cards.Where(x => x.Labels.Any(z => z.Name.Contains(entry.ValueAsString)));
                case CardsCondition.DoNotContains:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => x.Labels.All(z => !z.Name.Contains(y))); });
                    }

                    return cards.Where(x => x.Labels.All(z => !z.Name.Contains(entry.ValueAsString)));
                case CardsCondition.RegEx:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.Labels.Any(z => Regex.IsMatch(z.Name, y, RegexOptions.IgnoreCase))); });
                    }

                    return cards.Where(x => x.Labels.Any(z => Regex.IsMatch(z.Name, entry.ValueAsString, RegexOptions.IgnoreCase)));
                case CardsCondition.StartsWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return x.LabelIds.Count == entry.ValueAsStrings.Count && entry.ValueAsStrings.Any(y => x.Labels.Any(z => z.Name.StartsWith(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Labels.Any(z => z.Name.StartsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));
                case CardsCondition.DoNotStartWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => x.Labels.All(z => !z.Name.StartsWith(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Labels.All(z => !z.Name.StartsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));
                case CardsCondition.EndsWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.Labels.Any(z => z.Name.EndsWith(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Labels.Any(z => z.Name.EndsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));
                case CardsCondition.DoNotEndWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => x.Labels.All(z => !z.Name.EndsWith(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Labels.All(z => !z.Name.EndsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a Label Condition");
            }
        }

        private static IEnumerable<Card> FilterListName(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.AnyOfThese:
                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => y.Equals(x.List.Name, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => x.List.Name.Equals(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase));
                case CardsCondition.NoneOfThese:
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => !y.Equals(x.List.Name, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => !x.List.Name.Equals(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase));
                case CardsCondition.Contains:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => x.List.Name.Contains(y)));
                    }

                    return cards.Where(x => x.List.Name.Contains(entry.ValueAsString));
                case CardsCondition.DoNotContains:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => !x.List.Name.Contains(y)));
                    }

                    return cards.Where(x => !x.List.Name.Contains(entry.ValueAsString));
                case CardsCondition.RegEx:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => Regex.IsMatch(x.List.Name, y, RegexOptions.IgnoreCase)));
                    }

                    return cards.Where(x => Regex.IsMatch(x.List.Name, entry.ValueAsString, RegexOptions.IgnoreCase));
                case CardsCondition.StartsWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => x.List.Name.StartsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => x.List.Name.StartsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase));
                case CardsCondition.EndsWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => x.List.Name.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => x.List.Name.EndsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase));
                case CardsCondition.DoNotStartWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => !x.List.Name.StartsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => !x.List.Name.StartsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase));
                case CardsCondition.DoNotEndWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => !x.List.Name.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => !x.List.Name.EndsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase));
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a List Condition");
            }
        }

        private static IEnumerable<Card> FilterDueComplete(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.Equal:
                    return cards.Where(x => x.DueComplete == entry.ValueAsBoolean);
                case CardsCondition.NotEqual:
                    return cards.Where(x => x.DueComplete != entry.ValueAsBoolean);
                default:
                    throw new TrelloApiException($"{entry.Condition} on DueComplete Filter does not make sense");
            }
        }

        private static IEnumerable<Card> FilterCustomField(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            if (entry.CustomFieldEntry == null)
            {
                throw new TrelloApiException("CustomField was not provided");
            }

            var customFieldDefinition = entry.CustomFieldEntry;

            cards = cards.Where(x =>
            {
                CustomFieldItem customFieldItem = x.CustomFieldItems.FirstOrDefault(y => y.CustomFieldId == customFieldDefinition.Id);
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
                            case CardsCondition.Between:
                            {
                                if (entry.ValueAsDateTimeOffsets?.Count != 2)
                                {
                                    throw new TrelloApiException("Between Condition for Custom Field need 2 and only 2 Dates");
                                }

                                DateTimeOffset from = entry.ValueAsDateTimeOffsets.First();
                                DateTimeOffset to = entry.ValueAsDateTimeOffsets.Last();
                                return date.HasValue && date.Value >= from && date.Value <= to;
                            }
                            case CardsCondition.NotBetween:
                            {
                                if (entry.ValueAsDateTimeOffsets?.Count != 2)
                                {
                                    throw new TrelloApiException("NotBetween Condition for Custom Field need 2 and only 2 Dates");
                                }

                                DateTimeOffset from = entry.ValueAsDateTimeOffsets.First();
                                DateTimeOffset to = entry.ValueAsDateTimeOffsets.Last();
                                return (date.HasValue && date.Value > to) || (date.HasValue && date.Value < from);
                            }
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
                                return listValue.Length > entry.ValueAsNumber;
                            case CardsCondition.LessThan:
                                return listValue.Length < entry.ValueAsNumber;
                            case CardsCondition.GreaterThanOrEqual:
                                return listValue.Length >= entry.ValueAsNumber;
                            case CardsCondition.LessThanOrEqual:
                                return listValue.Length <= entry.ValueAsNumber;
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
                                return number == entry.ValueAsNumber;
                            case CardsCondition.NotEqual:
                                return number != entry.ValueAsNumber;
                            case CardsCondition.GreaterThan:
                                return number > entry.ValueAsNumber;
                            case CardsCondition.LessThan:
                                return number < entry.ValueAsNumber;
                            case CardsCondition.GreaterThanOrEqual:
                                return number >= entry.ValueAsNumber;
                            case CardsCondition.LessThanOrEqual:
                                return number <= entry.ValueAsNumber;
                            case CardsCondition.AnyOfThese:
                                return entry.ValueAsNumbers.Any(y => y == number);
                            case CardsCondition.NoneOfThese:
                                return entry.ValueAsNumbers.All(y => y != number);
                            case CardsCondition.Between:
                            {
                                if (entry.ValueAsNumbers?.Count != 2)
                                {
                                    throw new TrelloApiException("Between Condition for Custom Field need 2 and only 2 Numbers");
                                }

                                decimal from = entry.ValueAsNumbers.First();
                                decimal to = entry.ValueAsNumbers.Last();
                                return number >= from && number <= to;
                            }
                            case CardsCondition.NotBetween:
                            {
                                if (entry.ValueAsDateTimeOffsets?.Count != 2)
                                {
                                    throw new TrelloApiException("NotBetween Condition for Custom Field need 2 and only 2 Numbers");
                                }

                                decimal from = entry.ValueAsNumbers.First();
                                decimal to = entry.ValueAsNumbers.Last();
                                return number > to || number < from;
                            }
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
                                return textValue.Length > entry.ValueAsNumber;
                            case CardsCondition.LessThan:
                                return textValue.Length < entry.ValueAsNumber;
                            case CardsCondition.GreaterThanOrEqual:
                                return textValue.Length >= entry.ValueAsNumber;
                            case CardsCondition.LessThanOrEqual:
                                return textValue.Length <= entry.ValueAsNumber;
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
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => x.Due.HasValue && entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.All(y => y == x.Due.Value));
                    }

                    return cards.Where(x => x.Due.HasValue && x.Due == entry.ValueAsDateTimeOffset);
                case CardsCondition.NotEqual:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => x.Due.HasValue && entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.All(y => y != x.Due.Value));
                    }

                    return cards.Where(x => x.Due.HasValue && x.Due != entry.ValueAsDateTimeOffset);
                case CardsCondition.GreaterThan:
                    return cards.Where(x => x.Due.HasValue && x.Due > entry.ValueAsDateTimeOffset);
                case CardsCondition.LessThan:
                    return cards.Where(x => x.Due.HasValue && x.Due < entry.ValueAsDateTimeOffset);
                case CardsCondition.GreaterThanOrEqual:
                    return cards.Where(x => x.Due.HasValue && x.Due >= entry.ValueAsDateTimeOffset);
                case CardsCondition.LessThanOrEqual:
                    return cards.Where(x => x.Due.HasValue && x.Due <= entry.ValueAsDateTimeOffset);

                case CardsCondition.Between:
                {
                    if (entry.ValueAsDateTimeOffsets?.Count != 2)
                    {
                        throw new TrelloApiException("Between Condition for Due Date need 2 and only 2 Dates");
                    }

                    DateTimeOffset from = entry.ValueAsDateTimeOffsets.First();
                    DateTimeOffset to = entry.ValueAsDateTimeOffsets.Last();
                    cards = cards.Where(x => x.Due.HasValue && x.Due >= from);
                    cards = cards.Where(x => x.Due.HasValue && x.Due <= to);
                    return cards;
                }
                case CardsCondition.NotBetween:
                {
                    if (entry.ValueAsDateTimeOffsets?.Count != 2)
                    {
                        throw new TrelloApiException("NotBetween Condition for Due Date need 2 and only 2 Dates");
                    }

                    DateTimeOffset from = entry.ValueAsDateTimeOffsets.First();
                    DateTimeOffset to = entry.ValueAsDateTimeOffsets.Last();
                    cards = cards.Where(x => x.Due.HasValue && x.Due > to || x.Due < from);
                    return cards;
                }

                case CardsCondition.HasAnyValue:
                    return cards.Where(x => x.Due.HasValue);
                case CardsCondition.DoNotHaveAnyValue:
                    return cards.Where(x => !x.Due.HasValue);
                case CardsCondition.AnyOfThese:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => x.Due.HasValue && entry.ValueAsDateTimeOffsets.Any(y => y == x.Due));
                    }

                    return cards.Where(x => x.Due != null && entry.ValueAsDateTimeOffset != null && entry.ValueAsDateTimeOffset.Value.Date == x.Due.Value.Date);
                case CardsCondition.NoneOfThese:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => x.Due.HasValue && entry.ValueAsDateTimeOffsets.All(y => y != x.Due));
                    }

                    return cards.Where(x => x.Due != null && entry.ValueAsDateTimeOffset != null && entry.ValueAsDateTimeOffset.Value.Date != x.Due.Value.Date);
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a FilterDueDate");
            }
        }

        private static IEnumerable<Card> FilterDueDateNoDueComplete(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.Equal:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => !x.DueComplete && x.Due.HasValue && entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.All(y => y == x.Due.Value));
                    }

                    return cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due == entry.ValueAsDateTimeOffset);
                case CardsCondition.NotEqual:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => !x.DueComplete && x.Due.HasValue && entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.All(y => y != x.Due.Value));
                    }

                    return cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due != entry.ValueAsDateTimeOffset);
                case CardsCondition.GreaterThan:
                    return cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due > entry.ValueAsDateTimeOffset);
                case CardsCondition.LessThan:
                    return cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due < entry.ValueAsDateTimeOffset);
                case CardsCondition.GreaterThanOrEqual:
                    return cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due >= entry.ValueAsDateTimeOffset);
                case CardsCondition.LessThanOrEqual:
                    return cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due <= entry.ValueAsDateTimeOffset);

                case CardsCondition.Between:
                {
                    if (entry.ValueAsDateTimeOffsets?.Count != 2)
                    {
                        throw new TrelloApiException("Between Condition for Due Date need 2 and only 2 Dates");
                    }

                    DateTimeOffset from = entry.ValueAsDateTimeOffsets.First();
                    DateTimeOffset to = entry.ValueAsDateTimeOffsets.Last();
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due >= from);
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due <= to);
                    return cards;
                }
                case CardsCondition.NotBetween:
                {
                    if (entry.ValueAsDateTimeOffsets?.Count != 2)
                    {
                        throw new TrelloApiException("NotBetween Condition for Due Date need 2 and only 2 Dates");
                    }

                    DateTimeOffset from = entry.ValueAsDateTimeOffsets.First();
                    DateTimeOffset to = entry.ValueAsDateTimeOffsets.Last();
                    cards = cards.Where(x => !x.DueComplete && x.Due.HasValue && x.Due > to || x.Due < from);
                    return cards;
                }

                case CardsCondition.HasAnyValue:
                    return cards.Where(x => !x.DueComplete && x.Due.HasValue);
                case CardsCondition.DoNotHaveAnyValue:
                    return cards.Where(x => !x.Due.HasValue);
                case CardsCondition.AnyOfThese:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => !x.DueComplete && x.Due.HasValue && entry.ValueAsDateTimeOffsets.Any(y => y == x.Due));
                    }

                    return cards.Where(x => !x.DueComplete && x.Due != null && entry.ValueAsDateTimeOffset != null && entry.ValueAsDateTimeOffset.Value.Date == x.Due.Value.Date);
                case CardsCondition.NoneOfThese:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => !x.DueComplete && x.Due.HasValue && entry.ValueAsDateTimeOffsets.All(y => y != x.Due));
                    }

                    return cards.Where(x => !x.DueComplete && x.Due != null && entry.ValueAsDateTimeOffset != null && entry.ValueAsDateTimeOffset.Value.Date != x.Due.Value.Date);
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a FilterDueDate");
            }
        }

        private static IEnumerable<Card> FilterCreateDate(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.Equal:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => x.Created.HasValue && entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.All(y => y == x.Created.Value));
                    }

                    return cards.Where(x => x.Created.HasValue && x.Created == entry.ValueAsDateTimeOffset);
                case CardsCondition.NotEqual:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => x.Created.HasValue && entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.All(y => x.Created != null && y.Date != x.Created.Value));
                    }

                    return cards.Where(x => x.Created.HasValue && x.Created != entry.ValueAsDateTimeOffset);
                case CardsCondition.GreaterThan:
                    return cards.Where(x => x.Created.HasValue && x.Created > entry.ValueAsDateTimeOffset);
                case CardsCondition.LessThan:
                    return cards.Where(x => x.Created.HasValue && x.Created < entry.ValueAsDateTimeOffset);
                case CardsCondition.GreaterThanOrEqual:
                    return cards.Where(x => x.Created.HasValue && x.Created >= entry.ValueAsDateTimeOffset);
                case CardsCondition.LessThanOrEqual:
                    return cards.Where(x => x.Created.HasValue && x.Created <= entry.ValueAsDateTimeOffset);
                case CardsCondition.Between:
                {
                    if (entry.ValueAsDateTimeOffsets?.Count != 2)
                    {
                        throw new TrelloApiException("Between Condition for Created Date need 2 and only 2 Dates");
                    }

                    DateTimeOffset from = entry.ValueAsDateTimeOffsets.First();
                    DateTimeOffset to = entry.ValueAsDateTimeOffsets.Last();
                    cards = cards.Where(x => x.Created.HasValue && x.Created >= from);
                    cards = cards.Where(x => x.Created.HasValue && x.Created <= to);
                    return cards;
                }
                case CardsCondition.NotBetween:
                {
                    if (entry.ValueAsDateTimeOffsets?.Count != 2)
                    {
                        throw new TrelloApiException("NotBetween Condition for Created Date need 2 and only 2 Dates");
                    }

                    DateTimeOffset from = entry.ValueAsDateTimeOffsets.First();
                    DateTimeOffset to = entry.ValueAsDateTimeOffsets.Last();
                    cards = cards.Where(x => x.Created.HasValue && x.Created > to || x.Created < from);
                    return cards;
                }
                case CardsCondition.AnyOfThese:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsDateTimeOffsets.Any(y => x.Created != null && y == x.Created.Value));
                    }

                    return cards.Where(x => x.Created != null && entry.ValueAsDateTimeOffset == x.Created.Value);

                case CardsCondition.NoneOfThese:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsDateTimeOffsets.All(y => x.Created != null && y != x.Created.Value));
                    }

                    return cards.Where(x => x.Created != null && entry.ValueAsDateTimeOffset != x.Created.Value);
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a FilterCreateDate");
            }
        }

        private static IEnumerable<Card> FilterStartDate(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.Equal:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.All(y => y.Date == x.Start.Value.Date));
                    }

                    return cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date == entry.ValueAsDateTimeOffset.Value.Date);
                case CardsCondition.NotEqual:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.All(y => y.Date != x.Start.Value.Date));
                    }

                    return cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date != entry.ValueAsDateTimeOffset.Value.Date);
                case CardsCondition.GreaterThan:
                    return cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date > entry.ValueAsDateTimeOffset.Value.Date);
                case CardsCondition.LessThan:
                    return cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date < entry.ValueAsDateTimeOffset.Value.Date);
                case CardsCondition.GreaterThanOrEqual:
                    return cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date >= entry.ValueAsDateTimeOffset.Value.Date);
                case CardsCondition.LessThanOrEqual:
                    return cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffset != null && x.Start.Value.Date <= entry.ValueAsDateTimeOffset.Value.Date);
                case CardsCondition.Between:
                {
                    if (entry.ValueAsDateTimeOffsets?.Count != 2)
                    {
                        throw new TrelloApiException("Between Condition for Start Date need 2 and only 2 Dates");
                    }

                    DateTimeOffset from = entry.ValueAsDateTimeOffsets.First();
                    DateTimeOffset to = entry.ValueAsDateTimeOffsets.Last();
                    cards = cards.Where(x => x.Start.HasValue && x.Start >= from);
                    cards = cards.Where(x => x.Start.HasValue && x.Start <= to);
                    return cards;
                }
                case CardsCondition.NotBetween:
                {
                    if (entry.ValueAsDateTimeOffsets?.Count != 2)
                    {
                        throw new TrelloApiException("NotBetween Condition for Start Date need 2 and only 2 Dates");
                    }

                    DateTimeOffset from = entry.ValueAsDateTimeOffsets.First();
                    DateTimeOffset to = entry.ValueAsDateTimeOffsets.Last();
                    cards = cards.Where(x => x.Start.HasValue && x.Start > to || x.Start < from);
                    return cards;
                }
                case CardsCondition.HasAnyValue:
                    return cards.Where(x => x.Start.HasValue);
                case CardsCondition.DoNotHaveAnyValue:
                    return cards.Where(x => !x.Start.HasValue);
                case CardsCondition.AnyOfThese:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffsets.Any(y => y.Date == x.Start.Value.Date));
                    }

                    return cards.Where(x => x.Start != null && entry.ValueAsDateTimeOffset != null && entry.ValueAsDateTimeOffset.Value.Date == x.Start.Value.Date);

                case CardsCondition.NoneOfThese:
                    if (entry.ValueAsDateTimeOffsets != null && entry.ValueAsDateTimeOffsets.Count != 0)
                    {
                        return cards.Where(x => x.Start.HasValue && entry.ValueAsDateTimeOffsets.All(y => y.Date != x.Start.Value.Date));
                    }

                    return cards.Where(x => x.Start != null && entry.ValueAsDateTimeOffset != null && entry.ValueAsDateTimeOffset.Value.Date != x.Start.Value.Date);

                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a FilterStartDate");
            }
        }

        private static IEnumerable<Card> FilterListId(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.AnyOfThese:
                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => y == x.ListId));
                    }

                    return cards.Where(x => x.ListId == entry.ValueAsString);
                case CardsCondition.NoneOfThese:
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => y != x.ListId));
                    }

                    return cards.Where(x => x.ListId != entry.ValueAsString);
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a List Condition");
            }
        }

        private static IEnumerable<Card> FilterLabelId(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.AllOfThese:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => x.LabelIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => x.LabelIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));

                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => x.LabelIds.Count == entry.ValueAsStrings.Count && entry.ValueAsStrings.All(y => x.LabelIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)));
                    }

                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.LabelIds.Count == entry.ValueAsNumber.Value);
                    }

                    return cards.Where(x => x.LabelIds.Count == 1 && x.LabelIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));

                case CardsCondition.AnyOfThese:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.LabelIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }

                    return cards.Where(x => x.LabelIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));

                case CardsCondition.NoneOfThese:
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => !x.LabelIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }

                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.LabelIds.Count != entry.ValueAsNumber.Value);
                    }

                    return cards.Where(x => !x.LabelIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));
                case CardsCondition.DoNotHaveAnyValue:
                    return cards.Where(x => x.LabelIds.Count == 0);
                case CardsCondition.HasAnyValue:
                    return cards.Where(x => x.LabelIds.Count != 0);
                case CardsCondition.GreaterThan:
                    return cards.Where(x => x.LabelIds.Count > entry.ValueAsNumber);
                case CardsCondition.LessThan:
                    return cards.Where(x => x.LabelIds.Count < entry.ValueAsNumber);
                case CardsCondition.GreaterThanOrEqual:
                    return cards.Where(x => x.LabelIds.Count >= entry.ValueAsNumber);
                case CardsCondition.LessThanOrEqual:
                    return cards.Where(x => x.LabelIds.Count <= entry.ValueAsNumber);
                case CardsCondition.Between:
                {
                    if (entry.ValueAsNumbers?.Count != 2)
                    {
                        throw new TrelloApiException("Between Condition for Labels need 2 and only 2 Numbers");
                    }

                    decimal from = entry.ValueAsNumbers.First();
                    decimal to = entry.ValueAsNumbers.Last();
                    cards = cards.Where(x => x.LabelIds.Count >= from);
                    cards = cards.Where(x => x.LabelIds.Count <= to);
                    return cards;
                }
                case CardsCondition.NotBetween:
                {
                    if (entry.ValueAsNumbers?.Count != 2)
                    {
                        throw new TrelloApiException("NotBetween Condition for Labels need 2 and only 2 Numbers");
                    }

                    decimal from = entry.ValueAsNumbers.First();
                    decimal to = entry.ValueAsNumbers.Last();
                    cards = cards.Where(x => x.LabelIds.Count > to || x.LabelIds.Count < from);
                    return cards;
                }
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a Label Condition");
            }
        }

        private static IEnumerable<Card> FilterMemberId(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.AllOfThese:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => x.MemberIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }

                    return cards.Where(x => x.MemberIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));

                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return x.MemberIds.Count == entry.ValueAsStrings.Count && entry.ValueAsStrings.All(y => x.MemberIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }

                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.MemberIds.Count == entry.ValueAsNumber.Value);
                    }

                    return cards.Where(x => x.MemberIds.Count == 1 && x.MemberIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));

                case CardsCondition.AnyOfThese:

                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.MemberIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }

                    return cards.Where(x => x.MemberIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));

                case CardsCondition.NoneOfThese:
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => !x.MemberIds.Contains(y, StringComparer.InvariantCultureIgnoreCase)); });
                    }

                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.MemberIds.Count != entry.ValueAsNumber.Value);
                    }

                    return cards.Where(x => !x.MemberIds.Contains(entry.ValueAsString, StringComparer.InvariantCultureIgnoreCase));

                case CardsCondition.DoNotHaveAnyValue:
                    return cards.Where(x => x.MemberIds.Count == 0);
                case CardsCondition.HasAnyValue:
                    return cards.Where(x => x.MemberIds.Count != 0);
                case CardsCondition.GreaterThan:
                    return cards.Where(x => x.MemberIds.Count > entry.ValueAsNumber);
                case CardsCondition.LessThan:
                    return cards.Where(x => x.MemberIds.Count < entry.ValueAsNumber);
                case CardsCondition.GreaterThanOrEqual:
                    return cards.Where(x => x.MemberIds.Count >= entry.ValueAsNumber);
                case CardsCondition.LessThanOrEqual:
                    return cards.Where(x => x.MemberIds.Count <= entry.ValueAsNumber);
                case CardsCondition.Between:
                {
                    if (entry.ValueAsNumbers?.Count != 2)
                    {
                        throw new TrelloApiException("Between Condition for Members need 2 and only 2 Numbers");
                    }

                    decimal from = entry.ValueAsNumbers.First();
                    decimal to = entry.ValueAsNumbers.Last();
                    cards = cards.Where(x => x.MemberIds.Count >= from);
                    cards = cards.Where(x => x.MemberIds.Count <= to);
                    return cards;
                }
                case CardsCondition.NotBetween:
                {
                    if (entry.ValueAsNumbers?.Count != 2)
                    {
                        throw new TrelloApiException("NotBetween Condition for Members need 2 and only 2 Numbers");
                    }

                    decimal from = entry.ValueAsNumbers.First();
                    decimal to = entry.ValueAsNumbers.Last();
                    cards = cards.Where(x => x.MemberIds.Count > to || x.MemberIds.Count < from);
                    return cards;
                }
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a Member Condition");
            }
        }

        private static IEnumerable<Card> FilterMemberName(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.AllOfThese:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => x.Members.Any(z => z.FullName.Equals(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Members.Any(y => y.FullName.Equals(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));

                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return x.MemberIds.Count == entry.ValueAsStrings.Count && entry.ValueAsStrings.All(y => x.Members.Any(z => z.FullName.Equals(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.MemberIds.Count == 1 && x.Members.Any(z => z.FullName.Equals(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));

                case CardsCondition.AnyOfThese:

                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.Members.Any(z => z.FullName.Equals(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Members.Any(z => z.FullName.Equals(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));

                case CardsCondition.NoneOfThese:
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => !x.Members.Any(z => z.FullName.Equals(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => !x.Members.Any(z => z.FullName.Equals(entry.ValueAsString)));

                case CardsCondition.DoNotHaveAnyValue:
                    return cards.Where(x => x.MemberIds.Count == 0);
                case CardsCondition.HasAnyValue:
                    return cards.Where(x => x.MemberIds.Count != 0);
                case CardsCondition.GreaterThan:
                    return cards.Where(x => x.MemberIds.Count > entry.ValueAsNumber);
                case CardsCondition.LessThan:
                    return cards.Where(x => x.MemberIds.Count < entry.ValueAsNumber);
                case CardsCondition.GreaterThanOrEqual:
                    return cards.Where(x => x.MemberIds.Count >= entry.ValueAsNumber);
                case CardsCondition.LessThanOrEqual:
                    return cards.Where(x => x.MemberIds.Count <= entry.ValueAsNumber);
                case CardsCondition.Between:
                {
                    if (entry.ValueAsNumbers?.Count != 2)
                    {
                        throw new TrelloApiException("Between Condition for Members need 2 and only 2 Numbers");
                    }

                    decimal from = entry.ValueAsNumbers.First();
                    decimal to = entry.ValueAsNumbers.Last();
                    cards = cards.Where(x => x.MemberIds.Count >= from);
                    cards = cards.Where(x => x.MemberIds.Count <= to);
                    return cards;
                }
                case CardsCondition.NotBetween:
                {
                    if (entry.ValueAsNumbers?.Count != 2)
                    {
                        throw new TrelloApiException("NotBetween Condition for Members need 2 and only 2 Numbers");
                    }

                    decimal from = entry.ValueAsNumbers.First();
                    decimal to = entry.ValueAsNumbers.Last();
                    cards = cards.Where(x => x.MemberIds.Count > to || x.MemberIds.Count < from);
                    return cards;
                }
                case CardsCondition.Contains:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.Members.Any(z => z.FullName.Contains(y))); });
                    }

                    return cards.Where(x => x.Members.Any(z => z.FullName.Contains(entry.ValueAsString)));
                case CardsCondition.DoNotContains:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => x.Members.All(z => !z.FullName.Contains(y))); });
                    }

                    return cards.Where(x => x.Members.All(z => !z.FullName.Contains(entry.ValueAsString)));
                case CardsCondition.RegEx:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.Members.Any(z => Regex.IsMatch(z.FullName, y, RegexOptions.IgnoreCase))); });
                    }

                    return cards.Where(x => x.Members.Any(z => Regex.IsMatch(z.FullName, entry.ValueAsString, RegexOptions.IgnoreCase)));
                case CardsCondition.StartsWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.Members.Any(z => z.FullName.StartsWith(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Members.Any(z => z.FullName.StartsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));
                case CardsCondition.DoNotStartWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => x.Members.All(z => !z.FullName.StartsWith(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Members.All(z => !z.FullName.StartsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));
                case CardsCondition.EndsWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.Any(y => x.Members.Any(z => z.FullName.EndsWith(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Members.Any(z => z.FullName.EndsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));
                case CardsCondition.DoNotEndWith:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => { return entry.ValueAsStrings.All(y => x.Members.All(z => !z.FullName.EndsWith(y, StringComparison.InvariantCultureIgnoreCase))); });
                    }

                    return cards.Where(x => x.Members.All(z => !z.FullName.EndsWith(entry.ValueAsString, StringComparison.InvariantCultureIgnoreCase)));
                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a Member Condition");
            }
        }

        private static IEnumerable<Card> FilterName(IEnumerable<Card> cards, CardsFilterCondition entry)
        {
            switch (entry.Condition)
            {
                case CardsCondition.Equal:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Contains(x.Name, StringComparer.InvariantCultureIgnoreCase));
                    }

                    if (entry.ValueAsString != null)
                    {
                        return cards.Where(x => entry.ValueAsString.Equals(x.Name, StringComparison.CurrentCultureIgnoreCase));
                    }

                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.Name?.Length == entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.NotEqual:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => !entry.ValueAsStrings.Contains(x.Name, StringComparer.InvariantCultureIgnoreCase));
                    }

                    if (entry.ValueAsString != null)
                    {
                        return cards.Where(x => !entry.ValueAsString.Equals(x.Name));
                    }

                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.Name?.Length != entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.GreaterThan:
                    //Assume this means length of name
                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.Name?.Length > entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.LessThan:
                    //Assume this means length of name
                    if (entry.ValueAsNumber.HasValue)
                    {
                        cards = cards.Where(x => x.Name?.Length < entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.GreaterThanOrEqual:
                    //Assume this means length of name
                    if (entry.ValueAsNumber.HasValue)
                    {
                        cards = cards.Where(x => x.Name?.Length >= entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.LessThanOrEqual:
                    //Assume this means length of name
                    if (entry.ValueAsNumber.HasValue)
                    {
                        cards = cards.Where(x => x.Name?.Length <= entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.HasAnyValue:
                    return cards.Where(x => !string.IsNullOrWhiteSpace(x.Name));
                case CardsCondition.DoNotHaveAnyValue:
                    return cards.Where(x => string.IsNullOrWhiteSpace(x.Name));
                case CardsCondition.Contains:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => x.Name.Contains(y)));
                    }

                    return cards.Where(x => x.Name.Contains(entry.ValueAsString));

                case CardsCondition.DoNotContains:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => !x.Name.Contains(y)));
                    }

                    return cards.Where(x => !x.Name.Contains(entry.ValueAsString));

                case CardsCondition.AnyOfThese:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => y.Contains(x.Name)));
                    }

                    return cards.Where(x => entry.ValueAsString.Equals(x.Name, StringComparison.CurrentCultureIgnoreCase));

                case CardsCondition.NoneOfThese:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => !entry.ValueAsStrings.Contains(x.Name, StringComparer.InvariantCultureIgnoreCase));
                    }

                    return cards.Where(x => !entry.ValueAsString.Equals(x.Name, StringComparison.CurrentCultureIgnoreCase));

                case CardsCondition.RegEx:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => Regex.IsMatch(x.Name, y, RegexOptions.IgnoreCase)));
                    }

                    return cards.Where(x => Regex.IsMatch(x.Name, entry.ValueAsString, RegexOptions.IgnoreCase));

                case CardsCondition.StartsWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => x.Name.StartsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => x.Name.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));

                case CardsCondition.EndsWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => x.Name.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => x.Name.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));

                case CardsCondition.DoNotStartWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => !x.Name.StartsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => !x.Name.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));

                case CardsCondition.DoNotEndWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => !x.Name.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => !x.Name.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));

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
                        return cards.Where(x => entry.ValueAsStrings.Contains(x.Description, StringComparer.InvariantCultureIgnoreCase));
                    }
                    else if (entry.ValueAsString != null)
                    {
                        return cards.Where(x => entry.ValueAsString.Equals(x.Description, StringComparison.CurrentCultureIgnoreCase));
                    }
                    else if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.Description.Length == entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.NotEqual:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => !entry.ValueAsStrings.Contains(x.Description, StringComparer.InvariantCultureIgnoreCase));
                    }
                    else if (entry.ValueAsString != null)
                    {
                        return cards.Where(x => !entry.ValueAsString.Equals(x.Description, StringComparison.CurrentCultureIgnoreCase));
                    }
                    else if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.Description.Length != entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.GreaterThan:
                    //Assume this means length of Description
                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.Description.Length > entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.LessThan:
                    //Assume this means length of Description
                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.Description.Length < entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.GreaterThanOrEqual:
                    //Assume this means length of Description
                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.Description.Length >= entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.LessThanOrEqual:
                    //Assume this means length of Description
                    if (entry.ValueAsNumber.HasValue)
                    {
                        return cards.Where(x => x.Description.Length <= entry.ValueAsNumber);
                    }

                    break;
                case CardsCondition.HasAnyValue:
                    return cards.Where(x => !string.IsNullOrWhiteSpace(x.Description));
                case CardsCondition.DoNotHaveAnyValue:
                    return cards.Where(x => string.IsNullOrWhiteSpace(x.Description));
                case CardsCondition.Contains:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => x.Description.Contains(y)));
                    }

                    return cards.Where(x => x.Description.Contains(entry.ValueAsString));

                case CardsCondition.DoNotContains:
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => !x.Description.Contains(y)));
                    }

                    return cards.Where(x => !x.Description.Contains(entry.ValueAsString));

                case CardsCondition.AnyOfThese:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => y.Contains(x.Description)));
                    }

                    return cards.Where(x => entry.ValueAsString.Equals(x.Description, StringComparison.CurrentCultureIgnoreCase));

                case CardsCondition.AllOfThese:
                {
                    throw new TrelloApiException("AllOfThese on Description Filter does not make sense");
                }
                case CardsCondition.NoneOfThese:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => !entry.ValueAsStrings.Contains(x.Description, StringComparer.InvariantCultureIgnoreCase));
                    }

                    return cards.Where(x => !entry.ValueAsString.Equals(x.Description, StringComparison.CurrentCultureIgnoreCase));

                case CardsCondition.RegEx:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => Regex.IsMatch(x.Description, y, RegexOptions.IgnoreCase)));
                    }

                    return cards.Where(x => Regex.IsMatch(x.Description, entry.ValueAsString, RegexOptions.IgnoreCase));

                case CardsCondition.StartsWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => x.Description.StartsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => x.Description.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));

                case CardsCondition.EndsWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => x.Description.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => x.Description.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));

                case CardsCondition.DoNotStartWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.Any(y => !x.Description.StartsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => !x.Description.StartsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));

                case CardsCondition.DoNotEndWith:
                    // ReSharper disable once ConvertIfStatementToConditionalTernaryExpression
                    if (entry.ValueAsStrings != null && entry.ValueAsStrings.Count != 0)
                    {
                        return cards.Where(x => entry.ValueAsStrings.All(y => !x.Description.EndsWith(y, StringComparison.InvariantCultureIgnoreCase)));
                    }

                    return cards.Where(x => !x.Description.EndsWith(entry.ValueAsString, StringComparison.CurrentCultureIgnoreCase));

                default:
                    throw new TrelloApiException($"Condition '{entry.Condition}' does not make sense to apply to a Description Condition");
            }

            return cards;
        }
    }
}