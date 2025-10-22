using rpecot_lab_07.Configuration;
using rpecot_lab_07.Logic;
using rpecot_lab_07.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.Configure<DBConfiguration>(builder.Configuration.GetSection("Database"));

builder.Services.AddSingleton<ICityRepository, CitySQLRepository>();

builder.Services.AddSingleton<ICityLogic, CityLogic>();

builder.Services.Configure<ValidationConfiguration>(builder.Configuration.GetSection("Validation"));

builder.Services.AddCors(p => p.AddPolicy("cors_policy_allow_all", builder =>
{
    builder.WithOrigins("*").AllowAnyMethod().AllowAnyHeader();
}));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("cors_policy_allow_all");

app.UseAuthorization();

app.MapControllers();

app.Run();
