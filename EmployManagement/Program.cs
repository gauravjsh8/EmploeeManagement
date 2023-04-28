using EmployManagement.NewFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//links db context with sql Server database
builder.Services.AddDbContext<ApplicationDbContext>(options =>
options.
UseSqlServer(builder.Configuration.GetConnectionString("EmployeeDatabaseString")));
builder.Services.AddIdentity<IdentityUser,IdentityRole>()     // added
    .AddEntityFrameworkStores<ApplicationDbContext>();
//redirect to login page if not logged in
builder.Services.ConfigureApplicationCookie(configure =>
configure.LoginPath = "/Login/LoginUser");


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
app.UseAuthentication();  //added

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
