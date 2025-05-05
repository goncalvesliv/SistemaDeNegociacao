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

builder.Services.AddSingleton<NegociacoesRepository>(sp =>
{
    var connectionString = "Server=DESKTOP-3IP3FRO;Database=NegociacoesDb;Trusted_Connection=True;TrustServerCertificate=True;";
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
