using System.Collections.Generic;
using System;
using System.Linq;

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

        /// <summary>
        /// Create a Condition for a string-based Custom Field
        /// </summary>
        /// <param name="customField">The custom field to check</param>
        /// <param name="condition">The condition</param>
        /// <param name="strings">The string or strings to use in the condition</param>
        /// <returns>The Condition</returns>
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

        /// <summary>
        /// Create a Condition for a number-based Custom Field
        /// </summary>
        /// <param name="customField">The custom field to check</param>
        /// <param name="condition">The condition</param>
        /// <param name="numbers">The number, or numbers to use in the condition</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition CustomField(CustomField customField, CardsConditionNumber condition, params int[] numbers)
        {
            return CustomField(customField, condition, numbers.Select(Convert.ToDecimal).ToArray());
        }

        /// <summary>
        /// Create a Condition for a number-based Custom Field
        /// </summary>
        /// <param name="customField">The custom field to check</param>
        /// <param name="condition">The condition</param>
        /// <param name="numbers">The number, or numbers to use in the condition</param>
        /// <returns>The Condition</returns>
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

        /// <summary>
        /// Create a Condition for a date-based Custom Field
        /// </summary>
        /// <param name="customField">The custom field to check</param>
        /// <param name="condition">The condition</param>
        /// <param name="dateTimeOffsets">The DateTime, or DateTimes to use in the condition</param>
        /// <returns>The Condition</returns>
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

        /// <summary>
        /// Create a Condition for a boolean-based Custom Field (aka a Checkbox)
        /// </summary>
        /// <param name="customField">The custom field to check</param>
        /// <param name="condition">The condition</param>
        /// <param name="boolean">The Boolean to use in the condition</param>
        /// <returns>The Condition</returns>
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

        /// <summary>
        /// Create a condition to only return cards that are complete (DueComplete == true)
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition IsComplete()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.DueComplete,
                Condition = CardsCondition.Equal,
                ValueAsBoolean = true,
            };
        }

        /// <summary>
        /// Create a condition to only return cards that are not complete (DueComplete == false)
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition IsNotComplete()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.DueComplete,
                Condition = CardsCondition.Equal,
                ValueAsBoolean = false,
            };
        }

        /// <summary>
        /// Create a condition to only return cards that have at least 1 Label (not a specific one)
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition HasAnyLabel()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.LabelId,
                Condition = CardsCondition.HasAnyValue
            };
        }

        /// <summary>
        /// Create a condition to only return cards that have no labels at all assigned to them
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition HasNoLabels()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.LabelId,
                Condition = CardsCondition.DoNotHaveAnyValue
            };
        }

        /// <summary>
        /// Create a listId based condition (example only cards on a specific list)
        /// </summary>
        /// <param name="condition">The condition the listIds values should meet</param>
        /// <param name="listIds">One or more listIds that should bee the condition</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition ListId(CardsConditionIds condition, params string[] listIds)
        {
            return AdvancedStringCondition(CardsConditionField.ListId, (CardsConditionString)Convert.ToInt32(condition), listIds);
        }


        /// <summary>
        /// Create a listName based condition (example only cards on a specific list)
        /// </summary>
        /// <param name="condition">The condition the listNames values should meet</param>
        /// <param name="listNames">One or more listNames that should bee the condition</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition ListName(CardsConditionString condition, params string[] listNames)
        {
            return AdvancedStringCondition(CardsConditionField.ListName, condition, listNames);
        }

        /// <summary>
        /// Create a card-named based condition (example only cards whose names contains a specific word)
        /// </summary>
        /// <param name="condition">The condition that should apply to the name</param>
        /// <param name="textValues">One or more Texts the condition should adhere to</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition Name(CardsConditionString condition, params string[] textValues)
        {
            return AdvancedStringCondition(CardsConditionField.Name, condition, textValues);
        }

        /// <summary>
        /// Create a condition to only return cards that do not have a description
        /// </summary>
        /// <returns>The condition</returns>
        public static CardsFilterCondition HasNoDescription()
        {
            return AdvancedNumberCondition(CardsConditionField.Description, CardsConditionNumber.Equal, 0);
        }

        /// <summary>
        /// Create a condition to only return cards that do have a description
        /// </summary>
        /// <returns>The condition</returns>
        public static CardsFilterCondition HasDescription()
        {
            return AdvancedNumberCondition(CardsConditionField.Description, CardsConditionNumber.NotEqual, 0);
        }

        /// <summary>
        /// Create a description-named based condition (example only cards whose description start with a specific word)
        /// </summary>
        /// <param name="condition">The condition that should apply to the description</param>
        /// <param name="textValues">One or more Texts the condition should adhere to</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition Description(CardsConditionString condition, params string[] textValues)
        {
            return AdvancedStringCondition(CardsConditionField.Name, condition, textValues);
        }

        /// <summary>
        /// Create a label-based condition  (example only cards that have 2 specific labelIds)
        /// </summary>
        /// <param name="condition">The condition for the labelIds</param>
        /// <param name="labelIds">One of more labelIds that the condition should adhere to</param>
        /// <returns>The condition</returns>
        public static CardsFilterCondition LabelId(CardsConditionIds condition, params string[] labelIds)
        {
            return AdvancedStringCondition(CardsConditionField.LabelId, (CardsConditionString)Convert.ToInt32(condition), labelIds);
        }

        /// <summary>
        /// Create a label-based condition (example only cards that have 2 specific labelNames)
        /// </summary>
        /// <param name="condition">The condition for the labelNames</param>
        /// <param name="labelNames">One of more labelNames that the condition should adhere to</param>
        /// <returns>The condition</returns>
        public static CardsFilterCondition LabelName(CardsConditionString condition, params string[] labelNames)
        {
            return AdvancedStringCondition(CardsConditionField.LabelName, condition, labelNames);
        }

        /// <summary>
        /// Create a condition based on how many labels are on the cards (example only return cards that have over 3 labels on them)
        /// </summary>
        /// <param name="condition">The condition the label-count should meet</param>
        /// <param name="value">The count that the condition should adhere to</param>
        /// <returns>The condition</returns>
        public static CardsFilterCondition LabelCount(CardsConditionCount condition, int value)
        {
            return AdvancedNumberCondition(CardsConditionField.LabelId, (CardsConditionNumber)Convert.ToInt32(condition), value);
        }

        /// <summary>
        /// Create a condition based on how many cards have a label count in a certain range (Example the all cards that have between 1 and 3 labels)
        /// </summary>
        /// <param name="min">The minimum count</param>
        /// <param name="max">The maximum count</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition LabelCountBetween(int min, int max)
        {
            var decimals = new List<decimal>() { min, max };
            return AdvancedNumberCondition(CardsConditionField.LabelId, CardsConditionNumber.Between, decimals.ToArray());
        }

        /// <summary>
        /// Create a condition based on how many cards have a label count outside a certain range (Example the all cards that have below 2 or above 5 (ignoring cards with 2,3,4 and 5 labels))
        /// </summary>
        /// <param name="mustBeBelow">The lower bound (excluding the indicated number)</param>
        /// <param name="mustBeAbove">The upper bound (excluding the indicated number)</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition LabelCountNotBetween(int mustBeBelow, int mustBeAbove)
        {
            var decimals = new List<decimal>() { mustBeBelow, mustBeAbove };
            return AdvancedNumberCondition(CardsConditionField.LabelId, CardsConditionNumber.NotBetween, decimals.ToArray());
        }

        /// <summary>
        /// Create a condition to only return cards that have at least 1 Member (not a specific one)
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition HasAnyMember()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.MemberId,
                Condition = CardsCondition.HasAnyValue
            };
        }

        /// <summary>
        /// Create a condition to only return cards that have no members at all assigned to them
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition HasNoMembers()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.MemberId,
                Condition = CardsCondition.DoNotHaveAnyValue
            };
        }

        /// <summary>
        /// Create a member-based condition  (example only cards that have 2 specific memberIds)
        /// </summary>
        /// <param name="condition">The condition for the memberIds</param>
        /// <param name="memberIds">One of more memberIds that the condition should adhere to</param>
        /// <returns>The condition</returns>
        public static CardsFilterCondition MemberId(CardsConditionIds condition, params string[] memberIds)
        {
            return AdvancedStringCondition(CardsConditionField.MemberId, (CardsConditionString)Convert.ToInt32(condition), memberIds);
        }

        /// <summary>
        /// Create a member-based condition  (example only cards that have 2 specific memberNames)
        /// </summary>
        /// <param name="condition">The condition for the memberNames</param>
        /// <param name="memberNames">One of more memberNames that the condition should adhere to</param>
        /// <returns>The condition</returns>
        public static CardsFilterCondition MemberName(CardsConditionString condition, params string[] memberNames)
        {
            return AdvancedStringCondition(CardsConditionField.MemberName, condition, memberNames);
        }

        /// <summary>
        /// Create a condition based on how many members are on the cards (example only return cards that have over 2 members on them)
        /// </summary>
        /// <param name="condition">The condition the member-count should meet</param>
        /// <param name="value">The count that the condition should adhere to</param>
        /// <returns>The condition</returns>
        public static CardsFilterCondition MemberCount(CardsConditionCount condition, int value)
        {
            return AdvancedNumberCondition(CardsConditionField.MemberId, (CardsConditionNumber)Convert.ToInt32(condition), value);
        }

        /// <summary>
        /// Create a condition based on how many cards have a member count in a certain range (Example the all cards that have between 1 and 2 members)
        /// </summary>
        /// <param name="min">The minimum count</param>
        /// <param name="max">The maximum count</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition MemberCountBetween(int min, int max)
        {
            var decimals = new List<decimal>() { min, max };
            return AdvancedNumberCondition(CardsConditionField.MemberId, CardsConditionNumber.Between, decimals.ToArray());
        }

        /// <summary>
        /// Create a condition based on how many cards have a member count outside a certain range (Example the all cards that have below 3 or above 4 (ignoring cards with 3 and 4 members))
        /// </summary>
        /// <param name="mustBeBelow">The lower bound (excluding the indicated number)</param>
        /// <param name="mustBeAbove">The upper bound (excluding the indicated number)</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition MemberCountNotBetween(int mustBeBelow, int mustBeAbove)
        {
            var decimals = new List<decimal>() { mustBeBelow, mustBeAbove };
            return AdvancedNumberCondition(CardsConditionField.MemberId, CardsConditionNumber.Between, decimals.ToArray());
        }

        /// <summary>
        /// Create a Due-date based condition
        /// </summary>
        /// <param name="condition">The condition the Due date should have</param>
        /// <param name="includeCardsThatAreMarkedAsComplete">Indicate if cards with due dates but are marked as completed should be included or not</param>
        /// <param name="dateTimeOffset">One of more dates the conditions should adhere to</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition Due(CardsConditionDate condition, bool includeCardsThatAreMarkedAsComplete, params DateTimeOffset[] dateTimeOffset)
        {
            return AdvancedDateTimeOffsetCondition(includeCardsThatAreMarkedAsComplete ? CardsConditionField.Due : CardsConditionField.DueWithNoDueComplete, condition, dateTimeOffset);
        }

        /// <summary>
        /// Create a Condition where Due Date should be inside a certain date range
        /// </summary>
        /// <param name="includeCardsThatAreMarkedAsComplete">Indicate if cards with due dates but are marked as completed should be included or not</param>
        /// <param name="from">From Date</param>
        /// <param name="to">To Date</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition DueBetween(bool includeCardsThatAreMarkedAsComplete, DateTimeOffset from, DateTimeOffset to)
        {
            return AdvancedDateTimeOffsetCondition(includeCardsThatAreMarkedAsComplete ? CardsConditionField.Due : CardsConditionField.DueWithNoDueComplete, CardsConditionDate.Between, from, to);
        }

        /// <summary>
        /// Create a Condition where Due Date should be outside a certain date range
        /// </summary>
        /// <param name="includeCardsThatAreMarkedAsComplete">Indicate if cards with due dates but are marked as completed should be included or not</param>
        /// <param name="mustBeBefore">Lower bound or the excluded range</param>
        /// <param name="mustBeAfter">Upper bound of the excluded range</param>
        /// <returns>THe Condition</returns>
        public static CardsFilterCondition DueNotBetween(bool includeCardsThatAreMarkedAsComplete, DateTimeOffset mustBeBefore, DateTimeOffset mustBeAfter)
        {
            return AdvancedDateTimeOffsetCondition(includeCardsThatAreMarkedAsComplete ? CardsConditionField.Due : CardsConditionField.DueWithNoDueComplete, CardsConditionDate.NotBetween, mustBeBefore, mustBeAfter);
        }

        /// <summary>
        /// Create a Condition where Start Date should be inside a certain date range
        /// </summary>
        /// <param name="from">From Date</param>
        /// <param name="to">To Date</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition StartBetween(DateTimeOffset from, DateTimeOffset to)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Start, CardsConditionDate.Between, from, to);
        }

        /// <summary>
        /// Create a Condition where Start Date should be outside a certain date range
        /// </summary>
        /// <param name="mustBeBefore">Lower bound or the excluded range</param>
        /// <param name="mustBeAfter">Upper bound of the excluded range</param>
        /// <returns>THe Condition</returns>
        public static CardsFilterCondition StartNotBetween(DateTimeOffset mustBeBefore, DateTimeOffset mustBeAfter)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Start, CardsConditionDate.NotBetween, mustBeBefore, mustBeAfter);
        }

        /// <summary>
        /// Create a Condition where Created Date should be inside a certain date range
        /// </summary>
        /// <param name="from">From Date</param>
        /// <param name="to">To Date</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition CreatedBetween(DateTimeOffset from, DateTimeOffset to)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Created, CardsConditionDate.Between, from, to);
        }

        /// <summary>
        /// Create a Condition where Created Date should be outside a certain date range
        /// </summary>
        /// <param name="mustBeBefore">Lower bound or the excluded range</param>
        /// <param name="mustBeAfter">Upper bound of the excluded range</param>
        /// <returns>THe Condition</returns>
        public static CardsFilterCondition CreatedNotBetween(DateTimeOffset mustBeBefore, DateTimeOffset mustBeAfter)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Created, CardsConditionDate.NotBetween, mustBeBefore, mustBeAfter);
        }

        /// <summary>
        /// Create a condition that only return cards that have due dates past UTC Now (and are not marked as completed)
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition Overdue()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.DueWithNoDueComplete,
                Condition = CardsCondition.LessThanOrEqual,
                ValueAsDateTimeOffset = DateTimeOffset.UtcNow
            };
        }

        /// <summary>
        /// Create a condition that only return cards that are due Today (Due between start and end of today and are not marked as completed)
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition DueToday()
        {
            DateTime startOfToday = DateTimeOffset.UtcNow.Date;
            return new CardsFilterCondition
            {
                Field = CardsConditionField.DueWithNoDueComplete,
                Condition = CardsCondition.Between,
                ValueAsDateTimeOffsets = new List<DateTimeOffset> { startOfToday, startOfToday.AddDays(1).AddSeconds(-1) }
            };
        }

        /// <summary>
        /// Create a condition that only return cards that have a due date that is past UTC Now (and are not marked as completed)
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition NotOverdue()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.DueWithNoDueComplete,
                Condition = CardsCondition.GreaterThan,
                ValueAsDateTimeOffset = DateTimeOffset.UtcNow
            };
        }

        /// <summary>
        /// Create a Condition that only return cards that have a Start date that is past UTC Now
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition Started()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Start,
                Condition = CardsCondition.LessThanOrEqual,
                ValueAsDateTimeOffset = DateTimeOffset.UtcNow
            };
        }

        /// <summary>
        /// Create a Condition that only return cards that have a start date that are after UTC Now
        /// </summary>
        /// <returns>The condition</returns>
        public static CardsFilterCondition NotStarted()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Start,
                Condition = CardsCondition.GreaterThan,
                ValueAsDateTimeOffset = DateTimeOffset.UtcNow
            };
        }

        /// <summary>
        /// Create a condition to only return cards that have a due date assigned to them
        /// </summary>
        /// <param name="includeCardsThatAreMarkedAsComplete">Indicate if cards with due dates but are marked as completed should be included or not</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition HasDueDate(bool includeCardsThatAreMarkedAsComplete)
        {
            return new CardsFilterCondition
            {
                Field = includeCardsThatAreMarkedAsComplete ? CardsConditionField.Due : CardsConditionField.DueWithNoDueComplete,
                Condition = CardsCondition.HasAnyValue
            };
        }

        /// <summary>
        /// Create a condition to only return cards that have no due dates assigned to them
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition HasNoDueDate()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Due,
                Condition = CardsCondition.DoNotHaveAnyValue
            };
        }

        /// <summary>
        /// Create a condition to only return cards that have a start dates assigned to them
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition HasStartDate()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Start,
                Condition = CardsCondition.HasAnyValue
            };
        }

        /// <summary>
        /// Create a condition to only return cards that have no start dates assigned to them
        /// </summary>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition HasNoStartDate()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Start,
                Condition = CardsCondition.DoNotHaveAnyValue
            };
        }

        /// <summary>
        /// Create a Start-date based condition
        /// </summary>
        /// <param name="condition">The condition the Start date should have</param>
        /// <param name="dateTimeOffset">One of more dates the conditions should adhere to</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition Start(CardsConditionDate condition, params DateTimeOffset[] dateTimeOffset)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Start, condition, dateTimeOffset);
        }

        /// <summary>
        /// Create a Created-date based condition
        /// </summary>
        /// <param name="condition">The condition the Created date should have</param>
        /// <param name="dateTimeOffset">One of more dates the conditions should adhere to</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition Created(CardsConditionDate condition, params DateTimeOffset[] dateTimeOffset)
        {
            return AdvancedDateTimeOffsetCondition(CardsConditionField.Created, condition, dateTimeOffset);
        }

        /// <summary>
        /// Create a generic string-based Condition
        /// </summary>
        /// <param name="field">Field to Check</param>
        /// <param name="condition">Condition to have</param>
        /// <param name="strings">One or more string the condition should adhere to</param>
        /// <returns>The Condition</returns>
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

        /// <summary>
        /// Create a generic number-based Condition
        /// </summary>
        /// <param name="field">Field to Check</param>
        /// <param name="condition">Condition to have</param>
        /// <param name="numbers">One or more numbers the condition should adhere to</param>
        /// <returns>The Condition</returns>
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

        /// <summary>
        /// Create a generic date-based Condition
        /// </summary>
        /// <param name="field">Field to Check</param>
        /// <param name="condition">Condition to have</param>
        /// <param name="datetimeOffsets">One or more dates the condition should adhere to</param>
        /// <returns>The Condition</returns>
        public static CardsFilterCondition AdvancedDateTimeOffsetCondition(CardsConditionField field, CardsConditionDate condition, params DateTimeOffset[] datetimeOffsets)
        {
            List<DateTimeOffset> values = null;
            DateTimeOffset? value = null;
            if (datetimeOffsets.Length == 1)
            {
                value = datetimeOffsets[0];
            }
            else
            {
                values = datetimeOffsets.ToList();
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