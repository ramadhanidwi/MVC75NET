using Microsoft.EntityFrameworkCore;
using MVC75NET.Contexts;
using MVC75NET.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

//Configure Context to Sql Server Database 
var connectionString = builder.Configuration.GetConnectionString("connection"); //untuk mendapat connection string yang ada di appsettings.json 
builder.Services.AddDbContext<MyContext>( options => options.UseSqlServer(connectionString)); //untuk mendaftarkan MyContext.cs ke Sql Server 

//Dependency Injction 
builder.Services.AddScoped<UniversityRepository>();
builder.Services.AddScoped<EducationRepository>();

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
