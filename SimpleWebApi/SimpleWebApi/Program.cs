using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SimpleWebApi.Contexts;
using SimpleWebApi.Interfaces;
using SimpleWebApi.Models;
using SimpleWebApi.Repositories;
using SimpleWebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<SimpleApiContext>(options =>
{
    options.UseNpgsql(
        builder.Configuration.GetConnectionString("PostgreDb"));
    options.EnableSensitiveDataLogging(true);
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
   .AddJwtBearer(x =>
   {
       x.RequireHttpsMetadata = false;
       x.SaveToken = true;
       x.TokenValidationParameters = new TokenValidationParameters
       {
           ValidateIssuerSigningKey = true,
           IssuerSigningKey = AuthOptions.GetSymmetricSecurityKey(),
           ValidateIssuer = false,
           ValidateAudience = false
       };
});
builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("current",
        new OpenApiInfo
        {
            Title = "Simple Web API",
            Version = "v1"
        });
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });
});

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddTransient<IHashingService, HashingService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/current/swagger.json", "Simple Web API");
    });
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseMiddleware<AuthenticationMiddleware>();
app.UseRouting();
app.UseAuthorization();

app.MapControllers();

app.Run();
