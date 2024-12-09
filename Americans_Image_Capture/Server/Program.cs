using Americans_Image_Capture.Server.Contracts;
using Americans_Image_Capture.Server.Data;
using Americans_Image_Capture.Server.Data.Repositories;
using Americans_Image_Capture.Server.Services;
using Americans_Image_Capture.Shared.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.ResponseCompression;
using Microsoft.EntityFrameworkCore;
using MudBlazor;
using MudBlazor.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container
builder.Services.AddControllersWithViews();

builder.Services.AddRazorPages();
builder.Services.AddIdentity<AppUser, IdentityRole<int>>()
    .AddEntityFrameworkStores<ProjectdbContext>()
    .AddDefaultTokenProviders();

builder.Services.AddScoped<ITokenService, TokenService>();

// Database context configuration
builder.Services.AddDbContext<ProjectdbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ImageDataConnection")));

// Add MudBlazor Snackbar service if needed (likely unnecessary)
builder.Services.AddMudServices();

// Optional: Add CORS for API access
builder.Services.AddCors(options =>
{
    options.AddPolicy("CorsPolicy", builder =>
    {
        builder.AllowAnyOrigin()
               .AllowAnyMethod()
               .AllowAnyHeader();
    });
});

builder.Services.AddScoped<IMaagAmericansImageRepository, MaagAmericansImageRepository>();

builder.Services.AddTransient<MaagAmericansImageRepository>();
builder.Services.AddScoped<ILogindetailrepository, Logindetailrepository>();

builder.Services.AddTransient<Logindetailrepository>();

// Optional: Add Swagger for API documentation
builder.Services.AddEndpointsApiExplorer();


var app = builder.Build();

// Configure the HTTP request pipeline
if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
    app.UseDeveloperExceptionPage();
  
}
else
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}



app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseBlazorFrameworkFiles();
app.UseAuthentication();
app.UseAuthorization();


app.UseRouting();

// Use CORS
app.UseCors("CorsPolicy");

// Map endpoints
app.MapRazorPages();
app.MapControllers();
app.MapFallbackToFile("index.html");

app.Run();
