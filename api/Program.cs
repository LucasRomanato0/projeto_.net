using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World 2!");
app.MapPost("/", () => new { Name = "Lucas Romanato", Age = 22 });
app.MapGet("/AddHeader", (HttpResponse response) =>
{
    response.Headers.Add("Teste", "Lucas Romanato");
    return new { Name = "Lucas Romanato", Age = 22 };
});

app.MapPost("/products", (Product product) =>
{
    ProductRepository.Add(product);
});

//api.app.com/user/{code}
app.MapGet("/products/{code}", ([FromRoute] string code) =>
{
    var product = ProductRepository.GetBy(code);
    return product;
});

app.MapPut("/products", (Product product) =>
{
    var productSave = ProductRepository.GetBy(product.Code);
    productSave.Name = product.Name;
});

app.MapDelete("/products/{code}", ([FromRoute] string code) =>
{
    var productSave = ProductRepository.GetBy(code);
    ProductRepository.Remove(productSave);
});

/*
//api.app.com/users?datestart={date}&dateend={date}
app.MapGet("/getproduct", ([FromQuery] string dateStart, [FromQuery] string dateEnd) =>
{
    return dateStart + " - " + dateEnd;
});
*/

app.MapGet("/getproductheader", (HttpRequest req) =>
{
    return req.Headers["product-code"].ToString();
});

app.Run();

public static class ProductRepository
{
    public static List<Product> Products { get; set; }

    public static void Add(Product product)
    {
        if (Products == null)
            Products = new List<Product>();

        Products.Add(product);
    }

    public static Product GetBy(string code)
    {
        return Products.FirstOrDefault(p => p.Code == code); //retorna null se nao achar nada
    }

    public static void Remove(Product product)
    {
        Products.Remove(product);
    }
}

public class Product
{
    public string Code { get; set; }
    public string Name { get; set; }
}
