/*
 *Задание 9. Проверить сортировку стран и геозон в админке

1) на странице http://localhost/litecart/admin/?app=countries&doc=countries
а) проверить, что страны расположены в алфавитном порядке
б) для тех стран, у которых количество зон отлично от нуля -- открыть страницу этой страны и там проверить, что зоны расположены в алфавитном порядке

2) на странице http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones
зайти в каждую из стран и проверить, что зоны расположены в алфавитном порядке
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
    public class CheckSort
    {
        private IWebDriver driver;
        private WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        // 1) на странице http://localhost/litecart/admin/?app=countries&doc=countries
        [Test]
        public void CheckCountriesSort()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");

            // Лист стран
            IList<IWebElement> rows = driver.FindElements(By.CssSelector("table.dataTable tr.row"));
            for (int i = 0; i < rows.Count; i++)
            {
                if (i > 0)
                {  //начинаем со второго элемента
                    if (String.Compare(rows[i - 1].FindElements(By.CssSelector("td"))[4].FindElement((By.CssSelector("a"))).GetAttribute("textContent"),
                                        rows[i].FindElements(By.CssSelector("td"))[4].FindElement((By.CssSelector("a"))).GetAttribute("textContent"),
                                        StringComparison.InvariantCulture) > 0)  // лингвистическое сравнение i-1ого с iым элементоам
                    {
                        Assert.AreEqual(true, false); //сортировка не верная
                    }
                }
               if (rows[i].FindElements(By.CssSelector("td"))[5].GetAttribute("textContent")!="0") // количество зон не равно 0
                {
                    rows[i].FindElements(By.CssSelector("td"))[4].FindElement((By.CssSelector("a"))).Click();
                    IList<IWebElement> rowsZones = driver.FindElements(By.CssSelector("table#table-zones tr"));
                    for (int j = 1; j < rowsZones.Count - 2; j++)
                    {
                        if (String.Compare(rowsZones[j].FindElements(By.CssSelector("td"))[2].GetAttribute("textContent"),
                                            rowsZones[j + 1].FindElements(By.CssSelector("td"))[2].GetAttribute("textContent"),
                                                StringComparison.InvariantCulture) > 0)  // лингвистическое сравнение jтого с j+1ым элементоам
                        {
                            Assert.AreEqual(true, false); //сортировка Zones не верная

                        }
                    }
                    driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=countries&doc=countries");
                    rows = driver.FindElements(By.CssSelector("table.dataTable tr.row"));
                }
            }
        }



        // 2) на странице http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones
        [Test]
        public void CheckZonesSort()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/");
            driver.FindElement(By.Name("username")).SendKeys("admin");
            driver.FindElement(By.Name("password")).SendKeys("admin");
            driver.FindElement(By.Name("login")).Click();
            driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
            // Лист Zones
            IList<IWebElement> rows = driver.FindElements(By.CssSelector("table.dataTable tr.row"));

            for (int i = 0; i < rows.Count; i++)
            {
                if (i > 0)
                {  //начинаем со второго элемента
                    if (String.Compare(rows[i - 1].FindElements(By.CssSelector("td"))[2].FindElement((By.CssSelector("a"))).GetAttribute("textContent"),
                                        rows[i].FindElements(By.CssSelector("td"))[2].FindElement((By.CssSelector("a"))).GetAttribute("textContent"),
                                        StringComparison.InvariantCulture) > 0)  // лингвистическое сравнение i-1ого с iым элементоам
                    {
                        Assert.AreEqual(true, false); //сортировка не верная
                    }
                }

                   rows[i].FindElements(By.CssSelector("td"))[2].FindElement((By.CssSelector("a"))).Click();
                    IList<IWebElement> rowsZones = driver.FindElements(By.CssSelector("table#table-zones tr"));
                    for (int j = 1; j < rowsZones.Count - 2; j++)
                    {
                        if (String.Compare(rowsZones[j].FindElements(By.CssSelector("td"))[2].FindElement((By.CssSelector("select option[selected='selected']"))).GetAttribute("textContent"),
                                            rowsZones[j + 1].FindElements(By.CssSelector("td"))[2].FindElement((By.CssSelector("select option[selected='selected']"))).GetAttribute("textContent"),
                                                StringComparison.InvariantCulture) > 0)  // лингвистическое сравнение jтого с j+1ым элементоам
                        {
                            Assert.AreEqual(true, false); //сортировка Zones не верная
                        }
                    }
                    driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=geo_zones&doc=geo_zones");
                    rows = driver.FindElements(By.CssSelector("table.dataTable tr.row"));
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
