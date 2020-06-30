using System;
using Xunit;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest.Test
{
	public class AuthSeleniumTests
	{
		private ChromeDriver driver;

		public AuthSeleniumTests()
		{
			var chromeOptions = new ChromeOptions();
			// chromeOptions.AddArguments("headless");

			driver = new ChromeDriver("../../../", chromeOptions);
			driver.Manage().Window.Maximize();

			driver.Navigate().GoToUrl("https://raigiku.github.io/SI656-Experimentos_ISW-Frontend/");
		}

		#region Username_Tests

		[Fact]
		public void IsUsernameValid_UsernameShouldNotBeEmpty()
		{
			// Arrange
			string expected = "Requerido";
			string emptyText = "";

			// Act
			string actual;
			try
			{
				var usernameInput = driver.FindElementByXPath("//*[@id=\"root\"]/main/div/form/div[1]/div/input");
				usernameInput.SendKeys(emptyText);

				var warningElement =
					driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(1) > p");
				actual = warningElement.Text;
			}
			catch
			{
				actual = "";
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void IsUsernameValid_UsernameSizeShouldNotBeLessThan6Characters()
		{
			// Arrange
			string expected = "Debe tener entre 6 a 20 caracteres";
			string tooShortString = new string('A', 5);

			// Act
			string actual;
			try
			{
				var usernameInput = driver.FindElementByCssSelector(
					"#root > main > div > form > div:nth-child(1) > div > input");
				usernameInput.SendKeys(tooShortString);

				var warningElement =
					driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(1) > p");
				actual = warningElement.Text;
			}
			catch
			{
				actual = "";
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void IsUsernameValid_UsernameSizeShouldNotBeMoreThan20Characters()
		{
			// Arrange
			string expected = "Debe tener entre 6 a 20 caracteres";
			string tooLongUsername = new string('A', 21);

			// Act
			string actual;
			try
			{
				var usernameInput = driver.FindElementByCssSelector(
					"#root > main > div > form > div:nth-child(1) > div > input");
				usernameInput.SendKeys(tooLongUsername);

				var warningElement =
					driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(1) > p");
				actual = warningElement.Text;
			}
			catch
			{
				actual = "";
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void IsUsernameValid_UsernameShouldNotHaveSpacesWithin()
		{
			// Arrange
			string expected = "No debe incluir espacios en blanco";
			string usernameWithSpaces = "joaquin garcia";

			// Act
			string actual;
			try
			{
				var usernameInput = driver.FindElementByCssSelector(
					"#root > main > div > form > div:nth-child(1) > div > input");
				usernameInput.SendKeys(usernameWithSpaces);

				var warningElement =
					driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(1) > p");
				actual = warningElement.Text;
			}
			catch
			{
				actual = "";
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion

		#region Password Tests

		[Fact]
		public void IsPasswordValid_PasswordShouldNotBeEmpty()
		{
			// Arrange
			string expected = "Requerido";
			string emptyText = "";

			// Act
			string actual;
			try
			{
				
				var usernameInput = driver.FindElementByXPath("//*[@id=\"root\"]/main/div/form/div[2]/div/input");
				usernameInput.SendKeys(emptyText);
				
				var warningElement =
					driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(2) > p");
				actual = warningElement.Text;
			}
			catch
			{
				actual = "";
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void IsPasswordValid_PasswordSizeShouldNotBeLessThan8Characters()
		{
			// Arrange
			string expected = "Debe tener entre 8 a 20 caracteres";
			string tooShortPassword = new string('A', 7);

			// Act
			string actual;
			try
			{
				var passwordInput =
					driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(2) > div > input");
				passwordInput.SendKeys(tooShortPassword);
				
				var warningElement = driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(2) > p");
				actual = warningElement.Text;
			}
			catch
			{
				actual = "";
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void IsPasswordValid_PasswordSizeShouldNotBeMoreThan20Characters()
		{
			// Arrange
			string expected = "Debe tener entre 8 a 20 caracteres";
			string tooLongPassword = new string('A', 21);

			// Act
			string actual;
			try
			{
				var passwordInput =
					driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(2) > div > input");
				passwordInput.SendKeys(tooLongPassword);
				
				var warningElement = driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(2) > p");
				actual = warningElement.Text;
			}
			catch
			{
				actual = "";
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void IsPasswordValid_PasswordShouldNotHaveSpacesWithin()
		{
			// Arrange
			string expected = "No debe incluir espacios en blanco";
			string passwordWithSpaces = "super contraseña";

			// Act
			string actual;
			try
			{
				var passwordInput =
					driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(2) > div > input");
				passwordInput.SendKeys(passwordWithSpaces);
				
				var warningElement = driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(2) > p");
				actual = warningElement.Text;
			}
			catch
			{
				actual = "";
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion

		#region Funcionality

		[Fact]
		public void UserCanLogIn()
		{
			// Arrange
			bool expected = true;
			string username = "diegocas";
			string password = "12345678";

			// Act
			bool actual = false;
			try
			{

				var usernameInput =
					driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(1) > div > input");
				usernameInput.SendKeys(username);
				
				var passwordInput =
					driver.FindElementByCssSelector("#root > main > div > form > div:nth-child(2) > div > input");
				passwordInput.SendKeys(password);

				var loginButton = driver.FindElementByCssSelector("#root > main > div > form > button");
				loginButton.Click();

				var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
					By.CssSelector(
						"#root > div > div.MuiGrid-root.MuiGrid-container > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-2.MuiGrid-grid-md-1 > button")));
				actual = true;
			}
			catch
			{
				actual = false;
			}

			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion
	}
}