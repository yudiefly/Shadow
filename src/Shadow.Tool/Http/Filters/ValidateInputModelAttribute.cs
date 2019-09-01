using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Linq;

namespace Shadow.Tool.Http.Filters
{
    /// <summary>
    /// 验证输入的数据模型是否正确
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class ValidateInputModelAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext context)
        {
            if (!context.ModelState.IsValid)
            {
                var errors = context.ModelState.SelectMany(m => m.Value.Errors.Select(e => e.ErrorMessage));
                context.Result = new JsonResult(new { code = 401, message = "Invalid arguments. " + string.Join("", errors) });
            }
        }
    }
}
