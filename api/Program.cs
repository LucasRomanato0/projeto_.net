var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World 2!");
app.MapPost("/", () => new { Name = "Lucas Romanato", Age = 22 });
app.MapGet("/AddHeader", (HttpResponse response) =>
{
    response.Headers.Add("Teste", "Lucas Romanato");
    return new { Name = "Lucas Romanato", Age = 22 };
});

app.MapPost("/saveproduct", (Product product) =>
{
    return product.Code + " - " + product.Name;
});

app.Run();

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
}
