/*
 *Задание 7. Сделайте сценарий, который выполняет следующие действия в учебном приложении litecart.

1) входит в панель администратора http://localhost/litecart/admin
2) прокликивает последовательно все пункты меню слева, включая вложенные пункты
3) для каждой страницы проверяет наличие заголовка (то есть элемента с тегом h1)
*/

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class CheckMenuAdmin
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
        public void CheckMenuLoginAdmin()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            // Лист элементов основного меню
            IList<IWebElement> menu = driver.FindElements(By.XPath("//ul[@id='box-apps-menu']/li[@id='app-']")); 
            for (int i = 1; i <= menu.Count; i++)
            {
                driver.FindElement(By.XPath("(//ul[@id='box-apps-menu']/li[@id='app-'])["+i+"]")).Click();
                FindH1();  //Проверка заголовка Н1
                
                // Лист элементов под меню
                IList<IWebElement> Submenu = driver.FindElements(By.XPath("//ul[@id='box-apps-menu']/li[@id='app-'][@class='selected']/ul[@class='docs']/li[starts-with(@id,'doc-')]"));
                for (int j = 1; j <= Submenu.Count ; j++)
                {
                    driver.FindElement(By.XPath("(//ul[@class = 'docs']/li[starts-with(@id,'doc-')])[" + j + "]")).Click();
                    FindH1();  //Проверка заголовка Н1
                }
            }
        }

        public void FindH1()
        {
            driver.FindElement(By.CssSelector("td#content h1"));
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
