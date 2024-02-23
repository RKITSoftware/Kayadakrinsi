using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;

namespace HospitalAdvance.BusinessLogic
{
	/// <summary>
	/// Handles logic for JWT token
	/// </summary>
	public class BLTokenManager
	{
		/// <summary>
		/// Secret key for hashing
		/// </summary>
		public static string Secret = "ERMN05OPLoDvbTTa/QkqLNMI7cPLguaRyHzyg7n5qNBVjQmtBhz4SzYh4NBVCXi3KJHlSXKP+oi2+bXr6CUYTR==";

		/// <summary>
		/// Generates custom JWT Token
		/// </summary>
		/// <param name="username">username of user</param>
		/// <returns>Custom JWT token</returns>
		public static string GenerateToken(string username)
		{
			byte[] key = Convert.FromBase64String(Secret);
			SymmetricSecurityKey securityKey = new SymmetricSecurityKey(key);

			SecurityTokenDescriptor descriptor = new SecurityTokenDescriptor
			{
				Subject = new ClaimsIdentity(new[] {
					  new Claim(ClaimTypes.Name, username)}),
				Expires = DateTime.UtcNow.AddMinutes(30),
				SigningCredentials = new SigningCredentials(securityKey,
				SecurityAlgorithms.HmacSha256Signature)
			};

			JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

			JwtSecurityToken token = handler.CreateJwtSecurityToken(descriptor);

			return handler.WriteToken(token);
		}

		/// <summary>
		/// Gets principal from token 
		/// </summary>
		/// <param name="token">JWT token</param>
		/// <returns>Principal if valid token else null through catch</returns>
		public static ClaimsPrincipal GetPrincipal(string token)
		{
			try
			{
				JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

				JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

				if (jwtToken == null)
					return null;

				byte[] key = Convert.FromBase64String(Secret);

				TokenValidationParameters parameters = new TokenValidationParameters()
				{
					RequireExpirationTime = true,
					ValidateIssuer = false,
					ValidateAudience = false,
					IssuerSigningKey = new SymmetricSecurityKey(key)
				};

				SecurityToken securityToken;

				ClaimsPrincipal principal = tokenHandler.ValidateToken(token, parameters, out securityToken);

				return principal;
			}
			catch
			{
				return null;
			}
		}

		/// <summary>
		/// Validates token 
		/// </summary>
		/// <param name="token">JWT token</param>
		/// <returns>True if token is valid otherwise false</returns>
		public static bool ValidateToken(string token)
		{
			ClaimsPrincipal principal = GetPrincipal(token);
			if (principal == null)
				return false;
			return true;
		}

	}
}