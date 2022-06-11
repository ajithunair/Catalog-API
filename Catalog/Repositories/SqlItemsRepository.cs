using System;
using System.Collections.Generic;
using System.Linq;
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
        public void CreateItem(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public void DeleteItem(Guid id)
        {
            _context.Items.Remove(_context.Items.Where(i => i.Id == id).SingleOrDefault());
            _context.SaveChanges();
        }

        public Item GetItem(Guid id)
        {
            return _context.Items.Where(i => i.Id == id).SingleOrDefault();
        }

        public IEnumerable<Item> GetItems()
        {
            return _context.Items;
        }

        public void UpdateItem(Item item)
        {
            var existingItem = _context.Items.FirstOrDefault(i => i.Id == item.Id);
            if(existingItem != null)
                {
                    existingItem.Name = item.Name;
                    existingItem.Price = item.Price;
                }
            _context.SaveChanges();
        }
    }
}