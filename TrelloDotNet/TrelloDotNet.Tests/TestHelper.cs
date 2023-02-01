using TrelloDotNet.Interface;

namespace TrelloDotNet.Tests;

public static class TestHelper
{
    public static ITrelloClient GetClient()
    {
        ITrelloClient trelloClient = new TrelloClient("8000d9bde07ef82025e9e070c7ea82d8", "ATTA8a75349a982325c7b874caf6e2f174d78ddb39d38e4aff22064703d509142db45E058BAB");
        return trelloClient;
    }
}