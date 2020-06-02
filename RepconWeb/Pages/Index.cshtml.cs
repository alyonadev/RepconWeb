using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RepconWeb.Pages
{
    public class IndexModel : PageModel
    {
        [BindProperty]
        public Session SessionItem { get; set; }
        public Classroom CrItem { get; set; }
        public Process ProcItem { get; set; }

        public IActionResult OnPost(string log, int crnum, int placenum, string proc, DateTime startT, DateTime endT)
        {
            return RedirectToPage("Report", new { log, crnum, placenum, proc, startT, endT });
        }

    }
}
