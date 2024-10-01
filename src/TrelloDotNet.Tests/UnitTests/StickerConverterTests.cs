using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.UnitTests;

public class StickerConverterTests
{
    [Fact]
    public void DefaultImageToString()
    {
        Assert.Equal("check", Sticker.DefaultImageToString(StickerDefaultImageId.Check));
        Assert.Equal("clock", Sticker.DefaultImageToString(StickerDefaultImageId.Clock));
        Assert.Equal("frown", Sticker.DefaultImageToString(StickerDefaultImageId.Frown));
        Assert.Equal("heart", Sticker.DefaultImageToString(StickerDefaultImageId.Heart));
        Assert.Equal("huh", Sticker.DefaultImageToString(StickerDefaultImageId.Huh));
        Assert.Equal("laugh", Sticker.DefaultImageToString(StickerDefaultImageId.Laugh));
        Assert.Equal("rocketship", Sticker.DefaultImageToString(StickerDefaultImageId.RocketShip));
        Assert.Equal("smile", Sticker.DefaultImageToString(StickerDefaultImageId.Smile));
        Assert.Equal("star", Sticker.DefaultImageToString(StickerDefaultImageId.Star));
        Assert.Equal("thumbsdown", Sticker.DefaultImageToString(StickerDefaultImageId.ThumbsDown));
        Assert.Equal("thumbsup", Sticker.DefaultImageToString(StickerDefaultImageId.ThumbsUp));
        Assert.Equal("warning", Sticker.DefaultImageToString(StickerDefaultImageId.Warning));
        Assert.Throws<ArgumentOutOfRangeException>(() => Sticker.DefaultImageToString(StickerDefaultImageId.NotADefault));
    }
    
    [Fact]
    public void StringToDefaultImageId()
    {
        Assert.Equal(StickerDefaultImageId.Check, Sticker.StringToDefaultImageId("check"));
        Assert.Equal(StickerDefaultImageId.Clock, Sticker.StringToDefaultImageId("clock"));
        Assert.Equal(StickerDefaultImageId.Frown, Sticker.StringToDefaultImageId("frown"));
        Assert.Equal(StickerDefaultImageId.Heart, Sticker.StringToDefaultImageId("heart"));
        Assert.Equal(StickerDefaultImageId.Huh, Sticker.StringToDefaultImageId("huh"));
        Assert.Equal(StickerDefaultImageId.Laugh, Sticker.StringToDefaultImageId("laugh"));
        Assert.Equal(StickerDefaultImageId.RocketShip, Sticker.StringToDefaultImageId("rocketship"));
        Assert.Equal(StickerDefaultImageId.Smile, Sticker.StringToDefaultImageId("smile"));
        Assert.Equal(StickerDefaultImageId.Star, Sticker.StringToDefaultImageId("star"));
        Assert.Equal(StickerDefaultImageId.ThumbsDown, Sticker.StringToDefaultImageId("thumbsdown"));
        Assert.Equal(StickerDefaultImageId.ThumbsUp, Sticker.StringToDefaultImageId("thumbsup"));
        Assert.Equal(StickerDefaultImageId.Warning, Sticker.StringToDefaultImageId("warning"));
        Assert.Equal(StickerDefaultImageId.NotADefault, Sticker.StringToDefaultImageId(Guid.NewGuid().ToString()));
    }
}