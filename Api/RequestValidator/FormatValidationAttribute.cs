using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System.Collections.Generic;
using System.Text.Json;

namespace Api.RequestValidator
{
    public class FormatValidationAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorState = context.ModelState
                        .ToList();

                Dictionary<string, List<string>> errorMessage = [];

                foreach (var modelState in errorState)
                {
                    errorMessage.Add(modelState.Key, modelState.Value.Errors.Select(x => x.ErrorMessage).ToList());
                }

                var responseObj = new
                {
                    Code = 123456,
                    Message = "test message",
                    Errors = errorMessage
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }
        }
    }
}
