namespace TrelloDotNet.Model.Options.DueRecurrenceOptions
{
    /// <summary>
    /// 
    /// </summary>
    public enum DueRecurrenceType
    {
        /// <summary>
        /// Daily Recurrence
        /// </summary>
        Daily,
        /// <summary>
        /// Monday to Friday Recurrence
        /// </summary>
        Weekdays,
        /// <summary>
        /// Occur on the Specified days (Mon,Tue,Wed,Thur,Fri,Sat,Sun)
        /// </summary>
        Weekly,
        /// <summary>
        /// Occur on the Day of the Month of the Due Date Day
        /// </summary>
        DayOfMonth,
        /// <summary>
        /// Occur on the Xth Day of Week that the Due Date Day is. (Require you to set TimezoneId and XthPosition)
        /// </summary>
        XthDayOfWeekOfTheMonth
    }
}