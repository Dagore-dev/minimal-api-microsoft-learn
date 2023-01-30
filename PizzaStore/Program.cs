using Microsoft.OpenApi.Models;
using PizzaStore.DB;

internal class Program
{
  private static void Main(string[] args)
  {
    var builder = WebApplication.CreateBuilder(args);

    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(config => 
    {
      config.SwaggerDoc("v1", new OpenApiInfo {Title = "PizzaStore API", Description = "Making the pizzas you love", Version = "v1"});
    });

    var app = builder.Build();

    app.UseSwagger();
    app.UseSwaggerUI(config => config.SwaggerEndpoint("/swagger/v1/swagger.json", "Todo API V1"));

    app.MapGet("/", () => "Hello World!");
    app.MapGet("/pizzas", () => PizzaDB.GetPizzas());
    app.MapGet("/pizzas/{id}", (int id) => PizzaDB.GetPizza(id));
    app.MapPost("/pizzas", (Pizza pizza) => PizzaDB.CreatePizza(pizza));
    app.MapPut("/pizzas", (Pizza updatedPizza) => PizzaDB.UpdatePizza(updatedPizza));
    app.MapDelete("/pizzas/{id}", (int id) => PizzaDB.RemovePizza(id));

    app.Run();
  }
}