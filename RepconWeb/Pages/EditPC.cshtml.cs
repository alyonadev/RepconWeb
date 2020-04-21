using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace RepconWeb.Pages.Shared
{
    public class EditPCModel : PageModel
    {
        private readonly RepconContext _context;
        public List<Pc> Pc { get; set; }

        [BindProperty]
        public Pc PcItem { get; set; }
        public EditPCModel(RepconContext db) => _context = db;
        public async Task<IActionResult> OnGetAsync(int? id)
        {
            Pc = _context.Pc.ToList();
            Pc.Reverse();
            PcItem = await _context.Pc.FindAsync(id);
            return Page();
        }
        public async Task<IActionResult> OnPostAsync()
        {
            if (!ModelState.IsValid) return Page();
            string mode = Request.Form["_method"];

            string classnum = Request.Form["_classnum"];
            string placenum = Request.Form["_placenum"];

            switch (mode)
            {
                case "post":
                default:
                    try
                    {
                        var lastCrID = _context.Classroom.Max<Classroom>(v => v.CrId);

                        var lastPCID = _context.Pc.Max<Pc>(v => v.PcId);
                        PcItem.PcId = ++lastPCID;

                        Classroom CrItem = new Classroom()
                        {
                            CrId = ++lastCrID,
                            ClassNum = Convert.ToInt32(classnum),
                            PlaceNum = Convert.ToInt32(placenum)
                        };
                                                                                                       
                        _context.Classroom.Add(CrItem);
                        PcItem.CrId = CrItem.CrId;
                        _context.Pc.Add(PcItem);
                    }
                    catch (DbUpdateConcurrencyException) { }
                    break;

                case "put":
                    try
                    {
                        if (int.TryParse(Request.Form["_id"], out int id))
                        {
                            PcItem.PcId = id;
                            var has = _context.Pc.Where(e => e.PcId == id);
                            if (has != null)
                            {

                                _context.Pc.Update(PcItem);
                            }
                            else
                            {
                                
                                _context.Pc.Add(PcItem);
                            }                                
                        }
                    }
                    catch (DbUpdateConcurrencyException) { }
                    break;
            }
            await _context.SaveChangesAsync();
            return RedirectToPage("EditPC");
        }
        public async Task<IActionResult> OnPostDeleteAsync(int id)
        {
            var item = await _context.Pc.FindAsync(id);
            if (item != null)
            {
                _context.Pc.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public int? GetClassNum(int crid) => _context.Classroom.Where(v => v.CrId == crid).FirstOrDefault().ClassNum;

        public int? GetPlaceNum(int crid) => _context.Classroom.Where(v => v.CrId == crid).FirstOrDefault().PlaceNum;
    }
}