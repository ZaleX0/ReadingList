using Microsoft.AspNetCore.Http;
using ReadingList.Services.Exceptions;

namespace ReadingList.Services.Middleware;

public class ErrorHandlingMiddleware : IMiddleware
{
    public async Task InvokeAsync(HttpContext context, RequestDelegate next)
    {
		try
		{
			await next.Invoke(context);
		}
		catch (NotFoundException notFoundException)
		{
			context.Response.StatusCode = 404;
			await context.Response.WriteAsync(notFoundException.Message);
		}
		catch (Exception exception)
		{
			Console.WriteLine(exception.Message);

			context.Response.StatusCode = 500;
			await context.Response.WriteAsync("Something went wrong");
		}
    }
}
