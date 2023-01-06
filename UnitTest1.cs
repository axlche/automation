using NUnit.Framework;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Interactions;
using OpenQA.Selenium.Support.UI;
using seleniumCSharp.Base;
using seleniumCSharp.PageObjects;
using SeleniumExtras.WaitHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using Newtonsoft.Json;
using System.IO;
using Microsoft.Office.Interop.Excel;
using Faker;

namespace seleniumCSharp
{
    public class Tests : BaseClass
    {
        CoursePage coursePage;
        CreateUserPage createUserPage;
        ListOfUsersPage listOfUsersPage;

        public Dictionary<string, string> userData;

        public Tests()
        {
            userData = new Dictionary<string, string>();
            userData["email"] = "dictionary@email.com";
            userData["password"] = "password";
            userData["address1"] = "123 Random St.";
            userData["address2"] = "Apt. 12";
            userData["city"] = "RandomCity";
            userData["zip"] = "100001";
            userData["annualPayment"] = "225";
            userData["description"] = "Random description provided";
        }

        [Test]
        public void Homework1()
        {
            coursePage = new(driver);
            //CoursePage coursePage = new(driver);

            coursePage.VerifyLoggedIn("John Walker");
            coursePage.WaitFirstNavMenuLoaded();

            coursePage.VerifyColorNotRed(coursePage.DashboardMenuItem());
            coursePage.VerifyColorNotRed(coursePage.OrdersMenuItem());
            coursePage.VerifyColorNotRed(coursePage.ProductsMenuItem());
            coursePage.VerifyColorNotRed(coursePage.CustomersMenuItem());
            coursePage.VerifyColorNotRed(coursePage.ReportsMenuItem());
            coursePage.VerifyColorNotRed(coursePage.IntegrationsMenuItem());
            coursePage.VerifyColorNotRed(coursePage.CrtUsersMenuItem());
            coursePage.VerifyColorNotRed(coursePage.CrtManagerMenuItem());
            coursePage.VerifyColorNotRed(coursePage.CrtSubsMenuItem());
            coursePage.VerifyColorNotRed(coursePage.LstOfUsersMenuItem());
            coursePage.VerifyColorNotRed(coursePage.LstOfSubsMenuItem());
        }

        [Test]
        public void Homework2()
        {
            WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            driver.FindElement(By.TagName("body")).SendKeys(Keys.End);
            Thread.Sleep(200);
            coursePage = new(driver);
            coursePage.BtnCheckStatus().Click();
            wait.Until(ExpectedConditions.TextToBePresentInElement(coursePage.BtnCheckStatus(), "Loading.."));
            wait.Until(ExpectedConditions.TextToBePresentInElement(coursePage.BtnCheckStatus(), "Active"));

            Assert.IsTrue(coursePage.BtnCheckStatus().Text == "Active");

            coursePage.IdHeader().Click();
            coursePage.TableSorting("id", "asc");
            coursePage.IdHeader().Click();
            coursePage.TableSorting("id", "desc");
            coursePage.NameHeader().Click();
            coursePage.TableSorting("name", "asc");
            coursePage.NameHeader().Click();
            coursePage.TableSorting("name", "desc");
            //coursePage.AgeHeader().Click();
            //coursePage.TableSorting("age", "asc");
            //coursePage.AgeHeader().Click();
            //coursePage.TableSorting("age", "asc");

        }

