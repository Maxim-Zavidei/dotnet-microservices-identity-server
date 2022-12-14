using System.Diagnostics;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Movies.Client.ApiServices;
using Movies.Client.Models;

namespace Movies.Client;

[Authorize]
public class MoviesController : Controller
{
    private readonly IMovieApiService movieApiService;

    public MoviesController(IMovieApiService movieApiService)
    {
        this.movieApiService = movieApiService;
    }

    public async Task LogTokenAndClaims()
    {
        var identityToken = await HttpContext.GetTokenAsync(OpenIdConnectParameterNames.IdToken);

        Debug.WriteLine($"Identity token: {identityToken}");

        foreach (var claim in User.Claims)
        {
            Debug.WriteLine($"Claim type: {claim.Type} - Claim value: {claim.Value}");
        }
    }

    public async Task Logout()
    {
        await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        await HttpContext.SignOutAsync(OpenIdConnectDefaults.AuthenticationScheme);
    }

    // GET: Movies
    public async Task<IActionResult> Index()
    {
        var _ = LogTokenAndClaims();
        return View(await movieApiService.GetMovies());
    }

    // GET: Movies/Details/5
    public async Task<IActionResult> Details(int? id)
    {
        return View();
    }

    // GET: Movies/Create
    public IActionResult Create()
    {
        return View();
    }

    // POST: Movies/Create
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] Movie movie)
    {
        return View();
    }

    // GET: Movies/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
        return View();
    }

    // POST: Movies/Edit/5
    // To protect from overposting attacks, enable the specific properties you want to bind to.
    // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,Title,Genre,Rating,ReleaseDate,ImageUrl,Owner")] Movie movie)
    {
        return View();
    }

    [Authorize(Roles = "admin")]
    public async Task<IActionResult> OnlyAdmin()
    {
        var userInfo = await movieApiService.GetUserInfo();
        return View(userInfo);
    }

    // GET: Movies/Delete/5
    public async Task<IActionResult> Delete(int? id)
    {
        return View();
    }

    // POST: Movies/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        return View();
    }

    private bool MovieExists(int id)
    {
        return true;
    }
}
