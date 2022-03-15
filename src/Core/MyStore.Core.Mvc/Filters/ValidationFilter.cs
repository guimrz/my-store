using Microsoft.AspNetCore.Mvc.Filters;
using MyStore.Core.Mvc.Exceptions;

#nullable disable
namespace MyStore.Core.Mvc.Filters
{
    public class ValidationFilter : IAsyncActionFilter
    {
        public async Task OnActionExecutionAsync(ActionExecutingContext context, ActionExecutionDelegate next)
        {
            if (!context.ModelState.IsValid)
            {
                var errorsInModelState = context.ModelState
                    .Where(x => x.Value.Errors.Count > 0)
                    .ToDictionary(kvp => kvp.Key, kvp => kvp.Value.Errors.Select(x => x.ErrorMessage));

                throw new ValidationException(errorsInModelState);
            }

            await next();
        }
    }
}
