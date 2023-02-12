using AnimalChipization.Api.Configuration;

var builder = WebApplication.CreateBuilder(args);
builder.ConfigureBuilder();

var app = builder.Build();
app.UpdateEnvironmentVariables();
app.ConfigureMiddlewares();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.Run();