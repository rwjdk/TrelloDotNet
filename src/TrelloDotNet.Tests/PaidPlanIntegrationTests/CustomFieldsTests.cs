using TrelloDotNet.Model;
using TrelloDotNet.Model.Options.GetCardOptions;
using TrelloDotNet.Model.Options.GetListOptions;

namespace TrelloDotNet.Tests.PaidPlanIntegrationTests;

public class CustomFieldsTests : TestBase
{
    [Fact]
    public async Task CustomFields()
    {
        var board = await GetSpecialPaidSubscriptionBoard();
        if (board == null)
        {
            return; //Special Test-board not available
        }

        const string listPrefix = "UnitTestCustomFields";
        await CleanUp(board, listPrefix);
        try
        {
            var customFields = await TrelloClient.GetCustomFieldsOnBoardAsync(board.Id);
            Assert.Equal(5, customFields.Count);
            CustomField? fieldList = customFields.FirstOrDefault(x => x.Name == "Priority");
            CustomField? fieldCheckbox = customFields.FirstOrDefault(x => x.Name == "IsSomething");
            CustomField? fieldDate = customFields.FirstOrDefault(x => x.Name == "SomeDate");
            CustomField? fieldNumber = customFields.FirstOrDefault(x => x.Name == "SomeNumber");
            CustomField? fieldText = customFields.FirstOrDefault(x => x.Name == "SomeText");

            Assert.NotNull(fieldList);
            Assert.Equal(6, fieldList.Options.Count);
            Assert.NotNull(fieldCheckbox);
            Assert.NotNull(fieldDate);
            Assert.NotNull(fieldNumber);
            Assert.NotNull(fieldText);

            DateTimeOffset dateValue = DateTimeOffset.UtcNow;
            const decimal numberValue = 42.33M;
            const bool checkboxValue = true;
            const string textValue = "Hello World";
            CustomFieldOption listValue = fieldList.Options[2];

            List listForCustomFieldTests = await TrelloClient.AddListAsync(new List(listPrefix + Guid.NewGuid(), board.Id));
            Card card1 = await AddDummyCardToList(listForCustomFieldTests, "Card 1");

            var customValues = await TrelloClient.GetCustomFieldItemsForCardAsync(card1.Id);
            Assert.Empty(customValues);

            await TrelloClient.UpdateCustomFieldValueOnCardAsync(card1.Id, fieldList, listValue);
            await TrelloClient.UpdateCustomFieldValueOnCardAsync(card1.Id, fieldCheckbox, checkboxValue);
            await TrelloClient.UpdateCustomFieldValueOnCardAsync(card1.Id, fieldNumber, Convert.ToInt32(numberValue));
            await TrelloClient.UpdateCustomFieldValueOnCardAsync(card1.Id, fieldNumber, numberValue);
            await TrelloClient.UpdateCustomFieldValueOnCardAsync(card1.Id, fieldDate, dateValue);
            await TrelloClient.UpdateCustomFieldValueOnCardAsync(card1.Id, fieldText, textValue);

            customValues = await TrelloClient.GetCustomFieldItemsForCardAsync(card1.Id);
            Assert.Equal(5, customValues.Count);
            CustomFieldItem? valueList = customValues.FirstOrDefault(x => x.CustomFieldId == fieldList.Id && x.ValueId == listValue.Id);
            CustomFieldItem? valueCheckbox = customValues.FirstOrDefault(x => x.CustomFieldId == fieldCheckbox.Id && x.Value.Checked);
            CustomFieldItem? valueDate = customValues.FirstOrDefault(x => x.CustomFieldId == fieldDate.Id && x.Value.Date?.ToUnixTimeSeconds() == dateValue.ToUnixTimeSeconds());
            CustomFieldItem? valueNumber = customValues.FirstOrDefault(x => x.CustomFieldId == fieldNumber.Id && x.Value.Number == numberValue);
            CustomFieldItem? valueText = customValues.FirstOrDefault(x => x.CustomFieldId == fieldText.Id && x.Value.TextAsString == textValue);

            Assert.NotNull(valueList);
            Assert.NotNull(valueCheckbox);
            Assert.NotNull(valueDate);
            Assert.NotNull(valueNumber);
            Assert.NotNull(valueText);

            var cards = await TrelloClient.GetCardsOnBoardAsync(board.Id, new GetCardOptions
            {
                FilterConditions =
                [
                    CardsFilterCondition.ListId(CardsConditionIds.Equal, listForCustomFieldTests.Id),

                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.Equal, textValue),
                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.NotEqual, Guid.NewGuid().ToString()),
                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.AnyOfThese, textValue, Guid.NewGuid().ToString()),
                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.NoneOfThese, Guid.NewGuid().ToString(), Guid.NewGuid().ToString()),
                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.StartsWith, textValue),
                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.DoNotStartWith, Guid.NewGuid().ToString()),
                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.EndsWith, textValue),
                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.DoNotEndWith, Guid.NewGuid().ToString()),
                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.RegEx, textValue),
                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.Contains, textValue),
                    CardsFilterCondition.CustomField(fieldText, CardsConditionString.DoNotContains, Guid.NewGuid().ToString()),

                    CardsFilterCondition.CustomField(fieldCheckbox, CardsConditionBoolean.Equal, checkboxValue),
                    CardsFilterCondition.CustomField(fieldCheckbox, CardsConditionBoolean.NotEqual, !checkboxValue),

                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.Equal, dateValue),
                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.NotEqual, dateValue.AddDays(1)),
                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.Between, dateValue.AddDays(-1), dateValue.AddDays(1)),
                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.NotBetween, DateTimeOffset.MinValue, dateValue.AddDays(-1)),
                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.GreaterThan, dateValue.AddDays(-1)),
                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.GreaterThanOrEqual, dateValue.AddDays(-1)),
                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.LessThan, dateValue.AddDays(1)),
                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.LessThanOrEqual, dateValue.AddDays(1)),
                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.AnyOfThese, dateValue, dateValue.AddDays(1)),
                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.HasAnyValue),
                    CardsFilterCondition.CustomField(fieldDate, CardsConditionDate.NoneOfThese, DateTimeOffset.MinValue, DateTimeOffset.MaxValue),

                    CardsFilterCondition.CustomField(fieldNumber, CardsConditionNumber.Equal, numberValue),
                    CardsFilterCondition.CustomField(fieldNumber, CardsConditionNumber.NotEqual, -numberValue),
                    CardsFilterCondition.CustomField(fieldNumber, CardsConditionNumber.Between, numberValue - 1, numberValue + 1),
                    CardsFilterCondition.CustomField(fieldNumber, CardsConditionNumber.NotBetween, decimal.MinValue, numberValue - 1),
                    CardsFilterCondition.CustomField(fieldNumber, CardsConditionNumber.GreaterThan, numberValue - 1),
                    CardsFilterCondition.CustomField(fieldNumber, CardsConditionNumber.GreaterThanOrEqual, numberValue - 1),
                    CardsFilterCondition.CustomField(fieldNumber, CardsConditionNumber.LessThan, numberValue + 1),
                    CardsFilterCondition.CustomField(fieldNumber, CardsConditionNumber.LessThanOrEqual, numberValue + 1),
                    CardsFilterCondition.CustomField(fieldNumber, CardsConditionNumber.HasAnyValue, Array.Empty<decimal>()),
                    CardsFilterCondition.CustomField(fieldNumber, CardsConditionNumber.AnyOfThese, numberValue, numberValue + 1),

                    CardsFilterCondition.CustomField(fieldList, CardsConditionString.Equal, listValue.Id),
                    CardsFilterCondition.CustomField(fieldList, CardsConditionString.NotEqual, Guid.NewGuid().ToString()),
                ]
            });
            Assert.Single(cards);
            Assert.Equal(card1.Id, cards.First().Id);

            await TrelloClient.ClearCustomFieldValueOnCardAsync(card1.Id, fieldList);
            await TrelloClient.ClearCustomFieldValueOnCardAsync(card1.Id, fieldCheckbox);
            await TrelloClient.ClearCustomFieldValueOnCardAsync(card1.Id, fieldNumber);
            await TrelloClient.ClearCustomFieldValueOnCardAsync(card1.Id, fieldDate);
            await TrelloClient.ClearCustomFieldValueOnCardAsync(card1.Id, fieldText);

            customValues = await TrelloClient.GetCustomFieldItemsForCardAsync(card1.Id);
            Assert.Empty(customValues);
        }
        finally
        {
            await CleanUp(board, listPrefix);
        }
    }

    private async Task CleanUp(Board board, string listPrefix)
    {
        //Potential previous cleanup
        var lists = await TrelloClient.GetListsOnBoardAsync(board.Id, new GetListOptions
        {
            Filter = ListFilter.All
        });
        foreach (List list in lists.Where(x => x.Name.StartsWith(listPrefix)))
        {
            await TrelloClient.DeleteListAsync(list.Id);
        }
    }
}