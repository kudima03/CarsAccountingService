﻿using IdentityServer.Models;
using IdentityServer.Services;
using IdentityServer.ViewModels;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace IdentityServer.Controllers;

public class AccountController : Controller
{
    private readonly IConfiguration _configuration;
    private readonly IIdentityServerInteractionService _interaction;
    private readonly ILoginService<ApplicationUser> _loginService;
    private readonly UserManager<ApplicationUser> _userManager;


    public AccountController(
        ILoginService<ApplicationUser> loginService,
        IIdentityServerInteractionService interaction,
        UserManager<ApplicationUser> userManager,
        IConfiguration configuration)
    {
        _loginService = loginService;
        _interaction = interaction;
        _userManager = userManager;
        _configuration = configuration;
    }

    [HttpGet]
    public async Task<IActionResult> Login(string returnUrl)
    {
        var authorizationContext = await _interaction.GetAuthorizationContextAsync(returnUrl);
        var vm = BuildLoginViewModel(returnUrl, authorizationContext);
        ViewData["ReturnUrl"] = returnUrl;
        return View(vm);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Login(LoginViewModel model)
    {
        if (ModelState.IsValid)
        {
            var user = await _loginService.FindByUsername(model.Email);
            if (await _loginService.ValidateCredentials(user, model.Password))
            {
                var tokenLifeTime = _configuration.GetValue("TokenLifetimeMinutes", 120);
                var properties = new AuthenticationProperties
                {
                    ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(tokenLifeTime),
                    AllowRefresh = true,
                    RedirectUri = model.ReturnUrl,
                    IsPersistent = false
                };
                await _loginService.SignInAsync(user, properties);
                if (_interaction.IsValidReturnUrl(model.ReturnUrl)) return Redirect(model.ReturnUrl);
                return Redirect("~/");
            }

            ModelState.AddModelError("", "Invalid username or password.");
        }

        var vm = await BuildLoginViewModel(model);

        ViewData["ReturnUrl"] = model.ReturnUrl;

        return View(vm);
    }

    private LoginViewModel BuildLoginViewModel(string returnUrl, AuthorizationRequest authorizationContext)
    {
        return new LoginViewModel
        {
            ReturnUrl = returnUrl,
            Email = authorizationContext.LoginHint
        };
    }

    private async Task<LoginViewModel> BuildLoginViewModel(LoginViewModel model)
    {
        var context = await _interaction.GetAuthorizationContextAsync(model.ReturnUrl);
        var vm = BuildLoginViewModel(model.ReturnUrl, context);
        vm.Email = model.Email;
        return vm;
    }

    [HttpGet]
    [AllowAnonymous]
    public IActionResult Register(string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        return View();
    }

    [HttpPost]
    [AllowAnonymous]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Register(RegisterViewModel model, string returnUrl = null)
    {
        ViewData["ReturnUrl"] = returnUrl;
        if (ModelState.IsValid)
        {
            var user = new ApplicationUser
            {
                UserName = model.Email,
                Email = model.Email,
                City = model.User.City,
                Country = model.User.Country,
                LastName = model.User.LastName,
                Name = model.User.Name,
                Street = model.User.Street,
                PhoneNumber = model.User.PhoneNumber
            };
            var result = await _userManager.CreateAsync(user, model.Password);
            if (result.Errors.Any())
            {
                AddErrors(result);
                return View(model);
            }
        }

        if (returnUrl != null)
        {
            if (HttpContext.User.Identity.IsAuthenticated)
                return Redirect(returnUrl);
            if (ModelState.IsValid)
                return RedirectToAction("login", "account", returnUrl);
            return View(model);
        }

        return RedirectToAction("index", "home");
    }

    private void AddErrors(IdentityResult result)
    {
        foreach (var error in result.Errors) ModelState.AddModelError(string.Empty, error.Description);
    }
}