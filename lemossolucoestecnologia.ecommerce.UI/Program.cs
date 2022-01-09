using lemossolucoestecnologia.ecommerce.UI.Handlers;
using lemossolucoestecnologia.ecommerce.UI.Services;
using Refit;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();
//builder.Services.AddScoped<IUsersServices>();
//builder.Services.AddScoped<ILoginServices>();

var clientHandler = new HttpClientHandler();
builder.Services.AddTransient<BearerTokenMassageHandler>();
builder.Services.AddRefitClient<ILoginServices>()
    .AddHttpMessageHandler<BearerTokenMassageHandler>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WebApiBaseUri"));
    }).ConfigurePrimaryHttpMessageHandler(c => clientHandler);

builder.Services.AddRefitClient<IUsersServices>()
    .AddHttpMessageHandler<BearerTokenMassageHandler>()
    .ConfigureHttpClient(c =>
    {
        c.BaseAddress = new Uri(builder.Configuration.GetValue<string>("WebApiBaseUri"));
    }).ConfigurePrimaryHttpMessageHandler(c => clientHandler);
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
