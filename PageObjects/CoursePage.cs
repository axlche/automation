using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace seleniumCSharp.PageObjects
{
    public class CoursePage
    {
        private IWebDriver driver;

        public CoursePage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "user-label")]
        private IWebElement userLabel;
        public IWebElement LblUser()
        {
            return userLabel;
        }

        #region FirstNavMenu
        [FindsBy(How = How.Id, Using = "first-nav-block")]
        private IWebElement firstNavMenu;
        public IWebElement FirstNavMenu()
        {
            return firstNavMenu;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Dashboard']")]
        private IWebElement dashboardMenuItem;
        public IWebElement DashboardMenuItem()
        {
            return dashboardMenuItem;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Orders']")]
        private IWebElement ordersMenuItem;
        public IWebElement OrdersMenuItem()
        {
            return ordersMenuItem;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Products']")]
        private IWebElement productsMenuItem;
        public IWebElement ProductsMenuItem()
        {
            return productsMenuItem;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Customers']")]
        private IWebElement customersMenuItem;
        public IWebElement CustomersMenuItem()
        {
            return customersMenuItem;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Reports']")]
        private IWebElement reportsMenuItem;
        public IWebElement ReportsMenuItem()
        {
            return reportsMenuItem;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Integrations']")]
        private IWebElement integrationsMenuItem;
        public IWebElement IntegrationsMenuItem()
        {
            return integrationsMenuItem;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Create User']")]
        private IWebElement crtUsersMenuItem;
        public IWebElement CrtUsersMenuItem()
        {
            return crtUsersMenuItem;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Create Subscription']")]
        private IWebElement srtSubsMenuItem;
        public IWebElement CrtSubsMenuItem()
        {
            return srtSubsMenuItem;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='Create Manager']")]
        private IWebElement crtManagerMenuItem;
        public IWebElement CrtManagerMenuItem()
        {
            return crtManagerMenuItem;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='List of users']")]
        private IWebElement lstOfUsersMenuItem;
        public IWebElement LstOfUsersMenuItem()
        {
            return lstOfUsersMenuItem;
        }

        [FindsBy(How = How.XPath, Using = "//a[normalize-space()='List of Subscriptions']")]
        private IWebElement lstOfSubsMenuItem;
        public IWebElement LstOfSubsMenuItem()
        {
            return lstOfSubsMenuItem;
        }
        #endregion

        #region WeeklyLeadersTable

        #endregion

        [FindsBy(How = How.CssSelector, Using = "li.nav-item a.nav-link#status")]
        private IWebElement btnCheckStatus;
        public IWebElement BtnCheckStatus()
        {
            return btnCheckStatus;
        }

        public void VerifyLoggedIn(string userName)
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.TextToBePresentInElement(LblUser(), userName));
        }

        public void WaitFirstNavMenuLoaded()
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementExists(By.Id("first-nav-block")));

        }

        public void VerifyColorNotRed(IWebElement element)
        {
            Actions action = new(driver);
            action.MoveToElement(element).Perform();
            Assert.IsTrue(element.GetCssValue("color") != "rgba(255,0,0,1)", "Color is red!");
        }

        [FindsBy(How = How.CssSelector, Using = ".tabulator-col[tabulator-field='id']")]
        private IWebElement headerID;
        public IWebElement IdHeader()
        {
            return headerID;
        }

        [FindsBy(How = How.CssSelector, Using = ".tabulator-col[tabulator-field='name']")]
        private IWebElement nameHeader;
        public IWebElement NameHeader()
        {
            return nameHeader;
        }

        [FindsBy(How = How.CssSelector, Using = ".tabulator-col[tabulator-field='age']")]
        private IWebElement ageHeader;
        public IWebElement AgeHeader()
        {
            return ageHeader;
        }

        public void TableSorting(string column, string sortType)
        {
            IList<IWebElement> cells = driver.FindElements(By.CssSelector($".tabulator-cell[tabulator-field='{column}']"));
            IList<string> cellValues = new List<string>();
            foreach (IWebElement cell in cells)
            {
                cellValues.Add(cell.Text);
            }
            VerifySorting(cellValues, sortType);
        }

        public void VerifySorting(IList<string> list, string sortType)
        {
            for (int i = 0; i < list.Count - 1; i++)
            {
                string currentValue = list[i];
                string nextValue = list[i + 1];
                CompareSortedValues(i, currentValue, nextValue, sortType);
            }
        }

        public void CompareSortedValues(int row, string currentValue, string nextValue, string sortType)
        {
            int comparison = String.Compare(currentValue, nextValue);
            StringBuilder assertMsg = new($"Sorting not correct on position {row}.");
            assertMsg
                .AppendLine($"Value on postition {row}: {currentValue}.")
                .AppendLine($"Value on postition {row + 1}: {nextValue}");
            if (sortType == "asc")
            {
                Assert.IsTrue(comparison <= 0, assertMsg.ToString());
            }
            else if (sortType == "desc")
            {
                Assert.IsTrue(comparison >= 0, assertMsg.ToString());
            }
        }
    }
}