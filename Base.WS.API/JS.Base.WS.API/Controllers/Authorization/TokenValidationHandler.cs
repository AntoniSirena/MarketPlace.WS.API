using JS.Base.WS.API.Base;
using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Global;
using JS.Base.WS.API.Helpers;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Org.BouncyCastle.Asn1.Ocsp;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using System.Web;

namespace JS.Base.WS.API.Controllers.Authorization
{
    public class TokenValidationHandler : DelegatingHandler
    {

        private long currentUserId = 0;
        private string currentUserName = string.Empty;
        private DateTime lifeToken;
        private bool isVisitorUser = false;

        private static bool TryRetrieveToken(HttpRequestMessage request, out string token)
        {
            token = null;
            IEnumerable<string> authzHeaders;
            if (!request.Headers.TryGetValues("Authorization", out authzHeaders) || authzHeaders.Count() > 1)
            {
                return false;
            }
            var bearerToken = authzHeaders.ElementAt(0);
            token = bearerToken.StartsWith("Bearer ") ? bearerToken.Substring(7) : bearerToken;
            return true;
        }

        protected override Task<HttpResponseMessage> SendAsync(HttpRequestMessage request, CancellationToken cancellationToken)
        {
            HttpStatusCode statusCode;
            string token;

            // determine whether a jwt exists or not
            if (!TryRetrieveToken(request, out token))
            {
                statusCode = HttpStatusCode.Unauthorized;
                return base.SendAsync(request, cancellationToken);
            }

            try
            {
                var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
                var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
                var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
                var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
                int refressTime = Convert.ToInt16(ConfigurationManager.AppSettings["REFRESS_TIME"]);

                SecurityToken securityToken;
                var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
                TokenValidationParameters validationParameters = new TokenValidationParameters()
                {
                    ValidAudience = audienceToken,
                    ValidIssuer = issuerToken,
                    ValidateLifetime = true,
                    ValidateIssuerSigningKey = true,
                    LifetimeValidator = this.LifetimeValidator,
                    IssuerSigningKey = securityKey
                };

                //Delete Cache 
                CurrentUser.DeleteId();
                CurrentUser.DeleteName();
                CurrentUser.DeleteLifeToken();

                // Extract and assign Current Principal and user
                Thread.CurrentPrincipal = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                HttpContext.Current.User = tokenHandler.ValidateToken(token, validationParameters, out securityToken);
                
                string userName = Thread.CurrentPrincipal.Identity.Name;
                string[] userValue = userName.Split(',');

                currentUserName = userValue[0];
                currentUserId = Convert.ToInt64(userValue[1]);
                lifeToken = Convert.ToDateTime(userValue[2]);
                isVisitorUser = Convert.ToBoolean(userValue[3]);

                //Cache estorage by 5 minutes
                CacheStorage.Add("currentUserName", currentUserName, DateTimeOffset.UtcNow.AddMinutes(5));
                CacheStorage.Add("currentUserId", currentUserId, DateTimeOffset.UtcNow.AddMinutes(5));
                CacheStorage.Add("lifeToken", currentUserId, DateTimeOffset.UtcNow.AddMinutes(5));


                return base.SendAsync(request, cancellationToken);
            }
            catch (SecurityTokenValidationException ex)
            {
                statusCode = HttpStatusCode.Unauthorized;
            }
            catch (Exception ex)
            {
                statusCode = HttpStatusCode.InternalServerError;
            }

            var result = Task<HttpResponseMessage>.Factory.StartNew(() => new HttpResponseMessage(statusCode) { });


            return result;
        }


        public bool LifetimeValidator(DateTime? notBefore, DateTime? expires, SecurityToken securityToken, TokenValidationParameters validationParameters)
        {
            if (expires != null)
            {
                if (DateTime.UtcNow < expires) return true;
            }
            return false;
        }

    }
}