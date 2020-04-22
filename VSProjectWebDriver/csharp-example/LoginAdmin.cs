using System;
using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;




namespace csharp_example
{
    [TestFixture]
    public class Login
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void LoginAdmin()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            //driver.FindElement(By.Name("btnG")).Click(); + Keys.Enter
            //wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));
        }

        [TearDown]
        public void stop()
        {
            driver.Quit();
            driver = null;
        }
    }
}
