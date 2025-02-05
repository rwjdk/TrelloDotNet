using System.Collections.Generic;
using System;
using System.Linq;
using System.Runtime.InteropServices;

namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a condition 
    /// </summary>
    public class CardsFilterCondition
    {
        private CardsFilterCondition()
        {
            //Empty
        }

        internal CardsConditionField Field { get; set; }
        internal CustomField CustomFieldEntry { get; set; }
        internal CardsCondition Condition { get; set; }

        internal DateTimeOffset? ValueAsDateTimeOffset { get; set; }
        internal List<DateTimeOffset> ValueAsDateTimeOffsets { get; set; }

        internal string ValueAsString { get; set; }
        internal List<string> ValueAsStrings { get; set; }

        internal decimal? ValueAsNumber { get; set; }
        internal List<decimal> ValueAsNumbers { get; set; }
        internal bool ValueAsBoolean { get; set; }

        public static CardsFilterCondition CustomField(CustomField customField, CardsConditionString condition, params string[] strings)
        {
            List<string> values = null;
            string value = null;
            if (strings.Length == 1)
            {
                value = strings[0];
            }
            else
            {
                values = strings.ToList();
            }

            return new CardsFilterCondition
            {
                Field = CardsConditionField.CustomField,
                Condition = (CardsCondition)Convert.ToInt32(condition),
                ValueAsString = value,
                ValueAsStrings = values,
                CustomFieldEntry = customField,
            };
        }

        public static CardsFilterCondition CustomField(CustomField customField, CardsConditionNumber condition, params int[] numbers)
        {
            return CustomField(customField, condition, numbers.Select(Convert.ToDecimal).ToArray());
        }

        public static CardsFilterCondition CustomField(CustomField customField, CardsConditionNumber condition, params decimal[] numbers)
        {
            List<decimal> values = null;
            decimal? value = null;
            if (numbers.Length == 1)
            {
                value = numbers[0];
            }
            else
            {
                values = numbers.ToList();
            }

            return new CardsFilterCondition
            {
                Field = CardsConditionField.CustomField,
                Condition = (CardsCondition)Convert.ToInt32(condition),
                ValueAsNumber = value,
                ValueAsNumbers = values,
                CustomFieldEntry = customField,
            };
        }

        public static CardsFilterCondition CustomField(CustomField customField, CardsConditionDate condition, params DateTimeOffset[] dateTimeOffsets)
        {
            List<DateTimeOffset> values = null;
            DateTimeOffset? value = null;
            if (dateTimeOffsets.Length == 1)
            {
                value = dateTimeOffsets[0];
            }
            else
            {
                values = dateTimeOffsets.ToList();
            }

            return new CardsFilterCondition
            {
                Field = CardsConditionField.CustomField,
                Condition = (CardsCondition)Convert.ToInt32(condition),
                ValueAsDateTimeOffset = value,
                ValueAsDateTimeOffsets = values,
                CustomFieldEntry = customField,
            };
        }

        public static CardsFilterCondition CustomField(CustomField customField, CardsConditionBoolean condition, bool boolean)
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.CustomField,
                Condition = (CardsCondition)Convert.ToInt32(condition),
                ValueAsBoolean = boolean,
                CustomFieldEntry = customField,
            };
        }

        public static CardsFilterCondition IsComplete()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.CustomField,
                Condition = CardsCondition.Equal,
                ValueAsBoolean = true,
            };
        }

        public static CardsFilterCondition IsNotComplete()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.CustomField,
                Condition = CardsCondition.NotEqual,
                ValueAsBoolean = true,
            };
        }

        public static CardsFilterCondition HasAnyLabel()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Label,
                Condition = CardsCondition.HasAnyValue
            };
        }

        public static CardsFilterCondition HasNoLabels()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Label,
                Condition = CardsCondition.DoNotHaveAnyValue
            };
        }

        public static CardsFilterCondition ListId(CardsConditionId condition, params string[] listIds)
        {
            return AdvancedStringCondition(CardsConditionField.List, (CardsConditionString)Convert.ToInt32(condition), listIds);
        }

        public static CardsFilterCondition Name(CardsConditionString condition, params string[] textValues)
        {
            return AdvancedStringCondition(CardsConditionField.Name, condition, textValues);
        }

        public static CardsFilterCondition HasNoDescription()
        {
            return AdvancedNumberCondition(CardsConditionField.Description, CardsConditionNumber.Equal, 0);
        }

        public static CardsFilterCondition HasDescription()
        {
            return AdvancedNumberCondition(CardsConditionField.Description, CardsConditionNumber.NotEqual, 0);
        }

        public static CardsFilterCondition Description(CardsConditionString condition, params string[] textValues)
        {
            return AdvancedStringCondition(CardsConditionField.Name, condition, textValues);
        }

        public static CardsFilterCondition LabelId(CardsConditionId condition, params string[] labelIds)
        {
            return AdvancedStringCondition(CardsConditionField.Label, (CardsConditionString)Convert.ToInt32(condition), labelIds);
        }

        public static CardsFilterCondition LabelCount(CardsConditionCount condition, int value)
        {
            return AdvancedNumberCondition(CardsConditionField.Label, (CardsConditionNumber)Convert.ToInt32(condition), value);
        }

        public static CardsFilterCondition LabelCountBetween(int min, int max)
        {
            var decimals = new List<decimal>() { min, max };
            return AdvancedNumberCondition(CardsConditionField.Label, CardsConditionNumber.Between, decimals.ToArray());
        }

        public static CardsFilterCondition LabelCountNotBetween(int mustBeBelow, int mustBeAbove)
        {
            var decimals = new List<decimal>() { mustBeBelow, mustBeAbove };
            return AdvancedNumberCondition(CardsConditionField.Label, CardsConditionNumber.NotBetween, decimals.ToArray());
        }

        public static CardsFilterCondition HasAnyMember()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Member,
                Condition = CardsCondition.HasAnyValue
            };
        }

        public static CardsFilterCondition HasNoMembers()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Member,
                Condition = CardsCondition.DoNotHaveAnyValue
            };
        }

        public static CardsFilterCondition MemberId(CardsConditionId condition, params string[] memberIds)
        {
            return AdvancedStringCondition(CardsConditionField.Member, (CardsConditionString)Convert.ToInt32(condition), memberIds);
        }

        public static CardsFilterCondition MemberCount(CardsConditionCount condition, int value)
        {
            return AdvancedNumberCondition(CardsConditionField.Member, (CardsConditionNumber)Convert.ToInt32(condition), value);
        }

        public static CardsFilterCondition MemberCountBetween(int min, int max)
        {
            var decimals = new List<decimal>() { min, max };
            return AdvancedNumberCondition(CardsConditionField.Member, CardsConditionNumber.Between, decimals.ToArray());
        }

        public static CardsFilterCondition MemberCountNotBetween(int mustBeBelow, int mustBeAbove)
        {
            var decimals = new List<decimal>() { mustBeBelow, mustBeAbove };
            return AdvancedNumberCondition(CardsConditionField.Member, CardsConditionNumber.Between, decimals.ToArray());
        }

        public static CardsFilterCondition Due(CardsConditionDate condition, bool includeCardsThatAreMarkedAsComplete, params DateTimeOffset[] dateTimeOffset)
        {
            return AdvancedDateTimeOffsetCondition(includeCardsThatAreMarkedAsComplete ? CardsConditionField.Due : CardsConditionField.DueWithNoDueComplete, condition, dateTimeOffset);
        }

        public static CardsFilterCondition DueBetween(bool includeCardsThatAreMarkedAsComplete, DateTimeOffset from, DateTimeOffset to)
        {
            return AdvancedDateTimeOffsetCondition(includeCardsThatAreMarkedAsComplete ? CardsConditionField.Due : CardsConditionField.DueWithNoDueComplete, CardsConditionDate.Between, from, to);
        }

        public static CardsFilterCondition DueNotBetween(bool includeCardsThatAreMarkedAsComplete, DateTimeOffset mustBeBefore, DateTimeOffset mustBeAfter)
        {
            return AdvancedDateTimeOffsetCondition(includeCardsThatAreMarkedAsComplete ? CardsConditionField.Due : CardsConditionField.DueWithNoDueComplete, CardsConditionDate.NotBetween, mustBeBefore, mustBeAfter);
        }

        public static CardsFilterCondition StartBetween(DateTimeOffset from, DateTimeOffset to)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Start, CardsConditionDate.Between, from, to);
        }

        public static CardsFilterCondition StartNotBetween(DateTimeOffset mustBefore, DateTimeOffset mustBeAfter)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Start, CardsConditionDate.NotBetween, mustBefore, mustBeAfter);
        }

        public static CardsFilterCondition CreatedBetween(DateTimeOffset from, DateTimeOffset to)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Created, CardsConditionDate.Between, from, to);
        }

        public static CardsFilterCondition CreatedNotBetween(DateTimeOffset mustBefore, DateTimeOffset mustBeAfter)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Created, CardsConditionDate.NotBetween, mustBefore, mustBeAfter);
        }

        public static CardsFilterCondition Overdue()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.DueWithNoDueComplete,
                Condition = CardsCondition.LessThanOrEqual,
                ValueAsDateTimeOffset = DateTimeOffset.UtcNow
            };
        }

        public static CardsFilterCondition NotOverdue()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.DueWithNoDueComplete,
                Condition = CardsCondition.GreaterThan,
                ValueAsDateTimeOffset = DateTimeOffset.UtcNow
            };
        }

        public static CardsFilterCondition Started()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Start,
                Condition = CardsCondition.LessThanOrEqual,
                ValueAsDateTimeOffset = DateTimeOffset.UtcNow
            };
        }

        public static CardsFilterCondition NotStarted()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Start,
                Condition = CardsCondition.GreaterThan,
                ValueAsDateTimeOffset = DateTimeOffset.UtcNow
            };
        }

        public static CardsFilterCondition HasDueDate(bool includeCardsThatAreMarkedAsComplete)
        {
            return new CardsFilterCondition
            {
                Field = includeCardsThatAreMarkedAsComplete ? CardsConditionField.Due : CardsConditionField.DueWithNoDueComplete,
                Condition = CardsCondition.HasAnyValue
            };
        }

        public static CardsFilterCondition HasNoDueDate()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Due,
                Condition = CardsCondition.DoNotHaveAnyValue
            };
        }

        public static CardsFilterCondition HasStartDate()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Start,
                Condition = CardsCondition.HasAnyValue
            };
        }

        public static CardsFilterCondition HasNoStartDate()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Start,
                Condition = CardsCondition.DoNotHaveAnyValue
            };
        }

        public static CardsFilterCondition Start(CardsConditionDate condition, params DateTimeOffset[] dateTimeOffset)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Start, condition, dateTimeOffset);
        }

        public static CardsFilterCondition Created(CardsConditionDate condition, params DateTimeOffset[] dateTimeOffset)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Created, condition, dateTimeOffset);
        }

        public static CardsFilterCondition AdvancedStringCondition(CardsConditionField field, CardsConditionString condition, params string[] strings)
        {
            List<string> values = null;
            string value = null;
            if (strings.Length == 1)
            {
                value = strings[0];
            }
            else
            {
                values = strings.ToList();
            }

            return new CardsFilterCondition
            {
                Field = field,
                Condition = (CardsCondition)Convert.ToInt32(condition),
                ValueAsStrings = values,
                ValueAsString = value
            };
        }

        public static CardsFilterCondition AdvancedNumberCondition(CardsConditionField field, CardsConditionNumber condition, params decimal[] numbers)
        {
            List<decimal> values = null;
            decimal? value = null;
            if (numbers.Length == 1)
            {
                value = numbers[0];
            }
            else
            {
                values = numbers.ToList();
            }

            return new CardsFilterCondition
            {
                Field = field,
                Condition = (CardsCondition)Convert.ToInt32(condition),
                ValueAsNumbers = values,
                ValueAsNumber = value
            };
        }

        public static CardsFilterCondition AdvancedDateTimeOffsetCondition(CardsConditionField field, CardsConditionDate condition, params DateTimeOffset[] datetimes)
        {
            List<DateTimeOffset> values = null;
            DateTimeOffset? value = null;
            if (datetimes.Length == 1)
            {
                value = datetimes[0];
            }
            else
            {
                values = datetimes.ToList();
            }

            return new CardsFilterCondition
            {
                Field = field,
                Condition = (CardsCondition)Convert.ToInt32(condition),
                ValueAsDateTimeOffsets = values,
                ValueAsDateTimeOffset = value
            };
        }
    }
}