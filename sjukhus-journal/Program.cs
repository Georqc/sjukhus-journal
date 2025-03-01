using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using sjukhus_journal.Models;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<JournalDbContext>(options =>
    options.UseSqlite(builder.Configuration.GetConnectionString("DefaultConnection") ?? "Data Source=journal.db"));

builder.Services.AddControllers();
builder.Services.AddOpenApi();

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowFrontend",
        policy =>
        {
            policy.WithOrigins("http://localhost:5175") // Lägg till frontend-URL
                .AllowAnyHeader()
                .AllowAnyMethod();
        });
});



var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<JournalDbContext>();
    context.Database.EnsureCreated(); 

    if (!context.Journaler.Any())
    {
        context.Journaler.AddRange(new List<Journal>
        {
            new Journal { PatientId = 1, Anteckning = "Vi testar"},
            new Journal { PatientId = 2, Anteckning = "Andra anteckningen" },
            new Journal { PatientId = 3, Anteckning = "Tredje anteckningen" },
        });
        context.SaveChanges(); 
    }
}

app.UseCors("AllowFrontend");

app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference(options =>
    {
        options
            .WithTitle("Sjukhus Journal Databas")
            .WithTheme(ScalarTheme.Mars); 
    });
}

app.Run();