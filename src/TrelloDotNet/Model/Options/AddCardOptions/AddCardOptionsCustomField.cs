using System;

namespace TrelloDotNet.Model.Options.AddCardOptions
{
    /// <summary>
    /// Add Card Option for a Custom Field value
    /// </summary>
    public class AddCardOptionsCustomField
    {
        internal CustomField Field { get; }
        internal object Value { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="field">Custom Field to set</param>
        /// <param name="value">Value to set</param>
        public AddCardOptionsCustomField(CustomField field, string value)
        {
            if (field.Type != CustomFieldType.Text)
            {
                throw new TrelloApiException($"Incorrect value type for Custom Field '{field.Name}' as it is not a string based field", string.Empty);
            }

            Field = field;
            Value = value;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="field">Custom Field to set</param>
        /// <param name="value">Value to set</param>
        public AddCardOptionsCustomField(CustomField field, DateTimeOffset value)
        {
            if (field.Type != CustomFieldType.Date)
            {
                throw new TrelloApiException($"Incorrect value type for Custom Field '{field.Name}' as it is not a Date based field", string.Empty);
            }

            Field = field;
            Value = value;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="field">Custom Field to set</param>
        /// <param name="value">Value to set</param>
        public AddCardOptionsCustomField(CustomField field, bool value)
        {
            if (field.Type != CustomFieldType.Checkbox)
            {
                throw new TrelloApiException($"Incorrect value type for Custom Field '{field.Name}' as it is not a Boolean based field", string.Empty);
            }

            Field = field;
            Value = value;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="field">Custom Field to set</param>
        /// <param name="value">Value to set</param>
        public AddCardOptionsCustomField(CustomField field, decimal value)
        {
            if (field.Type != CustomFieldType.Number)
            {
                throw new TrelloApiException($"Incorrect value type for Custom Field '{field.Name}' as it is not a Number based field", string.Empty);
            }

            Field = field;
            Value = value;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="field">Custom Field to set</param>
        /// <param name="value">Value to set</param>
        public AddCardOptionsCustomField(CustomField field, int value)
        {
            if (field.Type != CustomFieldType.Number)
            {
                throw new TrelloApiException($"Incorrect value type for Custom Field '{field.Name}' as it is not a Number based field", string.Empty);
            }

            Field = field;
            Value = value;
        }


        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="field">Custom Field to set</param>
        /// <param name="value">Value to set</param>
        public AddCardOptionsCustomField(CustomField field, CustomFieldOption value)
        {
            if (field.Type != CustomFieldType.List)
            {
                throw new TrelloApiException($"Incorrect value type for Custom Field '{field.Name}' as it is not a Option based field", string.Empty);
            }

            Field = field;
            Value = value;
        }
    }
}