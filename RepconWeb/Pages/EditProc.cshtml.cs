using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RepconWeb.Pages.Shared
{
    public class EditProcModel : PageModel
    {
        private readonly RepconContext _context;
        public List<Process> Processes { get; set; }
        [BindProperty]
        public Process ProcessItem { get; set; }
        public EditProcModel(RepconContext db) => _context = db;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Processes = _context.Process.ToList();
            Processes.Reverse();
            ProcessItem = await _context.Process.FindAsync(id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            string mode = Request.Form["_method"];

            switch (mode)
            {
                case "post":
                default:
                    try
                    {
                        var lastID = _context.Process.Max<Process>(v => v.ProcId);
                        ProcessItem.ProcId = ++lastID;
                        _context.Process.Add(ProcessItem);
                    }
                    catch (DbUpdateConcurrencyException) { }
                    break;

                case "put":
                    try
                    {
                        if (int.TryParse(Request.Form["_id"], out int id))
                        {
                            ProcessItem.ProcId = id;
                            var has = _context.Process.Where(e => e.ProcId == id);
                            if (has != null)
                                _context.Process.Update(ProcessItem);
                            else
                                _context.Process.Add(ProcessItem);
                        }
                    }
                    catch (DbUpdateConcurrencyException) { }
                    break;
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("EditProc");
        }

        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _context.Process.FindAsync(id);
            if (item != null)
            {
                _context.Process.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }
    }
}