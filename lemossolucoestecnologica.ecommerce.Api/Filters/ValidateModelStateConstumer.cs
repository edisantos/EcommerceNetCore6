using lemossolucoestecnologica.ecommerce.Api.ErrorsValidation;
using Microsoft.AspNetCore.Mvc.Filters;

namespace lemossolucoestecnologica.ecommerce.Api.Filters
{
    public class ValidateModelStateConstumer:ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var validate = new ValidateFieldViewModel(context.ModelState.SelectMany(sm =>sm.Value.Errors).Select(m => m.ErrorMessage));
            }
           // base.OnActionExecuted(context);
        }
    }
}
