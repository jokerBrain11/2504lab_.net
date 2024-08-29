namespace WebApiForController.Middleware
{
    // 定義 UpdateLoggingMiddleware 類別，這是一個中介軟體 (Middleware) 類別，用於處理 HTTP 請求和響應。
    class UpdateLoggingMiddleware
    {
        // 保存下一個中介軟體委託的參考，這是為了能夠將請求傳遞給管道中的下一個中介軟體。
        private readonly RequestDelegate _next;

        // 構造函數，接收一個 RequestDelegate 參數，用於初始化 _next 字段。
        public UpdateLoggingMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        // 中介軟體的核心方法，用於處理每個 HTTP 請求。
        public async Task InvokeAsync(HttpContext context)
        {
            // 檢查當前請求的路徑是否以 "/api/User" 開頭，並且請求方法是否為 "GET"。
            if (context.Request.Path.StartsWithSegments("/api/User") && context.Request.Method == "GET")
            {
                // 如果條件滿足，輸出處理更新 API 請求的消息到控制台。
                Console.WriteLine("自訂中介軟體處理 Update API Request");
            }

            // 將請求傳遞給管道中的下一個中介軟體。
            await _next(context);

            // 再次檢查請求的路徑和方法，確認請求是否已完成。
            if (context.Request.Path.StartsWithSegments("/api/User") && context.Request.Method == "GET")
            {
                // 如果條件滿足，輸出完成更新 API 請求的消息到控制台。
                Console.WriteLine("自訂中介軟體處理 Update API Response");
                Console.WriteLine("==================================");
            }
        }
    }
}
