using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.LocomotiveService;

namespace WebApp.Pages.Locomotive
{
    public class IndexModel : PageModel
    {
        public List<ListLocomotiveDto> Locomotives { get; set; }
        [BindProperty(SupportsGet = true)]
        public QueryOptions QueryOptions { get; set; }
        public int NumberOfPages { get; set; }

        readonly ILocomotiveService _locomotiveService;

        public IndexModel(ILocomotiveService locomotiveService)
        {
            _locomotiveService = locomotiveService;
        }

        public void OnGet()
        {
            var result = _locomotiveService.GetListLocomotives(QueryOptions);

            Locomotives = result.Item1.ToList();
            QueryOptions.PageNumber = result.Item2;
            NumberOfPages = result.Item3;
        }
    }
}
