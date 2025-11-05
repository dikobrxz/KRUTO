using NUnit.Framework;
using System;
using System.Collections.Generic;

namespace InventorySystem.Tests
{
    public class InventoryTests
    {
        [Test]
        public void AddItem_IncreasesCount()
        {
            var inv = new Inventory();
            var item = new Item(Guid.NewGuid(), "Sword");

            inv.AddItem(item);

            Assert.AreEqual(1, inv.Count);
        }

        [Test]
        public void AddItem_Null_ThrowsArgumentNullException()
        {
            var inv = new Inventory();
            Assert.Throws<ArgumentNullException>(() => inv.AddItem(null));
        }

        [Test]
        public void AddItem_DuplicateId_ThrowsException()
        {
            var inv = new Inventory();
            var id = Guid.NewGuid();
            inv.AddItem(new Item(id, "Sword"));

            Assert.Throws<InvalidOperationException>(() => inv.AddItem(new Item(id, "Shield")));
        }

        [Test]
        public void RemoveItem_DecreasesCount()
        {
            var inv = new Inventory();
            var item = new Item(Guid.NewGuid(), "Sword");
            inv.AddItem(item);

            inv.RemoveItem(item.Id);

            Assert.AreEqual(0, inv.Count);
        }

        [Test]
        public void RemoveItem_Nonexistent_DoesNotThrow()
        {
            var inv = new Inventory();
            inv.RemoveItem(Guid.NewGuid());
        }

        [Test]
        public void ContainsItem_ReturnsCorrectValue()
        {
            var inv = new Inventory();
            var item = new Item(Guid.NewGuid(), "Sword");
            inv.AddItem(item);

            Assert.IsTrue(inv.ContainsItem(item.Id));
            Assert.IsFalse(inv.ContainsItem(Guid.NewGuid()));
        }

        [Test]
        public void GetAllItems_ReturnsReadOnlyCollection()
        {
            var inv = new Inventory();
            var item = new Item(Guid.NewGuid(), "Sword");
            inv.AddItem(item);

            var items = inv.GetAllItems();

            Assert.AreEqual(1, items.Count);
            Assert.AreEqual(item, items[0]);

            var asList = items as IList<Item>;
            Assert.IsNotNull(asList);
            Assert.IsTrue(asList.IsReadOnly);
            Assert.Throws<NotSupportedException>(() => asList.Add(new Item(Guid.NewGuid(), "Bow")));
        }

        [Test]
        public void GetAllItems_ReturnsDifferentReferenceEachCall()
        {
            var inv = new Inventory();
            inv.AddItem(new Item(Guid.NewGuid(), "Sword"));

            var items1 = inv.GetAllItems();
            var items2 = inv.GetAllItems();

            Assert.AreNotSame(items1, items2);
        }
    }
}
