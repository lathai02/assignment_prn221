using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Models;

namespace ScheduleManager.Pages.Class
{
    public class DetailsModel : PageModel
    {
        private readonly ScheduleManager.Models.ScheduleManagerContext _context;

        public DetailsModel(ScheduleManager.Models.ScheduleManagerContext context)
        {
            _context = context;
        }

      public GroupClass GroupClass { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.GroupClasses == null)
            {
                return NotFound();
            }

            var groupclass = await _context.GroupClasses.FirstOrDefaultAsync(m => m.Id == id);
            if (groupclass == null)
            {
                return NotFound();
            }
            else 
            {
                GroupClass = groupclass;
            }
            return Page();
        }
    }
}
