﻿using IzunaDrop.Services.Interface;
using Microsoft.AspNetCore.Mvc;

namespace IzunaDrop.Controllers
{
    public class ItemController : Controller
    {
        private readonly IItemService _itemService;
        private readonly ILogger<ItemController> _logger;

        public ItemController(IItemService itemService, ILogger<ItemController> logger)
        {
            _itemService = itemService;
            _logger = logger;
        }
        public async Task<IActionResult> Index(int gameId)
        {
            var items = await _itemService.GetAllItemsAsync(gameId);
            return View(items);
        }
        public async Task<IActionResult> Details(int gameId,int itemId)
        {
            var item = await _itemService.GetItemByIdAsync(gameId, itemId);
            if (item == null)
            {
                return NotFound();
            }
            return View(item);
        }
    }
}
