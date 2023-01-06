using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;
using seleniumCSharp.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seleniumCSharp.Base
{
    public class BaseClass
    {
        public IWebDriver driver;

        [SetUp]
        public void StartBrowser()
        {
            driver = new ChromeDriver();
            driver.Navigate().GoToUrl(ConfigurationManager.AppSettings["url"]);
            driver.Manage().Window.Maximize();

            driver.FindElement(By.Id("login")).SendKeys(ConfigurationManager.AppSettings["login"]);
            driver.FindElement(By.Id("password")).SendKeys(ConfigurationManager.AppSettings["password"]);
            driver.FindElement(By.CssSelector(".btn-primary.rounded-2")).Click();

            WebDriverWait wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.InvisibilityOfElementLocated(By.Id("spinner")));

        }

        [TearDown]
        public void CloseBrowser()
        {
            driver.Close();
        }
    }
}
