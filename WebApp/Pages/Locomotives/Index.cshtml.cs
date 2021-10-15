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
        
        public List<string> AllTags { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<string> Tags { get; set; }

        readonly ILocomotiveService _locomotiveService;

        public IndexModel(ILocomotiveService locomotiveService)
        {
            _locomotiveService = locomotiveService;
        }

        public void OnGet()
        {
            // Prepare query options
            if (Tags?.Count > 0)
            {
                QueryOptions.FilterOptions = new FilterOptions
                {
                    Tags = Tags
                };
            }

            // Do query
            var result = _locomotiveService.GetListLocomotives(QueryOptions);

            // Prepare data for view
            Locomotives = result.Item1.ToList();
            QueryOptions.PageNumber = result.Item2;
            NumberOfPages = result.Item3;
            
            AllTags = _locomotiveService.GetTags().ToList();
        }
    }
}
