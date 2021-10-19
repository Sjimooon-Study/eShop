using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using ServiceLayer.LocomotiveService;

namespace WebApp.Pages.Locomotives
{
    public class DetailsModel : PageModel
    {
        public DetailsLocomotiveDto Locomotive { get; set; }

        readonly ILocomotiveService _locomotiveService;

        public DetailsModel(ILocomotiveService locomotiveService)
        {
            _locomotiveService = locomotiveService;
        }

        public void OnGet(int id)
        {
            Locomotive = _locomotiveService.GetDetails(id);
        }
    }
}
