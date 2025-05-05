using OrdemApi.Repositories;
using OrdemApi.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


var connectionString = builder.Configuration.GetConnectionString("NegociacoesDb");

builder.Services.AddSingleton<NegociacoesRepository>(sp =>
{
    return new NegociacoesRepository(connectionString);
});




builder.Services.AddSingleton<IRabbitMqService, RabbitMqService>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();


if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCors("AllowAll");
app.UseAuthorization();
app.MapControllers();
app.Run();
