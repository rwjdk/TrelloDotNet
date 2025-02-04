using System;
using System.Collections.Generic;

namespace TrelloDotNet.Model.Options.AddCardOptions
{
    /// <summary>
    /// Options for the card you wish to Add
    /// </summary>
    public class AddCardOptions
    {
        /// <summary>
        /// Id of the List to add the Card to (Mandatory)
        /// </summary>
        public string ListId { get; set; }

        /// <summary>
        /// Name of Card
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of Card
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Start Date of Card
        /// </summary>
        public DateTimeOffset? Start { get; set; }

        /// <summary>
        /// Due Date and Time of the Card
        /// </summary>
        public DateTimeOffset? Due { get; set; }

        /// <summary>
        /// Checklists to add to the Card
        /// </summary>
        public List<Checklist> Checklists { get; set; }

        /// <summary>
        /// Cover to add to the Card
        /// </summary>
        public CardCover Cover { get; set; }

        /// <summary>
        /// File Attachments to add to the Card
        /// </summary>
        public List<AttachmentFileUpload> AttachmentFileUploads { get; set; }

        /// <summary>
        /// URL Link Attachments to add to the Card
        /// </summary>
        public List<AttachmentUrlLink> AttachmentUrlLinks { get; set; }

        /// <summary>
        /// Ids of Labels to add to the Card
        /// </summary>
        public List<string> LabelIds { get; set; }

        /// <summary>
        /// Ids of Members to add to the Card
        /// </summary>
        public List<string> MemberIds { get; set; }

        /// <summary>
        /// Custom Field Values to set on the Card
        /// </summary>
        public List<AddCardOptionsCustomField> CustomFields { get; set; }


        /// <summary>
        /// Position 
        /// </summary>
        public decimal? Position { get; set; }

        /// <summary>
        /// Named Position
        /// </summary>
        public NamedPosition? NamedPosition { get; set; }

        /// <summary>
        /// If Card is Complete
        /// </summary>
        public bool DueComplete { get; set; }

        /// <summary>
        /// If the card should be a template
        /// </summary>
        public bool IsTemplate { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="listId">Id of the list to add the card to</param>
        /// <param name="name">Name of the Card</param>
        /// <param name="description">Description of the Card</param>
        public AddCardOptions(string listId, string name, string description = null) : this()
        {
            ListId = listId;
            Name = name;
            Description = description;
        }

        /// <summary>
        /// Constructor
        /// </summary>
        public AddCardOptions()
        {
            Checklists = new List<Checklist>();
            AttachmentUrlLinks = new List<AttachmentUrlLink>();
            AttachmentFileUploads = new List<AttachmentFileUpload>();
            CustomFields = new List<AddCardOptionsCustomField>();
            LabelIds = new List<string>();
            MemberIds = new List<string>();
            NamedPosition = Model.NamedPosition.Bottom;
        }
    }
}