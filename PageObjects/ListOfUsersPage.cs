using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using seleniumCSharp.Base;
using SeleniumExtras.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace seleniumCSharp.PageObjects
{
    public class ListOfUsersPage : BaseClass
    {
        private IWebDriver driver;
        private IList<IWebElement> rows;
        public ListOfUsersPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
            rows = driver.FindElements(By.CssSelector(".tabulator-row"));

        }

        [FindsBy(How = How.Id, Using = "users-table")]
        private IWebElement usersTable;

        [FindsBy(How = How.CssSelector, Using = ".tabulator-col[tabulator-field='email']")]
        private IWebElement emailHeader;
        public IWebElement EmailHeader()
        {
            return emailHeader;
        }
    

        [FindsBy(How = How.CssSelector, Using = ".tabulator-cell[tabulator-field='{column}']")]
        private IWebElement cellForColumn;
        public IWebElement ColumnCell()
        {
            return cellForColumn;
        }

        [FindsBy(How = How.CssSelector, Using = "div[class='col-9'] h3")]
        private IWebElement pageHeader;

        public int RowsCount
        {
            get
            {
                return rows.Count;
            }
        }

        public bool IsPageLoaded() => pageHeader.Displayed;

        public bool IsTableLoaded() => usersTable.Displayed;

        public bool IsNewRowAdded(int newRow) => driver.FindElement(By.CssSelector($".tabulator-row:nth-child({newRow + 1})")).Displayed;
        
        public void WaitForTableLoaded()
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            wait.Until(ExpectedConditions.ElementIsVisible(By.Id("user-table")));
        }
        
    }
}
