using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PeriodicalsFinal.DataAccess.DAL;
using PeriodicalsFinal.DataAccess.Models;
using System.Linq;

namespace PeriodicalsFinal.DataAccess.Repository
{
    public class RoleRepository
    {
        private readonly RoleManager<IdentityRole> _roleManager = new RoleManager<IdentityRole>(
            new RoleStore<IdentityRole>(new ApplicationDbContext()));

        private readonly UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));

        public bool RoleExists(string name)
        {
            return _roleManager.RoleExists(name);
        }


        public bool CreateRole(string name)
        {
            var idResult = _roleManager.Create(new IdentityRole(name));
            return idResult.Succeeded;
        }


        public bool AddUserToRole(string userId, string roleName)
        {
            var idResult = _userManager.AddToRole(userId, roleName);
            return idResult.Succeeded;
        }

        public bool RemoveUserFromRole(string userId, string roleName)
        {
            var idResult = _userManager.RemoveFromRole(userId, roleName);
            return idResult.Succeeded;
        }

        public string GetUserRole(string userId)
        {
            return _userManager.GetRoles(userId).FirstOrDefault();
        }

    }
}
