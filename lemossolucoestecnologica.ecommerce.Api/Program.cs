using lemossolucoestecnologia.ecommerce.Data.Contexto;
using lemossolucoestecnologia.ecommerce.Domain.Entities.Account;
using lemossolucoestecnologia.ecommerce.Domain.Interfaces;
using lemossolucoestecnologia.ecommerce.Reposioty.Interface;
using lemossolucoestecnologia.ecommerce.Reposioty.Repositories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using System.Text;

#region Author
/*
  CREATE BY EDINALDO DOS SANTOS
  DATE: 08/01/2022
 */
#endregion
var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
string strConnection = builder.Configuration.GetConnectionString("DefaultConnection"); // obj Connection
var secret = Encoding.ASCII.GetBytes(builder.Configuration.GetSection("JwtConfigurations:Secret").Value); // Get token of the file appSetting
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(strConnection);
});

builder.Services.AddIdentity<Users, IdentityRole>(options =>
{
    options.SignIn.RequireConfirmedEmail = true;
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Lockout.AllowedForNewUsers = true;
   
    
})
    .AddEntityFrameworkStores<DataContext>()
    .AddDefaultTokenProviders();

IdentityBuilder identityBuilder = builder.Services.AddIdentityCore<Users>(options =>
    {
        /*He we set our setting to user to password.
         Case you prefer just set like false*/
        options.Password.RequireDigit = true;
        options.Password.RequiredLength = 6;
        options.Password.RequireNonAlphanumeric = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireLowercase = true;

    }
);
#region Scoped -> RelationShip between Interface and Repositoty

builder.Services.AddScoped<IUserServices, UserRepository>(); 
builder.Services.AddScoped<IAuthenticationServices, JwtServicesRepository>();
builder.Services.AddScoped<IProductsServices, ProductsRepository>();
#endregion

builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies()); // Setting to AutoMapper

builder.Services.AddControllers()
    .ConfigureApiBehaviorOptions(opt =>
    {
        opt.SuppressModelStateInvalidFilter = true;
    });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
         { 
            Description = "JWT Authorization header using the Bearer",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer"
          });
    c.AddSecurityRequirement(new OpenApiSecurityRequirement
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

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
 .AddJwtBearer(x =>
 {
     x.RequireHttpsMetadata = false;
     x.SaveToken = true;
     x.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
     {
         ValidateIssuerSigningKey = true,
         IssuerSigningKey = new SymmetricSecurityKey(secret),
         ValidateIssuer = false,
         ValidateAudience = false
     };
 });

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
