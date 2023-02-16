using Microsoft.Extensions.Configuration;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.IntegrationTests;

public class FullIntegrationTest
{
    /// <summary>
    /// This is a full test of all client features. It is done on a auto-generated board that is deleted at the end so will not touch any existing boards for the token
    /// </summary>
    /// <returns></returns>
    [Fact]
    public async Task CreateBoardAndDoNormalActions()
    {
        var client = new TestHelper().GetClient();
        var boardName = $"UnitTestBoard-{DateTime.Now:yyyyMMddHHmmss}";
        var boardDescription = $"BoardDescription-{DateTime.Now:yyyyMMddHHmmss}";


        string? boardId = null;
        try
        {
            var addedBoard = await client.AddBoardAsync(new Board(boardName, boardDescription));
            boardId = addedBoard.Id;
            //Test Board
            Assert.Equal(boardName, addedBoard.Name);
            Assert.Equal(boardDescription, addedBoard.Description);
            addedBoard.Name = boardName + "X";
            addedBoard.Description = boardDescription + "X";
            var updatedBoard = await client.UpdateBoardAsync(addedBoard);
            var getBoard = await client.GetBoardAsync(boardId);
            Assert.EndsWith("X", updatedBoard.Name);
            Assert.EndsWith("X", updatedBoard.Description);
            Assert.EndsWith("X", getBoard.Name);
            Assert.EndsWith("X", getBoard.Description);
            Assert.Equal(updatedBoard.Description, getBoard.Description);
            Assert.Equal(updatedBoard.Description, getBoard.Description);


            //Member Test
            var members = await client.GetMembersOfBoardAsync(boardId);
            Assert.Single(members);
            var member = await client.GetMember(members.First().Id);
            Assert.Equal(member.FullName, members.First().FullName);
            Assert.Equal(member.Username, members.First().Username);

            //Test Lists
            var newListName = "New List";
            var addedList = await client.AddListAsync(new List(newListName, boardId));
            Assert.Equal(newListName, addedList.Name);

            var lists = await client.GetListsOnBoardAsync(boardId);
            var listsFiltered = await client.GetListsOnBoardFilteredAsync(boardId, ListFilter.Closed);
            Assert.Equal(4, lists.Count); //3 default + added
            Assert.Empty(listsFiltered);
            var todoList = lists.FirstOrDefault(x => x.Name == "To Do");
            var doingList = lists.FirstOrDefault(x => x.Name == "Doing");
            var doneList = lists.FirstOrDefault(x => x.Name == "Done");
            var newList = lists.FirstOrDefault(x => x.Name == newListName);
            Assert.NotNull(todoList);
            Assert.NotNull(doingList);
            Assert.NotNull(doneList);
            Assert.NotNull(newList);

            doingList.Name = "In Progress";
            var updatedDoingList = await client.UpdateListAsync(doingList);
            var getDoingList = await client.GetListAsync(doingList.Id);
            Assert.Equal("In Progress", getDoingList.Name);
            Assert.Equal(updatedDoingList.Name, getDoingList.Name);

            //Test Labels
            var labels = await client.GetLabelsOfBoardAsync(boardId);
            Assert.Equal(6, labels.Count);

            //Test Cards
#pragma warning disable xUnit2013
            Assert.Equal(0, (await client.GetCardsOnBoardAsync(boardId)).Count);
            Assert.Equal(0, (await client.GetCardsOnBoardFilteredAsync(boardId, CardsFilter.All)).Count);
            Assert.Equal(0, (await client.GetCardsInListAsync(todoList.Id)).Count);
            Assert.Equal(0, (await client.GetCardsInListAsync(doingList.Id)).Count);
            Assert.Equal(0, (await client.GetCardsInListAsync(doneList.Id)).Count);
#pragma warning restore xUnit2013

            DateTimeOffset? start = new DateTimeOffset(new DateTime(2000, 1, 1, 12, 0, 0, DateTimeKind.Utc));
            DateTimeOffset? due = new DateTimeOffset(new DateTime(2099, 1, 1, 12, 0, 0, DateTimeKind.Utc));
            List<string> labelIds = new List<string>
            {
                labels[1].Id,
                labels[2].Id
            };

            List<string> memberIds = new List<string> { member.Id };
            var addedCard = await client.AddCardAsync(new Card(newList.Id, "Card", "Description", start, due, false, labelIds, memberIds));
            Assert.Equal("Card", addedCard.Name);
            Assert.Equal("Description", addedCard.Description);
            Assert.Equal(start, addedCard.Start);
            Assert.Equal(due, addedCard.Due);
            Assert.False(addedCard.DueComplete);
            Assert.Equal(2, addedCard.Labels.Count);
            Assert.Equal(labels[1].Color, addedCard.Labels[0].Color);
            Assert.Equal(labels[2].Color, addedCard.Labels[1].Color);
            Assert.Single(addedCard.MemberIds);
            Assert.Single((await client.GetCardsInListAsync(newList.Id)));

            var membersOfCardAsync = await client.GetMembersOfCardAsync(addedCard.Id);
            Assert.Single(membersOfCardAsync);

            addedCard.DueComplete = true;
            addedCard.Description = "New Description";
            var updateCard = await client.UpdateCardAsync(addedCard);
            Assert.True(updateCard.DueComplete);
            Assert.Equal("New Description", updateCard.Description);
            var getCard = await client.GetCardAsync(addedCard.Id);
            Assert.Equal(updateCard.Description, getCard.Description);
            Assert.Equal(updateCard.DueComplete, getCard.DueComplete);

            //Test Checklists
            var checklists = await client.GetChecklistsOnBoardAsync(boardId);
            Assert.Empty(checklists);

            var checklistItems = new List<ChecklistItem>
            {
                new("ItemA", due, member.Id),
                new("ItemB"),
                new("ItemC", null, member.Id)
            };
            var newChecklist = new Checklist("Sample Checklist", checklistItems);
            var addedChecklist = await client.AddChecklistAsync(getCard.Id, newChecklist);
            var addedChecklistNotTwice = await client.AddChecklistAsync(getCard.Id, newChecklist, true);
            
            Assert.Equal("Sample Checklist", addedChecklist.Name);
            Assert.Equal(3, addedChecklist.Items.Count);
            var itemA = addedChecklist.Items.FirstOrDefault(x => x.Name == "ItemA");
            var itemB = addedChecklist.Items.FirstOrDefault(x => x.Name == "ItemB");
            var itemC = addedChecklist.Items.FirstOrDefault(x => x.Name == "ItemC");

            Assert.NotNull(itemA);
            Assert.NotNull(itemB);
            Assert.NotNull(itemC);

            //Assert.Equal(due, itemA.Due); //This will fail on a free version of Trello so commented out
            //Assert.Equal(member.Id, itemA.MemberId); //This will fail on a free version of Trello so commented out

            Assert.Null(itemB.Due);
            Assert.Null(itemB.MemberId);

            Assert.Null(itemC.Due); 
            //Assert.Equal(member.Id, itemC.MemberId); //This will fail on a free version of Trello so commented out

            var doneCard = await client.AddCardAsync(new Card(doneList.Id, "Done Card"));
            await client.AddChecklistAsync(doneCard.Id, addedChecklist.Id);
            await client.AddChecklistAsync(doneCard.Id, addedChecklist.Id, true);

            var checklistsNow = await client.GetChecklistsOnBoardAsync(boardId);
            Assert.Equal(2, checklistsNow.Count);

            var cardsNow = await client.GetCardsOnBoardAsync(boardId);
            Assert.Equal(2, cardsNow.Count);

            await client.DeleteCard(doneCard.Id);
            var cardsNowAfterDelete = await client.GetCardsOnBoardAsync(boardId);
            Assert.Single(cardsNowAfterDelete);

            var rawGet = await client.GetAsync($"boards/{boardId}");
            Assert.NotNull(rawGet);

            var rawGetBoard = await client.GetAsync<Board>($"boards/{boardId}");
            Assert.Equal(boardId, rawGetBoard.Id);

            var rawPost = await client.PostAsync("cards", new QueryParameter("name", "Card"), new QueryParameter("idList", todoList.Id));
            Assert.NotNull(rawPost);

            var rawPostCard = await client.PostAsync<Card>("cards", new QueryParameter("name", "Card"), new QueryParameter("idList", todoList.Id));
            Assert.NotNull(rawPostCard.Id);

            var rawUpdate = await client.PutAsync($"cards/{rawPostCard.Id}", new QueryParameter("desc", "New Description"));
            Assert.NotNull(rawUpdate);

            var rawUpdateCard = await client.PutAsync<Card>($"cards/{rawPostCard.Id}", new QueryParameter("desc", "New Description2"));
            Assert.Equal("New Description2", rawUpdateCard.Description);

            //Raw exceptions
            try
            {
                client.Options.ApiCallExceptionOption = ApiCallExceptionOption.DoNotIncludeTheUrl;
                await client.GetAsync("xyz");
            }
            catch (TrelloApiException e)
            {
                //Empty
                Assert.Equal("XXXXX", e.DataSentToTrello);
            }
            try
            {
                client.Options.ApiCallExceptionOption = ApiCallExceptionOption.IncludeUrlButMaskCredentials;
                await client.GetAsync("xyz");
            }
            catch (TrelloApiException e)
            {
                //Empty
                Assert.Contains("XXXXXXXXXX", e.DataSentToTrello);
            }
            try
            {
                client.Options.ApiCallExceptionOption = ApiCallExceptionOption.IncludeUrlAndCredentials;
                await client.GetAsync("xyz");
            }
            catch (TrelloApiException e)
            {
                //Empty
                Assert.DoesNotContain("XXXXXXXXXX", e.DataSentToTrello);
            }

        }
        // ReSharper disable once RedundantCatchClause
#pragma warning disable CS0168
        catch (Exception e)
#pragma warning restore CS0168
        {
            throw;
        }
        finally
        {
            if (boardId != null)
            {
                try
                {
                    await client.DeleteBoard(boardId);
                }
                catch (Exception e)
                {
                    Assert.Equal("Deletion of Boards are disabled via Options.AllowDeleteOfBoards (You need to enable this as a secondary confirmation that you REALLY wish to use that option as there is no going back: https://support.atlassian.com/trello/docs/deleting-a-board/)", e.Message);
                }
                finally
                {
                    client.Options.AllowDeleteOfBoards = true;
                    await client.DeleteBoard(boardId);
                }
            }
        }
    }
}