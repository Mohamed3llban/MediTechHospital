using Microsoft.AspNetCore.Identity;

namespace MediTechHospital.Data
{
	public static class SeedRoles
	{
		public static async Task InitializeAsync(RoleManager<IdentityRole> roleManager)
		{
			string[] roleNames = { "Admin", "Doctor", "Patient" };

			foreach (var roleName in roleNames)
			{
				if (!await roleManager.RoleExistsAsync(roleName))
				{
					await roleManager.CreateAsync(new IdentityRole(roleName));
				}
			}
		}
	}
}
