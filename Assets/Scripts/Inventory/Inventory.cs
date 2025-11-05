using System;
using System.Collections.Generic;
using System.Linq;

namespace InventorySystem
{
    public class Inventory
    {
        private readonly List<Item> _items = new List<Item>();

        public void AddItem(Item item)
        {
            if (item == null)
                throw new ArgumentNullException(nameof(item));

            if (_items.Any(i => i.Id == item.Id))
                throw new InvalidOperationException($"Item with Id {item.Id} already exists.");

            _items.Add(item);
        }

        public void RemoveItem(Guid id)
        {
            var existing = _items.FirstOrDefault(i => i.Id == id);
            if (existing != null)
                _items.Remove(existing);
            else return;
        }

        public bool ContainsItem(Guid id)
        {
            return _items.Any(i => i.Id == id);
        }

        public IReadOnlyList<Item> GetAllItems()
        {
            return _items.ToList().AsReadOnly();
        }

        public int Count => _items.Count;
    }
}
