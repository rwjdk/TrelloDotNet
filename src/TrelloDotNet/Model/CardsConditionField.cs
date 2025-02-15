﻿namespace TrelloDotNet.Model
{
    /// <summary>
    /// Represent a Field a condition refer to
    /// </summary>
    public enum CardsConditionField
    {
        /// <summary>
        /// The name (title) field of a card
        /// </summary>
        Name,

        /// <summary>
        /// The list (id) of a Card
        /// </summary>
        List,

        /// <summary>
        /// The Label(s) of the Card
        /// </summary>
        Label,

        /// <summary>
        /// The Member(s) of the Card
        /// </summary>
        Member,

        /// <summary>
        /// The Description of the Card
        /// </summary>
        Description,

        /// <summary>
        /// The Due Date of the Card (NOT taking into account if that DueDate is marked as complete or not)
        /// </summary>
        Due,

        /// <summary>
        /// The Due Date of the Card (where DueDates marked as complete are not considered to be due anymore)
        /// </summary>
        DueWithNoDueComplete,

        /// <summary>
        /// The Start Date of the Card
        /// </summary>
        Start,

        /// <summary>
        /// The Created Date of the Card
        /// </summary>
        Created,

        /// <summary>
        /// A Custom Field on the Card
        /// </summary>
        CustomField,

        /// <summary>
        /// The Completed Field of a Card
        /// </summary>
        DueComplete,
    }
}