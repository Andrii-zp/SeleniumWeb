//Задание 11. Сделайте сценарий регистрации пользователя
//    1) регистрация новой учётной записи с достаточно уникальным адресом электронной почты(чтобы не конфликтовало с ранее созданными пользователями, в том числе при предыдущих запусках того же самого сценария),
//    2) выход(logout), потому что после успешной регистрации автоматически происходит вход,
//    3) повторный вход в только что созданную учётную запись,
//    4) и ещё раз выход.


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Linq;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class CreateAccount
    {
        private static Random random = new Random();
        private IWebDriver driver;
        private WebDriverWait wait;
        private string email;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        [Test]
        public void CheckCreateAccount()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/en/");
            IWebElement link = driver.FindElement(By.CssSelector("div#box-account-login a[href*=create_account]"));
            link.Click();


            driver.FindElement(By.Name("firstname")).SendKeys("Andrii");
            driver.FindElement(By.Name("lastname")).SendKeys("Terentiev");
            driver.FindElement(By.Name("address1")).SendKeys("L.Tolstoy str.");
            driver.FindElement(By.Name("postcode")).SendKeys("04809");
            driver.FindElement(By.Name("city")).SendKeys("Kiev");
            driver.FindElement(By.CssSelector("span.selection")).Click();
            driver.FindElement(By.CssSelector("input.select2-search__field")).SendKeys("Ukraine" + Keys.Enter);
            email = "Andrii_" + RandomString(7) + "@gmail.com";
            driver.FindElement(By.Name("email")).SendKeys(email);
            driver.FindElement(By.Name("phone")).SendKeys("+380994442211");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("confirmed_password")).SendKeys("admin");
            driver.FindElement(By.CssSelector("button[type=submit]")).Click();
            Thread.Sleep(200);
            logout();
            login(email, "admin");
            logout();


        }


        [TearDown]
        public void stop()
        {
            Thread.Sleep(200);
            driver.Quit();
            driver = null;
        }


        private void login(string email, string pwd)
        {

            IWebElement eLogin = driver.FindElement(By.CssSelector("div#box-account-login input[name=email]"));
            eLogin.Click();
            eLogin.Clear();
            eLogin.SendKeys(email);

            IWebElement ePWD = driver.FindElement(By.CssSelector("div#box-account-login input[name=password]"));
            ePWD.Click();
            ePWD.Clear();
            ePWD.SendKeys(pwd);

            driver.FindElement(By.CssSelector("div#box-account-login button[name=login]")).Click();
            Thread.Sleep(200);

        }
        private void logout()
        {
            driver.FindElement(By.CssSelector("div#box-account a[href*=logout]")).Click();
            Thread.Sleep(200);
        }

        public static string RandomString(int length)
        {
            const string chars = "abcdefghijklmnopqrstuvwxyz0123456789_.";
            return new string(Enumerable.Repeat(chars, length)
              .Select(s => s[random.Next(s.Length)]).ToArray());
        }

    }
}
