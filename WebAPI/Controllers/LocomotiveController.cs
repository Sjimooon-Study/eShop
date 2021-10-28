using Microsoft.AspNetCore.Mvc;
using ServiceLayer.LocomotiveService;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using static DataLayer.Models.ModelItem;
using static ServiceLayer.Utilities.Pagination;

namespace WebAPI.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LocomotiveController : Controller
    {
        private readonly ILocomotiveService _locomotiveService;
        
        public LocomotiveController(ILocomotiveService locomotiveService)
        {
            _locomotiveService = locomotiveService;
        }

        #region Get
        /// <summary>
        /// Get list of list locomotives.
        /// </summary>
        /// <param name="s">Search parameter.</param>
        /// <param name="o">Order option.</param>
        /// <param name="pgn">Page number.</param>
        /// <param name="pgs">Page size.</param>
        /// <returns>List of <see cref="ListLocomotiveDto"/>.</returns>
        [HttpGet]
        [Route("list")]
        public IEnumerable<ListLocomotiveDto> GetListLocomotives(string s = "", EOrderByOptions o = EOrderByOptions.Default, ushort pgn = 1, EPageSize pgs = EPageSize.PS10)
        {
            QueryOptions queryOptions = new()
            {
                SearchString = s,
                FilterOptions = new()
                {
                    //Tags = new List<string>(),
                    //Scales = new List<EScale>()
                    //Epochs = new List<EEpoch>(),
                    //Controls = new List<EControl>(),
                    //LocoTypes = new List<ELocoType>()
                },
                OrderByOptions = o,
                PageNumber = pgn,
                PageSize = pgs
            };

            var result = _locomotiveService.GetList(queryOptions);

            return result.Item1.ToList();
        }

        /// <summary>
        /// Get details locomotive.
        /// </summary>
        /// <param name="id">Locomotive ID.</param>
        /// <returns>Locomotive details (<see cref="DetailsLocomotiveDto"/>).</returns>
        [HttpGet]
        [Route("details/{id:int}")]
        public IActionResult GetDetailsLocomotive(int id)
        {
            DetailsLocomotiveDto locomotive = _locomotiveService.GetDetails(id);

            if (locomotive != null)
            {
                return Ok(locomotive);
            }
            
            return NotFound();
        }
        
        /// <summary>
        /// Get edit locomotive.
        /// </summary>
        /// <param name="id">Locomotive ID.</param>
        /// <returns>Locomotive to be edited (<see cref="EditLocomotiveDto"/>).</returns>
        [HttpGet]
        [Route("edit/{id:int}")]
        public IActionResult GetEditLocomotive(int id)
        {
            EditLocomotiveDto locomotive = _locomotiveService.GetEdit(id);

            if (locomotive != null)
            {
                return Ok(locomotive);
            }

            return NotFound();
        }
        #endregion
        
        #region Post
        /// <summary>
        /// Add locomotive in underlying database.
        /// </summary>
        /// <param name="locomotive">Locomotive to add.</param>
        /// <returns><see cref="OkResult"/> if added; otherwise <see cref="BadRequestResult"/>.</returns>
        [HttpPost]
        [Route("add")]
        public IActionResult AddLocomotive(AddLocomotiveDto locomotive)
        {
            int result = _locomotiveService.Add(locomotive);
        
            if (result > 0)
            {
                return Created("", locomotive);
            }
        
            return BadRequest();
        }
        #endregion
        
        #region Put
        /// <summary>
        /// Edit locomotive in underlying database.
        /// </summary>
        /// <param name="locomotive">Locomotive to edit.</param>
        /// <returns><see cref="OkResult"/> if added; otherwise <see cref="BadRequestResult"/>.</returns>
        [HttpPut]
        [Route("edit")]
        public IActionResult EditLocomotive(EditLocomotiveDto locomotive)
        {
            int result = _locomotiveService.Edit(locomotive);
        
            if (result > 0)
            {
                return Ok();
            }
        
            return BadRequest();
        }
        #endregion
        
        #region Delete
        /// <summary>
        /// Delete locomotive in underlying database.
        /// </summary>
        /// <param name="id">Locomotive ID.</param>
        /// <returns><see cref="OkResult"/> if added; otherwise <see cref="BadRequestResult"/>.</returns>
        [HttpDelete]
        [Route("delete/{id}")]
        public IActionResult DeleteLocomotive(int id)
        {
            int result = _locomotiveService.Delete(id);
        
            if (result > 0)
            {
                return NoContent();
            }
        
            return NotFound();
        }
        #endregion
    }
}
