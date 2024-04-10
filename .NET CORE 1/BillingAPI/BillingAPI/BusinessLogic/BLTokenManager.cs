using System.Diagnostics;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Text;
using BillingAPI.Models.POCO;
using Microsoft.IdentityModel.Tokens;

namespace BillingAPI.BusinessLogic
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
        /// Cache prefix to store tokens
        /// </summary>
        public const string CachePrefix = "JWTToken_";

        /// <summary>
        /// MemoryCache object 
        /// </summary>
        public static MemoryCache cache = MemoryCache.Default;

        /// <summary>
        /// Generates access tokens
        /// </summary>
        /// <param name="user">object of USR01</param>
        /// <returns>Token</returns>
        public void GenerateToken(USR01 user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.R01F02),
                    new Claim(ClaimTypes.Role, user.R01F04.ToString()) // Add roles as claims
 
                }),
                Expires = DateTime.UtcNow.AddMinutes(20),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);
            string cacheKey = CachePrefix + user.R01F02;

            cache.Set(cacheKey, tokenString, new CacheItemPolicy { AbsoluteExpiration = DateTime.UtcNow.AddMinutes(20) });
        }

        /// <summary>
        /// Generates refresh token
        /// </summary>
        /// <param name="token">Token entered by user</param>
        /// <returns>New token with new expiration time</returns>
        public string RefreshToken(USR01 user)
        {
            // Check if token exists in cache
            if (cache.Contains(CachePrefix + user.R01F02))
            {
                string token = (string)cache.Get(CachePrefix + user.R01F02);
                var key = Encoding.UTF8.GetBytes(Secret);

                var tokenHandler = new JwtSecurityTokenHandler();
                // Retrieve token from cache
                JwtSecurityToken oldToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

                // Update expiration time
                var newExpiration = DateTime.UtcNow.AddMinutes(20); // Refresh for another 2mins
                var newTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(oldToken.Claims),
                    Expires = newExpiration,
                    SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
                };

                // Generate new token
                var newToken = tokenHandler.CreateToken(newTokenDescriptor);
                var tokenString = tokenHandler.WriteToken(newToken);
                string cacheKey = CachePrefix + user.R01F02;

                cache.Set(cacheKey, tokenString, new CacheItemPolicy { AbsoluteExpiration = DateTime.UtcNow.AddMinutes(20) });
                Debug.WriteLine(tokenString);
                return tokenString;
            }

            // Token not found in cache
            return null;
        }

        /// <summary>
        /// Gets principal from token 
        /// </summary>
        /// <param name="token">JWT token</param>
        /// <returns>Principal if valid token else null through catch</returns>
        public ClaimsPrincipal GetPrincipal(string token)
        {
            try
            {
                JwtSecurityTokenHandler tokenHandler = new JwtSecurityTokenHandler();

                JwtSecurityToken jwtToken = (JwtSecurityToken)tokenHandler.ReadToken(token);

                if (jwtToken == null)
                    return null;

                byte[] key = Encoding.UTF8.GetBytes(Secret);

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
        /// <param name="token">Token entered by user</param>
        /// <returns>True if token is valid otherwise false</returns>
        public bool ValidateToken(string token)
        {
            try
            {
                var principal = GetPrincipal(token); 
                if (principal == null)
                {
                    return false;
                }
                return true; // Token is valid
            }
            catch (Exception ex)
            {
                // Token validation failed
                Console.WriteLine($"Token validation failed: {ex.Message}");
                return false;
            }
        }


       
    }
}



















///// <summary>
///// Checks token is expired or not
///// </summary>
///// <param name="token">Token entered by user</param>
///// <returns>True if token is expired otherwise false</returns>
//public bool IsTokenExpired(string token)
//{

//    var tokenHandler = new JwtSecurityTokenHandler();
//    var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

//    var expiration = jwtToken.ValidTo;

//    return expiration < DateTime.UtcNow;
//}