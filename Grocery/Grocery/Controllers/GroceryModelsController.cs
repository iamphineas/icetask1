using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Grocery.Data;
using Grocery.Models;

namespace Grocery.Controllers
{
    public class GroceryModelsController : Controller
    {
        private readonly GroceryContext _context;

        public GroceryModelsController(GroceryContext context)
        {
            _context = context;
        }

        // GET: GroceryModels
        public async Task<IActionResult> Index()
        {
            return View(await _context.GroceryModel.ToListAsync());
        }
        public async Task<IActionResult> Search(string SearchString)
        {
            return View("Index", await _context.GroceryModel.Where(g => g.FoodName!.Contains(SearchString)).ToListAsync());
        }

        // GET: GroceryModels/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryModel = await _context.GroceryModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groceryModel == null)
            {
                return NotFound();
            }

            return View(groceryModel);
        }

        // GET: GroceryModels/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: GroceryModels/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,FoodName,FoodType,Price")] GroceryModel groceryModel)
        {
            if (ModelState.IsValid)
            {
                _context.Add(groceryModel);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(groceryModel);
        }

        // GET: GroceryModels/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryModel = await _context.GroceryModel.FindAsync(id);
            if (groceryModel == null)
            {
                return NotFound();
            }
            return View(groceryModel);
        }

        // POST: GroceryModels/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,FoodName,FoodType,Price")] GroceryModel groceryModel)
        {
            if (id != groceryModel.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(groceryModel);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!GroceryModelExists(groceryModel.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            return View(groceryModel);
        }

        // GET: GroceryModels/Delete/5
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var groceryModel = await _context.GroceryModel
                .FirstOrDefaultAsync(m => m.Id == id);
            if (groceryModel == null)
            {
                return NotFound();
            }

            return View(groceryModel);
        }

        // POST: GroceryModels/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var groceryModel = await _context.GroceryModel.FindAsync(id);
            if (groceryModel != null)
            {
                _context.GroceryModel.Remove(groceryModel);
            }

            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool GroceryModelExists(int id)
        {
            return _context.GroceryModel.Any(e => e.Id == id);
        }
    }
}
