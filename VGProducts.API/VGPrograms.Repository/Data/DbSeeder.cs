using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System.Security.Claims;

namespace VGProducts.API.Extentions
{
    public static class DbSeeder
    {
        public static async Task SeedRoles(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();

            string[] roles = { "Admin", "User" };
            foreach (var role in roles)
            {
                if (!await roleManager.RoleExistsAsync(role))
                {
                    await roleManager.CreateAsync(new IdentityRole<Guid>(role));
                }
            }
        }

        public static async Task SeedRolesAndClaims(IServiceProvider serviceProvider)
        {
            var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var adminRole = await roleManager.FindByNameAsync("Admin");
            var userRole = await roleManager.FindByNameAsync("User");

            if (adminRole != null)
            {
                var adminClaims = await roleManager.GetClaimsAsync(adminRole);
                if (adminClaims.Any(c => c.Type == "Seeded"))
                    return;


                string[] adminPermission =
                {
                    "CreateCategory","UpdateCategory","DeleteCategory","ViewCategory",
                    "CreateSubCategory","UpdateSubCategory","DeleteSubCategory","ViewSubCategory",
                    "CreateProduct","UpdateProduct","DeleteProduct","ViewProduct",
                    "CreateCountry","GetCountry","GetCountryById","DeleteCountry",
                    "CreateState","GetState","GetStateById","DeleteState",
                    "CreateCity","GetCity","GetCityById","DeleteCity",
                    "GetAllUser","GetOrder"
                };

                var existingPermissions = adminClaims.Where(c => c.Type == "Permission").Select(c => c.Value).ToHashSet(StringComparer.OrdinalIgnoreCase);

                foreach (var permission in adminPermission)
                {
                    if (!existingPermissions.Contains(permission))
                    {
                        await roleManager.AddClaimAsync(adminRole, new Claim("Permission", permission));
                    }
                }
            }

            if (userRole != null)
            {
                var userClaims = await roleManager.GetClaimsAsync(userRole);

                string[] userPermission =
                {
                    "ViewCategory","ViewSubCategory","ViewProduct",
                    "CreateFavourites","DeleteFavourites","ViewFavourites",
                    "CreateCartItem","ViewCartItem","DeleteCartItemById","DeleteAllCartItemAsync","AddByIdCartItemAsync",
                    "GetAddress","GetAddressById","CreateAddress","DeleteAddress","UpdateAddress",
                    "GetCity","GetCityById",
                    "GetState","GetStateById",
                    "GetCountry","GetCountryById",
                    "CreateOrder","GetOrder","DeleteOrder","SelectPaymentMethod","GetPaymentQr",
                    "UpdateUser","ChangePassword","CreateOrUpdateReview","GetProductById","GetInvoice"
                };
                var existingPermissions = userClaims.Where(c => c.Type == "Permission").Select(c => c.Value).ToHashSet(StringComparer.OrdinalIgnoreCase);

                foreach (var permission in userPermission)
                {
                    if (!existingPermissions.Contains(permission))
                    {
                        await roleManager.AddClaimAsync(userRole, new Claim("Permission", permission));
                    }
                }
            }
        }
    }
}
