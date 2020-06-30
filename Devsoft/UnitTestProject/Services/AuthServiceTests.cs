using System;
using Devsoft.Api.Services.ServicesImpl;
using Microsoft.Extensions.DependencyInjection;
using Xunit;
using Xunit.Abstractions;

namespace UnitTestProject.Services
{
	public class AuthServiceTests
	{
		private readonly ITestOutputHelper _testOutputHelper;

		#region Constructor

		public AuthServiceTests(ITestOutputHelper testOutputHelper)
		{
			_testOutputHelper = testOutputHelper;
		}

		#endregion


		#region IsUsernameValidTests

		[Fact]
		public void IsUsernameValid_UsernameShouldNotBeEmpty()
		{
			// Arrange
			bool expected = false;

			// Act
			bool actual = AuthServiceImpl.IsUsernameValid("");

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsUsernameValid_UsernameShouldNotBeNull()
		{
			// Arrange
			bool expected = false;

			// Act
			bool actual = AuthServiceImpl.IsUsernameValid(null);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsUsernameValid_UsernameSizeShouldNotBeLessThan6Characters()
		{
			// Arrange
			bool expected = false;
			string tooShortString = new string('A', 5);

			// Act
			bool actual = AuthServiceImpl.IsUsernameValid(tooShortString);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsUsernameValid_UsernameSizeShouldNotBeMoreThan20Characters()
		{
			// Arrange
			bool expected = false;
			string tooLongDescription = new string('A', 21);

			// Act
			bool actual = AuthServiceImpl.IsUsernameValid(tooLongDescription);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsUsernameValid_UsernameShouldNotHaveSpacesWithin()
		{
			// Arrange
			bool expected = false;
			string wrongString = "joaquin garcia";

			// Act
			bool actual = AuthServiceImpl.IsUsernameValid(wrongString);

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion


		#region IsPasswordValidTests

		[Fact]
		public void IsPasswordValid_PasswordShouldNotBeEmpty()
		{
			// Arrange
			bool expected = false;

			// Act
			bool actual = AuthServiceImpl.IsPasswordValid("");

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsPasswordValid_PasswordShouldNotBeNull()
		{
			// Arrange
			bool expected = false;

			// Act
			bool actual = AuthServiceImpl.IsPasswordValid(null);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsPasswordValid_PasswordSizeShouldNotBeLessThan8Characters()
		{
			// Arrange
			bool expected = false;
			string tooShortString = new string('A', 7);

			// Act
			bool actual = AuthServiceImpl.IsPasswordValid(tooShortString);

			// Assert
			Assert.Equal(expected, actual);
		}


		[Fact]
		public void IsPasswordValid_PasswordSizeShouldNotBeMoreThan20Characters()
		{
			// Arrange
			bool expected = false;
			string tooLongDescription = new string('A', 21);

			// Act
			bool actual = AuthServiceImpl.IsPasswordValid(tooLongDescription);

			// Assert
			Assert.Equal(expected, actual);
		}

		[Fact]
		public void IsPasswordValid_PasswordShouldNotHaveSpacesWithin()
		{
			// Arrange
			bool expected = false;
			string wrongString = "super contraseña";

			// Act
			bool actual = AuthServiceImpl.IsPasswordValid(wrongString);

			// Assert
			Assert.Equal(expected, actual);
		}

		#endregion
	}
}