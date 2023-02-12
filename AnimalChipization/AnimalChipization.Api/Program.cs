using AnimalChipization.Api.Configuration;
using AnimalChipization.Data;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureBuilder();

var app = builder.Build();
app.UpdateEnvironmentVariables();
app.Services.GetRequiredService<ApplicationDbContext>().Database.Migrate();
app.ConfigureMiddlewares();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();