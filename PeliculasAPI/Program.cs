using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using NetTopologySuite;
using NetTopologySuite.Geometries;
using PeliculasAPI.AutoMapper;
using PeliculasAPI.Ayudantes;
using PeliculasAPI.Context;
using PeliculasAPI.Repositorio;
using PeliculasAPI.Servicios;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSqlServer<PeliculaDbContext>(builder.Configuration.GetConnectionString("DefaultConnection"),
    sqlServerOptions => sqlServerOptions.UseNetTopologySuite());
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddControllers().AddNewtonsoftJson();
builder.Services.AddSingleton<GeometryFactory>(NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326));
builder.Services.AddSingleton(provider =>
    new MapperConfiguration(config =>
    {
        var geometryFactory = provider.GetRequiredService<GeometryFactory>();
        config.AddProfile(new AutoMapperProfiles(geometryFactory));
    }).CreateMapper()
);
builder.Services.AddIdentity<IdentityUser, IdentityRole>()
    .AddEntityFrameworkStores<PeliculaDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = false,
        ValidateAudience = false,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["jwt:Key"])),
        ClockSkew = TimeSpan.Zero
    });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services
    .AddEndpointsApiExplorer()
    .AddSwaggerGen(options =>
    {
        options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
            BearerFormat = "jwt",
            In = ParameterLocation.Header
        });

        options.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {  new OpenApiSecurityScheme
               {
                   Reference = new OpenApiReference
                   {
                      Type = ReferenceType.SecurityScheme,
                      Id = "Bearer"
                   }
               },
               new string[]{}
            }
        });

    })
    .AddTransient<ICategoriaServicio, CategoriaServicio>()
    .AddTransient<IActorServicio, ActoresServicio>()
    .AddTransient<IPeliculaServicio, PeliculaServicio>()
    .AddTransient<IPeliculaRepositorio, PeliculaRepositorio>()
    .AddTransient<ISalaDeCineServicio, SalaDeCineServicio>()
    .AddTransient<ISalaDeCineRepositorio, SalaDeCineRepositorio>()
    .AddTransient<ICuentaServicio, CuentaServicio>()
    .AddTransient<IUsuarioRepositorio, UsuarioRepositorio>()
    .AddTransient<IReseñaServicio, ReseñaServicio>()
    .AddTransient<IReseñaRepositorio, ReseñaRepositorio>()
    .AddTransient(typeof(IRepositorio<>), typeof(Repositorio<>));
builder.Services.AddScoped<PeliculaExisteAttributo>();
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
