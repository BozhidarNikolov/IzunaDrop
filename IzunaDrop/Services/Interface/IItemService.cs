using IzunaDrop.Data.Models;

namespace IzunaDrop.Services.Interface
{
    public interface IItemService
    {
        Task<IEnumerable<Item>> GetAllItemsAsync(int gameId);
        Task<Item> GetItemByIdAsync(int gameId, int itemId);
    }
}
