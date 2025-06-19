The `GetActionsOptions` provides options for retrieving actions (changelog events) in Trello. 

| Property          | Description                                                                 |
|-----------------------|-----------------------------------------------------------------------------|
| AdditionalParameters  | Additional parameters not supported out-of-the-box.                        |
| Before                | An Action ID to retrieve actions before this ID.                          |
| Filter                | A set of event types to filter by (e.g., TrelloDotNet.Model.Webhook.WebhookActionTypes). |
| Limit                 | The number of recent events to retrieve (default: 50, max: 1000).          |
| Page                  | The page of results for actions.                                           |
| Since                 | An Action ID to retrieve actions since this ID.                           |

This class is used in methods such as [GetActionsForMemberAsync](GetActionsForMemberAsync), [GetActionsOnCardAsync](GetActionsOnCardAsync), and [GetActionsForListAsync](GetActionsForListAsync) to customize the retrieval of Trello actions.