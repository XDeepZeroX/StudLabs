using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudLab.Filters
{
    public class CustomActionFilter : Attribute, IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
        }

        public void OnActionExecuting(ActionExecutingContext context)
        {
            //Добавляет в ViewData название контроллера
            var controller = context.Controller as Controller;
            if (controller == null)
                return;
            controller.ViewData["controller"] = controller.ControllerContext.RouteData.Values["controller"];

            //Не работает(((((
            //var nameController = controller.ControllerContext.RouteData.Values["controller"];
            //context.HttpContext.Response.Cookies.Append("Controller", nameController.ToString());
        }
    }
}
