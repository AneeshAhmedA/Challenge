using AutoMapper;
using Challenge.DTO;
using Challenge.Entity;
using Challenge.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace Challenge.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ItemController : ControllerBase
    {
        private readonly IItemService _itemService;
        private readonly IMapper _mapper;

        public ItemController(IItemService itemService, IMapper mapper)
        {
            _itemService = itemService;
            _mapper = mapper;
        }

        [HttpGet]
        [Route("GetAllItems")]
        [Authorize(Roles = "Supplier")]
        public IActionResult GetAllItems()
        {
            try
            {
                List<Item> items = _itemService.GetAllItems();
                List<ItemDTO> itemDTOs = _mapper.Map<List<ItemDTO>>(items);
                return StatusCode(200, itemDTOs);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet]
        [Route("GetItem/{itemId}")]
        [Authorize(Roles = "Supplier")]
        public IActionResult GetItem(int itemId)
        {
            try
            {
                Item item = _itemService.GetItem(itemId);
                if (item != null)
                {
                    ItemDTO itemDTO = _mapper.Map<ItemDTO>(item);
                    return StatusCode(200, itemDTO);
                }
                else
                {
                    return NotFound();
                }
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPost]
        [Route("AddItem")]
        [Authorize(Roles = "Supplier")]
        public IActionResult AddItem([FromBody] ItemDTO itemDTO)
        {
            try
            {
                Item item = _mapper.Map<Item>(itemDTO);
                _itemService.AddItem(item);
                return StatusCode(200, item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpPut]
        [Route("UpdateItem/{itemId}")]
        [Authorize(Roles = "Supplier")]
        public IActionResult UpdateItem(int itemId, [FromBody] ItemDTO updatedItemDTO)
        {
            try
            {
                Item updatedItem = _mapper.Map<Item>(updatedItemDTO);
                _itemService.UpdateItem(itemId, updatedItem);
                return StatusCode(200, updatedItem);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete]
        [Route("DeleteItem/{itemId}")]
        [Authorize(Roles = "Admin")]
        public IActionResult DeleteItem(int itemId)
        {
            try
            {
                _itemService.DeleteItem(itemId);
                return StatusCode(200, new JsonResult($"Item with Id {itemId} is Deleted"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }
    }
}
