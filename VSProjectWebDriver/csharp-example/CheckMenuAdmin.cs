using System;
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


            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[1]")).Click(); FindH1(); //основные пункты
                driver.FindElement(By.CssSelector("li#doc-template.selected")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-logotype")).Click(); FindH1();
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[2]")).Click(); FindH1(); //основные пункты
                driver.FindElement(By.CssSelector("li#doc-catalog")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-product_groups")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-option_groups")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-manufacturers")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-suppliers")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-delivery_statuses")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-sold_out_statuses")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-quantity_units")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-csv")).Click(); FindH1();
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[3]")).Click(); FindH1(); //основные пункты
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[4]")).Click(); FindH1(); //основные пункты
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[5]")).Click(); FindH1(); //основные пункты
                driver.FindElement(By.CssSelector("li#doc-customers")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-csv")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-newsletter")).Click(); FindH1();
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[6]")).Click(); FindH1(); //основные пункты
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[7]")).Click(); FindH1(); //основные пункты
                driver.FindElement(By.CssSelector("li#doc-languages")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-storage_encoding")).Click(); FindH1();
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[8]")).Click(); FindH1(); //основные пункты
                driver.FindElement(By.CssSelector("li#doc-jobs")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-customer")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-shipping")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-payment")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-order_total")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-order_success")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-order_action")).Click(); FindH1();
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[9]")).Click(); FindH1(); //основные пункты
                driver.FindElement(By.CssSelector("li#doc-orders")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-order_statuses")).Click(); FindH1();
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[10]")).Click(); FindH1(); //основные пункты
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[11]")).Click(); FindH1(); //основные пункты
                driver.FindElement(By.CssSelector("li#doc-monthly_sales")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-most_sold_products")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-most_shopping_customers")).Click(); FindH1();
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[12]")).Click(); FindH1(); //основные пункты
                driver.FindElement(By.CssSelector("li#doc-store_info")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-defaults")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-general")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-listings")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-images")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-checkout")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-advanced")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-security")).Click(); FindH1();
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[13]")).Click(); FindH1();//основные пункты
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[14]")).Click(); FindH1();//основные пункты
                driver.FindElement(By.CssSelector("li#doc-tax_classes")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-tax_rates")).Click(); FindH1();
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[15]")).Click(); FindH1(); //основные пункты
                driver.FindElement(By.CssSelector("li#doc-search")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-scan")).Click(); FindH1();
                driver.FindElement(By.CssSelector("li#doc-csv")).Click(); FindH1();
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[16]")).Click(); FindH1(); //основные пункты
            driver.FindElement(By.XPath("(//ul[@id='box-apps-menu'][@class='list-vertical']/li[@id='app-'])[17]")).Click(); FindH1(); //основные пункты
                driver.FindElement(By.CssSelector("li#doc-vqmods")).Click(); FindH1();

        }

        public void FindH1()
        {
            driver.FindElement(By.CssSelector("td#content h1"));
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
