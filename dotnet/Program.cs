var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/info",async() =>
{
    string filePath = "info_file.txt";
    return Results.Content(await File.ReadAllTextAsync(filePath));
});

app.Run();