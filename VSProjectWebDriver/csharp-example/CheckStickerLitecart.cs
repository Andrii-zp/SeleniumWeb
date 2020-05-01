/*
 * Задание 8.
 * Сделайте сценарий, проверяющий наличие стикеров у всех товаров в учебном приложении litecart на главной странице. 
 * Стикеры -- это полоски в левом верхнем углу изображения товара, на которых написано New или Sale или что-нибудь другое.
 * Сценарий должен проверять, что у каждого товара имеется ровно один стикер.
 *
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
    public class CheckStickerLitecart
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
        public void CheckSticker()
        {
            //int sum = 0;
            driver.Navigate().GoToUrl("http://localhost/litecart/en/");
            IList<IWebElement> products = driver.FindElements(By.CssSelector("li.product"));
            for (int i = 0; i < products.Count; i++)
            {
                // IList < IWebElement > stickers = links[i].FindElements(By.XPath("./a[@class='link']/div[@class='image-wrapper']/div[starts-with(@class,'sticker')]"));
                IList<IWebElement> stickers = products[i].FindElements(By.CssSelector("a.link>div.image-wrapper>div[class^=sticker]"));
                Assert.AreEqual(1, stickers.Count); //Проверка количества стикеров на каждом елементе
            }
        }


        [TearDown]
        public void stop()
        {
            Thread.Sleep(3000);
            driver.Quit();
            driver = null;
        }
    }
}
