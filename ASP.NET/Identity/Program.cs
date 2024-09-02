using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

using Identity.Models;
using Identity.Helpers;
using Identity.Data;
using Identity.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<JWTConfig>(builder.Configuration.GetSection("JWTConfig"));

// 添加控制器服務
builder.Services.AddControllers();

// 註冊 EmailHelper 服務為 IEmailSender 的單例
builder.Services.AddSingleton<IEmailSender, EmailHepler>();

// 註冊 IJWTHelper 服務為 JWTHelper 的作用域服務
builder.Services.AddScoped<IJWTHelper, JWTHelper>();

// 註冊 IUserService 服務為 UserService 的作用域服務
builder.Services.AddScoped<IUserService, UserService>();

// 配置數據庫上下文，使用 SQL Server 和應用程式的連接字串
builder.Services.AddDbContext<MyDBContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});

// 配置 Identity 服務，包括用戶和角色
builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
{
    // 密碼配置
    options.Password.RequiredLength = 6;  // 密碼至少需要 6 個字符長度
    options.Password.RequireDigit = false;  // 密碼不必須包含數字
    options.Password.RequireLowercase = false;  // 密碼不必須包含小寫字母
    options.Password.RequireUppercase = false;  // 密碼不必須包含大寫字母
    options.Password.RequireNonAlphanumeric = false;  // 密碼不必須包含非字母數字字符

    // 帳戶鎖定配置
    options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(5);  // 帳戶鎖定時間為 5 分鐘
    options.Lockout.MaxFailedAccessAttempts = 5;  // 最多允許 5 次登錄失敗嘗試
    options.Lockout.AllowedForNewUsers = true;  // 新用戶也適用於鎖定策略

    // 用戶配置
    options.User.AllowedUserNameCharacters = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789-._@+";  // 允許的用戶名字符
    options.User.RequireUniqueEmail = true;  // 電子郵件必須唯一
})
.AddEntityFrameworkStores<MyDBContext>()
.AddDefaultTokenProviders();

// 添加 JWT 身份驗證服務
builder.Services
    .AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        var jwtConfig = builder.Configuration.GetSection("JWTConfig").Get<JWTConfig>();

        options.IncludeErrorDetails = true; // 當驗證失敗時，會顯示失敗的詳細錯誤原因

        options.TokenValidationParameters = new TokenValidationParameters
        {
            // 簽發者
            ValidateIssuer = true,
            ValidIssuer = jwtConfig?.Issuer,
            // 接收者
            ValidateAudience = false,
            ValidAudience = "JwtAuthDemo",
            // Token 的有效期間
            ValidateLifetime = true,
            // 如果 Token 中包含 key 才需要驗證，一般都只有簽章而已
            ValidateIssuerSigningKey = false,
            // key
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtConfig.SignKey))
        };
    });

// 配置授權策略
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminManager", policy => policy.RequireRole("admin", "manager1"));
    options.AddPolicy("RequireUser", policy => policy.RequireRole("user"));
});

// 添加 HttpContextAccessor 服務，允許在應用程式中訪問 HttpContext
builder.Services.AddHttpContextAccessor();

// 添加 Swagger 生成 API 文檔
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// 配置 HTTP 請求管道
if (app.Environment.IsDevelopment())
{
    // 開發環境中啟用 Swagger UI
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 使用 HTTPS 重定向
app.UseHttpsRedirection();

// 啟用身份驗證
app.UseAuthentication();

// 啟用授權
app.UseAuthorization();

// 配置控制器的路由映射
app.MapControllers();

// 運行應用程式
app.Run();
