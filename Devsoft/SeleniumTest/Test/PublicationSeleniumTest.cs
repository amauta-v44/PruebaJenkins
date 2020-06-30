using System;
using Xunit;
using System.Threading;
using OpenQA.Selenium;
using OpenQA.Selenium.Chrome;
using OpenQA.Selenium.Support.UI;

namespace SeleniumTest.Test
{
	public class PublicationSeleniumTests
	{
		private ChromeDriver driver;

		public PublicationSeleniumTests()
		{
			var chromeOptions = new ChromeOptions();
			//chromeOptions.AddArguments("headless");
			driver = new ChromeDriver("../../../", chromeOptions);
			driver.Navigate().GoToUrl("https://raigiku.github.io/SI656-Experimentos_ISW-Frontend/");
			driver.Manage().Window.Maximize();

			LogInUser();
			OpenPublicationDialog();
		}

		private void LogInUser()
		{
			// Usuario y contrasenia del usuario
			string username = "diegocas";
			string password = "12345678";

			// Proceso de login
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
		}

		private void OpenPublicationDialog()
		{
			var addPublicationButton = driver.FindElementByCssSelector(
				"#root > div > div.MuiGrid-root.MuiGrid-container > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-2.MuiGrid-grid-md-1 > button");
			addPublicationButton.Click();
		}

		#region Description_Tests

