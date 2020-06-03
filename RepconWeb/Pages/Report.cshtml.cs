using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace RepconWeb.Pages
{
    public class ReportModel : PageModel
    {
        private const string PieChartTemplatePart1 =
@"google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(function () { 
    const data = google.visualization.arrayToDataTable([
";
        private const string PieChartTemplatePart2 =
@"]);

const chart = new google.visualization.PieChart(document.getElementById('piechart'));
chart.draw(data);
});";

        private const string СoreСhartTemplatePart1 =
@"google.charts.load('current', { 'packages': ['corechart'] });
google.charts.setOnLoadCallback(function () { 
    const data = google.visualization.arrayToDataTable([
";
        private const string СoreСhartTemplatePart2 =
@"]);

const chart = new google.visualization.Histogram(document.getElementById('chart_div'));
chart.draw(data);
});";

        private readonly RepconContext _context;

        public string ReportUserCountProcessScript { get; set; }
        public string ReportUserProgressSessionScript { get; set; }

        public ReportModel(RepconContext db) => _context = db;

        public IActionResult OnGet(string log, int crnum, int placenum, string proc, DateTime startT, DateTime endT)
        {
            GenerateReport1(log);
          //  GenerateReport2();
            return Page();
        }

        private void GenerateReport1(string log)
        {
            var reportRequest = from sp in _context.SessionProc
                                         join p in _context.Process on sp.ProcId equals p.ProcId
                                         join s in _context.Session on sp.SessionId equals s.SessionId
                                         where s.Login == log
                                         group p by p.Name into g
                                         select new { Name = g.Key, Count = g.Count() };

            List<string> resValues = new List<string>
            {
                @"['Task', 'Test']"
            };
            foreach (var item in reportRequest)
                resValues.Add($"[\'{item.Name}\', {item.Count}]");
            ReportUserCountProcessScript =
                PieChartTemplatePart1 + string.Join(",\n", resValues) + PieChartTemplatePart2;
        }

        //private void GenerateReport2()
        //{
        //    var reportRequest = from s in _context.Session
        //                        group s by s.Login into g
        //                        select new { Login = g.Key, Count = g.Count() };

        //    List<string> resValues = new List<string>
        //    {
        //        @"['Task', 'Количество запусков']"
        //    };
        //    foreach (var item in reportRequest)
        //        resValues.Add($"[\'{item.Login}\', {item.Count}]");
        //    // У вас забился засор, принимайте диносор
        //    ReportUserProgressSessionScript =
        //        СoreСhartTemplatePart1 +
        //        string.Join(",\n", resValues) +
        //        СoreСhartTemplatePart2;

        //}
    }
}