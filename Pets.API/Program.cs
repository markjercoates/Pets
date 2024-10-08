using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Pets.Application.Entities;
using Pets.Application;
using Pets.Persistence;
using Pets.Persistence.Data;
using Pets.API.Middleware;
using Pets.API.Extensions;
using Pets.Application.Interfaces;
using Pets.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.AddIdentityServices(builder.Configuration);
builder.Services.AddSwaggerDocumentation();

builder.Services.AddScoped<ITokenService, TokenService>();
builder.Services.AddScoped<ILoggedInUserService, LoggedInUserService>();

builder.Services.AddHttpContextAccessor();

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseMiddleware<ExceptionMiddleware>();

// Use NWebsec to set security headers
app.UseXContentTypeOptions(); // Prevents content type sniffing
app.UseReferrerPolicy(opts => opts.NoReferrer()); // No referrer data is sent
app.UseXXssProtection(options => options.EnabledWithBlockMode()); // Enables the XSS filter in the browser
app.UseXfo(options => options.Deny()); // Prevents clickjacking
/*app.UseCsp(opts => opts
    .BlockAllMixedContent()
    .StyleSources(s => s.Self())
    .FontSources(s => s.Self())
    .FormActions(s => s.Self())
    .FrameAncestors(s => s.Self())
    .ImageSources(s => s.Self())
    .ScriptSources(s => s.Self())
);*/

app.UseCors(x => x.AllowAnyHeader()
                        .AllowAnyMethod()
                        .WithOrigins("http://localhost:4200", "https://localhost:4200"));

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

    //using var scope = app.Services.CreateScope();
    //var services = scope.ServiceProvider;
    //try
    //{
    //    var context = services.GetRequiredService<PetsDbContext>();
    //    var userManager = services.GetRequiredService<UserManager<AppUser>>();
    //    var roleManager = services.GetRequiredService<RoleManager<AppRole>>();
    //    //await context.Database.EnsureDeletedAsync();
    //    await context.Database.MigrateAsync();
    //    await Seed.SeedUsers(userManager, roleManager);
    //    await Seed.SeedTestData(context);
    //}
    //catch (Exception ex)
    //{
    //    var logger = services.GetService<ILogger<Program>>();
    //    logger?.LogError(ex, "An error occurred during migration");
    //}
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program;
