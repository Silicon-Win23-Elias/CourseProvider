using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace CourseProvider.Functions
{
    public class GraphQL(ILogger<GraphQL> logger, IGraphQLRequestExecutor graphQLRequestExecutor)
    {
        private readonly ILogger<GraphQL> _logger = logger;
        private readonly IGraphQLRequestExecutor _graphQLRequestExecutor = graphQLRequestExecutor;

        [Function("GraphQL")]
        public async Task<IActionResult> Run([HttpTrigger(AuthorizationLevel.Function, "post", Route = "graphql")] HttpRequest req)
        {
            if (!req.Headers.TryGetValue("Authorization", out var authHeader))
            {
                return new UnauthorizedResult();
            }


            var token = authHeader.FirstOrDefault()?.Split(" ").Last();
            if (string.IsNullOrEmpty(token))
            {
                return new UnauthorizedResult();
            }

            if (!ValidateToken(token, out ClaimsPrincipal claimsPrincipal))
            {
                return new UnauthorizedResult();
            }


            return await _graphQLRequestExecutor.ExecuteAsync(req);
        }

        private static bool ValidateToken(string token, out ClaimsPrincipal claimsPrincipal)
        {
            claimsPrincipal = null;
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("3e0f2db8-3ce3-4e22-b53a-7a609b4b7048");

            try
            {
                claimsPrincipal = tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = "TokenProvider",
                    ValidateAudience = true,
                    ValidAudience = "Silicon",
                    ValidateLifetime = true,
                }, out SecurityToken validatedToken);

                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}
