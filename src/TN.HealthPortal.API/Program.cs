using System.Text.Json.Serialization;
using TN.HealthPortal.Data.EF;
using TN.HealthPortal.Logic;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options => // TODO: This is not production ready
    options.AddPolicy("CorsPolicy", builder => builder
        .WithOrigins("https://localhost:7106/")
        .AllowAnyMethod()
        .AllowCredentials()
        .SetIsOriginAllowed((host) => true)
        .AllowAnyHeader()
    ));

builder.Services.AddLogicLayer();
builder.Services.AddDataLayer(builder.Configuration);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

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

app.UseRouting();

app.UseCors("CorsPolicy");

app.UseAuthorization();

app.MapControllers();

app.Run();
