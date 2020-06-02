using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RepconWeb.Pages
{
    public class ReportModel : PageModel
    {
        private readonly RepconContext _context;
        public List<Session> Sessions { get; set; }
        public Session SessionItem { get; set; }
        public async Task<IActionResult> OnGetAsync(string log, int crnum, int placenum, string proc, DateTime startT, DateTime endT)
        {
            //if (log == _context.Admn.Login)

            

            return Page();
        }
    }        
}