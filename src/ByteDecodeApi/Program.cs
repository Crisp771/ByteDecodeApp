using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "ByteDecode API", Description = "Decodes Byte Packets", Version = "v1" });
});

builder.Services.AddSingleton<IByteDecodeService, ByteDecodeService>();
builder.Services.AddSingleton<IConvertStringToByteArrayService, ConvertStringToByteArrayService>();


var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "ByteDecode API V1");
});
var byteDecodeService = app.Services.GetRequiredService<IByteDecodeService>();
var stringConvertService = app.Services.GetRequiredService<IConvertStringToByteArrayService>();
app.MapGet("/", () => {
   var result = stringConvertService.ConvertString("FF004102000108172701F0230000FF0006A6C92B3939393939316500DDCE0000D91100000100BC9EE00116C0EEFF");
Console.WriteLine($"{BitConverter.ToString(result)}");
return BitConverter.ToString(result);
});
app.MapGet("api/decode/{message}", (string message) =>
{
   return byteDecodeService.DecodeTripSegmentMessage(stringConvertService.ConvertString(message));
});
app.Run();