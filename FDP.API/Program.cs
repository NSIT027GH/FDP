using FDP.API;
using FDP.Application;
using FDP.Application.Address;
using FDP.Application.User;
using FDP.Application.User.Command.CreateUser;
using FDP.Infrastructure;
using FDP.Lib;
using FDP.Shared;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;


var builder = WebApplication.CreateBuilder(args);

// Configure Services
ConfigureServices(builder.Services, builder.Configuration, builder.Environment);

var app = builder.Build();

// Configure Middleware
ConfigureMiddleware(app);

app.Run();

void ConfigureServices(IServiceCollection services, IConfiguration configuration, IWebHostEnvironment environment)
{
    //var environment = builder.Environment;
    // CORS Configuration
    services.AddCors(options =>
    {
        options.AddDefaultPolicy(policy =>
            policy.AllowAnyHeader()
                  .AllowAnyMethod()
                  .AllowAnyOrigin());
    });

    // Authentication & JWT Configuration
    services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            .AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.IncludeErrorDetails = true;
                options.SaveToken = true;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidateAudience = true,
                    ValidAudience = configuration["Jwt:Audience"],
                    ValidIssuer = configuration["Jwt:Issuer"],
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Jwt:Key"] ?? "")),
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,
                };
            });
  
    // Swagger Configuration
    services.AddEndpointsApiExplorer();
    services.AddSwaggerGen(opt =>
    {
        opt.SwaggerDoc("v1", new OpenApiInfo { Title = "MyAPI", Version = "v1" });
        opt.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            In = ParameterLocation.Header,
            Description = "Please enter token",
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            BearerFormat = "JWT",
            Scheme = "bearer"
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
                    }
                },
                Array.Empty<string>()
            }
        });
    });

    // DB Context
    //services.AddDbContext<FdpContext>(options => options.UseSqlServer(configuration["SqlConnectionString"]));
    if (environment.IsEnvironment("Testing"))
    {
        services.AddDbContext<FdpContext>(opt =>
            opt.UseInMemoryDatabase("TestDb_" + Guid.NewGuid()));
            //opt.UseInMemoryDatabase("TestDb_"));
    }
    else
    {
        services.AddDbContext<FdpContext>(options =>
            options.UseSqlServer(configuration["SqlConnectionString"]));
    }

    services.AddHttpContextAccessor();

    // AutoMapper
    services.AddAutoMapper(typeof(AutoMapperConfigClass).Assembly);

    // Repositories and Services
    services.AddScoped(typeof(IGenericRepository<>), typeof(GenericRepository<>));
    services.AddScoped<IAddressService, AddressService>();
    services.AddScoped<IUserService, UserService>();
    services.AddScoped<ILoginUserService, LoginUserService>();



    //services.AddMediatR();
    services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<GetUserByIdQuery>(); });
    //services.AddMediatR(cfg => { cfg.RegisterServicesFromAssemblyContaining<CreateUserCommand>(); });
    services.AddScoped<IRequestHandler<GetUserByIdQuery, UserDetailsResponseModel>, GetUserByIdQueryHandler>();
    services.AddValidatorsFromAssemblyContaining<GetUserByIdQueryValidator>();
    services.AddValidatorsFromAssemblyContaining<CreateUserCommandValidator>();

    services.AddScoped<GetAddressQueryHandler>();
    services.AddScoped<GetAddressByIdQueryHandler>();
    services.AddScoped<GetAddressByIdQueryValidator>();
    services.AddScoped<GetAddressByUserIdQueryHandler>();
    services.AddScoped<GetAddressByUserIdQueryValidator>();
    services.AddScoped<CreateAddressCommandHandler>();
    services.AddScoped<CreateAddressCommandValidator>();
    services.AddScoped<UpdateAddressCommandHandler>();
    services.AddScoped<UpdateAddressCommandValidator>();

    services.AddTransient(typeof(IPipelineBehavior<,>),typeof(ValidationBehavior<,>));
    // Controller
    services.AddControllers();
}

void ConfigureMiddleware(WebApplication app)
{
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    app.UseHttpsRedirection();
    app.UseCors();
    app.UseAuthentication();
    app.UseAuthorization();
    app.MapControllers();
}

public partial class Program { }