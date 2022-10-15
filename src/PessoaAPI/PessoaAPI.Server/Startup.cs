using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UPDown.Common.PessoaAPI;

namespace PessoaAPI.Server
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            _ = services.AddCors(Options =>
                            Options.AddPolicy("AllowOrigion",
                                    builder =>
                                    {
                                        _ = builder.AllowAnyOrigin()
                                                .AllowAnyHeader()
                                                .AllowAnyMethod();
                                    }));

            _ = services.AddControllers();
            _ = services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PessoaAPI.Server", Version = "v1" });
            });

            _ = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = AuthenticationDTO.jwtIssuer,
                    ValidAudience = AuthenticationDTO.jwtAudience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(AuthenticationDTO.jwtKey))
                };

                options.Events = new JwtBearerEvents
                {
                    OnTokenValidated = AdditionalValidation
                };
            });

            _ = services.AddDbContext<Data.DatabaseContext>(options =>
            {
                _ = options.UseSqlite("Data Source = PessoaAPI.db");
            });
        }

        private static async Task<Task> AdditionalValidation(TokenValidatedContext context)
        {
            string userType = context.Principal.Claims.Where(e => e.Type == "UserType").FirstOrDefault().Value.ToString();
            string login = context.Principal.Identity.Name;

            if (string.IsNullOrWhiteSpace(userType))
            {
                context.Fail("Failed additional validation");
            }

            return Task.CompletedTask;
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                _ = app.UseDeveloperExceptionPage();
                _ = app.UseSwagger();
                _ = app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "UPDown.Server v1"));
            }

            _ = app.UseRouting();

            _ = app.UseCors("AllowOrigion");

            _ = app.UseAuthentication();

            _ = app.UseAuthorization();

            _ = app.UseEndpoints(endpoints =>
            {
                _ = endpoints.MapControllers();
            });
        }
    }
}
