using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PeriodicalsFinal.DataAccess.DAL;
using PeriodicalsFinal.DataAccess.Models;
using PeriodicalsFinal.DataAccess.Payment;

namespace PeriodicalsFinal.Tests.Payment
{
    [TestClass]
    public class PaymentBonusTest
    {

        [TestMethod]
        public void Bonus20Percent_ValidParameter_ReturnTwo()
        {
            PaymentBonus paymentBonus = new PaymentBonus();

            float result = paymentBonus.Bonus20Percent(10);

            Assert.AreEqual(2, result);
        }

        [TestMethod]
        public void Bonus20Percent_LargeThan10_ReturnNotTwo()
        {
            PaymentBonus paymentBonus = new PaymentBonus();

            float result = paymentBonus.Bonus20Percent(20);

            Assert.AreNotEqual(2, result);
        }

        [TestMethod]
        public void Bonus20Percent_ZeroParameter_NotZero()
        {
            PaymentBonus paymentBonus = new PaymentBonus();

            float result = paymentBonus.Bonus20Percent(0);

            Assert.AreNotEqual(1, result);
        }

        [TestMethod]
        public void Bonus20Percent_SmallerThanZero_NotZero()
        {
            PaymentBonus paymentBonus = new PaymentBonus();

            float result = paymentBonus.Bonus20Percent(-1);

            Assert.AreNotEqual(1, result);
        }

        [TestMethod]
        public void Bonus10Percent_ValidParameter_ReturnOne()
        {
            PaymentBonus paymentBonus = new PaymentBonus();

            float result = paymentBonus.Bonus10Percent(10);

            Assert.AreEqual(1, result);
        }

        [TestMethod]
        public void Bonus10Percent_LargeThan50_Return5Point1()
        {
            PaymentBonus paymentBonus = new PaymentBonus();

            float result = paymentBonus.Bonus10Percent(51);

            Assert.AreEqual(5.1.ToString(), @String.Format("{0:0.0}", result));
        }

        [TestMethod]
        public void Bonus10Percent_ZeroParameter_NotZero()
        {
            PaymentBonus paymentBonus = new PaymentBonus();

            float result = paymentBonus.Bonus10Percent(0);

            Assert.AreNotEqual(1, result);
        }

        [TestMethod]
        public void Bonus10Percent_SmallerThanZero_NotZero()
        {
            PaymentBonus paymentBonus = new PaymentBonus();

            float result = paymentBonus.Bonus20Percent(-1);

            Assert.AreNotEqual(1, result);
        }


        
    }
}
