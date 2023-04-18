public class TripSegmentMessageDto
{
    public TripSegmentMessageDto()
    {
        messageType = string.Empty;
        errors = new List<string>();
        deviceId = string.Empty;
        latitude = string.Empty;
        longtitude = string.Empty;
    }
    public string messageType { get; set; }
    public DateTime currentTime { get; set; }
    public string deviceId { get; set; }
    public uint currentSpeed { get; set; }
    public uint odometer { get; set; }
    public uint tripId { get; set; }
    public bool tripStart { get; set; }
    public bool tripEnd { get; set; }
    public string latitude { get; set; }
    public string longtitude { get; set; }
    public List<string> errors {get;set;}
}
