using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity;
using NexiusTestTodo.API.Exceptions.Handler;
using NexiusTestTodo.API.TodoItems.GetAllTodoItems;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var assembly = typeof(Program).Assembly;
builder.Services.AddMediatR(config =>
{
    config.RegisterServicesFromAssembly(assembly);
});
builder.Services.AddValidatorsFromAssembly(assembly);

//builder.Services.AddScoped<ITodoItemRepository, FakeRepository>();
builder.Services.AddSingleton<ITodoItemRepository, FakeRepository>(); // NOTE: just for testing purposes

builder.Services.AddCarter();

builder.Services.AddExceptionHandler<CustomExceptionHandler>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapCarter();

app.UseExceptionHandler(options =>
{

});

app.Run();
