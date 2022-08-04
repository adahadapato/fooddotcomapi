using Microsoft.AspNetCore.Identity;
using System;

namespace fooddotcomapi.Authorization
{
    public class SeedIdentityContext
    {
        public static void SeedData(UserManager<ApplicationUser> userManager, RoleManager<ApplicationRole> roleManager)
        {
            SeedRoles(roleManager);
            SeedUsers(userManager);
        }


        public static void SeedRoles(RoleManager<ApplicationRole> roleManager)
        {
            try
            {


                if (!roleManager.RoleExistsAsync("User").Result)
                {
                    ApplicationRole role = new ApplicationRole
                    {
                        Name = "User",
                        Description = "Perform user level functions."
                    };
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                }


                if (!roleManager.RoleExistsAsync("Admin").Result)
                {
                    ApplicationRole role = new ApplicationRole
                    {
                        Name = "Admin",
                        Description = "Perform admin level functions."
                    };
                    IdentityResult roleResult = roleManager.
                    CreateAsync(role).Result;
                }
            }
            catch (Exception) { }
        }

        public static void SeedUsers(UserManager<ApplicationUser> userManager)
        {
            //if (userManager.FindByNameAsync("adetunji.adesola82@gmail.com").Result == null)
            //{
            //    ApplicationUser user = new ApplicationUser();
            //    user.UserName = "adetunji.adesola82@gmail.com";
            //    user.Email = "adetunji.adesola82@gmail.com";
            //    user.FirstName = "Adeshola";
            //    user.LastName = "Adetunji";
            //    user.DateCreated = System.DateTime.Now.Date;
            //    user.PhoneNumber = "2349079820595";
            //    //.BirthDate = new DateTime(1960, 1, 1);

            //    IdentityResult result = userManager.CreateAsync(user, "Emypat04me#@!").Result;

            //    if (result.Succeeded)
            //    {
            //        userManager.AddToRoleAsync(user, "Admin").Wait();
            //    }
            //}

            try
            {
                if (userManager.FindByNameAsync("adahadapato").Result == null)
                {
                    ApplicationUser user = new ApplicationUser
                    {
                        UserName = "adahadapato",
                        Email = "adahadapato@gmail.com",
                        FirstName = "Enobong",
                        LastName = "Adahada",
                        DateCreated = DateTime.Now.Date,
                        PhoneNumber = "08023176049",
                        NormalizedEmail="ADAHADAPATO@GMAIL.COM",
                        NormalizedUserName= "ADAHADAPATO@GMAIL.COM"
                    };

                    IdentityResult result = userManager.CreateAsync(user, "Emypat04me#@!").Result;

                    if (result.Succeeded)
                    {
                        userManager.AddToRoleAsync(user, "Admin").Wait();
                    }
                }
            }
            catch (Exception) { }
           
        }
    }
}
