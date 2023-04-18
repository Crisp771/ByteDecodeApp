using System.Buffers.Binary;
public class ByteDecodeService : IByteDecodeService
{
    public TripSegmentMessageDto DecodeTripSegmentMessage(byte[] tripSegmentMessage)
    {
        var bytePointer = 0;
        Console.WriteLine(BitConverter.ToString(tripSegmentMessage));
        if (tripSegmentMessage.Length != 32)
        {
            throw new ArgumentException("Valid messages should be exactly 32 bytes long.");
        }

        var messageDto = new TripSegmentMessageDto();

        var messageTypeValue = BinaryPrimitives.ReadUInt16LittleEndian(new byte[] { tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++] });

        // Replace with switch as we learn about more message types.  Is schema picked based on type?
        if (messageTypeValue == 255)
        {
            messageDto.MessageType = "Developer Test Message";
        }
        else
        {
            messageDto.MessageType = "Unknown Message Type";
            messageDto.Errors.Add("Error in message type value.  Position 1");
        }
        messageDto.CurrentTime = (new DateTime(2000, 01, 01)).AddSeconds(BinaryPrimitives.ReadUInt32LittleEndian(new byte[] { tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++] }));
        messageDto.DeviceId = System.Text.Encoding.ASCII.GetString(new byte[] { tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++] });
        messageDto.CurrentSpeed = BinaryPrimitives.ReadUInt16LittleEndian(new byte[] { tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++] });
        messageDto.Odometer = BinaryPrimitives.ReadUInt32LittleEndian(new byte[] { tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++] });
        messageDto.TripId = BinaryPrimitives.ReadUInt32LittleEndian(new byte[] { tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++] });
        messageDto.TripStart = tripSegmentMessage[bytePointer++] == 1;
        messageDto.TripEnd = tripSegmentMessage[bytePointer++] == 1;
        messageDto.Latitude = (BinaryPrimitives.ReadInt32LittleEndian(new byte[] { tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++] }) / (double)600000).ToString();
        messageDto.Longtitude = (BinaryPrimitives.ReadInt32LittleEndian(new byte[] { tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++], tripSegmentMessage[bytePointer++] }) / (double)600000).ToString();

        if (messageDto.CurrentTime > DateTime.Now)
            messageDto.Errors.Add("CurrentTime is from the future. Position 3");
        if (messageDto.CurrentSpeed > 300)
            messageDto.Errors.Add("CurentSpeed is over 300. Position 13");

        foreach (var character in messageDto.DeviceId)
        {
            if (!(char.IsLetterOrDigit(character) || char.IsPunctuation(character) || char.IsSymbol(character) || char.IsWhiteSpace(character)))
            {
                messageDto.Errors.Add("DeviceId contains unprintable characters. Position 8");
                break;
            }
        }

        return messageDto;
    }

}