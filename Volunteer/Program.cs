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

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException("DefaultConnection string is not configured.");
}

builder.Services.AddDbContext<VolunteerContext>(options =>
    options.UseSqlServer(connectionString));

// ...existing code...
// Read timeout configuration once
var timeoutMinutes = builder.Configuration.GetValue<int>("BlazorServer:TimeoutMinutes", 20);
var timeout = TimeSpan.FromMinutes(timeoutMinutes);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DisconnectedCircuitRetentionPeriod = timeout;
        options.JSInteropDefaultCallTimeout = timeout;
    });
    
builder.Services.AddSignalR(options =>
{
    options.ClientTimeoutInterval = timeout;
    options.KeepAliveInterval = TimeSpan.FromMinutes(timeoutMinutes / 4);
});
// ...existing code...

builder.Services.AddScoped<UserSessionState>();

var app = builder.Build();

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
