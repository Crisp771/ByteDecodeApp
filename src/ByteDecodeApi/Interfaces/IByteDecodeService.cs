public interface IByteDecodeService {
    TripSegmentMessageDto DecodeTripSegmentMessage(byte[] tripSegmentMessage);
}