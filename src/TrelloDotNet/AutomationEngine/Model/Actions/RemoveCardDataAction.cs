using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TrelloDotNet.AutomationEngine.Interface;
using TrelloDotNet.Control;
using TrelloDotNet.Model;
using TrelloDotNet.Model.Actions;
using TrelloDotNet.Model.Options;
using TrelloDotNet.Model.Webhook;

namespace TrelloDotNet.AutomationEngine.Model.Actions
{
    /// <summary>
    /// Action to remove one or more field values on a card
    /// </summary>
    public class RemoveCardDataAction : IAutomationAction
    {
        /// <summary>
        /// A list of data to remove
        /// </summary>
        public RemoveCardDataType[] DataToRemove { get; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="dataToRemove">A list of data to remove</param>
        public RemoveCardDataAction(params RemoveCardDataType[] dataToRemove)
        {
            DataToRemove = dataToRemove;
        }

        /// <summary>
        /// The method called when an automation should be performed
        /// </summary>
        /// <param name="webhookAction">The Webhook Action that led to the Execution. This object also have the TrelloClient and information about the event at your disposal</param>
        /// <param name="processingResult">An object you can use to report back to the user if the action was performed and details about it</param>
        /// <returns>Void</returns>
        public async Task PerformActionAsync(WebhookAction webhookAction, ProcessingResult processingResult)
        {
            if (webhookAction.Data?.Card == null)
            {
                throw new AutomationException("Could not perform RemoveCardDataAction as WebhookAction did not involve a Card");
            }

            var card = await webhookAction.Data.Card.GetAsync();
            var queryParametersToUpdate = new List<QueryParameter>();
            foreach (var dataType in DataToRemove)
            {
                switch (dataType)
                {
                    case RemoveCardDataType.StartDate:
                        if (card.Start != null)
                        {
                            card.Start = null;
                            queryParametersToUpdate.Add(new QueryParameter(CardFieldsType.Start.GetJsonPropertyName(), (DateTimeOffset?)null));
                        }

                        break;
                    case RemoveCardDataType.DueDate:
                        if (card.Due != null)
                        {
                            card.Due = null;
                            queryParametersToUpdate.Add(new QueryParameter(CardFieldsType.Due.GetJsonPropertyName(), (DateTimeOffset?)null));
                        }

                        break;
                    case RemoveCardDataType.DueComplete:
                        if (card.DueComplete)
                        {
                            card.DueComplete = false;
                            queryParametersToUpdate.Add(new QueryParameter(CardFieldsType.DueComplete.GetJsonPropertyName(), false));
                        }

                        break;
                    case RemoveCardDataType.Description:
                        if (!string.IsNullOrWhiteSpace(card.Description))
                        {
                            card.Description = null;
                            queryParametersToUpdate.Add(new QueryParameter(CardFieldsType.Description.GetJsonPropertyName(), string.Empty));
                        }

                        break;
                    case RemoveCardDataType.AllLabels:
                        if (card.LabelIds.Any())
                        {
                            card.LabelIds = new List<string>();
                            queryParametersToUpdate.Add(new QueryParameter(CardFieldsType.LabelIds.GetJsonPropertyName(), new List<string>()));
                        }

                        break;
                    case RemoveCardDataType.AllMembers:
                        if (card.MemberIds.Any())
                        {
                            card.MemberIds = new List<string>();
                            queryParametersToUpdate.Add(new QueryParameter(CardFieldsType.MemberIds.GetJsonPropertyName(), new List<string>()));
                        }

                        break;
                    case RemoveCardDataType.Cover:
                        if (card.Cover != null && (card.Cover.Color != null || card.Cover.BackgroundImageId != null))
                        {
                            card.Cover = null;
                            queryParametersToUpdate.Add(new QueryParameter(CardFieldsType.Cover.GetJsonPropertyName(), (string)null));
                        }

                        break;
                    case RemoveCardDataType.AllChecklists:
                        var checklists = await webhookAction.TrelloClient.GetChecklistsOnCardAsync(card.Id);
                        foreach (Checklist checklist in checklists)
                        {
                            await webhookAction.TrelloClient.DeleteChecklistAsync(checklist.Id);
                        }

                        break;
                    case RemoveCardDataType.AllAttachments:
                        var attachments = await webhookAction.TrelloClient.GetAttachmentsOnCardAsync(card.Id);
                        foreach (Attachment attachment in attachments)
                        {
                            await webhookAction.TrelloClient.DeleteAttachmentOnCardAsync(card.Id, attachment.Id);
                        }

                        break;
                    case RemoveCardDataType.AllComments:
                        var comments = await webhookAction.TrelloClient.GetAllCommentsOnCardAsync(card.Id);
                        foreach (TrelloAction comment in comments)
                        {
                            await webhookAction.TrelloClient.DeleteCommentActionAsync(comment.Id);
                        }

                        break;
                    case RemoveCardDataType.AllStickers:
                        var stickers = await webhookAction.TrelloClient.GetStickersOnCardAsync(card.Id);
                        foreach (Sticker sticker in stickers)
                        {
                            await webhookAction.TrelloClient.DeleteStickerAsync(card.Id, sticker.Id);
                        }

                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            if (queryParametersToUpdate.Any())
            {
                processingResult.ActionsExecuted++;
                await webhookAction.TrelloClient.UpdateCardAsync(card.Id, queryParametersToUpdate);
            }
            else
            {
                processingResult.ActionsSkipped++;
            }
        }
    }
}