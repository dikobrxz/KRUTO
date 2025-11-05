using System;

namespace InventorySystem
{
    public class Item
    {
        public Guid Id { get; }
        public string Name { get; }

        public Item(Guid id, string name)
        {
            Id = id != Guid.Empty ? id : throw new ArgumentException("Id cannot be empty.");
            Name = name ?? throw new ArgumentNullException(nameof(name));
        }

        public override string ToString() => $"{Name} ({Id})";
    }
}