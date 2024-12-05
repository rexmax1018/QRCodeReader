var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

var mvcBuilder = builder.Services.AddRazorPages();

if (builder.Environment.IsDevelopment())
{
    mvcBuilder.AddRazorRuntimeCompilation();
}

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    // 將所有例外狀況重導向到 /Home/Error
    app.UseExceptionHandler("/Home/Error");

    // 設定 HSTS (僅用於生產環境)
    app.UseHsts();
}

// 啟用 HTTPS 強制重導向
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// 配置 StatusCodePages 來捕捉 404、403、401、500 等狀態碼，並重導向到自訂的 Error 頁面
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    // 根據不同的狀態碼重導向到指定的錯誤頁面
    if (response.StatusCode == 404)
    {
        response.Redirect("/Home/Error?statusCode=404");
    }
    else if (response.StatusCode == 403)
    {
        response.Redirect("/Home/Error?statusCode=403");
    }
    else if (response.StatusCode == 401)
    {
        response.Redirect("/Home/Error?statusCode=401");
    }
    else if (response.StatusCode == 500)
    {
        response.Redirect("/Home/Error?statusCode=500");
    }
});

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
