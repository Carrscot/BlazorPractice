using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using PracticeApp.Data;
using PracticeApp.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddServerSideBlazor();
builder.Services.AddDefaultIdentity<IdentityUser>(options =>
{
    options.SignIn.RequireConfirmedAccount = false;
})
.AddRoles<IdentityRole>()
.AddEntityFrameworkStores<ApplicationDbContext>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContextFactory<ApplicationDbContext>(options =>
    options.UseNpgsql(connectionString));

// Add services and controllers
builder.Services.AddScoped<IAppUserService, AppUserService>();
builder.Services.AddControllers();
builder.Services.AddHttpClient("AppUsersApi", client =>
{
    client.BaseAddress = new Uri("https://localhost:7093/");
});

// Seed admin
using (var scope = builder.Services.BuildServiceProvider().CreateScope())
{
    var userManager = scope.ServiceProvider.GetRequiredService<UserManager<IdentityUser>>();
    var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();
    if (!await roleManager.RoleExistsAsync("Admin"))
    {
        await roleManager.CreateAsync(new IdentityRole("Admin"));
    }
    var adminUser = await userManager.FindByEmailAsync("admin@example.com");
    if (adminUser == null)
    {
        adminUser = new IdentityUser { UserName = "admin@example.com", Email = "admin@example.com" };
        await userManager.CreateAsync(adminUser, "Admin123!");
        await userManager.AddToRoleAsync(adminUser, "Admin");
    }
}

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();

app.MapBlazorHub();
app.MapFallbackToPage("/_Host");
app.MapControllers();

app.Run();