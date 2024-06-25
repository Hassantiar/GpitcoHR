using Base.Entites;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Serverlibrary.Data;
using Serverlibrary.Helper;
using Serverlibrary.Repository.Contact;
using Serverlibrary.Repository.Implemintation;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//custom service
builder.Services.Configure<Jwtsection>(builder.Configuration.GetSection("Jwtsection"));
var JwtSection = builder.Configuration.GetSection(nameof(Jwtsection)).Get<Jwtsection>();
builder.Services.AddDbContext<AppDBContext>(option =>
{
    option.UseSqlServer(builder.Configuration.GetConnectionString("sqlconnection") ??
        throw new InvalidOperationException("sorry your connection string in not found"));
});
builder.Services.AddAuthentication(option =>
{
    option.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    option.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(option =>
{
    option.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateIssuerSigningKey = true,
        ValidateLifetime = true,
        ValidIssuer = JwtSection!.Issuer,
        ValidAudience = JwtSection!.Audience,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(JwtSection.Key))
    };
});

builder.Services.AddScoped<IUserAccount, UserAccountRepository>();
builder.Services.AddScoped<IGenaricReopInterface<Branch>, BranchRepostory>();
builder.Services.AddScoped<IGenaricReopInterface<City>,CityRepostory>();
builder.Services.AddScoped<IGenaricReopInterface<Country>, CountryRepostory>();
builder.Services.AddScoped<IGenaricReopInterface<Departrment>,DepartmentReposotory>();
builder.Services.AddScoped<IGenaricReopInterface<GeneralDepartment>, GenaricDepartmentReposotory>();
builder.Services.AddScoped<IGenaricReopInterface<Twon>,TownRepostory>();
builder.Services.AddCors(option =>
{
    option.AddPolicy("AllowBlazerWasm", builder => builder.WithOrigins("http://localhost:5292", "https://localhost:7095")
    .AllowAnyMethod()
    .AllowAnyHeader()
    .AllowCredentials());
});
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("AllowBlazerWasm");
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
