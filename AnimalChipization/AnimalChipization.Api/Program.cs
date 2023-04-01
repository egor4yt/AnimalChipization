using AnimalChipization.Api.Configuration;
using AnimalChipization.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureBuilder();

var app = builder.Build();
app.UpdateEnvironmentVariables();
app.ConfigureMiddlewares();

/* auto migrate database */
var scope = app.Services.CreateScope();
var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
dbContext.Database.Migrate();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();