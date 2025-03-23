using Business.Abstract;
using Business.Concreate;
using DataAccess.Abstract;
using DataAccess.Repositories;
using Entities.Context;
using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using DataAccess;
using Entities.Entities;
using OrderLineManager = Business.Concreate.OrderLineManager;

var builder = WebApplication.CreateBuilder(args);

// **Veritabaný baðlantýsý**
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<ETicaretContext>(options =>
    options.UseSqlServer(connectionString));

// **Identity Ayarlarý**
builder.Services.AddIdentity<AppUser, AppRole>(options =>
{
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(15);
    options.Lockout.MaxFailedAccessAttempts = 5;
    options.Password.RequireDigit = false;
    options.Password.RequireNonAlphanumeric = false;
    options.Password.RequireLowercase = false;
    options.Password.RequireUppercase = false;

})
.AddRoles<AppRole>()
.AddEntityFrameworkStores<ETicaretContext>()
.AddDefaultTokenProviders();

// **Baðýmlýlýk Enjeksiyonu (DI)**
builder.Services.AddScoped<IProductService, ProductManager>();
builder.Services.AddScoped<IProductDal, EfProductDal>();

builder.Services.AddScoped<ICartService, CartManager>();

builder.Services.AddScoped<IOrderService, OrderManager>();
builder.Services.AddScoped<IOrderDal, EfOrderDal>(); 

builder.Services.AddScoped<IOrderLineService, OrderLineManager>();
builder.Services.AddScoped<IOrderLineDal, EfOrderLineDal>();

builder.Services.AddScoped<IAdminProductService, AdminProductManager>();
builder.Services.AddScoped<IAdminOrderService, AdminOrderManager>();
builder.Services.AddScoped<IAdminCategoryService, AdminCategoryManager>();

builder.Services.AddScoped<ICategoryService, CategoryManager>();
builder.Services.AddScoped<ICategoryDal, EfCategoryDal>();

builder.Services.AddScoped<UserManager<AppUser>>();

// **Session ve Cache Servisleri**
builder.Services.AddDistributedMemoryCache(); // Session için gerekli
builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(30);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});
builder.Services.AddHttpContextAccessor(); // Session kullanýmý için gerekli

// **Controller ve View Servisleri**
builder.Services.AddControllersWithViews();

var app = builder.Build();

// **Seed Data çalýþtýr**
using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    await SeedData.InitializeAsync(services);
}

// **Middleware Konfigürasyonu**
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();
app.UseSession(); // **Session Middleware’i etkinleþtirildi**
app.UseAuthentication();
app.UseAuthorization();

// **Varsayýlan Rota**
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
