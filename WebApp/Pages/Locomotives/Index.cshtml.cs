using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.LocomotiveService;
using static DataLayer.Models.ModelItem;
using static DataLayer.Models.Products.Locomotive;

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

        [BindProperty(SupportsGet = true)]
        public List<EScale> Scales { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<EEpoch> Epochs { get; set; }

        [BindProperty(SupportsGet = true)]
        public List<ELocoType> LocoTypes { get; set; }
        [BindProperty(SupportsGet = true)]
        public List<EControl> Controls { get; set; }

        readonly ILocomotiveService _locomotiveService;

        public IndexModel(ILocomotiveService locomotiveService)
        {
            _locomotiveService = locomotiveService;
        }

        public void OnGet()
        {
            // Prepare query options
            if (Tags?.Count > 0
                || Scales?.Count > 0
                || Epochs?.Count > 0
                || Controls?.Count > 0
                || LocoTypes?.Count > 0)
            {
                QueryOptions.FilterOptions = new FilterOptions
                {
                    Tags = Tags,
                    Scales = Scales,
                    Epochs = Epochs,
                    Controls = Controls,
                    LocoTypes = LocoTypes
                };
            }



            var test = typeof(ELocoType).GetMember(typeof(ELocoType).GetEnumName(1)).First().GetCustomAttribute<DisplayAttribute>().Name;


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
