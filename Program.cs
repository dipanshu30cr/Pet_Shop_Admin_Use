using Microsoft.EntityFrameworkCore;
using Pet_Shop_Management.ImgAdder;
using Pet_Shop_Management.Repository;
using Pet_Shop_Management.Services;
using Pet_Shop_Management.context;
using Pet_Shop_Management.Repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddScoped<IuserServices,UserServices>();
//builder.Services.AddScoped<IAdminRepository,AdminRepository>();
//builder.Services.AddScoped<IAdminServices, AdminServices>();
builder.Services.AddScoped<IAdminRepository,AdminRepository>();
builder.Services.AddScoped<IAdminServices,AdminServices>();
builder.Services.AddScoped<IuserRepository,UserRepository>();
builder.Services.AddScoped<IPhotoImageProvider, FileUpload>();
var LocalConnectionString = builder.Configuration.GetConnectionString("LocalDbConnection");
builder.Services.AddDbContext<Pet_Shop_DBContext>(u => u.UseSqlServer(LocalConnectionString));
builder.Services.AddSession();


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
app.UseSession();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=UserRegister}/{action=UserLogin}/{id?}");
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Admin}/{action=AdminLogin}/{id?}");

app.Run();
