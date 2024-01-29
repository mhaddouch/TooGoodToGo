using Core.DomainServices;
using EcoPlatesApi.GraphQL;
using Infrastructure.EP_EF;
using Infrastructure.EP_EF.Repositories;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.




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
builder.Services.AddAuthorization(options =>
    options.AddPolicy("EmployeePolicy", policy => policy.RequireClaim("Employee")));
builder.Services.AddAuthorization(options =>
    options.AddPolicy("StudentPolicy", policy => policy.RequireClaim("Student")));

//dependency injection repositories
builder.Services.AddScoped<IPackageRepository, PackageRepository>();
builder.Services.AddScoped<IStudentRepository, StudentRepository>();
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<ICanteenRepository, CanteenRepository>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();

builder.Services.AddControllers().AddJsonOptions(x => x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles);


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services
    .AddGraphQLServer()
    .AddQueryType<Query>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();
// Migrate the database.
using (var scope = app.Services.CreateScope())
{
    await using var tooGoodtoGoCtx = scope.ServiceProvider.GetRequiredService<PackageDbContext>();
    await tooGoodtoGoCtx.Database.MigrateAsync();

    await using var securityCtx = scope.ServiceProvider.GetRequiredService<SecurityDbContext>();
    await securityCtx.Database.MigrateAsync();
}
app.MapGraphQL();
app.Run();
async Task SeedDatabase()
{
    //    using var scope = app.Services.CreateScope();
    //  var dbSeeder = scope.ServiceProvider.GetRequiredService<EcoPlatesSeedData>();
    //   await dbSeeder.EnsurePopulated(true);
}