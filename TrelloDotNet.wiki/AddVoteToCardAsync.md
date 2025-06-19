[Back to Member Features](TrelloClient#member-features)

Add a Vote to a Card for a specific member

### Examples

````cs
string cardId = "64f754e09e99df1f544045d5";
string memberIdToCastTheVote = "434fe3333dd3d3d4d43";

await TrelloClient.AddVoteToCardAsync(cardId, memberIdToCastTheVote);

````