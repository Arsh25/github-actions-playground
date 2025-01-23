using Newtonsoft.Json;

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
app.MapGet("/cat",async() =>
{
    string apiBase = "https://cataas.com/";
    string randomEndpoint = "cat?json=true";
    HttpClient client = new HttpClient();
    using HttpResponseMessage response = await client.GetAsync(apiBase+randomEndpoint); 
    string jsonStr = await response.Content.ReadAsStringAsync();
    dynamic json = JsonConvert.DeserializeObject(jsonStr);
    string catUrl = apiBase + "cat/"+json._id;
    string htmlStr = "<img id=\"cat_pic\" src=\"" + catUrl + "\">"+catUrl+"</img>";
    return Results.Content(htmlStr," text/html");
    
});

app.Run();