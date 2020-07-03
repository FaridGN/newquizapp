using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Intellect.Models;
using Intellect.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authentication;
using Microsoft.EntityFrameworkCore;

namespace Intellect.Controllers
{
    public class AccountController : Controller
    {
        private readonly IntellectDbContext _intellectDbContext;
        private readonly UserManager<ExamUser> _userManager;
        private readonly SignInManager<ExamUser> _signInManager;

        public AccountController(IntellectDbContext intellectDbContext, UserManager<ExamUser> userManager, SignInManager<ExamUser> signInManager)
        {
            _intellectDbContext = intellectDbContext;
            _userManager = userManager;
            _signInManager = signInManager;
        }

        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }

        private IActionResult RedirectToLocal(string returnUrl)
        {
            if (Url.IsLocalUrl(returnUrl))
            {

                return Redirect(returnUrl);
            }
            else
            {
                return RedirectToAction(nameof(HomeController.Index), "Home");
            }
        }

        //User registration

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> RegisterUser(int Id)
        {
            RegisterModel registermodel = new RegisterModel();
            registermodel.TestTaker = await _intellectDbContext.TestTakers.SingleOrDefaultAsync(x => x.Id == Id);
            return View(registermodel);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> RegisterUser(RegisterModel registermodel, int Id)
        {
            if (ModelState.IsValid)
            {
                registermodel.TestTaker = await _intellectDbContext.TestTakers.SingleOrDefaultAsync(x => x.Id == Id);

                ExamUser appUser = await _userManager.FindByEmailAsync(registermodel.Email);
                if (appUser != null)
                {
                    ModelState.AddModelError("", "Belə istifadəçi artıq mövcuddur");
                }
                else
                {
                    appUser = new ExamUser
                    {
                        UserName = registermodel.UserName,
                        Email = registermodel.Email,
                        TestTakerId = registermodel.TestTaker.Id
                    };

                    IdentityResult result = await _userManager.CreateAsync(appUser, registermodel.Password);

                    if (result.Succeeded)
                    {
                        IdentityResult newresult = await _userManager.AddToRoleAsync(appUser, RoleType.User.ToString());

                        if (newresult.Succeeded)
                        {
                            ViewBag.Alert = "Siz uğurla qeydiyyatdan keçdiniz";
                            await _signInManager.SignInAsync(appUser, isPersistent: false);
                        }

                        return RedirectToAction(nameof(HomeController.Index), "Home");
                    }
                    AddErrors(result);
                }

            }
            return View(registermodel);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginUser(string returnUrl = null)
        {
            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ViewData["ReturnUrl"] = returnUrl;

            return View();
        }
         
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LoginUser(LoginModel loginmodel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;

            if (ModelState.IsValid)
            {
                ExamUser currentuser = await _userManager.FindByEmailAsync(loginmodel.UserName);

                Microsoft.AspNetCore.Identity.SignInResult signInResult = await _signInManager.PasswordSignInAsync(loginmodel.UserName, loginmodel.Password, loginmodel.RememberMe, lockoutOnFailure: true);
                if (signInResult.Succeeded)
                {
                    var user = this.User;
                    return RedirectToAction(nameof(Ouruser));
                }
                else
                {  
                        ModelState.AddModelError("", "Username or password is incorrect");

                }
            }

            return View(loginmodel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogoutUser()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(HomeController.Index), "Home");
        }

        //Admin

        [HttpGet]
        public IActionResult AdminRegister()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminRegister(RegisterModel registerModel)
        {
            if (ModelState.IsValid)
            {
                ExamUser examUser = await _userManager.FindByEmailAsync(registerModel.Email);

                if(examUser != null)
                {
                    ModelState.AddModelError("", "This user already exists");
                }
                else
                {
                    examUser = new ExamUser
                    {
                        Email = registerModel.Email,
                        UserName = registerModel.UserName
                    };

                    if(registerModel.Password == registerModel.ConfirmPassword)
                    {
                        IdentityResult AdminResult = await _userManager.CreateAsync(examUser, registerModel.Password);

                        if (AdminResult.Succeeded)
                        {
                            IdentityResult result = await _userManager.AddToRoleAsync(examUser, RoleType.Admin.ToString());

                            if (result.Succeeded)
                            {
                                await _signInManager.SignInAsync(examUser, isPersistent: false);
                                return RedirectToAction(nameof(AdminController.Admin), "Admin");
                            }
                        }
                    }
                    else
                    {
                        ModelState.AddModelError("", "Confirm password is different");
                    }
   
                }
            }
            return View(registerModel);
        }

        [HttpGet]
        public IActionResult AdminLogin()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogin(LoginModel loginModel)
        {
            if (ModelState.IsValid)
            {
                Microsoft.AspNetCore.Identity.SignInResult result = await _signInManager.PasswordSignInAsync(loginModel.UserName, loginModel.Password, loginModel.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(AdminController.Admin), "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Username or password is incorrect");
                }
            }
            return View(loginModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> AdminLogout()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction(nameof(AdminLogin));
        }

        //User ops

        [HttpGet]
        public async Task<IActionResult> Ouruser()
        {
            AdminViewModel adminModel = new AdminViewModel();
            adminModel.Exams = await _intellectDbContext.Exams.ToListAsync();
            adminModel.Exam = await _intellectDbContext.Exams.LastOrDefaultAsync();
            return View(adminModel);
        }

        [HttpGet]
        public IActionResult Enroll()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Enroll(TestTaker testTaker, DateTime birthdate)
        {
            if (ModelState.IsValid)
            {
                testTaker.Birth = birthdate;
                testTaker.Result = 0;
                testTaker.UserQuestions = await _intellectDbContext.Questions.Where(q => q.ExamId == 3).Select(q => new UserQuestion { Question = q, TestTaker = testTaker }).ToListAsync();
                _intellectDbContext.TestTakers.Add(testTaker);
                await _intellectDbContext.SaveChangesAsync();
                return RedirectToAction(nameof(Proceed));
            }
            else
            {
                ModelState.AddModelError("", "Kecmedi");
                return View();
            }
        }

        [HttpGet]
        public async Task<IActionResult> GetProfile()
        {
            ExamUser examUser = new ExamUser();
            examUser.TestTaker = await _intellectDbContext.TestTakers.LastOrDefaultAsync();
            return View(examUser);
        }

        [HttpGet]
        public async Task<IActionResult> Proceed()
        {
            TestTaker testTaker = new TestTaker();
            testTaker = await _intellectDbContext.TestTakers.LastOrDefaultAsync();
            return View(testTaker);
        }

        [HttpGet]
        public IActionResult Myresult()
        {
            return View();
        }
    }
}