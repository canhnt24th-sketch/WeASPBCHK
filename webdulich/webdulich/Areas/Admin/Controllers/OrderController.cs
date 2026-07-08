using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Web.Models.EF;
using webdulich.Areas.Admin.Models;
using System.Linq.Dynamic.Core;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class OrderController : Controller
    {
        private readonly FoodContext _dbContext;

        public OrderController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return View();
        }
        public IActionResult Details(Guid id)
        {
            var order = _dbContext.Orders.Find(id);

            ViewBag.Customer = _dbContext.Customers.Find(order.CustomerId);

            return View(order);
        }
        [HttpPost]
        public async Task<IActionResult> getList(jDatatable model)
        {
            var items = (from i in _dbContext.Orders select i);

            int recordsTotal = 0;

            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name)
                && !string.IsNullOrEmpty(model.order[0].dir))
            {
                items = items.OrderBy(model.columns[model.order[0].column].name + " " + model.order[0].dir);
            }

            if (!string.IsNullOrEmpty(model.search.value))
            {
                items = items.Where(i => i.Customer.Name.Contains(model.search.value));
            }

            recordsTotal = items.Count();

            var data = await items.Select(i => new
            {
                i.Id,
                customerName = i.Customer.Name,
                i.CreatedOn,
                i.UpdatedOn
            })
            .Skip(model.start)
            .Take(model.length)
            .ToListAsync();

            var jsonData = new
            {
                draw = model.draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data
            };

            return Ok(jsonData);
        }
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var item = await _dbContext.Orders.FindAsync(id);

            if (item.UpdatedOn != null)
                return Ok(false);

            var details = await _dbContext.Details
                .Where(i => i.OrderId == id)
                .ToListAsync();

            _dbContext.Details.RemoveRange(details);

            _dbContext.Entry(item).State = EntityState.Deleted;

            await _dbContext.SaveChangesAsync();

            return Ok(true);
        }
    }
}