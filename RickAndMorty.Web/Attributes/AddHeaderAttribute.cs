using Microsoft.AspNetCore.Mvc.Filters;

namespace RickAndMorty.Web.Attributes;
public class AddHeaderAttribute(string name, string value) : ActionFilterAttribute
{
    private readonly string _name = name;
    private readonly string _value = value;

    public override void OnResultExecuting(ResultExecutingContext filterContext)
    {
        base.OnResultExecuting(filterContext);
        filterContext.HttpContext.Response.Headers[_name] = _value;
    }
}