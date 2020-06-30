using System;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using Devsoft.Api.Configuration;
using Devsoft.Api.Dtos.Authentication;
using Devsoft.Api.Entities;
using Devsoft.Api.Middlewares;
using Devsoft.Api.Repositories;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace Devsoft.Api.Services.ServicesImpl
{
	public class AuthServiceImpl : IAuthService
	{
		private readonly ILogger<AuthServiceImpl> _logger;
		private readonly AppSettings _appSettings;
		private readonly IUsersRepository _usersRepository;

		public AuthServiceImpl(
			ILogger<AuthServiceImpl> logger,
			IOptions<AppSettings> appSettings,
			IUsersRepository usersRepository
		)
		{
			_logger = logger;
			_appSettings = appSettings.Value;
			_usersRepository = usersRepository;
		}

		public async Task<JwtResponse> LoginAsync(string username, string password)
		{
			var found = await _usersRepository.FindOneByUsernameAsync(username);
			if (found == null)
				throw new HttpResponseException(HttpStatusCode.NotFound,
					$"User with username {username} was not found");

			password = GenerateHash(password);

			if (!ValidatePassword(password, found.Password))
				throw new HttpResponseException(HttpStatusCode.BadRequest, "Username or Password is incorrect");

			var token = GenerateToken(found);
			return new JwtResponse
			{
				Token = token
			};
		}

		public async Task<JwtResponse> RegisterAsync(string username, string password)
		{
			var found = await _usersRepository.FindOneByUsernameAsync(username);
			if (found != null)
				throw new HttpResponseException(HttpStatusCode.BadRequest, "User already exists");

			var newUser = new User
			{
				Username = username,
				Password = GenerateHash(password),
				CreatedAt = DateTime.Now,
				UpdatedAt = DateTime.Now
			};
			await _usersRepository.CreateOneAsync(newUser);

			var generatedToken = GenerateToken(new User());
			return new JwtResponse
			{
				Token = generatedToken
			};
		}

		private string GenerateToken(User user)
		{
			var tokenHandler = new JwtSecurityTokenHandler();
			var key = Encoding.ASCII.GetBytes(_appSettings.Secret);

			var tokenDescriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new Claim[]
				{
					new Claim(ClaimTypes.NameIdentifier, user.Id.ToString()),
				}),
				Expires = DateTime.UtcNow.AddYears(1),
				SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
					SecurityAlgorithms.HmacSha256Signature)
			};

			var token = tokenHandler.CreateToken(tokenDescriptor);
			return tokenHandler.WriteToken(token);
		}

		private bool ValidatePassword(string password, string hashedPassword)
		{
			return password == hashedPassword;
		}

		private string GenerateHash(string text)
		{
			string hash;

			using (SHA256 mySha256 = SHA256.Create())
			{
				byte[] bytes = Encoding.ASCII.GetBytes(text);
				byte[] hashValue = mySha256.ComputeHash(Encoding.UTF8.GetBytes(text));
				StringBuilder builder = new StringBuilder();
				foreach (var t in hashValue)
				{
					builder.Append(t.ToString("x2"));
				}

				hash = builder.ToString();
			}

			return hash;
		}


		// Validations
		public static bool IsUsernameValid(string username)
		{
			if (string.IsNullOrEmpty(username)) return false;
			username = username.Trim();
			
			// Searching blanc spaces
			var index = username.IndexOf(' ');
			if (index != -1) return false;
			
			return 6 <= username.Length && username.Length <= 20;
		}

		public static bool IsPasswordValid(string password)
		{
			if (string.IsNullOrEmpty(password)) return false;
			password = password.Trim();
			
			// Searching blanc spaces
			var index = password.IndexOf(' ');
			if (index != -1) return false;
			
			return 8 <= password.Length && password.Length <= 20;
		}
	}
}