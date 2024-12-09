using IzunaDrop.Data;
using IzunaDrop.Data.Models;
using IzunaDrop.Services.Interface;
using Microsoft.EntityFrameworkCore;

namespace IzunaDrop.Services
{
    public class ItemService : IItemService
    {
        private readonly IzunaDropDbContext _context;

        public ItemService(IzunaDropDbContext context)
        {
            _context = context;
        }
        public async Task<IEnumerable<Item>> GetAllItemsAsync(int gameId)
        {
            return await _context.Items
                .Where(i => i.GameId == gameId)
                .ToListAsync();
        }

        public async Task<Item> GetItemByIdAsync(int gameId, int itemId)
        {
            return await _context.Items
                .Where(i => i.GameId == gameId && i.Id == itemId)
                .FirstOrDefaultAsync();
        }
    }
}
