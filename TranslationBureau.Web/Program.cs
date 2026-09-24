using TranslationBureau.Application;
using TranslationBureau.Infrastructure;
var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllersWithViews();
// Точка сборки приложения: абстракции связываются с конкретными реализациями
builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);
var app = builder.Build();
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseAuthorization();
app.MapControllerRoute(
 name: "default",
 pattern: "{controller=Translators}/{action=Index}/{id?}");
var cultureInfo = new System.Globalization.CultureInfo("en-US");
cultureInfo.NumberFormat.NumberDecimalSeparator = ".";

System.Globalization.CultureInfo.DefaultThreadCurrentCulture = cultureInfo;
System.Globalization.CultureInfo.DefaultThreadCurrentUICulture = cultureInfo;

var localizedOptions = new RequestLocalizationOptions
{
    DefaultRequestCulture = new Microsoft.AspNetCore.Localization.RequestCulture(cultureInfo),
    SupportedCultures = new[] { cultureInfo },
    SupportedUICultures = new[] { cultureInfo }
};

app.UseRequestLocalization(localizedOptions);
app.Run();
