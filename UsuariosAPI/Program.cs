using Microsoft.EntityFrameworkCore;
using UsuariosAPI.Data;
using Microsoft.AspNetCore.Identity;
using UsuariosAPI.Services;
using Microsoft.Extensions.Options;
using UsuariosAPI.Models;
using UsuariosAPI.Authorization;
using Microsoft.AspNetCore.Authorization;
using Google.Protobuf.WellKnownTypes;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);
// Add services to the container.
var connString = builder.Configuration.GetConnectionString("UsuarioConnection");

builder.Services.AddDbContext<UsuarioDbContext>
    (opts =>
    {
        opts.UseMySql(connString, ServerVersion.AutoDetect(connString));
    });

var connectionString = builder.Configuration.GetConnectionString("SiteConnection");

builder.Services.AddDbContext<PrincipalContext>(opts =>
    opts.UseLazyLoadingProxies().UseMySql(connectionString, ServerVersion.AutoDetect(connectionString)));

builder.Services
    .AddIdentity<Usuario, IdentityRole>()
    .AddEntityFrameworkStores<UsuarioDbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddSingleton<IAuthorizationHandler, TipoAuthorization>();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<UsuarioService>();
builder.Services.AddScoped<TokenService>();
builder.Services.AddScoped<NoticiaService, NoticiaService>();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
    {
        ValidateIssuerSigningKey = true,
        IssuerSigningKey = new SymmetricSecurityKey
        (Encoding.UTF8.GetBytes("9ASHDA98H9ah9ha9H9A89n0f234v23rwer23r23")),
        ValidateAudience = false,
        ValidateIssuer = false,
        ClockSkew = TimeSpan.Zero
    };
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("TipoJornalista", policy =>
        policy.AddRequirements(new TipoJornalista(2))
    );
}); 

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<TokenService, TokenService>();
//builder.Services.AddDbContext<UserDbContext>(options => options.UseMySQL(connectionString));
//builder.Services.AddIdentity<IdentityUser<int>, IdentityRole<int>>().AddEntityFrameworkStores<UserDbContext>();




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