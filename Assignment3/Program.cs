
using System.Text;
using Assignment3.Interface;
using Assignment3.Middleware;
using Assignment3.Models;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

namespace Assignment3;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddTransient<IMyService, MyService>();

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        
        #region JWT Validation
        /*******************************************
           *  Start JWT Security Configuration
           *  ****************************************/
        
          
  
        var secret = "MyVerySuperSecureSecretSharedKey";
        var secretKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        var issuer = "http://www.uc.edu/IT3047C";
        var audience = "WebServerApplicationDevelopment";

        builder.Services.AddAuthentication(OptionsBuilderConfigurationExtensions =>
        {
            OptionsBuilderConfigurationExtensions.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
        })
        .AddJwtBearer(options =>
        {
            options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = secretKey,

                ValidateIssuer = true,
                ValidIssuer = issuer,

                ValidateAudience = true,
                ValidAudience = audience,

                ValidateLifetime = true,
                ClockSkew = TimeSpan.Zero
            };
        });

        /*****************************************
         *  End JWT Security Configuration
         *  **************************************/
        #endregion

        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

       // app.UseMiddleware<CustomMiddleware>();

        app.UseCustomMiddleware();   //ExtraCredit

       
        app.Use(async (context, next) =>
        {
            Console.WriteLine("Hello from In-Line Middleware!");
            await next(context);
            Console.WriteLine("Responding to request");
        });

        app.UseAuthorization();

        app.MapControllers();

        app.Run();

    }
}

