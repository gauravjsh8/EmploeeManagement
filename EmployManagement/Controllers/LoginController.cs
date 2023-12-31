﻿using EmployManagement.NewFolder;
using EmployManagement.View_Model;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmployManagement.Controllers
{
    public class LoginController : Controller
    {
        private readonly ApplicationDbContext _applicationDbContext;
        //manages user
        private readonly UserManager<IdentityUser> _userManager;
        //helps in Signin process
        private readonly SignInManager<IdentityUser> _signInManager;
        //Manages the role
        private readonly RoleManager<IdentityRole> _roleManager;    
        public LoginController(ApplicationDbContext applicationDbContext, 
            UserManager<IdentityUser> userManager,
            SignInManager<IdentityUser> signInManager,
            RoleManager<IdentityRole> roleManager)
            
        {
            _applicationDbContext = applicationDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult RegisterUser()
        {//get role details
            ViewBag.RoleList = _applicationDbContext.Roles.ToList();
            return View();
        }

        public async Task<IActionResult> RegisterUsers(RegisterViewModel registerViewModel) 
        {
            var user = new IdentityUser()
            {
                UserName = registerViewModel.Email,
                Email = registerViewModel.Email
            };
            var userCreateResult = await _userManager.CreateAsync(user,registerViewModel.Password);
            if(userCreateResult.Succeeded)
                {
                var result = await _userManager.AddToRoleAsync(user, registerViewModel.RoleName);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, false);
                    return RedirectToAction("LoginUser");
                }
            }
            else 
            {
                //handle error and display error on register view page
                foreach (var item in userCreateResult.Errors)
                {
                    //pass error on model to catch on view asp-validation-summary
                    ModelState.AddModelError(string.Empty, "Error!" + item.Description);
                    //get role details
                    ViewBag.RoleList = _applicationDbContext.Roles.ToList();

                    //return to same view
                    return View("RegisterUser",registerViewModel);
                }
            }
           
        
            return RedirectToAction("RegisterUser");
        }
        public IActionResult LoginUser() // from layout method name

        { 
            return View(); 
        }
        public async Task<IActionResult> Logout()
        {
            //for Logout
            await _signInManager.SignOutAsync();
            //redirect to login view after signout
            return RedirectToAction("LoginUser");
        }

        public async Task<IActionResult> LoginUsers(LoginViewModel loginViewModel)
        {
            var identityResult = await _signInManager.
                PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password, false, false);
            if (identityResult.Succeeded)
            {
                return RedirectToAction("Index", "Home");
            }
            return RedirectToAction("LoginUser"); 
        }
        [Authorize(Roles ="Admin")] //gives authorization to admin only
        public IActionResult Roles()
        {
            return View();
        }
        public async Task<IActionResult> AddRoleDetails(RoleViewModel roleViewModel)
       
        {
            var role = new IdentityRole()
            {
                Name = roleViewModel.RoleName
            };
            var result = await _roleManager.CreateAsync(role);
                if (result.Succeeded) 
            { 
            return RedirectToAction("Index", "Home");

            }
            {
                return RedirectToAction("Roles");
                    }
        }

        
    }
}
