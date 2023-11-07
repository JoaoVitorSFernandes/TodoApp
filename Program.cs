using System.Text.Json.Serialization;
using Microsoft.OpenApi.Models;
using TodoApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoContext>();

builder.Services.AddControllers()
.ConfigureApiBehaviorOptions(options =>{
    options.SuppressModelStateInvalidFilter = true;
});


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});


builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo{Title = "TodoApp", Version = "v1"});
});

var app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1"));

app.Run();
