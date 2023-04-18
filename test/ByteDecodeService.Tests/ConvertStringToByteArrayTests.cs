using Xunit;
namespace ConvertStringToByteArrayTests;

public class ConvertStringToByteArrayTests
{
    [Fact]
    public void TestMessageConverts()
    {
        var convertStringToByteArrayService = new ConvertStringToByteArrayService();
        var testMessage = new byte[] { 0xFF, 0x00, 0x06, 0xA6, 0xC9, 0x2B, 0x39, 0x39, 0x39, 0x39, 0x39, 0x31, 0x65, 0x00, 0xDD, 0xCE, 0x00, 0x00, 0xD9, 0x11, 0x00, 0x00, 0x01, 0x00, 0xBC, 0x9E, 0xE0, 0x01, 0x16, 0xC0, 0xEE, 0xFF };
        var stringMessage = "FF0006A6C92B3939393939316500DDCE0000D91100000100BC9EE00116C0EEFF";
        var messageResult = convertStringToByteArrayService.ConvertString(stringMessage);
        for (var i = 0; i < messageResult.Length; i++)
            Assert.True(testMessage[i] == messageResult[i], $"Expected value: '{testMessage[i]}', Actual: '{messageResult[i]}' at offset {i}.");
    }
}