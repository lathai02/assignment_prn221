using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Models;

namespace ScheduleManager.Pages.Slot
{
    public class DetailsModel : PageModel
    {
        private readonly ScheduleManager.Models.ScheduleManagerContext _context;

        public DetailsModel(ScheduleManager.Models.ScheduleManagerContext context)
        {
            _context = context;
        }

      public Models.Slot Slot { get; set; } = default!; 

        public async Task<IActionResult> OnGetAsync(int? id)
        {
            if (id == null || _context.Slots == null)
            {
                return NotFound();
            }

            var slot = await _context.Slots.FirstOrDefaultAsync(m => m.Id == id);
            if (slot == null)
            {
                return NotFound();
            }
            else 
            {
                Slot = slot;
            }
            return Page();
        }
    }
}
