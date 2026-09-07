using Microsoft.AspNetCore.Mvc.Filters;
using HotelListing.Api.Core.IServices;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;

namespace HotelListing.Api.Filters;
public class IdempotencyFilter : IAsyncActionFilter
{
    private readonly IIdempotencyService _idempotencyService;

    public IdempotencyFilter(IIdempotencyService idempotencyService)
    {
        _idempotencyService = idempotencyService;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        if (context.HttpContext.Request.Method != HttpMethod.Post.Method)
        {
            await next();
            return;
        }
        var idempotencyKey = context.HttpContext.Request.Headers["Idempotency-Key"].FirstOrDefault();
        if (string.IsNullOrEmpty(idempotencyKey))
        {
            await next();
            return;
        }
        var skipIdempotency = context.ActionDescriptor.EndpointMetadata
            .OfType<SkipIdempotencyAttribute>()
            .Any();
        if (skipIdempotency)
        {
            await next();
            return;
        }

        var cachedResponse = await _idempotencyService.GetCachedResponseAsync(idempotencyKey);
        if (cachedResponse != null)
        {
            context.Result = new OkObjectResult(JsonSerializer.Deserialize<object>(cachedResponse));
            return;
        }

        var executedContext = await next();

        if (executedContext.Result is ObjectResult {StatusCode: >= 200 and < 300} result)
        {
            var serialized = JsonSerializer.Serialize(result.Value);
            await _idempotencyService.StoreResponseAsync(
                idempotencyKey,
                context.HttpContext.Request.Path,
                serialized
            );
        }

    }

}