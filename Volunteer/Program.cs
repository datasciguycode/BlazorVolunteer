using Microsoft.FluentUI.AspNetCore.Components;
using Volunteer.Components;
using Microsoft.EntityFrameworkCore;
using Volunteer.Models;
using Volunteer.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<IInterestService, InterestService>();
builder.Services.AddScoped<ISkillService, SkillService>();
builder.Services.AddScoped<IRoleService, RoleService>();
builder.Services.AddScoped<ISourceService, SourceService>();
builder.Services.AddScoped<IUserSkillService, UserSkillService>();
builder.Services.AddScoped<IUserInterestService, UserInterestService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IStatusService, StatusService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddFluentUIComponents();

builder.Services.AddDbContext<VolunteerContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();
builder.Services.AddFluentUIComponents();
builder.Services.AddScoped<UserSessionState>();

var app = builder.Build();

// Seed data for testing
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<VolunteerContext>();
    var roleService = scope.ServiceProvider.GetRequiredService<IRoleService>();

    try
    {
        // Ensure database is created
        context.Database.EnsureCreated();

        // Check if Admin role exists, if not create it
        var roles = await roleService.ToListAsync();
        if (!roles.Any(r => r.Name.Equals("Admin", StringComparison.OrdinalIgnoreCase)))
        {
            var adminRole = new Role { Name = "Admin", Password = "admin" };
            await roleService.AddAsync(adminRole);
        }

        // Check if Basic role exists, if not create it
        if (!roles.Any(r => r.Name.Equals("Basic", StringComparison.OrdinalIgnoreCase)))
        {
            var basicRole = new Role { Name = "Basic", Password = "basic" };
            await roleService.AddAsync(basicRole);
        }

        // Check if Power role exists, if not create it
        if (!roles.Any(r => r.Name.Equals("Power", StringComparison.OrdinalIgnoreCase)))
        {
            var powerRole = new Role { Name = "Power", Password = "power" };
            await roleService.AddAsync(powerRole);
        }
    }
    catch (Exception ex)
    {
        Console.WriteLine($"Error seeding data: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();
