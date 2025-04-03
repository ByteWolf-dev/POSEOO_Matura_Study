namespace Base.Web.Controller;

using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.AspNetCore.Mvc;

public static class ControllerExtensions
{
    #region Url

    public static string GetCurrentUri(this ControllerBase controller)
    {
        if (controller.Request == null)
        {
            // unit test => no Request available
            return "dummy";
        }

        return controller.Request.GetCurrentUri();
    }

    public static string GetCurrentUri(this ControllerBase controller, string removeTrailing)
    {
        if (controller.Request == null)
        {
            // unit test => no Request available
            return "dummy";
        }

        var totalUri = controller.Request.GetCurrentUri();

        var filterIdx = totalUri.LastIndexOf('?');
        if (filterIdx > 0)
        {
            totalUri = totalUri.Substring(0, filterIdx - 1);
        }

        return totalUri.Substring(0, totalUri.Length - removeTrailing.Length);
    }

    #endregion

    #region Result

    public static async Task<ActionResult<T>> NotFoundOrOk<T>(this ControllerBase controller, T? obj)
    {
        if (obj == null)
        {
            await Task.CompletedTask; // avoid CS1998
            return controller.NotFound();
        }

        return controller.Ok(obj);
    }

    public static async Task<ActionResult<IEnumerable<T>>> NotFoundOrOk<T>(this ControllerBase controller, IList<T>? list)
    {
        if (list == null || list.Count == 0)
        {
            await Task.CompletedTask; // avoid CS1998
            return controller.NotFound();
        }

        return controller.Ok(list);
    }

    #endregion
}