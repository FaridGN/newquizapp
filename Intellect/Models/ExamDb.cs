using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Intellect.Models
{
    public class ExamDb
    {
        public static void Seed(IntellectDbContext intellectDbContext, RoleManager<IdentityRole> roleManager)
        {
            if (!intellectDbContext.Roles.Any())
            {
                string[] roleNames = Enum.GetNames(typeof(RoleType));
                foreach (string role in roleNames)
                {
                    IdentityRole IdentityRole = new IdentityRole(role);
                    roleManager.CreateAsync(IdentityRole).GetAwaiter().GetResult();

                }
                intellectDbContext.SaveChangesAsync();
            }
        }
    }
}
