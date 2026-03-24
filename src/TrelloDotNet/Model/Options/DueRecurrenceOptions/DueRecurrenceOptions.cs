using System;
using System.Collections.Generic;

namespace TrelloDotNet.Model.Options.DueRecurrenceOptions
{
    /// <summary>
    /// Options for Card Due Recurrence
    /// </summary>
    public class DueRecurrenceOptions
    {
        /// <summary>
        /// The recurrence of the Due Date
        /// </summary>
        public DueRecurrenceType Recurrence { get; set; }

        /// <summary>
        /// Optional first Due date. If not set the cards current due date will be used, or DateTimeOffset.UtcNow
        /// </summary>
        public DateTimeOffset? FirstDueDate { get; set; }

        /// <summary>
        /// Recur on Monday (only used for weekly schedules)
        /// </summary>
        public bool Monday { get; set; }

        /// <summary>
        /// Recur on Tuesday (only used for weekly schedules)
        /// </summary>
        public bool Tuesday { get; set; }

        /// <summary>
        /// Recur on Wednesday (only used for weekly schedules)
        /// </summary>
        public bool Wednesday { get; set; }

        /// <summary>
        /// Recur on Thursday (only used for weekly schedules)
        /// </summary>
        public bool Thursday { get; set; }

        /// <summary>
        /// Recur on Friday (only used for weekly schedules)
        /// </summary>
        public bool Friday { get; set; }

        /// <summary>
        /// Recur on Saturday (only used for weekly schedules)
        /// </summary>
        public bool Saturday { get; set; }

        /// <summary>
        /// Recur on Sunday (only used for weekly schedules)
        /// </summary>
        public bool Sunday { get; set; }

        /// <summary>
        /// Timezone to use: Example: 'Europe/Copenhagen' (only used of Xth day of week monthly schedules)
        /// </summary>
        public string TimezoneId { get; set; }

        /// <summary>
        /// The Xth position in a monthly days of week schedule
        /// </summary>
        public DueRecurrenceXthPosition XthPosition { get; set; }

        internal string GetDays()
        {
            if (Recurrence == DueRecurrenceType.Weekdays)
            {
                return "MO,TU,WE,TH,FR";
            }
            
            List<string> days = new List<string>();
            if (Sunday)
            {
                days.Add("SU");
            }
            if (Monday)
            {
                days.Add("MO");
            }
            if (Tuesday)
            {
                days.Add("TU");
            }
            if (Wednesday)
            {
                days.Add("WE");
            }
            if (Thursday)
            {
                days.Add("TH");
            }
            if (Friday)
            {
                days.Add("FR");
            }
            if (Saturday)
            {
                days.Add("SA");
            }

            return string.Join(",", days);
        }
    }
}