using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using ScheduleManager.Models;

namespace ScheduleManager.Pages.Subject
{
    public class IndexModel : PageModel
    {
        private readonly ScheduleManager.Models.ScheduleManagerContext _context;

        public IndexModel(ScheduleManager.Models.ScheduleManagerContext context)
        {
            _context = context;
        }

        public IList<Models.Subject> Subject { get;set; } = default!;

        public async Task OnGetAsync()
        {
            if (_context.Subjects != null)
            {
                Subject = await _context.Subjects.ToListAsync();
            }
        }
    }
}
