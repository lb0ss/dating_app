using System;
using System.Security.Claims;
using System.Threading.Tasks;
using dating_app.api.Data;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.Extensions.DependencyInjection;

namespace dating_app.api.Helpers
{
    public class LogUserActivity : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            var resultContext = await next();

            var userId = int.Parse(resultContext.HttpContext.User
            .FindFirst(ClaimTypes.NameIdentifier).Value);   // retrieve the userId from the token

            var repo = resultContext.HttpContext.RequestServices.GetService<IDatingRepository>();   // get an instance of the IDatingRepository

            var user = await repo.GetUser(userId);
            user.LastActive = DateTime.Now;
            await repo.SaveAll();
        }
    }
}