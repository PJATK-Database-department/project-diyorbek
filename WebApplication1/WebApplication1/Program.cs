using Syncfusion.Blazor;
using Syncfusion.Licensing;
using WebApplication1.Models;
using WebApplication1.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<AppDbContext>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IPolygonService, PolygonService>();
builder.Services.AddScoped<IWatchlistService, WatchlistService>();

SyncfusionLicenseProvider.RegisterLicense(
    "NjQ5NzQyQDMyMzAyZTMxMmUzMFBmemJXSWlIN1NUb2E5Z09BQ1lWdFAxdmxOcEhXTjZkQmFRUjNRTE9SQ2c9");

builder.Services.AddSyncfusionBlazor(options => { options.IgnoreScriptIsolation = true; });

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");

app.Run();