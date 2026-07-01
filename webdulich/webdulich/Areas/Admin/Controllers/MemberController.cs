using Microsoft.AspNetCore.Mvc;
using Web.Models.EF;
using webdulich.Areas.Admin.Models;
using System.Linq.Dynamic.Core;
using Microsoft.EntityFrameworkCore;
namespace webdulich.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MemberController : Controller
    {
        private readonly FoodContext _dbContext;

        public MemberController(FoodContext dbContext)
        {
            _dbContext = dbContext;
        }
        [HttpPost]
        public async Task<IActionResult> getList(jDatatable model)
        {
            var items = (from i in _dbContext.Members select i);

            int recordsTotal = 0;

            if (!string.IsNullOrEmpty(model.columns[model.order[0].column].name) && !string.IsNullOrEmpty(model.order[0].dir))
            {
                items = items.OrderBy(model.columns[model.order[0].column].name + " " + model.order[0].dir);
            }

            if (!string.IsNullOrEmpty(model.search.value))
            {
                items = items.Where(i => i.Name.Contains(model.search.value));
            }

            recordsTotal = items.Count();

            var data = await items.Select(i => new
            {
                i.Id,
                i.Name,
                groupName = i.Group.Name,
                i.LastLogin,
                i.Picture,
                
            }).Skip(model.start).Take(model.length).ToListAsync();

            var jsonData = new
            {
                draw = model.draw,
                recordsFiltered = recordsTotal,
                recordsTotal = recordsTotal,
                data = data
            };

            return Ok(jsonData);
        }

        public IActionResult Index()
        {
            return View();
        }
    }
}
