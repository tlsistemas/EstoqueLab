using EstoqueLab.Application;
using EstoqueLab.Data.Contexts;
using EstoqueLab.Uteis.Filter;
using EstoqueLab.Uteis.Options.Swagger;
using EstoqueLab.Uteis.Swagger.Filters;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using Newtonsoft.Json;
using System.Globalization;
using System.IO.Compression;

namespace EstoqueLab.Api
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "AllowSpecificOrigin";
        SwaggerOptions swaggerOptions = new SwaggerOptions();

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
            Configuration.GetSection("Swagger").Bind(swaggerOptions);
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            #region Infrastructure
            services.AddHttpContextAccessor();
            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
                options.Providers.Add<GzipCompressionProvider>();
            });
            services.Configure<GzipCompressionProviderOptions>(options =>
            {
                options.Level = CompressionLevel.Fastest;
            });

            //services.AddScoped<IEnviarEmail, SmtpEnviarEmail>();
            #endregion

            #region Context
            var connectionString = Configuration.GetConnectionString("DefaultConnection");
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 29));
            services.AddDbContext<LabContext>(
            dbContextOptions => dbContextOptions
                .UseMySql(connectionString, serverVersion)
                .LogTo(Console.WriteLine, LogLevel.Information)
                .EnableSensitiveDataLogging()
                .EnableDetailedErrors()
                );
            #endregion

            DependenciesInjector.Register(services);

            services.Configure<IISServerOptions>(options =>
            {
                options.AutomaticAuthentication = false;
            });


            services.AddControllers();

            ConfigureSwagger(services);
            ConfigureLocalization(services);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "C2P.API v1"));

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }

        #region Custom Configuration
        private void ConfigureSwagger(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });
            services.AddMvc(config =>
            {
                config.Filters.Add(typeof(CustomExceptionFilter));
            })
            .AddNewtonsoftJson(options =>
            {
                options.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
            }
            ).SetCompatibilityVersion(CompatibilityVersion.Version_3_0);


            services.AddMvcCore().AddApiExplorer();
            services.AddSwaggerGen(options =>
            {
                services.ConfigureSwaggerGen(options =>
                {
                    options.CustomSchemaIds(x => x.FullName);
                });
                options.CustomSchemaIds(type => type.ToString());
                options.EnableAnnotations();
                options.SwaggerDoc(swaggerOptions.APIVersion, new OpenApiInfo
                {
                    Version = swaggerOptions.APIVersion,
                    Title = swaggerOptions.APIName,
                    Description = swaggerOptions.Description,
                    Contact = new OpenApiContact
                    {
                        Name = "AutoLab.API",
                        Email = String.Empty,
                    },
                    License = new OpenApiLicense
                    {
                        Name = "Use under LICX",
                    }
                });


                var xmlFile = $"{System.Reflection.Assembly.GetExecutingAssembly().GetName().Name}.xml";
                var xmlPath = Path.Combine(AppContext.BaseDirectory, xmlFile);
                options.IncludeXmlComments(xmlPath);

                options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Bearer",
                    BearerFormat = "JWT",
                    Scheme = "bearer",
                    Description = "Specify the authorization token.",
                    In = ParameterLocation.Header,
                    Type = SecuritySchemeType.Http,
                });

                options.AddSecurityRequirement(new OpenApiSecurityRequirement {
               {
                 new OpenApiSecurityScheme
                 {
                   Reference = new OpenApiReference
                   {
                     Type = ReferenceType.SecurityScheme,
                     Id = "Bearer"
                   }
                  },
                  new String[] { }
                }
              });

                //options.DescribeAllEnumsAsStrings();
                options.OperationFilter<SecurityRequirementsOperationFilter>();
                options.OperationFilter<ReplaceFileParamOperationFilter>();
                options.OperationFilter<FormDataOperationFilter>();
            });
        }
        private void ConfigureLocalization(IServiceCollection services)
        {
            services.Configure<RequestLocalizationOptions>(options =>
            {
                options.DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture("pt-BR");
                options.SupportedCultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
                options.SupportedUICultures = new List<CultureInfo> { new CultureInfo("pt-BR") };
                options.RequestCultureProviders.Clear();
            });
        }
        #endregion

    }
}
