using CONSUMIR_API_TEST.Servicios;

WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
// Asi inyectamos nuestra interface (IServicio_API) con su clase (Servicio_API)
// para poder usarlo dentro de cualquier controlador que este en el proyecto
builder.Services.AddScoped<IServicio_API, Servicio_API>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
