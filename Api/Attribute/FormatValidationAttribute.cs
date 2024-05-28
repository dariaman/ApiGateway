using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Api.Attribute
{
    public class FormatValidationAttribute : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errorState = context.ModelState.ToList();

                Dictionary<string, List<string>> ModelErrorList = [];
                List<string> MessageErrorList = [];

                foreach (var modelState in errorState)
                {
                    if (string.IsNullOrEmpty(modelState.Key))
                        MessageErrorList.Add("Can Not Read Any Input Parameter");
                    else
                        ModelErrorList.Add(modelState.Key, modelState.Value.Errors.Select(x => x.ErrorMessage).ToList());
                }

                var responseObj = new
                {
                    ErrorMessages = MessageErrorList,
                    ModelStateErrors = ModelErrorList
                };

                context.Result = new JsonResult(responseObj)
                {
                    StatusCode = 400
                };
            }

        }
    }
}
