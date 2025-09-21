using Microsoft.AspNetCore.Identity;

namespace MediTechHospital.Data
{
	public static class Identity
	{
		public static async Task SeedRolesAndAdminAsync(IServiceProvider serviceProvider)
		{
			var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
			var userManager = serviceProvider.GetRequiredService<UserManager<IdentityUser>>();

			string[] roles = { "Admin", "Doctor"};

			foreach (var role in roles)
			{
				if (!await roleManager.RoleExistsAsync(role))
					await roleManager.CreateAsync(new IdentityRole(role));
			}

			var adminEmail = "admin@meditech.com";
			var adminPassword = "Admin123!";

			var admin = await userManager.FindByEmailAsync(adminEmail);
			if (admin == null)
			{
				var user = new IdentityUser
				{
					UserName = adminEmail,
					Email = adminEmail,
					EmailConfirmed = true
				};

				var result = await userManager.CreateAsync(user, adminPassword);
				if (result.Succeeded)
				{
					await userManager.AddToRoleAsync(user, "Admin");
				}
			}
		}
	}
}
