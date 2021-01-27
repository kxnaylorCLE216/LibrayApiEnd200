using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public static class ControllerExtensions
    {
        // C# Extension Methods - Generic
        public static ActionResult Maybe<T>(this ControllerBase _, T obj)
        {
            if(obj == null)
            {
                return new NotFoundResult();
            } else
            {
                return new OkObjectResult(obj);
            }
        }
    }
}
