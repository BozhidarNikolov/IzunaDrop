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

        public async Task<Item> CreateItemAsync(Item item)
        {
             _context.Items.Add(item);
            await _context.SaveChangesAsync();
            return item;
        }

        public async Task<bool> DeleteItemAsync(int itemId)
        {
            var item = await _context.Items.FindAsync(itemId);
            if (item == null)
            {
                return false;
            }
            item.IsDeleted = true;
            await _context.SaveChangesAsync();
            return true;
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

        public async Task<bool> UpdateItemAsync(Item updatedItem)
        {
            var existingItem = await _context.Items.FindAsync(updatedItem.Id);
            if (existingItem == null)
            {
                return false;
            }
            existingItem.Name = updatedItem.Name;
            existingItem.Description = updatedItem.Description;
            existingItem.GameId = updatedItem.GameId;
            existingItem.ImagePath = updatedItem.ImagePath;
            _context.Items.Update(existingItem);
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
