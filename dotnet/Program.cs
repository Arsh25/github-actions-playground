var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGet("/info",async() =>
{
    string filePath = "info_file.txt";
    string content;
    try
    {
        content = await File.ReadAllTextAsync(filePath);
    }
    catch (FileNotFoundException e)
    {
        return Results.StatusCode(500);
    }
    return Results.Content(content);
});

app.Run();