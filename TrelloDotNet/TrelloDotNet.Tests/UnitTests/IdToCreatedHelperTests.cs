using System.Text;
using System.Text.Json;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.UnitTests;

public class IdToCreatedHelperTests
{
    [Fact]
    public void GetCreatedFromIdGivenNullReturnNull()
    {
        var dateTimeOffset = IdToCreatedHelper.GetCreatedFromId(null);
        Assert.Null(dateTimeOffset);
    }

    [Fact]
    public void GetCreatedFromIdGivenValueReturnDateTimeOffset()
    {
        var dateTimeOffset = IdToCreatedHelper.GetCreatedFromId("63e28c76da4e34aecc487b1f");
        Assert.NotNull(dateTimeOffset);
    }
}