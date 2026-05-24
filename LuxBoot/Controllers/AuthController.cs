using LuxBoot.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LuxBoot.Controllers
{
    public class AuthController : Controller
    {
        private readonly UserManager<UserApp> _context;
        private readonly SignInManager<UserApp> _signInManager;

        public AuthController(UserManager<UserApp> context, SignInManager<UserApp> signInManager)
        {
            _context = context;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Account");
            }
            return View();
        }

        [HttpPost]

        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if(ModelState.IsValid)
            {
                var login = await _context.FindByNameAsync(model.UserName);
                
                if(login != null)
                {
                    PasswordHasher<string> hasher = new();
                    var result = hasher.VerifyHashedPassword(
                                null,
                                login.PasswordHash,
                                model.Password
                            );


                    if (result == PasswordVerificationResult.Success)
                    {
                        await _signInManager.SignInAsync(login, false, null);
                        return RedirectToAction("Home", "Account");
                    }
                    else
                    {
                        TempData["Message"] = "Password Was Not correct!";
                        return View(model);
                    }
                   
                }

                TempData["Message"] = "Invalid username or password.";
                return View(model);

            }
            TempData["Message"] = "Please fill out all feilds.";
            return View(model);
        }
        
        public IActionResult Register()
        {
            if (User.Identity.IsAuthenticated)
            {
                return RedirectToAction("Home", "Account");
            }
            return View();
        }


        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel info)
        {
            if (ModelState.IsValid)
            {
                Console.WriteLine("Model state is valid");

                

                var user = await _context.CreateAsync(new UserApp() { UserName = info.UserName }, info.Password);
                if (user.Succeeded)
                {
                    var findtheuser = await _context.FindByNameAsync(info.UserName);

                    var finddata = await _context.Users.Include(x => x.AccountInfo).FirstOrDefaultAsync(x => x.Id == findtheuser.Id);

                    AccountInfoModel change = new AccountInfoModel()
                    {
                        MemberSince = $"{DateTime.UtcNow}",
                        CurrentPlan = "Basic",
                        UserId = findtheuser.Id,
                        AttacksLeft = 10,
                    };

                    finddata.AccountInfo = change;
                   

                    await _context.UpdateAsync(finddata);
                    TempData["Message"] = "User was made succesfuly";
                    return RedirectToAction("Home", "Account", TempData);

                }

                Console.WriteLine("User was not made");
                TempData["Message"] = "Something went wrong while making the account, please try again.";
                return View(info);

            }

            TempData["Message"] = "Please check all feilds again.";
            
            return View(info);

            
        }
        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Login", "Auth");
        }
    }
}
