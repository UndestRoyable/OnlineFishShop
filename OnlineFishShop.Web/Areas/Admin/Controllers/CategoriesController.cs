using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using OnlineFishShop.Data.Models;
using OnlineFishShop.Services.Contracts;
using OnlineFishShop.Web.Areas.Admin.Models;
using OnlineFishShop.Web.Infrastructure.Constants;

namespace OnlineFishShop.Web.Areas.Admin.Controllers
{
    public class CategoriesController : AdminBaseController
    {
        private readonly IGenericDataService<Category> categories;

        public CategoriesController(IGenericDataService<Category> categories)
        {
            this.categories = categories;
        }

        // GET: Categories
        public async Task<IActionResult> Index()
        {
            return View(await this.categories.GetAllAsync());
        }

        // GET: Categories/Details/5
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await this.categories.GetSingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // GET: Categories/Create
        public IActionResult Create()
        {
            return View();
        }

        // POST: Categories/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        public async Task<IActionResult> Create(EditCategoryModel category)
        {
            var dbCategory = await this.categories.GetSingleOrDefaultAsync(x => x.Name == category.Name);

            if (dbCategory != null)
            {
                Danger(WebConstants.SuchCategoryExists);
                return RedirectToAction(nameof(Create));
            }

            if (this.ModelState.IsValid)
            {
                var categoryToAdd = new Category()
                {
                    Name = category.Name
                };

                this.categories.Add(categoryToAdd);
                Success(string.Format(WebConstants.CategoryCreated, category.Name));
                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Edit/5
        public async Task<IActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await this.categories.GetSingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Edit(int id, EditCategoryModel category)
        {
            var dbCategory = await this.categories.GetSingleOrDefaultAsync(x => x.Name == category.Name);

            if (dbCategory == null)
            {
                return NotFound();
            }

            if (this.ModelState.IsValid)
            {
                dbCategory.Name = category.Name;

                this.categories.Update(dbCategory);

                return RedirectToAction(nameof(Index));
            }
            return View(category);
        }

        // GET: Categories/Delete/5
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var category = await this.categories.GetSingleOrDefaultAsync(m => m.Id == id);
            if (category == null)
            {
                return NotFound();
            }

            return View(category);
        }

        // POST: Categories/Delete/5
        [HttpPost, ActionName(nameof(Delete))]
        [Authorize(Roles = "Admin, Manager")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var category = await this.categories.GetSingleOrDefaultAsync(m => m.Id == id);
            this.categories.Remove(category);

            return RedirectToAction(nameof(Index));
        }

        private async Task<bool> CategoryExists(int id)
        {
            return await categories.AnyAsync(e => e.Id == id);
        }
    }
}
