# Changelog: 
*Below is the version history of [TrelloDotNet](https://github.com/rwjdk/TrelloDotNet) (An wrapper of the Trello API)*

## 1.3.0 (27th of March 2023)
- Added: Webhook Automation Engine that makes it even easier to consume Webhooks (just define your automation rules and give the engine the Webhook JSON and it does the rest :-))
- Added: WebhookAction now have reference to the TrelloClient and the sub-objects can get their Full Objects
- Added: Struct WebhookActionTypes that list all Types of Webhook events
- Added: Support for Basic Events 'OnDeleteCustomField','OnAddCustomField','OnUpdateCustomField' and 'OnUpdateCustomFieldItem'
- Added: Method 'DeleteChecklistAsync'
- Added: ListBefore and ListAfter to TrelloActionData
- Added: Method GetTokenInformationAsync() to get information about the Trello Token used for connecting
- Added: Method GetTokenMemberAsync() that returns the user that owns the Trello Token used for connecting
- Added: Support for Retrieving, setting, and removing Custom Field values of cards
- Fixed: Various incorrect XML Summaries and parameter names (all nonbreaking changes)
- Fixed: Null values in strings for an update of objects are now considered empty strings so you do not end up with 'null' values (aka the word 'null' as a string)

<hr>

## 1.2.1 (25th of Feb. 2023)
- Added: Generic WebHookNotification that does not care if Webhook returned from a Board, List, Card, etc
- Added: Added Method 'UpdateCommentActionAsync' for the ability to update Comments
- Added: Added Methods 'GetAllCommentsOnCardAsync' and 'GetPagedCommentsOnCardAsync' for the ability to Get existing comments on a card
- Changed: AddChecklistAsync will now add positions of check items automatically in the same order as the list if none is specified.
- Fixed: 'AddStickerToCardAsync' did not work as intended (You got an 'Invalid ImageId' error) due to a late refactoring and poor testing on my part (last time I skimp on integration-tests!) :-(
- Fixed: 'AddCommentAsync' had a wrong return object (Trello API is just weird and not consistent!)

<hr>

## 1.2.0 (23rd of Feb. 2023)
- Fixed: various methods were missing 'Async' suffix so it was added (Sorry for this breaking change (oversight by me) but better now than later :-/ ... and it should be easy to fix) [COMPILE TIME BREAKING CHANGE]
- Changed: Overridden ToString() methods on Models are removed and DebuggerDisplay Attributes are now used instead.
- Added: Added properties 'OrganizationId', 'EnterpriseId', and 'Pinned' to the Board Object
- Added: Support for Card Stickers (CRUD) :-)
- Added: Support to add Comments on cards :-)

<hr>

## 1.1.1 (17th of Feb. 2023)
- Fixed: Method 'SetStartDateOnCardAsync' was renamed to 'SetStartDateAndDueDateOnCardAsync' [COMPILE TIME BREAKING CHANGE]
- Fixed: Smart-events did not do a proper internal await so could technically be delayed
- Fixed: Methods 'ArchiveListAsync' and 'ReOpenListAsync' incorrectly return a Card and not a List [COMPILE TIME BREAKING CHANGE]
- Fixed: Methods 'CloseBoardAsync' and 'ReOpenBoardAsync' incorrectly return a Card and not a Board [COMPILE TIME BREAKING CHANGE]
- Fixed: Method 'RemoveLabelsFromCardAsync' did not work (wrongly implemented)
- Added: Added Property 'Closed' on Board so you can see if a board is Closed or not

<hr>

## 1.1.0 (8th of Feb. 2023)
- Added: Webhook System (See video on how to get going [here](https://youtu.be/A3_B-SLBm_0))
- Added: Various 'Ease of use methods' to do common actions (For example add or remove Members/Labels from Cards)
- Fixed: Trello icon edges where white
- Changed: More detailed README.md
- Fixed: Made class 'EnumViaJsonPropertyConverter' internal (incorrectly public) [COMPILE TIME BREAKING CHANGE]

<hr>

## 1.0.0 (6th of Feb. 2023)
- Added: First official version :-)

<hr>

## 0.9.6 Alpha
- Bumped dependency version of System.Text.Json to 6.0.0 as 5.0.0 have known dependency vulnerabilities and 6.0.0 is also the LTS version
- Fixed various bugs

<hr>

## 0.9.1 - 0.9.5 Alpha
- Initial releases with various bugs and breaking changes
