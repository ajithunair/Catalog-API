using System;
using System.Collections.Generic;
using System.Linq;
using Catalog.Entities;

namespace Catalog.Repositories
{
    public class InMemItemsRepository : IItemsRepository
    {
        public readonly List<Item> items = new()
        {
            new Item{Id=Guid.NewGuid(), Name="NoteBook", Price=5, CreatedDate=DateTimeOffset.UtcNow},
            new Item{Id=Guid.NewGuid(), Name="Pencil", Price=2, CreatedDate=DateTimeOffset.UtcNow},
            new Item{Id=Guid.NewGuid(), Name="Pen", Price=3, CreatedDate=DateTimeOffset.UtcNow}
        };

        public IEnumerable<Item> GetItems()
        {
            return items;
        }

        public Item GetItem(Guid id)
        {
            return items.Where(i => i.Id == id).SingleOrDefault();
        }
    }
}