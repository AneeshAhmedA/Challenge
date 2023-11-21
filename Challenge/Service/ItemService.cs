using Challenge.Database;
using Challenge.DTO;
using Challenge.Entity;
using Challenge.Migrations;

namespace Challenge.Services
{
    public class ItemService : IItemService
    {
        private readonly MyDbContext _context;

        public ItemService(MyDbContext context)
        {
            _context = context;
        }

        public List<Item> GetAllItems()
        {
            return _context.Items.ToList();
        }

        public Item GetItem(int itemId)
        {
            return _context.Items.Find(itemId);
        }

        public void AddItem(Item item)
        {
            _context.Items.Add(item);
            _context.SaveChanges();
        }

        public void UpdateItem(int itemId, Item updatedItem)
        {
            var existingItem = _context.Items.Find(itemId);

            if (existingItem != null)
            {
                existingItem.ItemDescription = updatedItem.ItemDescription;

                _context.Items.Update(existingItem);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Item not found");
            }
        }

        public void DeleteItem(int itemId)
        {
            var itemToRemove = _context.Items.Find(itemId);

            if (itemToRemove != null)
            {
                _context.Items.Remove(itemToRemove);
                _context.SaveChanges();
            }
            else
            {
                throw new InvalidOperationException("Item not found");
            }
        }

        public List<Item> GetItemsByName(string itemName)
        {
            return _context.Items.Where(item => item.ItemDescription.Contains(itemName)).ToList();
        }

    }
}
