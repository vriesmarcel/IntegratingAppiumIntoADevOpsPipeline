using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CarvedRock.Models;

namespace CarvedRock.Services
{
    public class MockDataStore : IDataStore<Item>
    {
        static List<Item> items;

        public MockDataStore()
        {
            if (items == null)
            {
                items = new List<Item>()
                {
                    new Item { Id = Guid.NewGuid().ToString(), Text = "First item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Second item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Third item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Fourth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Fifth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Sixth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Seventh item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Eighth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Nineth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Tenth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Eleventh item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Twelfth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Thirteenth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Fourteenth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Fifteenth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Sixteenth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Seventeenth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Eightteenth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Twentieth item", Description="This is an item description." },
                    new Item { Id = Guid.NewGuid().ToString(), Text = "Twenty first item", Description="This is an item description." }
                };
            }
        }

        public async Task<bool> AddItemAsync(Item item)
        {
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> UpdateItemAsync(Item item)
        {
            var oldItem = items.Where((Item arg) => arg.Id == item.Id).FirstOrDefault();
            items.Remove(oldItem);
            items.Add(item);

            return await Task.FromResult(true);
        }

        public async Task<bool> DeleteItemAsync(string id)
        {
            var oldItem = items.Where((Item arg) => arg.Id == id).FirstOrDefault();
            items.Remove(oldItem);

            return await Task.FromResult(true);
        }

        public async Task<Item> GetItemAsync(string id)
        {
            return await Task.FromResult(items.FirstOrDefault(s => s.Id == id));
        }

        public async Task<IEnumerable<Item>> GetItemsAsync(bool forceRefresh = false)
        {
            return await Task.FromResult(items);
        }
    }
}