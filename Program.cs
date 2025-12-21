using Microsoft.EntityFrameworkCore;
using BootcampMvp.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Versioning;
using Microsoft.AspNetCore.Mvc.ApiExplorer;
using Microsoft.OpenApi;


var builder = WebApplication.CreateBuilder(args);

// Konfigurasi koneksi database
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
                        throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Menambahkan DbContext dengan SQL Server
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddControllers(); // Menambahkan layanan controller
builder.Services.AddEndpointsApiExplorer(); // Untuk Swagger
builder.Services.AddSwaggerGen(); // Untuk Swagger
builder.Services.ConfigureOptions<ConfigureSwaggerOptions>();
builder.Services.AddApiVersioning(options =>
{
    options.DefaultApiVersion = new ApiVersion(1, 0);
    options.AssumeDefaultVersionWhenUnspecified = true;
    options.ReportApiVersions = true;

    // Mendukung URI Versioning DAN Query String Versioning
    options.ApiVersionReader = ApiVersionReader.Combine(
        new UrlSegmentApiVersionReader(),
        new QueryStringApiVersionReader("api-version"));
});
// Menambahkan layanan untuk menjelajahi versi API
builder.Services.AddVersionedApiExplorer(options =>
{
    options.GroupNameFormat = "'v'VVV"; // Format grup versi API
    options.SubstituteApiVersionInUrl = true;
    // Mendukung URI Versioning DAN Query String Versioning
});

// Add services to the container.
builder.Services.AddControllersWithViews()
    .AddNewtonsoftJson();

// Add services to the container.
builder.Services.AddControllers() // Untuk API saja
    .AddNewtonsoftJson();

// builder.Services.AddSingleton<IStudentService, StudentService>();
// builder.Services.AddSingleton<IAttendanceService, AttendanceService>();

var app = builder.Build(); // Create the app

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger(); // Enable middleware to serve generated Swagger as a JSON endpoint.
    app.UseSwaggerUI(options =>
    {
        var provider = app.Services.GetRequiredService<IApiVersionDescriptionProvider>();
        foreach (var description in provider.ApiVersionDescriptions)
        {
            options.SwaggerEndpoint($"/swagger/{description.GroupName}/swagger.json",
                description.GroupName.ToUpperInvariant());
        }
    });
}

// Migrasi database saat aplikasi dijalankan
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    try
    {
        var context = services.GetRequiredService<ApplicationDbContext>();
        context.Database.Migrate(); // Menerapkan migrasi yang belum diterapkan
        DbInitializer.Initialize(context); // Seed data awal ke database
    }
    catch (Exception ex)
    {
        var logger = services.GetRequiredService<ILogger<Program>>();
        logger.LogError(ex, "An error occurred creating the DB.");
    }
}

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();

app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.UseHttpsRedirection(); // Redirect HTTP requests to HTTPS
app.UseAuthorization(); // Enable authorization middleware
app.MapControllers(); // Map controller routes
app.Run();
