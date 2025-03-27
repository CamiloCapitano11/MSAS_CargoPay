using Microsoft.AspNetCore.Http;
using MSAS_CargoPay.Core.Interfaces;
using System.Linq;
using System.Threading.Tasks;

namespace MSAS_CargoPay.API.ExceptionMiddleware
{
    public class JwtMiddleware
    {
        private readonly RequestDelegate _next;
        public JwtMiddleware(RequestDelegate next)
        {
            _next = next;
        }
        public async Task Invoke(HttpContext context, IJwtUtils jwtUtils)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            var userId = await jwtUtils.ValidateJwtTokenAsync(token);
            if (userId != null)
            {
                await _next(context);
            }
        }
    }
}
