using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MediTechHospital.Models;
using System.Threading.Tasks;

namespace MediTechHospital.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<IdentityUser> _userManager;
		private readonly SignInManager<IdentityUser> _signInManager;

		public AccountController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager)
		{
			_userManager = userManager;
			_signInManager = signInManager;
		}

		// GET: /Account/Login
		[HttpGet]
		public IActionResult Login(string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			return View();
		}

		// POST: /Account/Login
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model, string returnUrl = null)
		{
			ViewData["ReturnUrl"] = returnUrl;
			if (!ModelState.IsValid) return View(model);

			var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: false);
			if (result.Succeeded)
			{
				if (!string.IsNullOrEmpty(returnUrl) && Url.IsLocalUrl(returnUrl))
					return Redirect(returnUrl);

				return RedirectToAction("Index", "Dashboard");
			}

			ModelState.AddModelError(string.Empty, "محاولة تسجيل الدخول غير صالحة.");
			return View(model);
		}

		// GET: /Account/Register
		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		// POST: /Account/Register
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var user = new IdentityUser { UserName = model.Email, Email = model.Email };
			var result = await _userManager.CreateAsync(user, model.Password);
			//if (result.Succeeded)
			//{
			//	// Optionally add default role here:
			//	// await _userManager.AddToRoleAsync(user, "Patient");

			//	await _signInManager.SignInAsync(user, isPersistent: false);
			//	return RedirectToAction("Login", "Account");
			//}


			if (result.Succeeded)
			{
				// Default role is Patient
				await _userManager.AddToRoleAsync(user, "Patient");

				await _signInManager.SignInAsync(user, isPersistent: false);
				return RedirectToAction("Index", "Dashboard");
			}


			foreach (var err in result.Errors)
				ModelState.AddModelError(string.Empty, err.Description);

			return View(model);
		}

		// POST: /Account/Logout
		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Login", "Account");
		}

		// ===================== Forgot / Reset Password =====================
		[HttpGet]
		public IActionResult ForgotPassword()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null)
			{
				// لا نكشف للمهاجمين ما إذا كان البريد موجودًا
				return RedirectToAction(nameof(ForgotPasswordConfirmation));
			}

			var token = await _userManager.GeneratePasswordResetTokenAsync(user);

			// في بيئة حقيقية: أرسل رابط عبر البريد يحتوي على token (يجب URL-encode)
			var resetLink = Url.Action("ResetPassword", "Account", new { token = token, email = model.Email }, Request.Scheme);
			TempData["ResetLink"] = resetLink; // للاختبار فقط

			return RedirectToAction(nameof(ForgotPasswordConfirmation));
		}

		[HttpGet]
		public IActionResult ForgotPasswordConfirmation()
		{
			ViewBag.ResetLink = TempData["ResetLink"];
			return View();
		}

		[HttpGet]
		public IActionResult ResetPassword(string token = null, string email = null)
		{
			if (token == null || email == null) return RedirectToAction("Login", "Account");

			var model = new ResetPasswordViewModel { Token = token, Email = email };
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (!ModelState.IsValid) return View(model);

			var user = await _userManager.FindByEmailAsync(model.Email);
			if (user == null) return RedirectToAction(nameof(ResetPasswordConfirmation));

			var result = await _userManager.ResetPasswordAsync(user, model.Token, model.Password);
			if (result.Succeeded) return RedirectToAction(nameof(ResetPasswordConfirmation));

			foreach (var err in result.Errors)
				ModelState.AddModelError(string.Empty, err.Description);

			return View(model);
		}

		[HttpGet]
		public IActionResult ResetPasswordConfirmation()
		{
			return View();
		}


		

	}
}

