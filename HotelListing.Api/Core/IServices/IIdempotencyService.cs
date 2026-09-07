namespace HotelListing.Api.Core.IServices
{
    public interface IIdempotencyService
    {
        Task<string?> GetCachedResponseAsync(string key);
        Task StoreResponseAsync(string key, string endpoint, string response);
    }
}