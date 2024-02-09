using BugTrackerUI.Data;
using BugTrackerUICore.Helper;
using BugTrackerUICore.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Reflection;
using BugTrackerAPICall.APICall;
using Microsoft.Extensions.Caching.Distributed;
using BugTrackerUICore.Interfaces;
using BugTrackerUICore.Services;
using BugTrackerAPICall.Interfaces;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using BugTrackerAPICall.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddScoped<IHttpMethods, ApiCallFunctions>();
builder.Services.AddScoped<ICachingService, CachingService>();
builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IEncryptionService, EncryptionService>();
builder.Services.AddRazorPages();
builder.Services.AddHttpClient();
builder.Services.AddDistributedMemoryCache();
builder.Services.AddSession(options =>
{
    options.Cookie.HttpOnly = true;
});
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters
        {

        };
    });



var app = builder.Build();



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}
app.Lifetime.ApplicationStarted.Register(() =>
{
    var currentTime = DateTime.Now.ToString();
    byte[] encodedCurrentTime = System.Text.Encoding.UTF8.GetBytes(currentTime);
    var options = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(15));
    app.Services.GetService<IDistributedCache>().Set("CachedTime", encodedCurrentTime, options);
});

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseSession();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();


app.UseEndpoints(endpoints =>
{
    endpoints.MapRazorPages();
    endpoints.MapControllerRoute("default", "{Controller=Error}/{Action=Index}/{id?}");

    
});
//app.MapControllerRoute("default", "{Controller=Error}/{Action=Index}/{id?}");




//app.MapRazorPages();

app.Run();
