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

            var lastCrID = _context.Classroom.Max<Classroom>(v => v.CrId);

            var CrItem = new Classroom();

            if (int.TryParse(classnum, out int clnu))
                CrItem.ClassNum = clnu;
            else
                CrItem.ClassNum = 0;

            if (int.TryParse(placenum, out int plnu))
                CrItem.PlaceNum = plnu;
            else
                CrItem.PlaceNum = 0;

            switch (mode)
            {
                case "post":
                default:
                    try
                    {
                        var lastPCID = _context.Pc.Max<Pc>(v => v.PcId);

                        PcItem.PcId = ++lastPCID;
                        CrItem.CrId = ++lastCrID;

                        PcItem.CrId = CrItem.CrId;

                        _context.Classroom.Add(CrItem);
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

                            var has = _context.Pc.Where(e => e.PcId == id).AsNoTracking().SingleOrDefault();
                            if (has != null)
                            {
                                if (has.CrId != null)
                                {
                                    PcItem.CrId = CrItem.CrId = has.CrId.GetValueOrDefault(0);
                                    var cr = _context.Classroom.Find(CrItem.CrId);
                                    cr.ClassNum = CrItem.ClassNum;
                                    cr.PlaceNum = CrItem.PlaceNum;
                                    _context.Classroom.Update(cr);
                                }
                                else
                                {
                                    CrItem.CrId = ++lastCrID;
                                    PcItem.CrId = CrItem.CrId;
                                    _context.Classroom.Add(CrItem);
                                }

                                _context.Pc.Update(PcItem);
                            }
                            else
                            {
                                CrItem.CrId = ++lastCrID;
                                PcItem.CrId = CrItem.CrId;

                                _context.Classroom.Add(CrItem);
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
                if (item.CrId != null) 
                {
                    var critem = _context.Classroom.Where(e => e.CrId == item.CrId).FirstOrDefault();
                    _context.Classroom.Remove(critem);
                }

                _context.Pc.Remove(item);
                await _context.SaveChangesAsync();
            }
            return RedirectToPage();
        }

        public int? GetClassNum(int crid)
        {
            if (crid == 0) return null;
            return _context.Classroom.Where(v => v.CrId == crid).FirstOrDefault().ClassNum;
        }
        public int? GetPlaceNum(int crid) 
        {
            if (crid == 0) return null;
            return _context.Classroom.Where(v => v.CrId == crid).FirstOrDefault().PlaceNum;
        } 
    }
}