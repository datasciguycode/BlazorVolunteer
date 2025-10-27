using Microsoft.FluentUI.AspNetCore.Components;
using Volunteer.Components;
using Microsoft.EntityFrameworkCore;
using Volunteer.Models;
using Volunteer.Services;
using Microsoft.AspNetCore.Components.Server.Circuits;

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

// Timeout configuration
var timeoutMinutes = builder.Configuration.GetValue<int>("BlazorServer:TimeoutMinutes", 20);
var timeout = TimeSpan.FromMinutes(timeoutMinutes);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(options =>
    {
        options.DisconnectedCircuitRetentionPeriod = timeout;
        options.JSInteropDefaultCallTimeout = timeout;
        options.DetailedErrors = true;
    });
    
builder.Services.AddSignalR(options =>
{
    options.ClientTimeoutInterval = TimeSpan.FromSeconds(30);  // Default client timeout
    options.KeepAliveInterval = TimeSpan.FromSeconds(15);      // Send keep-alive every 15 seconds
    options.HandshakeTimeout = TimeSpan.FromSeconds(15);       // Handshake timeout
    options.MaximumParallelInvocationsPerClient = 10;
});

builder.Logging.SetMinimumLevel(LogLevel.Debug);

builder.Services.AddScoped<UserSessionState>();

// Add circuit handler for diagnostics
builder.Services.AddScoped<CircuitHandler, DiagnosticCircuitHandler>();

var app = builder.Build();

// Add error logging middleware
app.Use(async (context, next) =>
{
    try
    {
        await next();
    }
    catch (Exception ex)
    {
        var logger = context.RequestServices.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "[ERROR MIDDLEWARE] Unhandled exception: {Message}", ex.Message);
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;
        // Don't rethrow - let the exception handler middleware handle it
    }
});

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();

    // Add security headers
    app.Use(async (context, next) =>
    {
        context.Response.Headers.Append("X-Content-Type-Options", "nosniff");
        context.Response.Headers.Append("X-Frame-Options", "DENY");
        context.Response.Headers.Append("X-XSS-Protection", "1; mode=block");
        context.Response.Headers.Append("Referrer-Policy", "strict-origin-when-cross-origin");
        await next();
    });

    app.UseHttpsRedirection();
}

app.UseAntiforgery();

app.MapStaticAssets();
app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode();

app.Run();

public class DiagnosticCircuitHandler : CircuitHandler
{
    private readonly ILogger<DiagnosticCircuitHandler> _logger;

    public DiagnosticCircuitHandler(ILogger<DiagnosticCircuitHandler> logger)
    {
        _logger = logger;
    }

    public override async Task OnCircuitClosedAsync(Circuit circuit, CancellationToken cancellationToken)
    {
        _logger.LogWarning("Circuit disconnected: {CircuitId}", circuit.Id);
        await base.OnCircuitClosedAsync(circuit, cancellationToken);
    }
}