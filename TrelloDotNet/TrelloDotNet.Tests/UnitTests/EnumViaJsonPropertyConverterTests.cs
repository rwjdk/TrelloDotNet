using System.Text.Json;
using System.Text;
using TrelloDotNet.Control;
using TrelloDotNet.Model;

namespace TrelloDotNet.Tests.UnitTests;

public class EnumViaJsonPropertyConverterTests
{
    [Fact]
    public void Read()
    {
        var enumViaJsonPropertyConverter = new EnumViaJsonPropertyConverter<CardCoverColor>();
        byte[] bytes = Encoding.UTF8.GetBytes("\"pink\"");
        Utf8JsonReader stringReader = new Utf8JsonReader(bytes.AsSpan());
        stringReader.Read();
        var readColor = enumViaJsonPropertyConverter.Read(ref stringReader, typeof(CardCoverColor), JsonSerializerOptions.Default);
        Assert.Equal(CardCoverColor.Pink, readColor);
    }

    [Fact]
    public void ReadEmpty()
    {
        var enumViaJsonPropertyConverter = new EnumViaJsonPropertyConverter<CardCoverColor>();
        byte[] bytes = Encoding.UTF8.GetBytes("\"\"");
        Utf8JsonReader stringReader = new Utf8JsonReader(bytes.AsSpan());
        stringReader.Read();
        var readColor = enumViaJsonPropertyConverter.Read(ref stringReader, typeof(CardCoverColor), JsonSerializerOptions.Default);
        Assert.Equal(CardCoverColor.None, readColor);
    }
    
    [Fact]
    public void ReadInvalid()
    {
        var enumViaJsonPropertyConverter = new EnumViaJsonPropertyConverter<DateTimeKind>();
        byte[] bytes = Encoding.UTF8.GetBytes("\"aaa\"");
        Assert.Throws<Exception>(() =>
        {
            Utf8JsonReader stringReader = new Utf8JsonReader(bytes.AsSpan());
            stringReader.Read();
            return enumViaJsonPropertyConverter.Read(ref stringReader, typeof(DateTimeKind), JsonSerializerOptions.Default);
        });
    }

    [Fact]
    public void Write()
    {
        var enumViaJsonPropertyConverter = new EnumViaJsonPropertyConverter<CardCoverColor>();
        var memoryStream = new MemoryStream();
        var utf8JsonWriter = new Utf8JsonWriter(memoryStream);
        enumViaJsonPropertyConverter.Write(utf8JsonWriter, CardCoverColor.Pink, JsonSerializerOptions.Default);
        utf8JsonWriter.Flush();
        string json = Encoding.UTF8.GetString(memoryStream.ToArray());

        Assert.Equal("\"pink\"", json);
    }
}