using CharityAuction.Application.Interfaces.Logging;
using CharityAuction.Application.Services.Logger;
using CharityAuction.Infrastructure.Options;
using CharityAuction.Infrastructure.Persistence;
using CharityAuction.Infrastructure.Repositories.Interfaces.Base;
using CharityAuction.Infrastructure.Repositories.Realizations.Base;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using Microsoft.AspNetCore.Identity;
using CharityAuction.Domain.Entities;
using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using CharityAuction.Application.Interfaces.User;
using CharityAuction.Application.Services.User.JWT;
using CharityAuction.Application.Services.User;
using CharityAuction.Application;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Identity.UI.Services;
using CharityAuction.Infrastructure.Services;
using CharityAuction.Application.Interfaces;
using CharityAuction.Application.Services;
using CharityAuction.Infrastructure.Repositories.Interfaces;
using CharityAuction.Infrastructure.Repositories;
using Hangfire;
using Hangfire.PostgreSql;
using Npgsql;
using Microsoft.AspNetCore.Authentication.Cookies;
using CharityAuction.SignalR;
using CharityAuction.SignalR.Services;
using CharityAuction.Application.Interfaces.Admin;
using CharityAuction.Application.Services.Admin;
using CharityAuction.Payment.Interfaces;
using CharityAuction.Application.Services.Email;


namespace CharityAuction.WebAPI.Extensions
{
    public static class ServiceCollectionExtension
    {
        public static void AddRepositoryServices(this IServiceCollection services)
        {
            services.AddScoped<IRepositoryWrapper, RepositoryWrapper>();
            services.AddScoped<IAuctionRepository, AuctionRepository>();
            services.AddScoped<IBidRepository, BidRepository>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
        }

        public static void AddCustomServices(this IServiceCollection services)
        {
            services.AddRepositoryServices();
            services.AddHttpClient();
            services.AddScoped<ILoggerService, LoggerService>();
            services.AddScoped<IClaimsService, ClaimsService>();
            services.AddScoped<ITokenService, TokenService>();
            services.AddScoped<ICurrentUserService, CurrentUserService>();
            services.AddScoped<IUserService,  UserService>();
            services.AddScoped<IEmailSender, SmtpEmailSender>();

            services.AddScoped<IGoogleAuthService, GoogleAuthService>();
            services.AddScoped<IAuctionService,AuctionService>();
            services.AddScoped<IAuctionClosingService, AuctionClosingService>();
            services.AddScoped<IBidService, BidService>();
            services.AddScoped<INotificationService, NotificationService>();
            services.AddScoped<INotificationSender, NotificationSender>();
            services.AddScoped<IAdminService, AdminService>();
            services.AddScoped<IStatsService, StatsService>();
            services.AddHttpClient<IFacebookAuthService, FacebookAuthService>();
            services.AddScoped<IPaymentService, FondyPaymentService>();
            services.AddScoped<IPaymentService, LiqPayService>();
            services.AddScoped<IPaymentServiceStrategy, PaymentServiceStrategy>();
            services.AddScoped<IGoogleTokenValidator, GoogleTokenValidator>();
            services.AddScoped<ISmtpClient, SmtpClientWrapper>();
            services.AddScoped<Func<ISmtpClient>>(sp => () => sp.GetRequiredService<ISmtpClient>());

            // services.AddHttpClient<IContentModerationService, OpenAiModerationService>();
            services.AddHttpClient<IContentModerationService, PerspectiveModerationService>();
            var currentAssemblies = AppDomain.CurrentDomain.GetAssemblies();
            services.AddAutoMapper(currentAssemblies);
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblies(currentAssemblies));
            services.AddSignalRServices();

        }

