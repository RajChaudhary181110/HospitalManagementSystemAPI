using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using MultipleAction_API.DBContext;
using MultipleAction_API.Interface;
using MultipleAction_API.Repository;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


/// 1️⃣ Add CORS service
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins("http://localhost:4200")  // 👈 Your Angular frontend origin
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowAnyOrigin();
    });
});
// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddTransient<DapperDBContext>();
builder.Services.AddTransient<ILoginSignUpService, LoginSignUpReps>();
builder.Services.AddTransient<IDashboard, DashboardReps>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//BindingAddress JWT Bearer Token- (Start)*************************************************************
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    { 
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:key"]))
    };
});
//BindingAddress JWT Bearer Token- (End)*************************************************************


var app = builder.Build();

app.UseAuthentication();

// 2️⃣ Use CORS middleware — Must be BEFORE `UseAuthorization()`
app.UseCors("AllowAngularApp");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
