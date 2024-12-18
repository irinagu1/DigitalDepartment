using DigitalDepartment;
using DigitalDepartment.Extensions;
using DigitalDepartment.Presentation.ActionFilters;
using Entities.Models;
using Microsoft.AspNetCore.HttpOverrides;
using Microsoft.AspNetCore.Mvc;
using Service.Contracts;
using System.Reflection.Metadata.Ecma335;

var builder = WebApplication.CreateBuilder(args);

builder.Services.ConfigureCors();
builder.Services.ConfigureIISIntegration();

builder.Services.ConfigureRepositoryManager();
builder.Services.ConfigureServiceManager();
builder.Services.ConfigureChecker();
builder.Services.ConfigureDocumentVersionService();
builder.Services.ConfigureSqlContext(builder.Configuration);
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.ConfigureFilesFolders(builder.Configuration);

builder.Services.AddControllers()
.AddApplicationPart(
    typeof(DigitalDepartment.Presentation.AssemblyReference).Assembly);

builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ValidationFilterAttribute>();
builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.SuppressModelStateInvalidFilter = true;
});

builder.Services.AddAuthentication();
builder.Services.ConfigureAuthorization();

builder.Services.ConfigureIdentity();
builder.Services.ConfigureJWT(builder.Configuration);
builder.Services.AddJwtConfiguration(builder.Configuration);
builder.Services.ConfigureSwagger();

var app = builder.Build();

app.ConfigureExceptionHandler();

if (app.Environment.IsProduction())
    app.UseHsts();

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseForwardedHeaders(new ForwardedHeadersOptions
{
    ForwardedHeaders = ForwardedHeaders.All
});
app.UseCors("CorsPolicy");


app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI(s =>
{
    s.SwaggerEndpoint("/swagger/v1/swagger.json", "Digital Department API v1");
});

app.Run();
