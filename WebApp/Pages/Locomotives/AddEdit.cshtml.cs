using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.FileProviders;
using ServiceLayer;
using ServiceLayer.LocomotiveService;
using WebApp.Helpers;

namespace WebApp.Pages.Locomotives
{
    public class AddEditModel : PageModel
    {
        [BindProperty]
        public EditLocomotiveDto Locomotive { get; set; }
        [BindProperty]
        public List<IFormFile> Images { get; set; }

        private readonly ILocomotiveService _locomotiveService;
        private readonly IWebHostEnvironment _environment;

        public AddEditModel(ILocomotiveService locomotiveService, IWebHostEnvironment environment)
        {
            _locomotiveService = locomotiveService;
            _environment = environment;
        }

        public IActionResult OnGet(int id)
        {
            if (!HttpContext.IsAdmin())
            {
                return RedirectToPage("./Index");
            }
                
            Locomotive = _locomotiveService.GetEdit(id);

            return Page();

        }

        public IActionResult OnPost()
        {
            if (!ModelState.IsValid)
            {
                return Page();
            }

            foreach (IFormFile image in Images)
            {
                if (image?.Length > 0)
                {
                    DateTime date = DateTime.Now;
                    string fileName = $"{date.Year}_{date.Month}_{date.Day}_{Guid.NewGuid()}.jpeg";
                    string filepath = new PhysicalFileProvider(Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "img", "products")).Root + fileName;

                    using (var stream = System.IO.File.Create(filepath))
                    {
                        image.CopyTo(stream);
                    }

                    Locomotive.AddedImages.Add(new AddImageDto { Path = fileName });
                }
            }

            _locomotiveService.Edit(Locomotive);

            return RedirectToPage("./Index");
        }
    }
}
