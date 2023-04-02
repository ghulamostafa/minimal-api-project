using GetStartedDigitalOceanDotNET001;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = 
    "Server=digitalocean-db-do-user-6906066-0.b.db.ondigitalocean.com;" +
    "Port=25060;" +
    "Database=defaultdb;" +
    "Uid=doadmin;" +
    "Pwd=AVNS_h2LAREw0KACGaaN0SzH;" +
    "SslMode=Require;" +
    "Trust Server Certificate=true";

builder
    .Services
    .AddDbContext<DigitalOceanDbContext>
    (options => options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
       new WeatherForecast
       (
           DateTime.Now.AddDays(index),
           Random.Shared.Next(-20, 55),
           summaries[Random.Shared.Next(summaries.Length)]
       ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

app.MapPost("/add-note", async (Note note, DigitalOceanDbContext db) =>
{
    await db.AddAsync(note);
    await db.SaveChangesAsync();
});

app.MapGet("/get-notes", async (DigitalOceanDbContext db) =>
{
    var notes = await db.Notes.ToListAsync();
    return notes;
});

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}