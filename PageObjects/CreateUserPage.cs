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
    public class CreateUserPage : BaseClass
    {
        private IWebDriver driver;

        public CreateUserPage(IWebDriver driver)
        {
            this.driver = driver;
            PageFactory.InitElements(driver, this);
        }

        [FindsBy(How = How.Id, Using = "email")]
        private IWebElement emailInput;
        public IWebElement EmailInput()
        {
            return emailInput;
        }

        [FindsBy(How = How.Id, Using = "password")]
        private IWebElement passwordInput;
        public IWebElement PasswordInput()
        {
            return passwordInput;
        }

        [FindsBy(How = How.Id, Using = "address1")]
        private IWebElement address1Input;
        public IWebElement Address1Input()
        {
            return address1Input;
        }

        [FindsBy(How = How.Id, Using = "address2")]
        private IWebElement address2Input;
        public IWebElement Address2Input()
        {
            return address2Input;
        }

        [FindsBy(How = How.Id, Using = "city")]
        private IWebElement cityInput;
        public IWebElement CityInput()
        {
            return cityInput;
        }

        [FindsBy(How = How.Id, Using = "zip")]
        private IWebElement zipInput;
        public IWebElement ZipInput()
        {
            return zipInput;
        }

        [FindsBy(How = How.Id, Using = "description")]
        private IWebElement descriptionInput;
        public IWebElement DescriptionlUser()
        {
            return descriptionInput;
        }

        [FindsBy(How = How.Id, Using = "anual")]
        private IWebElement annualInput;
        public IWebElement AnnualInput()
        {
            return annualInput;
        }

        [FindsBy(How = How.CssSelector, Using = "button[type='submit']")]
        private IWebElement btnCreate;
        public IWebElement BtnCreate()
        {
            return btnCreate;
        }



        public void CreateNewUser(string email, string password, string address1, string address2, string city, string zip, string annualPayment, string description)
        {
            emailInput.SendKeys(email);
            passwordInput.SendKeys(password);
            address1Input.SendKeys(address1);
            address2Input.SendKeys(address2);
            cityInput.SendKeys(city);
            zipInput.SendKeys(zip);
            annualInput.SendKeys(annualPayment);
            descriptionInput.SendKeys(description);

            btnCreate.Click();
        }

        public void CreateRandomUser()
        {   
            emailInput.SendKeys(RandomString() + "@test.ts");
            passwordInput.SendKeys(RandomString());
            address1Input.SendKeys(RandomString());
            address2Input.SendKeys(RandomString());
            cityInput.SendKeys(RandomString());
            zipInput.SendKeys("001266");
            annualInput.SendKeys("100");
            descriptionInput.SendKeys(RandomString());

            btnCreate.Click();
        }

        public String RandomString()
        {
            Random random = new Random();
            int length = 10;
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < length; i++)
            {
                // Generate a random number between 0 and 26 (the number of letters in the alphabet)
                int num = random.Next(0, 26);

                // Convert the number to a character and add it to the string builder
                char c = Convert.ToChar(num + 'a');
                sb.Append(c);
            }
            string randomString = sb.ToString();

            return randomString;
        }
    }
}
