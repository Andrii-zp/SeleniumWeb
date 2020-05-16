//[x] Задание 14. Проверьте, что ссылки открываются в новом окне
//Сделайте сценарий, который проверяет, что ссылки на странице редактирования страны открываются в новом окне.
//Сценарий должен состоять из следующих частей:
//1) зайти в админку
//2) открыть пункт меню Countries(или страницу http://localhost/litecart/admin/?app=countries&doc=countries)
//3) открыть на редактирование какую-нибудь страну или начать создание новой
//4) возле некоторых полей есть ссылки с иконкой в виде квадратика со стрелкой -- они ведут на внешние страницы и открываются в новом окне, именно это и нужно проверить.
//Конечно, можно просто убедиться в том, что у ссылки есть атрибут target = "_blank".Но в этом упражнении требуется именно кликнуть по ссылке, чтобы она открылась в новом окне, потом переключиться в новое окно, закрыть его, вернуться обратно, и повторить эти действия для всех таких ссылок.
//Не забудьте, что новое окно открывается не мгновенно, поэтому требуется ожидание открытия окна.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class CheckNewWindow
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
        public void NewWindow()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=edit_country&country_code=AF");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();

            String CurrWindow = driver.CurrentWindowHandle;
            IList<IWebElement> hrefItems = driver.FindElements(By.CssSelector("td#content a[target = '_blank']"));
            int hrefItemsCount = hrefItems.Count;
            IList<String> OldListWin;
            int OldListWinCount;
            for (int i = 0; i < hrefItemsCount; i++) // проход по каждой внешней ссылке
            {
                OldListWin = driver.WindowHandles;
                OldListWinCount = OldListWin.Count;
                new Actions(driver)
                    .KeyDown(Keys.Shift)
                    .Click(driver.FindElements(By.CssSelector("td#content a[target = '_blank']"))[i])
                    .KeyUp(Keys.Shift)
                    .Build()
                    .Perform()
                    ;

                wait.Until(driver => driver.WindowHandles.Count == OldListWinCount + 1); //ожидание открытие нового окна
                driver.SwitchTo().Window(driver.WindowHandles[driver.WindowHandles.Count-1]); //переход в новое окно
                driver.Close();
                driver.SwitchTo().Window(CurrWindow);// возврат в litecart
            }
        }

        [TearDown]
        public void stop()
        {
            Thread.Sleep(200);
            driver.Quit();
            driver = null;
        }
    }
}
