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
    // �N�Ҧ��ҥ~���p���ɦV�� /Home/Error
    app.UseExceptionHandler("/Home/Error");

    // �]�w HSTS (�ȥΩ�Ͳ�����)
    app.UseHsts();
}

// �ҥ� HTTPS �j��ɦV
app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// �t�m StatusCodePages �Ӯ��� 404�B403�B401�B500 �����A�X�A�í��ɦV��ۭq�� Error ����
app.UseStatusCodePages(async context =>
{
    var response = context.HttpContext.Response;

    // �ھڤ��P�����A�X���ɦV����w�����~����
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
