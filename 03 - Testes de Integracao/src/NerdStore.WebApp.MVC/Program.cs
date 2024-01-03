using NerdStore.WebApp.MVC.Configurations;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddConfigureServices(builder.Configuration);

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseConfigureServices(app.Environment);

app.Run();