		[Fact]
		public void IsDescriptionValid_DescriptionShouldNotBeEmpty()
		{
			// Arrange
			string expected = "Requerido";
			string emptyString = "";

			// Act
			string actual;
			try
			{
				var descriptionInput =
					driver.FindElementByXPath("/html/body/div[2]/div[3]/div/div[2]/div/div[4]/div/div/textarea[1]");
				descriptionInput.SendKeys(emptyString);

				var warningElement = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-8 > div > p");
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
		public void IsDescriptionValid_DescriptionSizeShouldNotBeLessThan10Characters()
		{
			// Arrange
			string expected = "Debe tener entre 10 a 300 caracteres";
			string tooShortDescription = new string('A', 9);

			// Act
			string actual;
			try
			{
				var descriptionInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-8 > div > div > textarea:nth-child(1)");
				descriptionInput.SendKeys(tooShortDescription);

				var warningElement = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-8 > div > p");
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
		public void IsDescriptionValid_DescriptionSizeShouldNotBeMoreThan200Characters()
		{
			// Arrange
			string expected = "Debe tener entre 10 a 300 caracteres";
			string tooLongDescription = new string('A', 301);

			// Act
			string actual;
			try
			{
				var descriptionInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-8 > div > div > textarea:nth-child(1)");
				descriptionInput.SendKeys(tooLongDescription);

				var warning = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-8 > div > p");
				actual = warning.Text;
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

		#region Stock_Tests

		[Fact]
		public void IsStockValid_ShouldNotBeNegative()
		{
			// Arrange
			string expected = "Debe ser un número entero positivo";
			string negativeStock = "-10";

			// Act
			string actual;
			try
			{
				var stockInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(6) > div > div > input");
				stockInput.SendKeys(negativeStock);
				var warning = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(6) > div > p");
				actual = warning.Text;
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
		public void IsStockValid_ShouldNotHaveDecimal()
		{
			// Arrange
			string expected = "Debe ser un número entero positivo";
			string negativeStock = "10.5";

			// Act
			string actual;
			try
			{
				var stockInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(6) > div > div > input");
				stockInput.SendKeys(negativeStock);
				var warning = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(6) > div > p");
				actual = warning.Text;
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

		#region PricePerUnit_Tests

		[Fact]
		public void IsPricePerUnitValid_ShouldNotHaveMoreThan3Decimals()
		{
			// Arrange
			string expected = "Debe tener como máximo 2 decimales";
			string wrongPricePerUnit = "10.123";

			// Act
			string actual;
			try
			{
				var pricePerUnitInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(7) > div > div > input");
				pricePerUnitInput.SendKeys(wrongPricePerUnit);

				var warningElement = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(7) > div > p");
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
		public void IsPricePerUnitValid_ShouldNotBeNegative()
		{
			// Arrange
			string expected = "Debe ser un número positivo";
			string wrongPricePerUnit = "-10";

			// Act
			string actual;
			try
			{
				var pricePerUnitInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(7) > div > div > input");
				pricePerUnitInput.SendKeys(wrongPricePerUnit);

				var warning = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(7) > div > p");
				actual = warning.Text;
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

		#region ImageUrl_Tests

		[Fact]
		public void IsImageUrlValid_UrlShouldNotBeInvalid()
		{
			// Arrange
			string expected = "Debe iniciar con `http://` o `https://`";
			string wrongUrlFormat = "WrongUrl";

			// Act
			string actual;
			try
			{
				var imageUrlInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-7 > div > div > input");
				imageUrlInput.SendKeys(wrongUrlFormat);

				var warning = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-7 > div > p");
				actual = warning.Text;
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
		public void IsImageUrlValid_UrlSizeShouldNotBeMoreThan2048Characters()
		{
			// Arrange
			string expected = "Debe tener entre 17 a 2048 caracteres";
			string longImageUrl = $"https://{new string('A', 2037)}.jpg";

			// Act
			string actual;
			try
			{
				var imageUrlInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-7 > div > div > input");
				imageUrlInput.SendKeys(longImageUrl);

				// Esperar hasta que se escriba el text
				Thread.Sleep(10000);

				var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(100));
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementExists(
					By.CssSelector(
						"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-7 > div > p")));

				var warningElement = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-7 > div > p");
				actual = warningElement.Text;
			}
			catch
			{
				actual = "";
			}

			Thread.Sleep(10000);
			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void IsImageUrlValid_UrlShouldBeValidUrl()
		{
			// Arrange
			string expected = "";
			string rightUrlFormat =
				"https://p7.hiclipart.com/preview/460/489/163/tomato-juice-cherry-tomato-stock-photography-two-tomatoes.jpg";

			// Act
			string actual;
			try
			{
				var imageUrlInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-7 > div > div > input");
				imageUrlInput.SendKeys(rightUrlFormat);

				var warning = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-7 > div > p");
				actual = warning.Text;
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

		#region Food_Tests

		[Fact]
		public void IsFoodValid_FoodShouldBeChoose()
		{
			// Arrange
			string expected = "";

			// Act
			string actual;
			try
			{
				var box = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(3) > div > div > div");
				box.Click();

				var chosenFood = driver.FindElementByCssSelector(
					"#menu- > div.MuiPaper-root.MuiMenu-paper.MuiPopover-paper.MuiPaper-elevation8.MuiPaper-rounded > ul > li:nth-child(2)");
				chosenFood.Click();

				var warningElement = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(3) > div > p");
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

		#region Visibility_Tests

		[Fact]
		public void IsVisibilityValid_VisibilityShouldBeChoose()
		{
			// Arrange
			string expected = "";

			// Act
			string actual;
			try
			{
				var visibility = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(5) > div > div > label:nth-child(1)");
				visibility.Click();

				var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementIsVisible(
					By.CssSelector(
						"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(5) > div > div > label:nth-child(1)")));

				var warningElement = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(5) > div > p");
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

		#region CreatePublication

		[Fact]
		public void IsCreatePublication_PublicationShouldBePublicated()
		{
			// Arrange
			bool expected = true;
			string rightUrlFormat =
				"https://p7.hiclipart.com/preview/460/489/163/tomato-juice-cherry-tomato-stock-photography-two-tomatoes.jpg";
			string description = "Tomates importados del Monte Olimpo cosechados por la mismisima diosa Demeter";
			string stock = "1";
			string priceUnit = "69";

			// Act
			bool actual;
			try
			{
				actual = false;

				//URL
				var imageUrlInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-7 > div > div > input");
				imageUrlInput.SendKeys(rightUrlFormat);


				// Food
				var box = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(3) > div > div > div");
				box.Click();

				var chosenFood = driver.FindElementByCssSelector(
					"#menu- > div.MuiPaper-root.MuiMenu-paper.MuiPopover-paper.MuiPaper-elevation8.MuiPaper-rounded > ul > li:nth-child(2)");
				chosenFood.Click();


				// Description
				var descriptionInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div.MuiGrid-root.MuiGrid-item.MuiGrid-grid-xs-12.MuiGrid-grid-md-8 > div > div > textarea:nth-child(1)");
				descriptionInput.SendKeys(description);


				// Visibility
				var wait = new WebDriverWait(driver, TimeSpan.FromSeconds(10));
				wait.Until(SeleniumExtras.WaitHelpers.ExpectedConditions.ElementToBeClickable(
					By.CssSelector(
						"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(5) > div > div > label:nth-child(1)")));
				var visibility = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(5) > div > div > label:nth-child(1)");
				visibility.Click();


				// Stock
				var stockInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(6) > div > div > input");
				stockInput.SendKeys(stock);


				// Price Unit
				var pricePerUnitInput = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogContent-root > div > div:nth-child(7) > div > div > input");
				pricePerUnitInput.SendKeys(priceUnit);


				// Save
				var saveButton = driver.FindElementByCssSelector(
					"body > div.MuiDialog-root > div.MuiDialog-container.MuiDialog-scrollPaper > div > div.MuiDialogActions-root.MuiDialogActions-spacing > button.MuiButtonBase-root.MuiButton-root.MuiButton-text.MuiButton-textPrimary");
				saveButton.Click();


				//VERIFICATION
				actual = true;
			}
			catch
			{
				actual = false;
			}

			Thread.Sleep(5000);
			driver.Close();

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion
	}
}