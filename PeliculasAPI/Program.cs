using Microsoft.EntityFrameworkCore;
using PeliculasAPI.Context;
using PeliculasAPI.Repositorio;
using PeliculasAPI.Servicios;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSqlServer<PeliculaDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptions => sqlServerOptions.UseNetTopologySuite());
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers().AddNewtonsoftJson();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen()
    .AddTransient<ICategoriaServicio, CategoriaServicio>()
    .AddTransient<IActorServicio, ActorServicio>()
    .AddTransient<IPeliculaServicio, PeliculaServicio>()
    .AddTransient<IPeliculaRepositorio, PeliculaRepositorio>()
    .AddTransient<ISalaDeCineServicio, SalaDeCineServicio>()
    .AddTransient(typeof(IRepositorio<>), typeof(Repositorio<>));
//builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivosAzure>();
builder.Services.AddTransient<IAlmacenadorArchivos, AlmacenadorArchivoLocal>();
builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();   

app.UseAuthorization();

app.MapControllers();

app.Run();
