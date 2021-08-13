using System;
using System.Diagnostics;
using Microsoft.AspNetCore.Mvc.Filters;

namespace Timesheets.Filters
{
    public class MyActionFilter : Attribute, IResourceFilter
    {

        public void OnResourceExecuted(ResourceExecutedContext context)
        {

        }

        public void OnResourceExecuting(ResourceExecutingContext context)
        {
             context.HttpContext.Response.Cookies.Append("LastVisit1", DateTime.Now.ToString("dd/MM/yyyy hh-mm-ss"));
        }
    }
}
