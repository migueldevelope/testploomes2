using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.PlatformAbstractions;
using Microsoft.IdentityModel.Tokens;
using TestPloomes.Core.Interfaces;
using TestPloomes.Data;
using TestPloomes.Services.Auth;
using Tools;

namespace TestPloomes.Api
{
  /// <summary>
  /// Controle de inicialização da API
  /// </summary>
  public class Startup
  {
    /// <summary>
    /// Construtor do controle
    /// </summary>
    /// <param name="configuration"></param>
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }
    /// <summary>
    /// Propriedade de configuração
    /// </summary>
    public IConfiguration Configuration { get; }
    private const string Secret = "db3OIsj+BXE9NZDyjg8f3TcNekrF+2d/1sFnWG4HnV8TZY30r2k0tVWJG8abWvB1GlOgJuQZdcF2Luqm/hccMw==";

    /// <summary>
    /// Registrador de serviços
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    public void RegistreServices(IServiceCollection services)
    {
      DataContext _context = new DataContext("Server=testploomes.database.windows.net;Database=testploomes;User Id=test;Password=x14r53p5!a;");
      _ = services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
      _ = services.AddScoped<IServiceAuthentication>(_ => new ServiceAuthentication(_context));
      _ = services.AddScoped<IServiceUser>(_ => new ServiceUser(_context));
      _ = services.AddScoped<IServiceHQ>(_ => new ServiceHQ(_context));
    }
    // This method gets called by the runtime. Use this method to add services to the container.
    /// <summary>
    /// Configurador de servicos
    /// </summary>
    /// <param name="services">Coleção de serviços</param>
    public void ConfigureServices(IServiceCollection services)
    {
      _ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
      .AddJwtBearer(options =>
      {
        options.TokenValidationParameters = new TokenValidationParameters
        {
          ValidateIssuer = false,
          ValidateAudience = false,
          ValidateLifetime = true,
          ValidateIssuerSigningKey = true,
          IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret))
        };
        options.Events = new JwtBearerEvents
        {
          OnAuthenticationFailed = context =>
          {
            Console.WriteLine("OnAuthenticationFailed: " + context.Exception.Message);
            return Task.CompletedTask;
          },
          OnTokenValidated = context =>
          {
            Console.WriteLine("OnTokenValidated: " + context.SecurityToken);
            return Task.CompletedTask;
          }
        };
      });
      _ = services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
                  builder => builder.WithOrigins("http://localhosts/*", "10.0.0.16", "https://fluidstate.com.br/*", "https://dev.fluidstate.com.br/*", "https://hml.fluidstate.com.br/*")
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  //.WithExposedHeaders("x-total-count")
                  .AllowCredentials());
      });
      _ = services.AddRazorPages();
      _ = services.AddControllersWithViews().SetCompatibilityVersion(CompatibilityVersion.Version_3_0);
      // Configurando o serviço de documentação do Swagger
      _ = services.AddSwaggerGen(c =>
      {
        c.SwaggerDoc("v1",
            new Microsoft.OpenApi.Models.OpenApiInfo
            {
              Title = "TestPloomes",
              Version = "v1",
              Description = "Sistema de HQs"
            });
        string caminhoAplicacao = PlatformServices.Default.Application.ApplicationBasePath;
        string nomeAplicacao = PlatformServices.Default.Application.ApplicationName;
        string caminhoXmlDoc = Path.Combine(caminhoAplicacao, $"{nomeAplicacao}.xml");
        c.IncludeXmlComments(caminhoXmlDoc);
      });
      RegistreServices(services);
    }
    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    /// <summary>
    /// Configuração de aplicação
    /// </summary>
    /// <param name="app">Aplicação</param>
    public void Configure(IApplicationBuilder app)
    {
      _ = app.UseRouting();
      _ = app.UseCors(options => options.SetIsOriginAllowed(x => _ = true).AllowAnyMethod().AllowAnyHeader().AllowCredentials().WithExposedHeaders("x-total-count"));
      _ = app.UseAuthentication();
      _ = app.UseAuthorization();
      _ = app.UseEndpoints(endpoints =>
      {
        endpoints.MapDefaultControllerRoute();
        endpoints.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });
      // Ativando middlewares para uso do Swagger
      _ = app.UseSwagger();
      _ = app.UseSwaggerUI(c =>
      {
        c.RoutePrefix = "help";
        c.SwaggerEndpoint("../swagger/v1/swagger.json", "TestPloomes");
      });
    }
  }
}
