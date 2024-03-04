﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using ScheduleManager.Models;

namespace ScheduleManager.Pages.Subject
{
    public class CreateModel : PageModel
    {
        private readonly ScheduleManager.Models.ScheduleManagerContext _context;

        public CreateModel(ScheduleManager.Models.ScheduleManagerContext context)
        {
            _context = context;
        }

        public IActionResult OnGet()
        {
            return Page();
        }

        [BindProperty]
        public Models.Subject Subject { get; set; } = default!;
        

        // To protect from overposting attacks, see https://aka.ms/RazorPagesCRUD
        public async Task<IActionResult> OnPostAsync()
        {
          if (!ModelState.IsValid || _context.Subjects == null || Subject == null)
            {
                return Page();
            }

            _context.Subjects.Add(Subject);
            await _context.SaveChangesAsync();

            return RedirectToPage("./Index");
        }
    }
}
