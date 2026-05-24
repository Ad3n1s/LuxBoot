using LuxBoot;
using LuxBoot.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using LuxBoot.DOS_Servers;


var builder = WebApplication.CreateBuilder(args);



builder.Services.AddSingleton<ServerState>();


// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<AppDbContext>( options =>
{
    options.UseSqlServer(@"Server=(localdb)\Local;Database=LuxBootDatabase;Trusted_Connection=True;ConnectRetryCount=0");
});


builder.Services.AddIdentity<UserApp, IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

var _server = new Servers(new Attack_Methods());



var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

public class ServerState
{
    public int ServerCount { get; set; } = 20;
}





















