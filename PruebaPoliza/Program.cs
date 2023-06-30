using API.Abstract;
using Domain.Middlewares;
using Domain.Settings;
using MediatR;
using Service.EventHandlers.Profiles;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddCustomSwagger();
builder.Services.Configure<PolizaDatabaseSettings>(
    builder.Configuration.GetSection("PolizaStoreDatabase")
)
    .AddMediatR(Assembly.Load("Service.EventHandlers"))
    .AddAutoMapper(typeof(Program), typeof(UserProfile))
    .RegisterServices()
    .AddCustomAuthentication(builder.Configuration);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();
