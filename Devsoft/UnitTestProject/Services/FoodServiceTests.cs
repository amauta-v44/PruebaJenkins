using Devsoft.Api.Services.ServicesImpl;
using Xunit;

namespace UnitTestProject.Services
{
	public class FoodServiceTests
	{
		private readonly FoodServiceImpl _foodService;

		#region Constructor

		public FoodServiceTests()
		{
			_foodService = new FoodServiceImpl(null, null, null);
		}

		#endregion


		#region IsNameValidTests

		[Fact]
		public void IsNameValid_NameShouldNotBeEmpty()
		{
			// Arrange
			bool expected = false;

			// Act
			bool actual = _foodService.IsNameValid("");

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsNameValid_NameShouldNotBeNull()
		{
			// Arrange
			bool expected = false;

			// Act
			bool actual = _foodService.IsNameValid(null);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsNameValid_NameSizeShouldNotBeLessThan5Characters()
		{
			// Arrange
			bool expected = false;
			string tooShortString = new string('A', 4);

			// Act
			bool actual = _foodService.IsNameValid(tooShortString);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsNameValid_NameSizeShouldNotBeMoreThan50Characters()
		{
			// Arrange
			bool expected = false;
			string tooLongDescription = new string('A', 51);

			// Act
			bool actual = _foodService.IsNameValid(tooLongDescription);

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion
	}
}