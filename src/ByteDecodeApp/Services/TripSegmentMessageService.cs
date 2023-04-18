using System.Text.Json;
using System.Text.Json.Serialization;

public class TripSegmentMessageService
{
    private readonly HttpClient _httpClient;
    const string _baseUrl = "http://localhost:5282/";
    const string _endpoint = "api/decode/";
    const string _host = "genius-song-lyrics1.p.rapidapi.com";
    const string _key = "{key}";

    public TripSegmentMessageService(HttpClient httpClient)
    {
        _httpClient = httpClient;
        _httpClient.BaseAddress = new Uri(_baseUrl);
    }       

    public async Task<TripSegmentMessageDto> Decode(string bytes)
    {
        var response = await _httpClient.GetAsync(_endpoint + bytes);
        var response_outcome = response.EnsureSuccessStatusCode;

        using var stream = await response.Content.ReadAsStreamAsync();

        return await JsonSerializer.DeserializeAsync<TripSegmentMessageDto>(stream);
    }
}