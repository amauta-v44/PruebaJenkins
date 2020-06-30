using System;
using Xunit;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest.Test
{
	public class PublicationManagementSeleniumTests
	{
		private ChromeDriver driver;

		public PublicationManagementSeleniumTests()
		{
			var chromeOptions = new ChromeOptions();
			//chromeOptions.AddArguments("headless");
			driver = new ChromeDriver("../../../", chromeOptions);
			driver.Navigate().GoToUrl("https://raigiku.github.io/SI656-Experimentos_ISW-Frontend/");
			driver.Manage().Window.Maximize();
			
			LogInUser();
		}

		private void LogInUser()
		{
			// Usuario y contrasenia del usuario
			string username = "diegocas";
			string password = "12345678";

			var inputUsername =
				driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(1) > div > input");
			inputUsername.SendKeys(username);
			var inputPassword =
				driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(2) > div > input");
			inputPassword.SendKeys(password);

			var loginButton = driver.FindElementByCssSelector("#root > main > div > form > button");
			loginButton.Click();

			var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
			wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
				By.CssSelector(
					"#root > div > div.MuiPaper-root.MuiTableContainer-root.MuiPaper-elevation1.MuiPaper-rounded > table > tbody")));
		}

		[Fact]
		public void Publication_UserCanViewPublications()
		{
			// Arrange
			bool expected = true;

			// Act
			bool actual = false;
			
			try
			{
				var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
					By.CssSelector(
						"#root > div > div.MuiPaper-root.MuiTableContainer-root.MuiPaper-elevation1.MuiPaper-rounded > table > tbody > tr")));

				var publicationList = driver.FindElementsByCssSelector(
					"#root > div > div.MuiPaper-root.MuiTableContainer-root.MuiPaper-elevation1.MuiPaper-rounded > table > tbody > tr");

				if (publicationList.Count > 0)
				{
					actual = true;
				}
			}
			catch
			{
				actual = false;
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void Publication_UserCanDeletePublications()
		{
			// Arrange
			bool expected = true;

			// Act
			bool actual = false;
			try
			{
				var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));

				var publications = driver.FindElementsByCssSelector(
					"#root > div > div.MuiPaper-root.MuiTableContainer-root.MuiPaper-elevation1.MuiPaper-rounded > table > tbody > tr");
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
					By.CssSelector(
						"#root > div > div.MuiPaper-root.MuiTableContainer-root.MuiPaper-elevation1.MuiPaper-rounded > table > tbody > tr:nth-child(1) > td:nth-child(1) > button:nth-child(2")));
				
				var deleteButton = driver.FindElementByCssSelector(
					"#root > div > div.MuiPaper-root.MuiTableContainer-root.MuiPaper-elevation1.MuiPaper-rounded > table > tbody > tr:nth-child(1) > td:nth-child(1) > button:nth-child(2)");
				deleteButton.Click();

				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.AlertIsPresent());
				var alert = driver.SwitchTo().Alert();
				alert.Accept();
				actual = true;
				
				// Thread.Sleep(100000);
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
					By.CssSelector(
						"#root > div > div.MuiPaper-root.MuiTableContainer-root.MuiPaper-elevation1.MuiPaper-rounded > table > tbody")));

				
				for (int i = 0; i < 5; ++i)
				{
					Thread.Sleep(10000);
					var newPublications = driver.FindElementsByCssSelector(
						"#root > div > div.MuiPaper-root.MuiTableContainer-root.MuiPaper-elevation1.MuiPaper-rounded > table > tbody > tr");

					if (publications.Count - newPublications.Count == 1)
					{
						actual = true;
						break;
					}
				}
			}
			catch
			{
				actual = false;
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}
	}
}