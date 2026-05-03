using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using StudyBuddyFinal1.Data;
using StudyBuddyFinal.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
// Set up Entity Framework with SQL Server and enable retry on failure for Azure.
builder.Services.AddDbContext<ApplicationDbContext>(options =>
    options.UseSqlServer(
    connectionString,
    sqlOptions => sqlOptions.EnableRetryOnFailure()
));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
// Set up Identity with default options and link it to our database context
builder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<ApplicationDbContext>();
builder.Services.AddControllersWithViews();

// Add our custom services to the dependency injection container
builder.Services.AddScoped<CourseService>();
builder.Services.AddScoped<AssignmentService>();
builder.Services.AddScoped<StudySessionService>();
builder.Services.AddSession();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseMigrationsEndPoint();
}
else
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapStaticAssets();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}")
    .WithStaticAssets();

app.MapRazorPages()
   .WithStaticAssets();

app.Run();
