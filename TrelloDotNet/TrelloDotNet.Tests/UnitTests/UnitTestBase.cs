namespace TrelloDotNet.Tests.UnitTests;

public abstract class UnitTestBase
{
    protected TrelloClient TrelloClient;
    
    protected UnitTestBase()
    {
        TrelloClient = new TestHelper().GetClient();
    }
}