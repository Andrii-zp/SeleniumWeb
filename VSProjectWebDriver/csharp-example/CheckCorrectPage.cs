
//Задание 10. Проверить, что открывается правильная страница товара
//Более точно, нужно открыть главную страницу, выбрать первый товар в блоке Campaigns и проверить следующее:
//а) на главной странице и на странице товара совпадает текст названия товара
//б) на главной странице и на странице товара совпадают цены(обычная и акционная)
//в) обычная цена зачёркнутая и серая(можно считать, что "серый" цвет это такой, у которого в RGBa представлении одинаковые значения для каналов R, G и B)
//г) акционная жирная и красная(можно считать, что "красный" цвет это такой, у которого в RGBa представлении каналы G и B имеют нулевые значения)
//(цвета надо проверить на каждой странице независимо, при этом цвета на разных страницах могут не совпадать)
//д) акционная цена крупнее, чем обычная(это тоже надо проверить на каждой странице независимо)

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
    public class CheckCorrectPage
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
        public void CheckPage()
        {
            driver.Navigate().GoToUrl("http://localhost/litecart/en/");

            IWebElement Campaign = driver.FindElement(By.CssSelector("div#box-campaigns.box li"));
            String NameCampaignMain = Campaign.FindElement(By.CssSelector("div.name")).GetAttribute("textContent");
            String RegPriceMain = Campaign.FindElement(By.CssSelector("s")).GetAttribute("textContent");
            String CampPriceMain = Campaign.FindElement(By.CssSelector("strong")).GetAttribute("textContent");
            // характеристики обычной цены на главной стр
            String RegPriceMain_Font = Campaign.FindElement(By.CssSelector("s")).GetCssValue("font-size"); //font - size: 14px;
            String RegPriceMain_Color = Campaign.FindElement(By.CssSelector("s")).GetCssValue("color"); // color: #777;
            String RegPriceMain_TextDecor = Campaign.FindElement(By.CssSelector("s")).GetCssValue("text-decoration-line"); //text - decoration: line - through;
            // характеристики акцион цены на главной стр
            String CampPriceMain_Font = Campaign.FindElement(By.CssSelector("strong")).GetCssValue("font-size"); //font - size: 18px;
            String CampPriceMain_Color = Campaign.FindElement(By.CssSelector("strong")).GetCssValue("color"); //color: #c00;
            String CampPriceMain_TextDecor = Campaign.FindElement(By.CssSelector("strong")).GetCssValue("font-weight"); //font - weight: bold;

            Campaign.Click(); //переход на страницу товара
            IWebElement CampaignPage = driver.FindElement(By.CssSelector("div#box-product"));
            String NameCampaignPage = CampaignPage.FindElement(By.CssSelector("h1")).GetAttribute("textContent");
            String RegPricePage = CampaignPage.FindElement(By.CssSelector("s")).GetAttribute("textContent");
            String CampPricePage = CampaignPage.FindElement(By.CssSelector("strong")).GetAttribute("textContent");
            // характеристики обычной цены на стр товара
            String RegPricePage_Font = CampaignPage.FindElement(By.CssSelector("s")).GetCssValue("font-size"); //font - size: 14px;
            String RegPricePage_Color = CampaignPage.FindElement(By.CssSelector("s")).GetCssValue("color"); // color: #777;
            String RegPricePage_TextDecor = CampaignPage.FindElement(By.CssSelector("s")).GetCssValue("text-decoration-line"); //text - decoration: line - through;
            // характеристики акцион цены на стр товара
            String CampPricePage_Font = CampaignPage.FindElement(By.CssSelector("strong")).GetCssValue("font-size"); //font - size: 18px;
            String CampPricePage_Color = CampaignPage.FindElement(By.CssSelector("strong")).GetCssValue("color"); //color: #c00;
            String CampPricePage_TextDecor = CampaignPage.FindElement(By.CssSelector("strong")).GetCssValue("font-weight"); //font - weight: bold;

            // достаем цвет
            RegPriceMain_Color = RegPriceMain_Color.Substring(5, RegPriceMain_Color.Length - 6);
            CampPriceMain_Color = CampPriceMain_Color.Substring(5, CampPriceMain_Color.Length - 6);
            RegPricePage_Color = RegPricePage_Color.Substring(5, RegPricePage_Color.Length - 6);
            CampPricePage_Color = CampPricePage_Color.Substring(5, CampPricePage_Color.Length - 6);

            // а)
            Assert.AreEqual(NameCampaignMain, NameCampaignPage); //проверка совпадения названия товара 
            // б)
            Assert.AreEqual(RegPriceMain, RegPricePage); //проверка совпадения обычной цены   
            Assert.AreEqual(CampPriceMain, CampPricePage); //проверка совпадения акционной цены  

            // в) проверка обычная цена зачёркнутая и серая
            Assert.True(RegPriceMain_TextDecor == "line-through" && RegPricePage_TextDecor == "line-through"); //проверка обычная цена зачёркнутая
            string[] words = RegPriceMain_Color.Split(new char[] { ' ' });
            Assert.True(words[0]== words[1] && words[1] == words[2]); //проверка обычная цена  серая  на главной стр
            words = RegPricePage_Color.Split(new char[] { ' ' });
            Assert.True(words[0] == words[1] && words[1] == words[2]); //проверка обычная цена  серая на стр товара

            // г) проверка акционная цена жирная и красная
            Assert.True(CampPriceMain_TextDecor == "700" && CampPricePage_TextDecor == "700"); //проверка акционная цена жирная 
            words = CampPriceMain_Color.Split(new char[] { ' ' });
            Assert.True(words[1] == words[2] && words[2] == "0,"); //проверка акц цена  красная  на главной стр
            words = CampPricePage_Color.Split(new char[] { ' ' });
            Assert.True(words[1] == words[2] && words[2] == "0,"); //проверка акц цена  красная на стр товара

            // д) проверка акционная цена крупнее, чем обычная
            Assert.True(double.Parse(CampPriceMain_Font.Substring(0, CampPriceMain_Font.Length - 2), CultureInfo.InvariantCulture) >
                          double.Parse(RegPriceMain_Font.Substring(0, RegPriceMain_Font.Length - 2), CultureInfo.InvariantCulture)); //проверка   на главной стр
            Assert.True(double.Parse(CampPricePage_Font.Substring(0, CampPricePage_Font.Length - 2), CultureInfo.InvariantCulture) >
                          double.Parse(RegPricePage_Font.Substring(0, RegPricePage_Font.Length - 2), CultureInfo.InvariantCulture)); //проверка  на стр товара
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
