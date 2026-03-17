var builder = WebApplication.CreateBuilder(args);

// เพิ่มตัวนี้เข้าไปเพื่อให้ใช้ Controller ได้
builder.Services.AddControllersWithViews();
builder.Services.AddRazorPages();

var app = builder.Build();

// ... (ส่วนอื่นๆ เหมือนเดิม)

app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();

// เพิ่ม Route สำหรับ Controller
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=BackHome}/{action=Index}/{id?}");

app.MapRazorPages();
app.Run();