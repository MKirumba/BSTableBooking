
using Microsoft.EntityFrameworkCore;
using BSTableBooking.Data;
using BSTableBooking.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<BSTableBookingAppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("BSTableBooking") ?? throw new InvalidOperationException("Connection string 'BSTableBookingContext' not found.")));

builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
       .AddEntityFrameworkStores<BSTableBookingAppDbContext>()
       .AddDefaultTokenProviders();

builder.Services.AddScoped<ISessionServices, SessionService>();
builder.Services.AddScoped<ITableAreaService, TableAreaService>();
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IBookingService, BookingService>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for Sessionion scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
