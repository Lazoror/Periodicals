using System.Collections.Generic;
using System.Data.Entity;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.AspNet.Identity;
using PeriodicalsFinal.DataAccess.Repository;

namespace PeriodicalsFinal.DataAccess.DAL
{
    public class ApplicationInitializer : CreateDatabaseIfNotExists<ApplicationDbContext>
    {
        RoleRepository roleRepository = new RoleRepository();

        protected override void Seed(ApplicationDbContext context)
        {
            var roles = new List<string>
            {
                "User",
                "Admin",
                "Publisher"
            };

            foreach (string role in roles)
            {
                roleRepository.CreateRole(role);
            }
        }
    }
}
