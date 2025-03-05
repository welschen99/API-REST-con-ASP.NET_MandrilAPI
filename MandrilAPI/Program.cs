var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();//servicio de controllers
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline - Configuración del Middleware.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

//Habilita la autenticación/autorización en la API.
app.UseAuthorization();

//Mapea las rutas de los controladores.
app.MapControllers();

app.Run();
