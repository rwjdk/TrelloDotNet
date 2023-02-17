# Changelog: 
*Below is the version history of [TrelloDotNet](https://github.com/rwjdk/TrelloDotNet) (An wrapper of the Trello API)*

## 1.1.1 (17th of Feb. 2023)
- Fixed: Method 'SetStartDateOnCardAsync' was renamed to 'SetStartDateAndDueDateOnCardAsync' [COMPILE TIME BREAKING CHANGE]
- Fixed: Smart-events did not do a proper internal await so could technically be delayed
- Fixed: Methods 'ArchiveListAsync' and 'ReOpenListAsync' incorrectly return a Card and not a List [COMPILE TIME BREAKING CHANGE]
- Fixed: Methods 'CloseBoardAsync' and 'ReOpenBoardAsync' incorrectly return a Card and not a Board [COMPILE TIME BREAKING CHANGE]
- Fixed: Method 'RemoveLabelsFromCardAsync' did not work (wrongly implemented)
- Added: Added Property 'Closed' on Board so you can see if a board is Closed or not

## 1.1.0 (8th of Feb. 2023)
- Added: Webhook System (See video how to get going [here](https://youtu.be/A3_B-SLBm_0))
- Added: Various 'Ease of use methods' to do common actions (Example add or remove Members/Labels from Cards)
- Fixed: Trello icon edges where white
- Changed: More detailed README.md
- Fixed: Made class 'EnumViaJsonPropertyConverter' internal (incorrectly public) [COMPILE TIME BREAKING CHANGE]

<hr>

## 1.0.0 (6th of Feb. 2023)
- Added: First official version :-)

<hr>

## 0.9.6 Alpha
- Bumped dependency version of System.Text.Json to 6.0.0 as 5.0.0 have know dependency vulnerabilities and 6.0.0 is also the LTS version
- Fixed various bugs

<hr>

## 0.9.1 - 0.9.5 Alpha
- Initial releases with various bugs and breaking changes