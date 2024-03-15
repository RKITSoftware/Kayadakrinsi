﻿using System;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
using System.Web.Http.Controllers;
using System.Web.Http.Filters;
using MusicCompany.BusinessLogic;

namespace MusicCompany.Auth
{
	/// <summary>
	/// Authenticates user who wants to get JWT token
	/// </summary>
	public class TokenAuthentication : ActionFilterAttribute
	{
		/// <summary>
		/// Authenticates user and creates response accordingly
		/// </summary>
		/// <param name="actionContext">Information about executing cintext</param>
		public override void OnActionExecuting(HttpActionContext actionContext)
		{
			// Authorization header
			var authHeader = actionContext.Request.Headers.Authorization;

			if (authHeader == null)
			{
				actionContext.Response = actionContext.Request.CreateErrorResponse(HttpStatusCode.Unauthorized,
										 "invalid headers");
				throw new Exception("invalid headers");
			}
			else if (authHeader.Scheme == "Basic")
			{
				string authToken = authHeader.Parameter;
				byte[] authBytes = Convert.FromBase64String(authToken);
				authToken = Encoding.UTF8.GetString(authBytes);
				string[] usernamePassword = authToken.Split(':');

				//Extracts username and password from the decoded token.
				string username = usernamePassword[0];
				string password = usernamePassword[1];

				var user = BLUser.Select().Any(u => u.R01F02 == username && u.R01F03 == password);

				if (user != null)
				{
					// Generates token
					var token = BLTokenManager.GenerateToken(username);

					// Attaches principal to token
					var principal = BLTokenManager.GetPrincipal(token);

					if (principal == null)
					{
						actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.Unauthorized,
												 "Invalid token");
						throw new Exception("Invalid token");
					}

					//Set the current principal for the request
					Thread.CurrentPrincipal = principal;

					actionContext.Response = actionContext.Request.CreateResponse(HttpStatusCode.OK, token);
				}
				base.OnActionExecuting(actionContext);
			}
		}
	}
}