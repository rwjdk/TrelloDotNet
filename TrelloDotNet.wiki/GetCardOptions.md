In the various methods that return one or more Cards, you can optionally provide a `GetCardOptions` object to affect what parts of the Trello Card are returned and in case of multiple cards what cards are included. If this object is not provided, Trello's default REST API Settings are used

## Properties to control what parts of a card are returned
| Property | Description |
| ----- | ------ |
| IncludeAttachments | Controls if cards should include their attachments (Default: False) |
| IncludeMembers | Whether to return member objects for members on the card (Default: False) |
| IncludeChecklists | Whether to return checklist objects for members on the card (Default: False) |
| IncludeBoard | Whether to return Board object the card is on (Default: False) |
| IncludeList | Whether to return List object the card is in (Default: False)|
| IncludePluginData | Whether to return Plugin object of the card (Default: False) |
| IncludeStickers |  Whether to return Sticker objects of the card (Default: False) |
| IncludeMemberVotes | Whether to return MemberVotes objects of the card (Default: False) |
| IncludeCustomFieldItems | Whether to return CustomFieldsItem objects of the card (Default: False) |
| ActionTypesToInclude | Provide one or more Card-action types there (TrelloDotNet.Model.Webhook.WebhookActionTypes) to get them included with the Card |
| CardFields | Here you can specify what fields you wish to have back in your result. This is primarily done for performance reasons in that if you example get all cards on a board just to make a bullet list of card-names with a URL, there is no need to waste time getting all the cards Description as an example |
| AttachmentFields |  What attachments-fields to include if IncludeAttachments are set to 'True' or 'Cover' |
| MemberFields | What member-fields to include if IncludeMembers are set to True |
| ChecklistFields | What checklist-fields to include if IncludeChecklists are set to True |
| MembersVotedFields | What member-fields to include if IncludeMemberVotes are set to True |
| StickerFields | What stickers-fields to include if IncludeStickers are set to True |
| BoardFields | What board-fields to include if IncludeBoard are set to True |
| Limit | Limit how many objects are returned (Default All) [Only used by methods where multiple objects are returned |
| Before | Pagination (Only return cards before this Card Id was created) |
| Since | Pagination (Only return cards since this Card Id was created) |
| AdditionalParameters | Send these Additional Parameters not supported out-of-the-box (should you need to do something to the query-parameters not yet supported by this API) |

## Properties to control what/how cards are returned
| Property | Description |
| ----- | ----- |
| Filter | What Kind of Cards should be included (Active, Closed/Archived, or All) [Only used on [GetCardsForBoard](GetCardsOnBoardAsync) Calls] |
| [FilterConditions](Filter-Condition-System) | Option to filter the cards return based on a filter (Example: _give me all cards on board that have the Red Label, 1-2 Members and the Description contains the word 'Urgent'_) **NB: Please note that the filter is an In-Memory Filter as Trello's do not allow Server-side filtering** |
| [OrderBy](OrderBy) | In what order the cards should be returned |
