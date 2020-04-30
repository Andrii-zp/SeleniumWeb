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

            driver.Navigate().GoToUrl("http://localhost/litecart/en/");
            IList<IWebElement> links = driver.FindElements(By.CssSelector("li.product.column.shadow.hover-light"));
            for (int i = 0; i < links.Count; i++)
            {
                // IList < IWebElement > stickers = links[i].FindElements(By.XPath("/a[@class='link']/div[@class='image-wrapper']/div[starts-with(@class,'sticker')]"));
                IList<IWebElement> stickers = links[i].FindElements(By.CssSelector("a.link>div.image-wrapper>div[class^=sticker]"));
                Assert.AreEqual(stickers.Count, 1); //Проверка количества стикеров на каждом елементе
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
