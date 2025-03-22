
using DataAccess.Identity;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Threading.Tasks;
namespace DataAccess
{
    public static class SeedData
    {
        public static async Task InitializeAsync(IServiceProvider serviceProvider)
        {
            using (var scope = serviceProvider.CreateScope()) // 🚀 Yeni Scope Açalım
            {
                var userManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.UserManager<AppUser>>();

                var roleManager = scope.ServiceProvider.GetRequiredService<Microsoft.AspNetCore.Identity.RoleManager<AppRole>>();
                if (roleManager == null)
                {
                    throw new Exception("RoleManager service could not be resolved. Make sure it is registered in Program.cs");
                }

                string[] roleNames = { "Admin", "User" };
                foreach (var roleName in roleNames)
                {
                    var roleExist = await roleManager.RoleExistsAsync(roleName);
                    if (!roleExist)
                    {
                        await roleManager.CreateAsync(new AppRole(roleName));
                    }
                }
                // Admin Kullanıcısı oluşturuluyor
                string adminEmail = "emrecankahraman17@example.com";
                var adminUser = await userManager.FindByEmailAsync(adminEmail);
                if (adminUser == null)
                {
                    adminUser = new AppUser
                    {
                        UserName = "ek",
                        Email = adminEmail,
                        Name = "Emrecan",
                        SurName = "Kahraman",
                        PhoneNumberConfirmed= false,
                        TwoFactorEnabled=false,
                        LockoutEnabled=false,
                        AccessFailedCount=1,
                        // Diğer alanlarınız varsa onları da ekleyebilirsiniz
                    };

                    var result = await userManager.CreateAsync(adminUser, "123456");
                    if (result.Succeeded)
                    {
                        // Admin rolüne ekliyoruz
                        await userManager.AddToRoleAsync(adminUser, "Admin");
                    }
                    else
                    {    // Her bir IdentityError’un Description değerini alarak tek bir string haline getiriyoruz
                        var errorDescriptions = result.Errors.Select(e => e.Description);
                        throw new Exception("Admin kullanıcısı oluşturulamadı: " + string.Join(", ", errorDescriptions));
                        throw new Exception("Admin kullanıcısı oluşturulamadı: "
                            + string.Join(", ", result.Errors));
                    }
                }
            }
        }
    }
}

