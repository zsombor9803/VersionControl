using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTestExample.Controllers;

namespace UnitTestExample.Test
{
    public class AccountControllerTestFixture
    {
        [
            Test,
            TestCase("jelszo03", false),
            TestCase("valami@gmail", false),
            TestCase("valami.gmail.com", false),
            TestCase("valami@gmail.com", true)
        ]
        public void TestValidateEmail(string email, bool expectedResult)
        {
            var accountController = new AccountController();

            var actualResult = accountController.ValidateEmail(email);

            Assert.AreEqual(expectedResult, actualResult);
        }

        [
            Test,
            TestCase("Valamiasd", false),
            TestCase("VALAMIASD", false),
            TestCase("valamiasd", false),
            TestCase("valami", false),
            TestCase("Valami123", true)
        ]
        public void TestValidatePassword(string password, bool expectedResult)
        {
            var accountController = new AccountController();

            var actualResult = accountController.ValidatePassword(password);

            Assert.AreEqual(expectedResult, actualResult);
        }
    }
}
