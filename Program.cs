

var builder = WebApplication.CreateBuilder(args);

// Đăng ký các controller có view cho phần Admin
builder.Services.AddControllersWithViews();

// Đăng ký các controller chỉ trả về JSON cho API
builder.Services.AddControllers();

// Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Đăng ký CORS và cho phép truy cập từ http://localhost:3000
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigin",
        policy => policy.WithOrigins("http://localhost:3000")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});

var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
else if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
    // Chuyển hướng đến Swagger UI khi khởi động ở chế độ phát triển
    app.MapGet("/", context =>
    {
        context.Response.Redirect("/swagger");
        return Task.CompletedTask;
    });
}


app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// Sử dụng CORS với cấu hình đã thêm
app.UseCors("AllowSpecificOrigin");


app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

// Route cho API (chỉ trả về JSON)
app.MapControllers();

app.Run();
