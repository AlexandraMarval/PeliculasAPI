using PeliculasAPI.Context;
using PeliculasAPI.Repositorio;
using PeliculasAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSqlServer<PeliculaDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"));
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddTransient<ICategoriaServicio, CategoriaServicio>();
builder.Services.AddTransient<IActorServicio, ActorServicio>();
builder.Services.AddTransient(typeof(IRepositorio<>), typeof(Repositorio<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
