using HotelListing.Api.Models;
using Microsoft.AspNetCore.Mvc.Filters;
using HotelListing.Api.Data;


namespace HotelListing.Api.Filters;

public class TransactionFilter : IAsyncActionFilter
{
    private readonly HotelListingDbContext _context;

    public TransactionFilter(HotelListingDbContext context)
    {
        _context = context;
    }

    public async Task OnActionExecutionAsync(
        ActionExecutingContext context,
        ActionExecutionDelegate next)
    {
        var method = context.HttpContext.Request.Method;
        if (method == HttpMethod.Get.Method)
        {
            await next();
            return;
        }

        using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            var executedContext = await next();
            if (executedContext.Exception == null)
            {
                await transaction.CommitAsync();
            }
            else
            {
                await transaction.RollbackAsync();
            }
        }
        catch (Exception)
        {
            await transaction.RollbackAsync();
            throw;
        }
    }

}