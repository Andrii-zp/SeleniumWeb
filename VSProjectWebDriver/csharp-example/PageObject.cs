//Задание 19. Реализовать многослойную архитектуру
//Переделайте созданный в задании 13 сценарий для добавления товаров в корзину и удаления товаров из корзины, 
//    чтобы он использовал многослойную архитектуру.
//А именно, выделите вспомогательные классы 
//1) для работы с главной страницей(откуда выбирается товар),
//2) для работы со страницей товара(откуда происходит добавление товара в корзину),
//3) со страницей корзины(откуда происходит удаление), 
//и реализуйте сценарий, который не напрямую обращается к операциям Selenium, а оперирует вышеперечисленными объектами-страницами.

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
    public class Shopping
    {
        public IWebDriver driver;
        public WebDriverWait wait;

        [SetUp]
        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }


        [Test]
        public void CheckShopping()
        {
            int CountCart;
            driver.Navigate().GoToUrl("http://localhost/litecart/en/");

            CountCart = int.Parse(driver.FindElement(By.CssSelector("div#cart a.content span.quantity")).Text); //первоначальное количество товара в корзине

            for (int i = 0; i < 3; i++) // цикл три раза добавляем в корзину товар
            {
                driver.FindElement(By.CssSelector("div#box-most-popular a.link")).Click(); //выбираем первый товар
                if (isSelectPresent(driver)) // Проверка нужно ли выбирать Size 
                {
                    new SelectElement(driver.FindElement(By.CssSelector("div.buy_now select"))).SelectByValue("Small"); //выбор Size 
                }
                driver.FindElement(By.CssSelector("div.buy_now button[name='add_cart_product']")).Click(); //в корзину
                CountCart++;
                wait.Until(ExpectedConditions.TextToBePresentInElementLocated(By.CssSelector("div#cart a.content span.quantity"), CountCart.ToString()));
                driver.Navigate().GoToUrl("http://localhost/litecart/en/");
            }

            driver.FindElement(By.CssSelector("div#cart-wrapper a.link")).Click();

            IList<IWebElement> rows = driver.FindElements(By.CssSelector("table.dataTable tr>td.item"));//таблица товаров в корзине
            CountCart = rows.Count; //количество в корзине УНИКАЛЬНЫХ товаров
            for (int i = 0; i < CountCart; i++) // цикл  удаляем товар из корзины
            {
                if (i != CountCart - 1) { driver.FindElement(By.CssSelector("ul.shortcuts a")).Click(); } // стабилизируем кнопку удаления из корзины
                driver.FindElement(By.CssSelector("button[name='remove_cart_item']")).Click();
                wait.Until(driver => driver.FindElements(By.CssSelector("table.dataTable tr>td.item")).Count == CountCart - 1 - i);

            }
        }


        protected bool isSelectPresent(IWebDriver driver)
        {
            try
            {

                driver.FindElement(By.CssSelector("div.buy_now select"));
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
            Thread.Sleep(20000);
            driver.Quit();
            driver = null;
        }
    }


        public class MainPage {
        //open

        public IWebDriver driver;
        public WebDriverWait wait;

        public void start()
        {
            driver = new ChromeDriver();
            wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
        }
        //login
        //logout
    }
    public class ItemPage {
//firstRandomItem
//addToCart
    }
    public class CartPage { }
//deleteAll
}

