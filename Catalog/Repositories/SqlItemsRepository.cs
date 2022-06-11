using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class SqlItemsRepository : IItemsRepository
    {
        private readonly ItemContext _context;

        public SqlItemsRepository(ItemContext context)
        {
            _context = context;
        }
        public async Task CreateItemAsync(Item item)
        {
            _context.Items.Add(item);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteItemAsync(Guid id)
        {
            _context.Items.Remove(_context.Items.Where(i => i.Id == id).SingleOrDefault());
            await _context.SaveChangesAsync();
        }

        public async Task<Item> GetItemAsync(Guid id)
        {
            var result = _context.Items.Where(i => i.Id == id).SingleOrDefault();
            return await Task.FromResult(result);
        }

        public async Task<IEnumerable<Item>> GetItemsAsync()
        {
            var result = _context.Items;
            return await Task.FromResult(result);
            
        }

        public async Task UpdateItemAsync(Item item)
        {
            var existingItem = _context.Items.FirstOrDefault(i => i.Id == item.Id);
            if(existingItem != null)
                {
                    existingItem.Name = item.Name;
                    existingItem.Price = item.Price;
                }
            await _context.SaveChangesAsync();
        }
    }
}