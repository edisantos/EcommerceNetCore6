using lemossolucoestecnologia.ecommerce.UI.Handlers;
using lemossolucoestecnologia.ecommerce.UI.Services;
using lemossolucoestecnologia.ecommerce.UI.ViewModels.Account;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Identity;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
builder.Services.AddHttpContextAccessor();

var clientHandler = new HttpClientHandler()
{
    ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) =>
    {
        return true; // He we set to ignore the SSL;
    }


};


builder.Services.AddTransient<BearerTokenMassageHandler>();

/*Interface Login*/
builder.Services.AddRefitClient<ILoginServices>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WebAPIBaseUri"));
    }).ConfigurePrimaryHttpMessageHandler(c => clientHandler);
/*Interface User Register*/
builder.Services.AddRefitClient<IUsersServices>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WebAPIBaseUri"));
    }).ConfigurePrimaryHttpMessageHandler(c=> clientHandler);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie();

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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
