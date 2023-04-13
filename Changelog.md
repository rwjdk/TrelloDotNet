# Changelog: 
*Below is the version history of [TrelloDotNet](https://github.com/rwjdk/TrelloDotNet) (An wrapper of the Trello API)*

## Unreleased
- Fixed that 'Constraint' was spelled incorrectly (missing an 's') in 2 places ('ListConditionCon**s**traint' and 'CardMovedToListTriggerCon**s**traint') [COMPILE TIME BREAKING CHANGE] (sorry but better to change now than later ;-( ) 
- Fixed that 'CardCoverCondition' was not evaluated correctly in all scenarios
- Added support for managing Labels on the Board
- Fixed that 'SetFieldsOnCardAction' did not update processing result (Executed and Skipped Actions counts)
- Added support for managing Attachments
- Added support to set/remove Attachment Cover on a Card
- Added 'UpdateChecklistItemAsync'
- Added 'GetBoardsForMemberAsync'
- Added 'GetBoardsCurrentTokenCanAccessAsync'

<hr>

## 1.4.0 (3rd of April 2023)
- Enhanced 'UpdateCardAsync' so it now has the ability to Add/Update/Remove the Cover of a card
- Added methods 'AddCoverToCardAsync', 'UpdateCoverOnCardAsync', and 'RemoveCoverOnCardAsync'
- Added quality of life methods to maintain webhooks by callback URL instead of Id
- Added RegEx option for string-comparison in Automation Engine
- Added 'CardUpdatedTrigger' to Automation Engine

<hr>

## 1.3.1 (30th of March 2023)
- Added that 'ChecklistIncompleteCondition' now has the option to define 'ChecklistNameMatchCriteria' if you example want to check lists with a certain name prefix.

<hr>

## 1.3.0 (27th of March 2023)
- Added Webhook Automation Engine that makes it even easier to consume Webhooks (just define your automation rules and give the engine the Webhook JSON and it does the rest :-))
- WebhookAction now have reference to the TrelloClient and the sub-objects can get their Full Objects
- Added struct 'WebhookActionTypes' that list all Types of Webhook events
- Added support for Basic Events 'OnDeleteCustomField','OnAddCustomField','OnUpdateCustomField' and 'OnUpdateCustomFieldItem'
- Added method 'DeleteChecklistAsync'
- Added 'ListBefore' and 'ListAfter' to 'TrelloActionData'
- Added method 'GetTokenInformationAsync()' to get information about the Trello Token used for connecting
- Added method 'GetTokenMemberAsync()' that returns the user that owns the Trello Token used for connecting
- Added support for Retrieving, setting, and removing Custom Field values of cards
- Fixed various incorrect XML Summaries and parameter names (all nonbreaking changes)
- Fixed that Null values in strings for an update of objects are now considered empty strings so you do not end up with 'null' values (aka the word 'null' as a string)

<hr>

## 1.2.1 (25th of Feb. 2023)
- Added generic WebHookNotification that does not care if Webhook returned from a Board, List, Card, etc
- Added method 'UpdateCommentActionAsync' for the ability to update Comments
- Added methods 'GetAllCommentsOnCardAsync' and 'GetPagedCommentsOnCardAsync' for the ability to Get existing comments on a card
- AddChecklistAsync will now add positions of check items automatically in the same order as the list if none is specified.
- Fixed that 'AddStickerToCardAsync' did not work as intended (You got an 'Invalid ImageId' error) due to a late refactoring and poor testing on my part (last time I skimp on integration-tests!) :-(
- Fixed that 'AddCommentAsync' had a wrong return object (Trello API is just weird and not consistent!)

<hr>

## 1.2.0 (23rd of Feb. 2023)
- Fixed that various methods were missing 'Async' suffix so it was added (Sorry for this breaking change (oversight by me) but better now than later :-/ ... and it should be easy to fix) [COMPILE TIME BREAKING CHANGE]
- Overridden ToString() methods on Models are removed and DebuggerDisplay Attributes are now used instead.
- Added properties 'OrganizationId', 'EnterpriseId', and 'Pinned' to the Board Object
- Added support for Card Stickers (CRUD).
- Added support to add Comments on cards.

<hr>

## 1.1.1 (17th of Feb. 2023)
- Method 'SetStartDateOnCardAsync' was renamed to 'SetStartDateAndDueDateOnCardAsync' [COMPILE TIME BREAKING CHANGE]
- Fixed that Smart-events did not do a proper internal await so could technically be delayed
- Fixed that methods 'ArchiveListAsync' and 'ReOpenListAsync' incorrectly return a Card and not a List [COMPILE TIME BREAKING CHANGE]
- Fixed that methods 'CloseBoardAsync' and 'ReOpenBoardAsync' incorrectly return a Card and not a Board [COMPILE TIME BREAKING CHANGE]
- Fixed that method 'RemoveLabelsFromCardAsync' did not work (wrongly implemented)
- Added Property 'Closed' on Board so you can see if a board is Closed or not

<hr>

## 1.1.0 (8th of Feb. 2023)
- Added Webhook System (See video on how to get going [here](https://youtu.be/A3_B-SLBm_0))
- Added various 'Ease of use methods' to do common actions (For example add or remove Members/Labels from Cards)
- Fixed that Trello icon edges where white
- Added more detailed README.md
- Made class 'EnumViaJsonPropertyConverter' internal (incorrectly public) [COMPILE TIME BREAKING CHANGE]

<hr>

## 1.0.0 (6th of Feb. 2023)
- First official version :-)

<hr>

## 0.9.6 Alpha
- Bumped dependency version of System.Text.Json to 6.0.0 as 5.0.0 have known dependency vulnerabilities and 6.0.0 is also the LTS version
- Fixed various bugs

<hr>

## 0.9.1 - 0.9.5 Alpha
- Initial releases with various bugs and breaking changes
