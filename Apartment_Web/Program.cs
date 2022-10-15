using Apartment_Web;
using Apartment_Web.Services;
using Apartment_Web.Services.IServices;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddAutoMapper(typeof(MapProperties));

builder.Services.AddHttpClient<IApartmentService, ApartmentService>();
builder.Services.AddScoped<IApartmentService, ApartmentService>();

builder.Services.AddHttpClient<IApartmentNumberService, ApartmentNumberService>();
builder.Services.AddScoped<IApartmentNumberService, ApartmentNumberService>();
builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
builder.Services.AddHttpClient<IAuthorizationService, AuthorizationService>();
builder.Services.AddScoped<IAuthorizationService, AuthorizationService>();

builder.Services.AddSession(options =>
{
    options.IdleTimeout = TimeSpan.FromMinutes(10);
    options.Cookie.HttpOnly = true;
    options.Cookie.IsEssential = true;
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();
app.UseSession();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
