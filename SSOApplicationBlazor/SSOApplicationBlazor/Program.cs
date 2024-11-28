using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.Identity.Web;
using SSOApplicationBlazor.Components;
using SSOApplicationBlazor.Providers;
using SSOApplicationBlazor.Services;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

// Configure Azure AD Authentication
builder.Services
    .AddAuthentication(OpenIdConnectDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApp(builder.Configuration.GetSection("AzureAd"));

// Add Authorization
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAuthenticatedUser", policy =>
        policy.RequireAuthenticatedUser());
});

// Add Razor Components
builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents();

// Add Authentication Services
builder.Services.AddCascadingAuthenticationState();
builder.Services.AddScoped<AuthenticationStateProvider, CustomAuthenticationStateProvider>();

// Add HTTP Context Accessor
builder.Services.AddHttpContextAccessor();

// Add custom services
builder.Services.AddScoped<AuthService>();

var app = builder.Build();

// Configure the HTTP request pipeline
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Authentication and Authorization
app.UseAuthentication();
app.UseAuthorization();

// Anti-forgery middleware
app.UseAntiforgery();

// Endpoint Routing
app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
