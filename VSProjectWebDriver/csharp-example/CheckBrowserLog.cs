//Задание 17. Проверьте отсутствие сообщений в логе браузера
//Сделайте сценарий, который проверяет, не появляются ли в логе браузера сообщения при открытии страниц в учебном приложении, а именно -- страниц товаров в каталоге в административной панели.
//Сценарий должен состоять из следующих частей:
//1) зайти в админку
//2) открыть каталог, категорию, которая содержит товары(страница http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1)
//3) последовательно открывать страницы товаров и проверять, не появляются ли в логе браузера сообщения(любого уровня)


using System;
using System.Collections.Generic;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;

using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class CheckBrowserLog
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
        public void CheckLog()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            // Лист элементов Catalog
            IList<IWebElement> CatalogItems = driver.FindElements(By.CssSelector("table.dataTable a[href*=product_id][title=Edit]"));
            for (int i = 0; i < CatalogItems.Count; i++)
            {
                driver.FindElements(By.CssSelector("table.dataTable a[href*=product_id][title=Edit]"))[i].Click();
                FindBrowserLogs();
                driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog&category_id=1");

            }
        }

        public void FindBrowserLogs()
        {
            foreach (LogEntry l in driver.Manage().Logs.GetLog("browser"))
            {
                Console.WriteLine(l);
            }
        }

        [TearDown]
        public void stop()
        {
            Thread.Sleep(500);
            driver.Quit();
            driver = null;
        }
    }
}
