using TodoApp.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<TodoContext>();
builder.Services.AddControllers();

var app = builder.Build();

app.MapControllers();

app.Run();
