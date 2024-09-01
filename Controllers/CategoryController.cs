using crud.Data;
using crud.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace crud.Controllers{
    public class CategoryController: Controller
    {
        private readonly ApplicationDbContext _context;
        public CategoryController(ApplicationDbContext context){
            _context= context;
        }
        
        //GET categories
        public async Task<IActionResult> Index()
        {
            var _Category= await _context.Category.ToListAsync();
            if(_Category.Count<1)
            {
                await CreateTestData();
            }
            return _context.Category !=null ? 
            View(await _context.Category.ToListAsync()):
            Problem("Entity set 'ApplicationDbContext.Catrgory' is null");
        }

        //Delete 
        public async Task<IActionResult> Delete(long? id)
        {
            if(id== null)
            {
            return NotFound();
            }
            var category =await _context.Category.FirstOrDefaultAsync(m=>m.Id==id);
            if(category==null){
                return NotFound();
            }
            return View(category);
        }
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(long id)
        {
            var category= await _context.Category.FindAsync(id);
            if(category!=null)
            {
                _context.Category.Remove(category);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
  public async Task<IActionResult> Edit(long? id)
{
    if (id == null || _context.Category == null)
    {
        return NotFound();
    }

    var category = await _context.Category.FindAsync(id);
    if (category == null)
    {
        return NotFound();
    }
    return View(category);
}

[HttpPost]
[ValidateAntiForgeryToken]
public async Task<IActionResult> Edit(long id, [Bind("Id,Name,Description")] Category category)
{
    if (id != category.Id)
    {
        return NotFound();
    }

    if (ModelState.IsValid)
    {
        try
        {
            _context.Update(category);
            await _context.SaveChangesAsync();
        }
        catch (DbUpdateConcurrencyException)
        {
            if (!CategoryExists(category.Id))
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
    return View(category);
}

        public IActionResult Create(){
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,Name,Description")] Category category)

        {
            if(ModelState.IsValid)
            {
                _context.Add(category);
               await _context.SaveChangesAsync();
               return RedirectToAction(nameof(Index));

            }
            return View(category);
        }

          public async Task<IActionResult> Details(long? id)
        {
            if (id == null || _context.Category == null)
            {
                return NotFound();
            }

            var category = await _context.Category
                .FirstOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }
          private bool CategoryExists(long id)
        {
          return (_context.Category?.Any(e => e.Id == id)).GetValueOrDefault();
        }

        private async Task CreateTestData() 
        {
            foreach (var item in GetCategoryList())
            {
                _context.Category.Add(item);
                await _context.SaveChangesAsync();
                
            }
        }

        private IEnumerable<Category> GetCategoryList() 
        {
            return new List<Category>{};

        }

    }
}