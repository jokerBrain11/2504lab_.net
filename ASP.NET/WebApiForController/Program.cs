using WebApiForController.Middleware;
using WebApiForController.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();

builder.Services.AddScoped<IUserServices, UserServices>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// 使用 HTTPS 重定向
app.UseHttpsRedirection();

// 使用授權中介軟體
app.UseAuthorization();

// 啟用路由
app.UseRouting();

// 引用自訂的中介軟體
app.UseMiddleware<UpdateLoggingMiddleware>();

// 配置控制器路由，這是終端中介軟體
app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();  // 確保控制器端點已映射
});

// 在這裡處理任何特定於 "/api/user" 的路由邏輯
app.Map("/api/user", apiApp =>
{
    apiApp.Use(async (context, next) =>
    {
        Console.WriteLine("廣域中介軟體處理 API 請求");
        await next();  // 確保這裡有調用 next()
        Console.WriteLine("廣域中介軟體處理 API 響應");
        Console.WriteLine("==================================");
    });

    apiApp.Run(async context =>
    {
        Console.WriteLine("廣域中介軟體處理至Run");
    });
});

// 最後，啟動應用程序
app.Run();
