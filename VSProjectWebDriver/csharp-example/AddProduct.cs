// Задание 12. Сделайте сценарий добавления товара
//Сделайте сценарий для добавления нового товара(продукта) в учебном приложении litecart(в админке).
//Для добавления товара нужно открыть меню Catalog, в правом верхнем углу нажать кнопку "Add New Product", заполнить поля с информацией о товаре и сохранить.
//Достаточно заполнить только информацию на вкладках General, Information и Prices.Скидки (Campains) на вкладке Prices можно не добавлять.
//Переключение между вкладками происходит не мгновенно, поэтому после переключения можно сделать небольшую паузу (о том, как делать более правильные ожидания, будет рассказано в следующих занятиях).
//Картинку с изображением товара нужно уложить в репозиторий вместе с кодом.При этом указывать в коде полный абсолютный путь к файлу плохо, на другой машине работать не будет.Надо средствами языка программирования преобразовать относительный путь в абсолютный.
//После сохранения товара нужно убедиться, что он появился в каталоге (в админке). Клиентскую часть магазина можно не проверять.

using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace csharp_example
{
    [TestFixture]
    public class AddProduct
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
        public void CheckAddProduct()
        {
                //login
                driver.Navigate().GoToUrl("http://localhost/litecart/admin/");
                driver.FindElement(By.Name("username")).SendKeys("admin");
                driver.FindElement(By.Name("password")).SendKeys("admin");
                driver.FindElement(By.Name("login")).Click();
                //catalog
                driver.Navigate().GoToUrl("http://localhost/litecart/admin/?app=catalog&doc=catalog");
                if (!driver.Title.Equals("Catalog | My Store"))
                {
                    Assert.Fail();
                }

  
                //General tab
                driver.FindElement(By.CssSelector("td#content a[href$='doc=edit_product']")).Click();
                if (!driver.Title.Equals("Add New Product | My Store"))
                {
                    Assert.Fail();
                }

                string ProdName = "Black Duck"; //название продукта
                driver.FindElement(By.CssSelector("div#tab-general input[type='radio'][value='1']")).Click();
                driver.FindElement(By.CssSelector("div#tab-general input[type='text'][name='name[en]']")).SendKeys(ProdName);
                driver.FindElement(By.CssSelector("div#tab-general input[type='text'][name='code']")).SendKeys("BD001");

                driver.FindElement(By.CssSelector("div#tab-general input[type='checkbox'][value='1']")).Click();
                driver.FindElement(By.CssSelector("div#tab-general input[type='checkbox'][value='2']")).Click();
                driver.FindElement(By.CssSelector("div#tab-general input[type='checkbox'][value='1-3']")).Click();

                IWebElement quantity = driver.FindElement(By.CssSelector("div#tab-general input[name='quantity']"));
                quantity.Clear();
                quantity.SendKeys("100");

                //C:\Users\Andrii\Documents\GitHub\SeleniumWeb\VSProjectWebDriver\csharp-example\blackDuck.png
                IWebElement uploadFile = driver.FindElement(By.CssSelector("div#tab-general input[type='file'][name='new_images[]']"));
                uploadFile.SendKeys(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location) + @"\blackDuck.png");

                driver.FindElement(By.CssSelector("div#tab-general input[type='date'][name='date_valid_from']")).SendKeys("01.01.2000");
                driver.FindElement(By.CssSelector("div#tab-general input[type='date'][name='date_valid_to']")).SendKeys("01.01.2100");

                //Prices tab
                driver.FindElement(By.CssSelector("div.tabs a[href='#tab-prices']")).Click();
                IWebElement price = driver.FindElement(By.CssSelector("div#tab-prices input[type='number'][name='purchase_price']"));
                price.Clear();
                price.SendKeys("99");
                new SelectElement(driver.FindElement(By.CssSelector("div#tab-prices select[name='purchase_price_currency_code']"))).SelectByValue("USD");

                //save
                driver.FindElement(By.CssSelector("span.button-set button[type='submit'][value='Save']")).Click();

                //wait.until(titleIs("Catalog | My Store"));
                if (!driver.Title.Equals("Catalog | My Store"))
                {
                    Assert.Fail();
                }

                IWebElement table = driver.FindElement(By.CssSelector("table.dataTable"));
                if (!isElementPresent(driver, ProdName)) //проверка наличия добавленного елемента
                {
                Assert.Fail();
                }
                else
                {
                    
                }
            }



        protected bool isElementPresent(IWebDriver driver, string ProdName)
        {
            try
            {
                driver.FindElement(By.LinkText(ProdName));
                return true;
            }
            catch (NoSuchElementException e)
            {
                return false;
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
