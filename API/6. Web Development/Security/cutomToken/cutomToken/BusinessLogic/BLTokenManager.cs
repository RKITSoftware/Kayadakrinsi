using System;
using System.IdentityModel.Tokens.Jwt;
using System.Runtime.Caching;
using System.Security.Claims;
using System.Text;
using cutomToken.Models;
using Microsoft.IdentityModel.Tokens;

namespace cutomToken
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
        /// Generates access tokens
        /// </summary>
        /// <param name="user">object of USR01</param>
        /// <returns>Token</returns>
        public static string GenerateToken(USR01 user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.UTF8.GetBytes(Secret);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim(ClaimTypes.Name, user.R01F02),
                    new Claim(ClaimTypes.Role, user.R01F04) // Add roles as claims
 
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };

            var token = tokenHandler.CreateToken(tokenDescriptor);
            var tokenString = tokenHandler.WriteToken(token);

            // Store token in cache
            var cache = MemoryCache.Default;
            cache.Set(CachePrefix + tokenString, token, new CacheItemPolicy { AbsoluteExpiration = DateTime.UtcNow.AddMinutes(20) });
           
            return tokenString;
        }

        /// <summary>
        /// Generates refresh token
        /// </summary>
        /// <param name="token">Token entered by user</param>
        /// <returns>New token with new expiration time</returns>
        public static string RefreshToken(string token)
        {
            var cache = MemoryCache.Default;

            // Check if token exists in cache
            if (cache.Contains(CachePrefix + token))
            {
                // Retrieve token from cache
                var oldToken = (JwtSecurityToken)cache.Get(CachePrefix + token);

                // Update expiration time
                var newExpiration = DateTime.UtcNow.AddMinutes(20); // Refresh for another hour
                var newTokenDescriptor = new SecurityTokenDescriptor
                {
                    Subject = new ClaimsIdentity(oldToken.Claims),
                    Expires = newExpiration,
                    SigningCredentials = oldToken.SigningCredentials
                };

                // Generate new token
                var tokenHandler = new JwtSecurityTokenHandler();
                var newToken = tokenHandler.CreateToken(newTokenDescriptor);
                var tokenString = tokenHandler.WriteToken(newToken);

                // Store new token in cache
                cache.Set(CachePrefix + tokenString, tokenString, new CacheItemPolicy { AbsoluteExpiration = newExpiration });

                return tokenHandler.WriteToken(newToken);
            }

            // Token not found in cache
            return null;
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
        public static bool ValidateToken(string token)
        {
            var cache = MemoryCache.Default;

            // Check if token exists in cache
            if (cache.Contains(CachePrefix + token))
            {
                //// Retrieve token from cache
                //var cachedToken = (JwtSecurityToken)cache.Get(CachePrefix + token);

                // Token validation logic
                var tokenHandler = new JwtSecurityTokenHandler();
                var validationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Secret)),
                    ValidateIssuer = false, // Update as per your requirements
                    ValidateAudience = false // Update as per your requirements
                };

                try
                {
                    tokenHandler.ValidateToken(token, validationParameters, out SecurityToken validatedToken);

                    return true;
                }
                catch (SecurityTokenException)
                {
                    // Token validation failed
                    return false;
                }
            }

            // Token not found in cache
            return false;
        }

        /// <summary>
        /// Checks token is expired or not
        /// </summary>
        /// <param name="token">Token entered by user</param>
        /// <returns>True if token is expired otherwise false</returns>
        public static bool IsTokenExpired(string token)
        {

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;

            var expiration = jwtToken.ValidTo;

            return expiration < DateTime.UtcNow;
        }
    }

}
