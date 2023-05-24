using _02_boardapp.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace _02_boardapp.Controllers
{
    // 사용자 회원가입, 로그인, 로그아웃
    public class AccountController : Controller
    {
        
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;

        public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
        {
            _userManager = userManager ?? throw new ArgumentNullException(nameof(userManager));
            _signInManager = signInManager ?? throw new ArgumentNullException(nameof(signInManager));
        }

        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        // 비동기가 아니면 return IActionResult
        // 비동기가 되면 Task<IActionResult>
        public async Task<IActionResult> Register(RegisterModel model)
        {
            ModelState.Remove("PhoneNumber"); // phonenumber는 입력값 검증 x
            if (ModelState.IsValid)
            {
                // ASP.NET users - aspnetusers 테이블에 데이터를 넣기 위해 매핑되는 인스턴스 생성
                var user = new IdentityUser() { 
                    UserName = model.Email,
                    Email = model.Email,
                    PhoneNumber = model.PhoneNumber
                };

                // aspnetusers 테이블에 사용자 데이터 대입
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Index", "Home");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }
            return View(model);

        }

        [HttpGet]
        public IActionResult Login()
        {
            return View();
        }
    }
}
