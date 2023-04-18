using Xunit;
namespace ByteDecodeServiceTests;

public class ByteDecodeServiceTests
{
    [Fact]
    public void MessageShouldNotBeEmpty()
    {
        var byteDecodeService = new ByteDecodeService();
        var testMesage = new byte[] { }; // Empty message
        Assert.Throws<ArgumentException>(() => byteDecodeService.DecodeTripSegmentMessage(testMesage));
    }

    [Fact]
    public void MessageShouldNotBeTooBig()
    {
        var byteDecodeService = new ByteDecodeService();
        var testMesage = new byte[33];
        for (int i = 0; i < testMesage.Length; i++) testMesage[i] = 0xFF; // Populate message
        Assert.Throws<ArgumentException>(() => byteDecodeService.DecodeTripSegmentMessage(testMesage));
    }

    
    [Fact]
    public void TestMessageDecodesCorrectly()
    {
        var byteDecodeService = new ByteDecodeService();
        var testMessage = new byte[] { 0xFF, 0x00, 0x06, 0xA6, 0xC9, 0x2B, 0x39, 0x39, 0x39, 0x39, 0x39, 0x31, 0x65, 0x00, 0xDD, 0xCE, 0x00, 0x00, 0xD9, 0x11, 0x00, 0x00, 0x01, 0x00, 0xBC, 0x9E, 0xE0, 0x01, 0x16, 0xC0, 0xEE, 0xFF };
        var messageResult = byteDecodeService.DecodeTripSegmentMessage(testMessage);
        Assert.Equal("Developer Test Message", messageResult.MessageType); 
        Assert.Equal(new DateTime(2023, 04, 12, 17, 25, 26, 0, 0),  messageResult.CurrentTime);
        Assert.Equal("999991", messageResult.DeviceId);
        Assert.Equal((uint)101, messageResult.CurrentSpeed);
        Assert.Equal((uint)52957, messageResult.Odometer);
        Assert.Equal((uint)4569, messageResult.TripId);
        Assert.True(messageResult.TripStart);
        Assert.False(messageResult.TripEnd);
        Assert.Equal("52.49652666666667", messageResult.Latitude);
        Assert.Equal("-1.8841233333333334", messageResult.Longtitude);
        Assert.Equal(0, messageResult.Errors.Count);  // This should be a valid message with zero defects
    }

    [Fact]
    public void TestMessageErrorsAreReported()
    {
        var byteDecodeService = new ByteDecodeService();
        var testMessage = new byte[] { 0xFE, 0x00, 0x06, 0xA6, 0xC9, 0xFF, 0x39, 0x39, 0x39, 0x39, 0x04, 0x31, 0xFF, 0xFF, 0xDD, 0xCE, 0x00, 0x00, 0xD9, 0x11, 0x00, 0x00, 0x01, 0x00, 0xBC, 0x9E, 0xE0, 0x01, 0x16, 0xC0, 0xEE, 0xFF };
        var messageResult = byteDecodeService.DecodeTripSegmentMessage(testMessage);
        Assert.Equal("Unknown Message Type", messageResult.MessageType); 
        Assert.Equal(new DateTime(2135, 12, 28, 01, 01, 58, 0, 0),  messageResult.CurrentTime);
        Assert.Equal($"9999{(char)4}1", messageResult.DeviceId);
        Assert.Equal((uint)65535, messageResult.CurrentSpeed);
        Assert.Equal((uint)52957, messageResult.Odometer);
        Assert.Equal((uint)4569, messageResult.TripId);
        Assert.True(messageResult.TripStart);
        Assert.False(messageResult.TripEnd);
        Assert.Equal("52.49652666666667", messageResult.Latitude);
        Assert.Equal("-1.8841233333333334", messageResult.Longtitude);
        Assert.Equal(4, messageResult.Errors.Count);  // This should have four errors
    }
}