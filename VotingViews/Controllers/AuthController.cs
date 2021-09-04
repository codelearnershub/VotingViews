﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using VotingViews.Domain.IService;
using VotingViews.DTOs;
using VotingViews.Models;

namespace VotingViews.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAdminService _adminService;
        private readonly IVoterService _voterService;
        private readonly IUserService _userService;

        public AuthController(IUserService userService, IVoterService voterService, IAdminService adminService)
        {
            _userService = userService;
            _voterService = voterService;
            _adminService = adminService;
        }

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Register()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Register(AuthVM.Register model)
        {
            RegisterUserDto user = new RegisterUserDto
            {
                Type = "voter",
                FirstName = model.FirstName,
                LastName = model.LastName,
                MiddleName = model.MiddleName,
                Email = model.Email,
                Password = model.Password,
                Address = model.Address,
            };

            var registeredUser = _userService.Register(user);

            return RedirectToAction(nameof(Login));
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(AuthVM.Login model)
        {
            if(!ModelState.IsValid)
            {
                return View(model);
            }

            LoginUserDto loginUser = new LoginUserDto
            {
                Email = model.Email,
                Password = model.Password
            };

            LoggedInUserDto user = null;

            try
            {
                user = _userService.Login(loginUser);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
                return View(model);
            }

            if(user != null)
            {
                string fullName = String.Empty;

                switch (user.Role.Name)
                {
                    case "voter":
                        var voter = _voterService.GetVoter(user.Id);
                        fullName = $"{voter.FirstName} {voter.LastName}";
                        break;
                    case "admin":
                        var admin = _adminService.GetAdmin(user.Id);
                        fullName = $"{admin.FirstName} {admin.LastName}";
                        break;
                }

                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
                    new Claim(ClaimTypes.Name, fullName),
                    new Claim(ClaimTypes.Role, user.Role.Name),
                };

                var identity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                var principal = new ClaimsPrincipal(identity);
                var props = new AuthenticationProperties();

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal, props);

                switch (user.Role.Name)
                {
                    case "voter":
                        return RedirectToAction(actionName: "Dashboard", controllerName: "Voter");
                    case "admin":
                        return RedirectToAction(actionName: "Dashboard", controllerName: "Admin");
                    default:
                        ViewBag.loginError = "A user validation occurred. Please contact support";
                        return View();
                }
            }
            ViewBag.loginError = "Invalid email or password";
            return View();

        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }

    }
}
