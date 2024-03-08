
using Hangfire;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using PayRollProject.Repository;
using PayRollProject.Repository.Interface;
using PayRollProject.Repository.Interfaces;
using PayRollProject.Services;
using sib_api_v3_sdk.Client;
using System.Text;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

builder.Services.AddControllers();

var building = new ConfigurationBuilder()
    .SetBasePath(Directory.GetCurrentDirectory())
    .AddJsonFile("appsettings.json");
IConfigurationRoot config = building.Build();
string secretKey = config.GetSection("AppSettings:SecretKey").Value!;
//var config = builder.Configuration;

//Configuration.Default.ApiKey.Add("api-key", builder.Configuration["BrevoApi:ApiKey"]);


builder.Services.AddScoped<IUserDetailsRepository, UserRepository>();
builder.Services.AddScoped<ILeaveDetailsRepository, LeaveRepository>();
builder.Services.AddScoped<IEmployeeLogRepository, EmployeeLogRepository>();
builder.Services.AddScoped<ISalaryCreditRepository, SalaryCreditRepository>();
builder.Services.AddScoped<ILeaveRecordsRepository, LeaveRecordsRepository>();
builder.Services.AddScoped<IAuthCredRepository, AuthCredRepository>();
builder.Services.AddScoped<IAuditRepository, AuditRepository>();

builder.Services.AddScoped<AuditServices>();
builder.Services.AddScoped<SalaryCreditService>();
builder.Services.AddScoped<UserDetailsService>();
builder.Services.AddScoped<UserRepository>();
builder.Services.AddScoped<SalaryCreditRepository>();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddTransient<UserDetailsService>();
builder.Services.AddTransient<LeaveDetailsService>();
builder.Services.AddTransient<EmployeeLogService>();
builder.Services.AddTransient<SalaryCreditService>();
builder.Services.AddTransient<JobService>();
builder.Services.AddTransient<LeaveRecordsService>();
builder.Services.AddTransient<AuthCredService>();


builder.Services.AddSwaggerGen();
builder.Services.AddHangfire((sp, config) =>
{
    var connString = sp.GetRequiredService<IConfiguration>().GetConnectionString("DefaultConnection");
    config.UseSqlServerStorage(connString);
});
builder.Services.AddHangfireServer();

Console.WriteLine(secretKey);
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.IncludeErrorDetails = true;
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes("6Gd#!9hsFWT72bds0=-11YQHS&2@9sa*VQG*")), // Replace with your actual secret key
            ValidateIssuer = true,
            ValidIssuer = "https://localhost:7125", // Replace with the issuer you set when generating the token
            ValidateAudience = true, // Validate audience (optional)
            ValidAudience = "http://localhost:4200", // Replace with the intended audience
        //   ValidateLifetime = true, // Validate token expiration
           
        };
    });
builder.Services.AddAuthorization();
/*
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("Admin", policy => policy.RequireRole("Admin"));
    options.AddPolicy("HR", policy => policy.RequireRole("HR", "Admin"));
    options.AddPolicy("Employee", policy => policy.RequireRole("Employee", "HR", "Admin"));
});
*/
builder.Services.AddCors(options => options.AddPolicy(name: "FrontendUI",
    policy =>
    {
        policy.WithOrigins("http://localhost:4200").AllowAnyMethod().AllowAnyHeader();
    }
    ));


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseHttpsRedirection();
app.UseCors("FrontendUI");

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.UseHangfireDashboard();

app.Run();
