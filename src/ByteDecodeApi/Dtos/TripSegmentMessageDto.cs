public class TripSegmentMessageDto
{
    public TripSegmentMessageDto()
    {
        MessageType = string.Empty;
        Errors = new List<string>();
        DeviceId = string.Empty;
        Latitude = string.Empty;
        Longtitude = string.Empty;
    }
    public string MessageType { get; set; }
    public DateTime CurrentTime { get; set; }
    public string DeviceId { get; set; }
    public uint CurrentSpeed { get; set; }
    public uint Odometer { get; set; }
    public uint TripId { get; set; }
    public bool TripStart { get; set; }
    public bool TripEnd { get; set; }
    public string Latitude { get; set; }
    public string Longtitude { get; set; }
    public List<string> Errors {get;set;}
}
