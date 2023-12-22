using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using nba_dotnet;

var builder = WebApplication.CreateBuilder(args);

//! BD
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    {
        options.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection"));
        options.EnableSensitiveDataLogging();
    }
);

//! fix bug object cycle err 500 al hacer peticion lista qe contiene lista dentro
builder.Services.AddControllers().AddJsonOptions( x => 
    x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles 
); 

//! configuracion automapper
builder.Services.AddAutoMapper(typeof(Program));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
