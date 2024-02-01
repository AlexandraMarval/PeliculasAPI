using FluentAssertions;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Moq;
using PeliculasApi.Tests.PruebasUnitarias;
using PeliculasAPI.Modelos;
using PeliculasAPI.Repositorio;
using PeliculasAPI.Servicios;
using System;
using Xunit;

namespace PeliculasApi.Tests.Servicio
{
    public class CuentasControllerTests : BasePruebas
    {
        [Fact]
        public async Task CrearUsuario()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            await CrearUsuarioHelper(nombreBD);
            var context2 = ConstruirContext(nombreBD);
            var conteo = await context2.Users.CountAsync();
           // assert  Verificar
           Assert.Equal(1, conteo);
        }

        [Fact]
        public async Task UsuarioNoPuedeLoguearse()
        {
            //arrange Preparar
            var nombreBD = Guid.NewGuid().ToString();
            await CrearUsuarioHelper(nombreBD);
            
            //Act Ejecutar
            var servicio = ConstruirCuentasServicio(nombreBD);
            var userInfo = new CredencialesUsuario() { Email = "ejemplo@hotmail.com", Password = "malPassword" };
            var accion = () => servicio.Login(userInfo);
            
            // assert  Verificar
            await accion.Should().ThrowAsync<Exception>()
               .WithMessage("Login Incorrecto");
            accion.Should().NotBeNull();
        }

        [Fact]
        public async Task UsuarioPuedeLoguearse()
        {
            //arrange Preparar

            var nombreBD = Guid.NewGuid().ToString();
            await CrearUsuarioHelper(nombreBD);

            var servicio = ConstruirCuentasServicio(nombreBD);
            var userInfo = new CredencialesUsuario() { Email = "ejemplo@hotmail.com", Password = "Aa123456!"};

            //Act Ejecutar

            var resultado = await servicio.Login(userInfo);

            // assert  Verificar

            resultado.Token.Should().NotBeNull();
        }

        private async Task CrearUsuarioHelper(string nombreBD)
        {
            //arrange Preparar
            var cuentasServicio = ConstruirCuentasServicio(nombreBD);
            //Act Ejecutar
            var userInfo = new CredencialesUsuario() { Email = "ejemplo@hotmail.com", Password = "Aa123456!" };
            await cuentasServicio.Registrar(userInfo);
        }

        private CuentaServicio ConstruirCuentasServicio(string nombreBD)
        {
            var context = ConstruirContext(nombreBD);          
            var miUserStore = new UserStore<IdentityUser>(context);
            var userManager = BuildUserManager(miUserStore);
            var mapper = ConfigurarAutoMapper();

            var repositorioMock = new Mock<IUsuarioRepositorio>(); 

            var httpContext = new DefaultHttpContext();
            MockAutenticacion(httpContext);
            var signInManager = SetupSignInManager(userManager, httpContext);

            var miConfiguracion = new Dictionary<string, string>
            {
                {"JWT:Key", "A1ex@ndraIs@be1Marva1@costa12345-1995_24897734$%28-Tauro%casada-hijos2.16-05-1995_Mayo"}
            };

            var configuracion = new ConfigurationBuilder().AddInMemoryCollection(miConfiguracion).Build();

            return new CuentaServicio(configuracion, userManager, signInManager, repositorioMock.Object, mapper);
        }

        // Source: https://github.com/dotnet/aspnetcore/blob/master/src/Identity/test/Shared/MockHelpers.cs
        // Source: https://github.com/dotnet/aspnetcore/blob/master/src/Identity/test/Identity.Test/SignInManagerTest.cs
        // Some code was modified to be adapted to our project.

        private UserManager<TUser> BuildUserManager<TUser>(IUserStore<TUser>? store = null) where TUser : class
        {
            store = store ?? new Mock<IUserStore<TUser>>().Object;
            var options = new Mock<IOptions<IdentityOptions>>();
            var idOptions = new IdentityOptions();
            idOptions.Lockout.AllowedForNewUsers = false;

            options.Setup(o => o.Value).Returns(idOptions);

            var userValidators = new List<IUserValidator<TUser>>();

            var validator = new Mock<IUserValidator<TUser>>();
            userValidators.Add(validator.Object);
            var pwdValidators = new List<PasswordValidator<TUser>>();
            pwdValidators.Add(new PasswordValidator<TUser>());

            var userManager = new UserManager<TUser>(store, options.Object, new PasswordHasher<TUser>(),
                userValidators, pwdValidators, new UpperInvariantLookupNormalizer(),
                new IdentityErrorDescriber(), null,
                new Mock<ILogger<UserManager<TUser>>>().Object);

            validator.Setup(v => v.ValidateAsync(userManager, It.IsAny<TUser>()))
                .Returns(Task.FromResult(IdentityResult.Success)).Verifiable();

            return userManager;
        }

        private static SignInManager<TUser> SetupSignInManager<TUser>(UserManager<TUser> manager,
            HttpContext context, ILogger? logger = null, IdentityOptions? identityOptions = null,
            IAuthenticationSchemeProvider? schemeProvider = null) where TUser : class
        {
            var contextAccessor = new Mock<IHttpContextAccessor>();
            contextAccessor.Setup(a => a.HttpContext).Returns(context);
            identityOptions = identityOptions ?? new IdentityOptions();
            var options = new Mock<IOptions<IdentityOptions>>();
            options.Setup(a => a.Value).Returns(identityOptions);
            var claimsFactory = new UserClaimsPrincipalFactory<TUser>(manager, options.Object);
            schemeProvider = schemeProvider ?? new Mock<IAuthenticationSchemeProvider>().Object;
            var sm = new SignInManager<TUser>(manager, contextAccessor.Object, claimsFactory, options.Object, null, schemeProvider, new DefaultUserConfirmation<TUser>());
            sm.Logger = logger ?? (new Mock<ILogger<SignInManager<TUser>>>()).Object;
            return sm;
        }

        private Mock<IAuthenticationService> MockAutenticacion(HttpContext context)
        {
            var autenticacion = new Mock<IAuthenticationService>();
            context.RequestServices = new ServiceCollection().AddSingleton(autenticacion.Object).BuildServiceProvider();
            return autenticacion;
        }
    }
}
