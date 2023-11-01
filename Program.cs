using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Swagger;
using TodoApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoContext>();
builder.Services.AddControllers();

builder.Services.AddSwaggerGen(c => {
    c.SwaggerDoc("v1", new OpenApiInfo{Title = "TodoApp", Version = "v1"});
});

var app = builder.Build();

app.MapControllers();

app.UseSwagger();
app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.jason", "v1"));

app.Run();
