using Autofac.Extensions.DependencyInjection;
using Hardwares.DAL.Context;
using Hardwares.DAL.Repositories;
using Hardwares.Interfaces.Repositories;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Host.UseServiceProviderFactory(new AutofacServiceProviderFactory());

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddDbContext<HardwaresDb>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
