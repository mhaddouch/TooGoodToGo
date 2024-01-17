using Core.DomainServices;
using Infrastructure.EP_EF;
using Infrastructure.EP_EF.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Infrastructure.EP_EF.SeedData;

var builder = WebApplication.CreateBuilder(args);

//migrations  dependencies
builder.Services.AddDbContext<PackageDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Default") ??
                           throw new InvalidOperationException("Database is not configured");
    options.UseSqlServer(connectionString);
    options.EnableSensitiveDataLogging(true);
});

builder.Services.AddDbContext<SecurityDbContext>(options =>
{
    var connectionString = builder.Configuration.GetConnectionString("Security") ??
                           throw new InvalidOperationException("Database is not configured");
    options.UseSqlServer(connectionString);
});


builder.Services.AddIdentity<IdentityUser, IdentityRole>(opts => opts.SignIn.RequireConfirmedAccount = false)
    .AddEntityFrameworkStores<SecurityDbContext>()
    .AddDefaultTokenProviders()
    .AddRoles<IdentityRole>();
//dependency injection repositories
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
//builder.Services.AddScoped<EcoPlatesSeedData>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
    await SeedDatabase();
}
else
{
    //Seeding Database Trigger
    await SeedDatabase();
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthentication();
app.UseAuthorization();


app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Migrate the database.
using (var scope = app.Services.CreateScope())
{
    await using var tooGoodtoGoCtx = scope.ServiceProvider.GetRequiredService<PackageDbContext>();
    await tooGoodtoGoCtx.Database.MigrateAsync();

     await using var securityCtx = scope.ServiceProvider.GetRequiredService<SecurityDbContext>();
     await securityCtx.Database.MigrateAsync();
}
    app.Run();
async Task SeedDatabase()
{
//    using var scope = app.Services.CreateScope();
 //  var dbSeeder = scope.ServiceProvider.GetRequiredService<EcoPlatesSeedData>();
 //   await dbSeeder.EnsurePopulated(true);
}