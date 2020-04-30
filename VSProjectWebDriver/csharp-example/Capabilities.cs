using System;
using System.Threading;
using System.Drawing;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.IE;
using OpenQA.Selenium.Support.UI;



namespace csharp_example
{
    [TestFixture]
    public class CapabilitiesTest
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            //driver.Manage().Timeouts().ImplicitWait(10,TimeSpan.FromSeconds(10));
            //driver = new InternetExplorerDriver();
            //System.Diagnostics.Debug.WriteLine(driver);
            ChromeOptions Options = new ChromeOptions();
            System.Console.WriteLine(Options);
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }

        [Test]
        public void Capabilities()
        {


            //driver.Navigate().GoToUrl("http://localhost/litecart/admin/");
            //driver.FindElement(By.Name("username")).SendKeys("admin");
            //driver.FindElement(By.Name("password")).SendKeys("admin");


            driver.Navigate().GoToUrl("https://habr.com/ru/sandbox/112936/");
            //driver.FindElement(By.Name("q")).SendKeys("webdriver" + Keys.Enter);
            //driver.FindElement(By.Name("btnG")).Click();
           // wait.Until(ExpectedConditions.TitleIs("webdriver - Поиск в Google"));

            driver.Manage().Cookies.AddCookie(new Cookie("foo", "bar"));

            // Get cookie details with named cookie 'foo'
            var cookie = driver.Manage().Cookies.GetCookieNamed("foo");
            System.Console.WriteLine(cookie);

        }

        [TearDown]
        public void stop()
        {
            Thread.Sleep(4000);
            driver.Quit();
            driver = null;
        }
    }
}
