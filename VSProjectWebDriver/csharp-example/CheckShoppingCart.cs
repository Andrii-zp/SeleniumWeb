//[x] Задание 13. Сделайте сценарий работы с корзиной

//Сделайте сценарий для добавления товаров в корзину и удаления товаров из корзины.

//1) открыть главную страницу
//2) открыть первый товар из списка
//3) добавить его в корзину(при этом может случайно добавиться товар, который там уже есть, ничего страшного)
//4) подождать, пока счётчик товаров в корзине обновится
//5) вернуться на главную страницу, повторить предыдущие шаги ещё два раза, чтобы в общей сложности в корзине было 3 единицы товара
//6) открыть корзину(в правом верхнем углу кликнуть по ссылке Checkout)
//7) удалить все товары из корзины один за другим, после каждого удаления подождать, пока внизу обновится таблица


using System;
using System.Collections.Generic;
using System.Drawing;
using System.Globalization;
using System.Threading;
using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;



namespace csharp_example
{
    [TestFixture]
    public class CheckShoppingCart
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
        public void CheckShopping()
        {
            int CountCart;
            driver.Navigate().GoToUrl("http://localhost/litecart/en/");

            CountCart= int.Parse(driver.FindElement(By.CssSelector("div#cart a.content span.quantity")).Text); //первоначальное количество товара в корзине

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
                if (i != CountCart-1) { driver.FindElement(By.CssSelector("ul.shortcuts a")).Click(); } // стабилизируем кнопку удаления из корзины
                driver.FindElement(By.CssSelector("button[name='remove_cart_item']")).Click();
                wait.Until(driver => driver.FindElements(By.CssSelector("table.dataTable tr>td.item")).Count== CountCart - 1 -i);

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
}
