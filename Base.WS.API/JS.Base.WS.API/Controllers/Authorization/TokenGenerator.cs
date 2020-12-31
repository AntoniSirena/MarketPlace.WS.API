using JS.Base.WS.API.DBContext;
using JS.Base.WS.API.Global;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Claims;
using System.Web;

namespace JS.Base.WS.API.Controllers.Authorization
{
    internal static class TokenGenerator
    {
        /// <summary>
        /// JWT Token generator class using "secret-key"
        /// more info: https://self-issued.info/docs/draft-ietf-oauth-json-web-token.html
        /// </summary>
        /// 



        public static string GenerateTokenJwt(string username)
        {
            // appsetting for Token JWT
            var secretKey = ConfigurationManager.AppSettings["JWT_SECRET_KEY"];
            var audienceToken = ConfigurationManager.AppSettings["JWT_AUDIENCE_TOKEN"];
            var issuerToken = ConfigurationManager.AppSettings["JWT_ISSUER_TOKEN"];
            string expireTime = Constants.ConfigurationParameter.LoginTime;
            var expireTimeUserVisitador = ConfigurationManager.AppSettings["JWT_EXPIRE_MINUTES_USER_VISITADOR"];

            var securityKey = new SymmetricSecurityKey(System.Text.Encoding.Default.GetBytes(secretKey));
            var signingCredentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256Signature);

            string[] payLoad = username.Split(',');
            long currentUserId = Convert.ToInt64(payLoad[1]);
            bool isVisitorUser = Convert.ToBoolean(payLoad[3]);

            if (isVisitorUser)
            {
                expireTime = expireTimeUserVisitador;
            }

            // create a claimsIdentity
            ClaimsIdentity claimsIdentity = new ClaimsIdentity(new[] { new Claim(ClaimTypes.Name, username) });

            // create token to the user
            var tokenHandler = new System.IdentityModel.Tokens.Jwt.JwtSecurityTokenHandler();
            var jwtSecurityToken = tokenHandler.CreateJwtSecurityToken(
                audience: audienceToken,
                issuer: issuerToken,
                subject: claimsIdentity,
                notBefore: DateTime.UtcNow,
                expires: DateTime.UtcNow.AddMinutes(Convert.ToInt32(expireTime)),
                signingCredentials: signingCredentials);

            var jwtTokenString = tokenHandler.WriteToken(jwtSecurityToken);
            return jwtTokenString;
        }
    }
}