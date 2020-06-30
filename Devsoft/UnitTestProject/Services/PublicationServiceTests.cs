using System;
using System.Text;
using Devsoft.Api.Services.ServicesImpl;
using Xunit;
using Xunit.Abstractions;

namespace UnitTestProject.Services
{
	public class PublicationServiceTests
	{
		private readonly ITestOutputHelper _testOutputHelper;
		private PublicationServiceImpl _publicationService;

		#region Constructor

		public PublicationServiceTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
			_publicationService = new PublicationServiceImpl(
				null,
				null,
				null,
				null
			);
		}

		#endregion


		#region IsDescriptionValidTests

		[Fact]
		public void IsDescriptionValid_DescriptionShouldNotBeEmpty()
		{
			// Arrange
			bool expected = false;

			// Act
			bool actual = _publicationService.IsDescriptionValid("");

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsDescriptionValid_DescriptionShouldNotBeNull()
		{
			// Arrange
			bool expected = false;

			// Act
			bool actual = _publicationService.IsDescriptionValid(null);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsDescriptionValid_DescriptionSizeShouldNotBeLessThan10Characters()
		{
			// Arrange
			bool expected = false;
			string tooShortString = new string('A', 9);

			// Act
			bool actual = _publicationService.IsDescriptionValid(tooShortString);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsDescriptionValid_DescriptionSizeShouldNotBeMoreThan300Characters()
		{
			// Arrange
			bool expected = false;
			string tooLongDescription = new string('A', 301);

			// Act
			bool actual = _publicationService.IsDescriptionValid(tooLongDescription);

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion


		#region IsStockValidTests

		[Fact]
		public void IsStockValid_ShouldNotBeNegative()
		{
			// Arrange
			bool expected = false;
			int negativeStock = -10;

			// Act
			bool actual = _publicationService.IsStockValid(negativeStock);

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion


		#region IsPricePerUnitValidTests

		[Fact]
		public void IsPricePerUnitValid_ShouldNotBeNegative()
		{
			// Arrange
			bool expected = false;
			float negativePricePerUnit = -10.0f;

			// Act
			bool actual = _publicationService.IsPricePerUnitValid(negativePricePerUnit);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsPricePerUnitValid_ShouldNotHaveMoreThan3Decimals()
		{
			// Arrange
			bool expected = false;
			float wrongPricePerUnit = 10.123f;

			// Act
			bool actual = _publicationService.IsPricePerUnitValid(wrongPricePerUnit);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsPricePerUnitValid_ShouldHaveAtMost2Decimals()
		{
			// Arrange
			bool expected = true;
			float wrongPricePerUnitA = 10.12f;
			float wrongPricePerUnitB = 10.1f;
			float wrongPricePerUnitC = 10.0f;
			float wrongPricePerUnitD = 10f;

			// Act
			bool actualA = _publicationService.IsPricePerUnitValid(wrongPricePerUnitA);
			bool actualB = _publicationService.IsPricePerUnitValid(wrongPricePerUnitB);
			bool actualC = _publicationService.IsPricePerUnitValid(wrongPricePerUnitC);
			bool actualD = _publicationService.IsPricePerUnitValid(wrongPricePerUnitD);

			// Assert
			Assert.Equal(expected, actualA);
			Assert.Equal(expected, actualB);
			Assert.Equal(expected, actualC);
			Assert.Equal(expected, actualD);
		}

		#endregion


		#region AreDatesValidTests

		[Fact]
		public void AreDatesValid_StartDateShouldNotBeAfterEndDate()
		{
			// Arrange
			bool expected = false;
			DateTime endDate = DateTime.Now;
			DateTime startDate = DateTime.Now.AddDays(1);

			// Act
			bool actual = _publicationService.AreDatesValid(startDate, endDate);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void AreDatesValid_DatesDifferenceShouldNotBeLessThan1Days()
		{
			// Arrange
			bool expected = false;
			DateTime startDate = DateTime.Now;
			DateTime endDate = DateTime.Now.AddHours(23);

			// Act
			bool actual = _publicationService.AreDatesValid(startDate, endDate);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void AreDatesValid_DatesDifferenceShouldBeAtLeast1Days()
		{
			// Arrange
			bool expected = true;
			DateTime startDate = DateTime.Now;
			DateTime endDate = DateTime.Now.AddHours(25);

			// Act
			bool actual = _publicationService.AreDatesValid(startDate, endDate);

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion


		#region IsImageUrlValidTests

		[Fact]
		public void IsImageUrlValid_UrlShouldNotBeInvalid()
		{
			// Arrange
			bool expected = false;
			string wrongUrlFormat = "WrongUrl";

			// Act
			bool actual = _publicationService.IsImageUrlValid(wrongUrlFormat);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsImageUrlValid_UrlShouldBeValidUrl()
		{
			// Arrange
			bool expected = true;
			string rightUrlFormat = "https://image.png";

			// Act
			bool actual = _publicationService.IsImageUrlValid(rightUrlFormat);

			// Assert
			Assert.Equal(expected, actual);
		}

		
		[Fact]
		public void IsImageUrlValid_UrlSizeShouldNotBeLessThan17Characters()
		{
			// Arrange
			bool expected = false;
			string shortImageUrl = new string('A', 16);

			// Act
			bool actual = _publicationService.IsImageUrlValid(shortImageUrl);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsImageUrlValid_UrlSizeShouldNotBeMoreThan2048Characters()
		{
			// Arrange
			bool expected = false;
			string longImageUrl = new string('A', 2049);

			// Act
			bool actual = _publicationService.IsImageUrlValid(longImageUrl);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsImageUrlValid_UrlSizeShouldBeAtMost2048Characters()
		{
			// Arrange
			bool expected = true;
			string longImageUrl = $"https://{new string('A', 2036)}.jpg";

			// Act
			bool actual = _publicationService.IsImageUrlValid(longImageUrl);

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion
	}
}