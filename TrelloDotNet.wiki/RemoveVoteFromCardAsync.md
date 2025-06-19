[Back to Member Features](TrelloClient#member-features)

Remove a vote from a member from a card

### Example

````cs
string cardId = "423432423j33kj43jj3";
string memberIdToFromAsVote = "r4rt5t6t7h7h77h555223";
await TrelloClient.RemoveVoteFromCardAsync(cardId, memberIdToFromAsVote);

````