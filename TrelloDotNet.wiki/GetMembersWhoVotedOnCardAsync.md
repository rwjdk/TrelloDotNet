[Back to Member Features](TrelloClient#member-features)

Get a list of members who voted on a specific card

`````cs

string cardId = "6567h55tttt4455";
List<Member> membersWhoVoted = await trelloClient.GetMembersWhoVotedOnCardAsync(cardId);

````