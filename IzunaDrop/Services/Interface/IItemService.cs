using IzunaDrop.Data.Models;

namespace IzunaDrop.Services.Interface
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync(int gameId);
        Task<Item> GetItemByIdAsync(int gameId, int itemId);
        Task<Item> CreateItemAsync(Item item);

        Task<bool> UpdateItemAsync(Item item);

        Task<bool> DeleteItemAsync(int itemId);
    }
}
