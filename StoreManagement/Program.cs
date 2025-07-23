using APIStoreManagement.Models;
using APIStoreManagement.Interfaces; // اضافه کردن فضای نام ICostsRepository
// اضافه کردن فضای نام CostsRepository
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using System.Text;
using APIStoreManagement.Repository;
using APIStoreManagement.Services;
using APIStoreManagement.UnitOfWork;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.WithOrigins("http://localhost:5014") // آدرس کلاینت خود را اینجا وارد کنید
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials(); // اجازه دادن به اعتبارسنجی
    });
});

// تنظیمات JWT و سرویس‌ها
var jwtSettings = builder.Configuration.GetSection("JwtSettings");
var secretKey = jwtSettings["SecretKey"];


//.AddJwtBearer(options =>
//{
//    options.TokenValidationParameters = new TokenValidationParameters
//    {
//        ValidateIssuer = true,
//        ValidateAudience = true,
//        ValidateLifetime = true,
//        ValidateIssuerSigningKey = true,
//        ValidIssuer = jwtSettings["Issuer"],
//        ValidAudience = jwtSettings["Audience"],
//        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey))
//    };
//});

// افزودن سرویس‌ها و کنترلرها
builder.Services.AddAuthorization();
builder.Services.AddControllers();

// ثبت ICostsRepository و پیاده‌سازی آن
builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
builder.Services.AddScoped<IClothingRepository, ClothingRepository>();
builder.Services.AddScoped<ICostsRepository, CostRepository>();
builder.Services.AddScoped<IInventoryRepository, InventoryRepository>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<ISalesRepository, SalesRepository>();
builder.Services.AddScoped<IPatternRepository, PatternRepository>();
builder.Services.AddScoped<ISizeRepository, SizesRepository>();
//Services
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddScoped<ISaleService, SaleService>();
builder.Services.AddScoped<IOrderService, OrderService>();
builder.Services.AddScoped<IInventoryService, InventoryService>();


// تنظیمات پایگاه داده
builder.Services.AddDbContext<DataContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

var app = builder.Build();
app.UseRouting();
app.UseCors("AllowSpecificOrigins");


// فقط در محیط توسعه Swagger را فعال کنید (در صورت نیاز)
if (app.Environment.IsDevelopment())
{
    // برای غیرفعال کردن Swagger به‌طور کامل، این خطوط را کامنت کنید:
    //app.UseSwagger();
    //app.UseSwaggerUI();
}

// تنظیمات پیش‌فرض برای فایل‌های استاتیک و صفحه اصلی
app.UseDefaultFiles(new DefaultFilesOptions
{
    DefaultFileNames = new List<string> { "index.html" }
});
app.UseStaticFiles();

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.MapFallbackToFile("/index.html");


app.Run();
