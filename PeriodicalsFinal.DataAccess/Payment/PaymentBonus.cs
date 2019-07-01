using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using PeriodicalsFinal.DataAccess.DAL;
using PeriodicalsFinal.DataAccess.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PeriodicalsFinal.DataAccess.Payment
{
    public class PaymentBonus
    {
        private readonly UserManager<ApplicationUser> _userManager = new UserManager<ApplicationUser>(
            new UserStore<ApplicationUser>(new ApplicationDbContext()));


        public bool IsUserBirthdayBoy(string userName)
        {
            ApplicationUser user = _userManager.FindByName(userName);

            if(user == null)
            {
                return false;
            }

            DateTime.TryParse(user.BirthDate.ToString(), out DateTime userBirthday);

            if (userBirthday.Day == DateTime.UtcNow.Day && userBirthday.Month == DateTime.UtcNow.Month)
            {
                return true;
            }
            else
            {
                return false;
            }
        }

        public float PayBonusIfBirhday(string userName, float payAmount)
        {

            if (IsUserBirthdayBoy(userName))
            {
                if (payAmount <= 10)
                {
                    return Bonus20Percent(payAmount);
                }
                else if (payAmount <= 50)
                {
                    return Bonus15Percent(payAmount);
                }
                else
                {
                    return Bonus10Percent(payAmount);
                }
            }

            return 0f;

        }

        public float IsLargePayment(float payAmount)
        {
            if(payAmount >= 50f)
            {
                return Bonus5Percent(payAmount);
            }

            return 0f;
        }

        public float Bonus20Percent(float payAmount)
        {
            return payAmount * 0.2f;
        }

        public float Bonus15Percent(float payAmount)
        {
            return payAmount * 0.15f;
        }

        public float Bonus10Percent(float payAmount)
        {
            return payAmount * 0.1f;
        }

        public float Bonus5Percent(float payAmount)
        {
            return payAmount * 0.05f;
        }
    }
}
