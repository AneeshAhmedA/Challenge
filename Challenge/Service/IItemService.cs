using Challenge.DTO;
using Challenge.Entity;
using Challenge.Migrations;
using System;
using System.Collections.Generic;

namespace Challenge.Services
{
    public interface IItemService
    {
        List<Item> GetAllItems();
        Item GetItem(int ItemId);
        void AddItem(Item item);
        void UpdateItem(int itemId, Item updatedItem);
        void DeleteItem(int itemId);
        List<Item> GetItemsByName(string itemName);
    }
}