        public static void AddApplicationServices(this IServiceCollection services, ConfigurationManager configuration)
        {
            var environment = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT") ?? "Local";

            services.AddHttpClient();
            services.Configure<ConnectionStringsOptions>(configuration.GetSection(ConnectionStringsOptions.SectionName));
            services.Configure<JwtSettingsOptions>(configuration.GetSection(JwtSettingsOptions.SectionName));
            services.Configure<EmailSettingsOptions>(configuration.GetSection(EmailSettingsOptions.SectionName));
            services.Configure<LiqPayOptions> (configuration.GetSection(LiqPayOptions.SectionName));
            services.Configure<FondyPayOptions>(configuration.GetSection(FondyPayOptions.SectionName));
            // services.Configure<OpenAIOptions>(configuration.GetSection(OpenAIOptions.SectionName));
            

            services.Configure<PerspectiveApiOptions>(configuration.GetSection(PerspectiveApiOptions.SectionName));
            services.AddDbContext<ApplicationDbContext>((provider, options) =>
            {
                var dbOptions = provider.GetRequiredService<IOptionsSnapshot<ConnectionStringsOptions>>().Value;
                options.UseNpgsql(dbOptions.DefaultConnection);
            });

            services.AddScoped<ApplicationDbContext>();

            services.AddApplicationServices();
            services.AddFluentValidationAutoValidation();
            services.AddLogging();
            services.AddControllers();
        }
        public static void AddIdentityServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddIdentity<ApplicationUser, IdentityRole>( o =>
            {
                o.Password.RequireNonAlphanumeric = false;
                o.Password.RequireDigit = true;
                o.Password.RequireLowercase = true;
                o.Password.RequireUppercase = false;
                o.Password.RequiredLength = 8;
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

        }
        public static void AddAuthServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddOptions<JwtSettingsOptions>()
                .Bind(configuration.GetSection(JwtSettingsOptions.SectionName))
                .ValidateDataAnnotations()
                .ValidateOnStart();

            services.AddSingleton<IValidateOptions<JwtSettingsOptions>, ValidateJwtSettingsOptions>();

            // take a settings from jwt
            var jwtSettings = configuration.GetSection(JwtSettingsOptions.SectionName).Get<JwtSettingsOptions>();

            if (jwtSettings == null || string.IsNullOrEmpty(jwtSettings.Secret))
            {
                throw new InvalidOperationException("JWT Secret key is missing in configuration.");
            }

            var key = Encoding.UTF8.GetBytes(jwtSettings.Secret);

            var googleSettings = configuration.GetSection(GoogleAuthOptions.SectionName).Get<GoogleAuthOptions>();

            if (googleSettings == null || string.IsNullOrEmpty(googleSettings.ClientSecret))
            {
                throw new InvalidOperationException("Google Secret key is missing in configuration.");
            }

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme, options =>
            {
                options.LoginPath = "/login";
                options.AccessDeniedPath = "/accessdenied";
            })
            .AddJwtBearer(options =>
            {

                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    ValidIssuer = jwtSettings.Issuer,
                    ValidAudience = jwtSettings.Audience,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtSettings.Secret)),
                    ClockSkew = TimeSpan.Zero // ????? ????? ????? ???? ????? ??? ??????!
                };
                options.Events = new JwtBearerEvents
                {
                    OnMessageReceived = context =>
                    {
                        // ??????? ??????? ??????? ?? ?????????
                        if (context.Request.Headers.TryGetValue("Authorization", out var authorizationHeader))
                        {
                            context.Token = authorizationHeader.ToString().Split(" ").Last();
                        }

                        // ???? ??? ?????? ? ?????????, ???? ? ????
                        if (string.IsNullOrEmpty(context.Token) && context.Request.Cookies.TryGetValue("AuthToken", out var cookieToken))
                        {
                            context.Token = cookieToken;
                        }

                        return Task.CompletedTask;
                    }
                };
            })
            .AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = googleSettings.ClientId;
                googleOptions.ClientSecret = googleSettings.ClientSecret;
            });

            services.AddAuthorization();
        }

        public static void AddSwaggerServices(this IServiceCollection services)
        {
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen(opt =>
            {
                opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyApi", Version = "v1" });
                opt.CustomSchemaIds(x => x.FullName);

                opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = ParameterLocation.Header,
                    Description = "Please enter a valid token in the following format: {your token here} do not add the word 'Bearer' before it."
                });
                opt.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                        new OpenApiSecurityScheme
                        {
                            Reference = new OpenApiReference
                            {
                                Type = ReferenceType.SecurityScheme,
                                Id = "Bearer"
                            },
                            Scheme = "oauth2",
                            Name = "Bearer",
                            In = ParameterLocation.Header,
                        },
                        new List<string>()
                    }
                });
            });
        }
        public static void AddHangfireServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddHangfire((provider, config) =>
            {
                var connectionStringsOptions = provider.GetRequiredService<IOptions<ConnectionStringsOptions>>().Value;
                var connectionString = connectionStringsOptions.DefaultConnection;

                config.SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                      .UseSimpleAssemblyNameTypeSerializer()
                      .UseRecommendedSerializerSettings()
                      .UsePostgreSqlStorage(connectionString);
            });

            services.AddHangfireServer();
        }
    }
}

