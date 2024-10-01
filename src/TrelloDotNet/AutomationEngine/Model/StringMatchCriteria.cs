﻿namespace TrelloDotNet.AutomationEngine.Model
{
    /// <summary>
    /// A String Match Criteria
    /// </summary>
    public enum StringMatchCriteria
    {
        /// <summary>
        /// String should match Equal
        /// </summary>
        Equal = 1,
        /// <summary>
        /// String should start with the provided
        /// </summary>
        StartsWith = 2,
        /// <summary>
        /// String should end with the provided
        /// </summary>
        EndsWith = 3,
        /// <summary>
        /// String should contain the provided
        /// </summary>
        Contains = 4,
        /// <summary>
        /// String should match a specific RegEx
        /// </summary>
        RegEx = 5,
    }
}