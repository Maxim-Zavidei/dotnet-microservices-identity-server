using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Movies.Client.ApiServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IMovieApiService, MovieApiService>();

// Add services to the container.
builder.Services.AddControllersWithViews();

builder.Services
    .AddAuthentication(opt =>
    {
        opt.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
        opt.DefaultChallengeScheme = OpenIdConnectDefaults.AuthenticationScheme;
    })
    .AddCookie(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddOpenIdConnect(OpenIdConnectDefaults.AuthenticationScheme, opt =>
    {
        opt.Authority = "https://localhost:5005";

        opt.ClientId = "movies_mvc_client";
        opt.ClientSecret = "secret";
        opt.ResponseType = "code";

        opt.Scope.Add("openid");
        opt.Scope.Add("profile");

        opt.SaveTokens = true;
        opt.GetClaimsFromUserInfoEndpoint = true;
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

app.UseAuthentication();
app.UseAuthorization();

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();