        [Test]
        public void Homework3()
        {
            //WebDriverWait wait = new(driver, TimeSpan.FromSeconds(10));
            // Verifying number of rows existed in table before 1st user creation
            coursePage = new(driver);
            coursePage.LstOfUsersMenuItem().Click();
            listOfUsersPage = new(driver);
            var rowsNumber = listOfUsersPage.RowsCount;
            
            //Create first user
            coursePage.CrtUsersMenuItem().Click();
            createUserPage = new(driver);
            createUserPage.CreateRandomUser();
            Thread.Sleep(2000);
            // Verifying that new row with created user added
            Assert.IsTrue(listOfUsersPage.IsNewRowAdded(rowsNumber));
            var rowsNumber2 = listOfUsersPage.RowsCount;

            // Creating user using dictionary
            coursePage.CrtUsersMenuItem().Click();
            createUserPage.CreateNewUser(userData["email"], userData["password"], userData["address1"], userData["address2"], userData["city"], userData["zip"], userData["annualPayment"], userData["description"]);
            Thread.Sleep(2000);
            Assert.IsTrue(listOfUsersPage.IsNewRowAdded(rowsNumber2));
            var rowsNumber3 = listOfUsersPage.RowsCount;

            // Creating from JSON file
            string currentDirectory = AppDomain.CurrentDomain.BaseDirectory;
            string filePath = Path.Combine(currentDirectory, "json1.json");
            string jsonString = File.ReadAllText(filePath);
            User user = JsonConvert.DeserializeObject<User>(jsonString);
            coursePage.CrtUsersMenuItem().Click();
            createUserPage.EmailInput().SendKeys(user.Email);
            createUserPage.PasswordInput().SendKeys(user.Password);
            createUserPage.Address1Input().SendKeys(user.Address1);
            createUserPage.Address2Input().SendKeys(user.Address2);
            createUserPage.CityInput().SendKeys(user.City);
            createUserPage.ZipInput().SendKeys(user.Zip);
            createUserPage.AnnualInput().SendKeys(user.AnnualPayment);
            createUserPage.DescriptionlUser().SendKeys(user.Description);
            createUserPage.BtnCreate().Click();
            Thread.Sleep(2000);
            Assert.IsTrue(listOfUsersPage.IsNewRowAdded(rowsNumber3));
            var rowsNumber4 = listOfUsersPage.RowsCount;

            //Thread.Sleep(1000);
            coursePage.CrtUsersMenuItem().Click();
            // Creating using Faker
            createUserPage.EmailInput().SendKeys(Internet.Email());
            createUserPage.PasswordInput().SendKeys(Lorem.GetFirstWord());
            createUserPage.Address1Input().SendKeys(Address.StreetAddress());
            createUserPage.Address2Input().SendKeys(Address.SecondaryAddress());
            createUserPage.CityInput().SendKeys(Address.City());
            createUserPage.ZipInput().SendKeys(Address.ZipCode());
            createUserPage.AnnualInput().SendKeys(RandomNumber.Next(1, 100).ToString());
            createUserPage.DescriptionlUser().SendKeys(Lorem.Sentence());
            createUserPage.BtnCreate().Click();
            Thread.Sleep(2000);
            Assert.IsTrue(listOfUsersPage.IsNewRowAdded(rowsNumber4));
            var rowsNumber5 = listOfUsersPage.RowsCount;

            // Creating from XLSX file
            //Application excel = new Application();
            //Workbook workbook = excel.Workbooks.Open("userXlsx.xlsx");
            //Worksheet worksheet = (Worksheet)workbook.Worksheets[1];
            //createUserPage.EmailInput().SendKeys((string)worksheet.Cells[2, 1].Value2);
            //createUserPage.PasswordInput().SendKeys(worksheet.Cells[2, 2].GetValue<string>());
            //createUserPage.Address1Input().SendKeys(worksheet.Cells[2, 3].GetValue<string>());
            //createUserPage.Address2Input().SendKeys(user.Address2);
            //createUserPage.CityInput().SendKeys(user.City);
            //createUserPage.ZipInput().SendKeys(user.Zip);
            //createUserPage.AnnualInput().SendKeys(user.AnnualPayment);
            //createUserPage.DescriptionlUser().SendKeys(user.Description);
            //createUserPage.BtnCreate().Click();




        }

        //[Test]
        public void Draft()
        {
           
        }
        
    }
}