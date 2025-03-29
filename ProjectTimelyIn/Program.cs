using ProjectTimelyIn.Core;
using ProjectTimelyIn.Core.Services;
using ProjectTimelyIn.Services;
using ProjectTimelyIn.Dtat.Repositories;
using ProjectTimelyIn.Data;
using ProjectTimelyIn.Data.Repositories;
using ProjectTimelyIn.Core.Repositorys;
using ProjectTimelyIn.Services.Implementations;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using ProjectTimelyIn.Api.Middlewares;
using NLog;
using NLog.Web;

var builder = WebApplication.CreateBuilder(args);
Console.WriteLine(builder.Configuration["USERNAME"]);

//var logger = NLogBuilder.ConfigureNLog("nlog.config").GetCurrentClassLogger();
//builder.Logging.ClearProviders();
//builder.Host.UseNLog();


builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    options.JsonSerializerOptions.WriteIndented = true;
});
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["JWT:Issuer"],
            ValidAudience = builder.Configuration["JWT:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["JWT:Key"]))
        };
    });
builder.Services.AddScoped<IEmployeeRepository, EmployeRepository>();
builder.Services.AddScoped<IEmployeeServices, EmployeeService>();
builder.Services.AddScoped<IWorkHoursRepository, WorkHoursRepository>();
builder.Services.AddScoped<IWorkHoursServices, WorkHoursService>();
builder.Services.AddScoped<IVacationRepository, VacationRepository>();

builder.Services.AddScoped<IVacationRepository, VacationRepository>();
builder.Services.AddScoped<VacationRepository>(); 
builder.Services.AddScoped<IVacationServices, VacationServices>();
builder.Services.AddDbContext<DataContext>();

builder.Services.AddAutoMapper(typeof(MapperProFile));

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.UseShabbatMiddleware();

app.MapControllers();

app.Run();
