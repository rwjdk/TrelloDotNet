using System.Collections.Generic;
using System;
using System.Linq;

namespace TrelloDotNet.Model
{
    public class CardsFilterCondition
    {
        private CardsFilterCondition()
        {
            //Empty
        }

        internal CardsConditionField Field { get; set; }
        internal string CustomFieldId { get; set; }
        internal CardsCondition Condition { get; set; }

        internal DateTimeOffset? ValueAsDateTimeOffset { get; set; }
        internal List<DateTimeOffset> ValueAsDateTimeOffsets { get; set; }

        internal string ValueAsString { get; set; }
        internal List<string> ValueAsStrings { get; set; }

        internal int? ValueAsInteger { get; set; }
        internal List<int> ValueAsIntegers { get; set; }

        internal decimal? ValueAsDecimal { get; set; }
        internal List<decimal> ValueAsDecimals { get; set; }
        internal bool ValueAsBoolean { get; set; }

        public static CardsFilterCondition HasAnyLabel()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Label,
                Condition = CardsCondition.HasAnyValue
            };
        }

        public static CardsFilterCondition HasNoLabel()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Label,
                Condition = CardsCondition.DoNotHaveAnyValue
            };
        }

        public static CardsFilterCondition ListId(CardsConditionId condition, params string[] listIds)
        {
            return AdvancedStringCondition(CardsConditionField.List, (CardsCondition)Convert.ToInt32(condition), listIds);
        }

        public static CardsFilterCondition Name(CardsConditionText condition, params string[] textValues)
        {
            return AdvancedStringCondition(CardsConditionField.Name, (CardsCondition)Convert.ToInt32(condition), textValues);
        }

        public static CardsFilterCondition NameLenght(CardsConditionCount condition, int value)
        {
            return AdvancedIntegerCondition(CardsConditionField.Name, (CardsCondition)Convert.ToInt32(condition), value);
        }

        public static CardsFilterCondition DescriptionLenght(CardsConditionCount condition, int value)
        {
            return AdvancedIntegerCondition(CardsConditionField.Description, (CardsCondition)Convert.ToInt32(condition), value);
        }

        public static CardsFilterCondition Description(CardsConditionText condition, params string[] textValues)
        {
            return AdvancedStringCondition(CardsConditionField.Name, (CardsCondition)Convert.ToInt32(condition), textValues);
        }

        public static CardsFilterCondition LabelId(CardsConditionId condition, params string[] labelIds)
        {
            return AdvancedStringCondition(CardsConditionField.Label, (CardsCondition)Convert.ToInt32(condition), labelIds);
        }

        public static CardsFilterCondition LabelCount(CardsConditionCount condition, int value)
        {
            return AdvancedIntegerCondition(CardsConditionField.Label, (CardsCondition)Convert.ToInt32(condition), value);
        }

        public static CardsFilterCondition HasAnyMember()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Member,
                Condition = CardsCondition.HasAnyValue
            };
        }

        public static CardsFilterCondition HasNoMember()
        {
            return new CardsFilterCondition
            {
                Field = CardsConditionField.Member,
                Condition = CardsCondition.DoNotHaveAnyValue
            };
        }

        public static CardsFilterCondition MemberId(CardsConditionId condition, params string[] memberIds)
        {
            return AdvancedStringCondition(CardsConditionField.Member, (CardsCondition)Convert.ToInt32(condition), memberIds);
        }

        public static CardsFilterCondition MemberCount(CardsConditionCount condition, int value)
        {
            return AdvancedIntegerCondition(CardsConditionField.Member, (CardsCondition)Convert.ToInt32(condition), value);
        }

        public static CardsFilterCondition AdvancedStringCondition(CardsConditionField field, CardsCondition condition, params string[] strings)
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
                Condition = condition,
                ValueAsStrings = values,
                ValueAsString = value
            };
        }

        public static CardsFilterCondition AdvancedIntegerCondition(CardsConditionField field, CardsCondition condition, params int[] integers)
        {
            List<int> values = null;
            int? value = null;
            if (integers.Length == 1)
            {
                value = integers[0];
            }
            else
            {
                values = integers.ToList();
            }

            return new CardsFilterCondition
            {
                Field = field,
                Condition = condition,
                ValueAsIntegers = values,
                ValueAsInteger = value
            };
        }
    }
}