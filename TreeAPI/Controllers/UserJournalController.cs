using DataAccess.Diagnostics.View;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using TreeAPI.Services.TreeAPI.Services;

namespace TreeAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserJournalController : ControllerBase
    {
        private readonly JournalService _journalService;

        public UserJournalController(JournalService journalService)
        {
            _journalService = journalService;
        }

        [HttpPost("GetRange")]
        public IActionResult GetRange(
            [FromHeader(Name = "skip"), Required] int skip,
            [FromHeader(Name = "take"), Required] int take,
            [FromBody] VJournalFilter searchParameters)
        {
        

            // Perform the logic with the provided parameters
            // For demonstration purposes, let's assume we are using the JournalService
            var rangeData = _journalService.GetJournalByFilter(searchParameters, skip, take);
            if (rangeData == null)
            {
                return BadRequest("no journal entries in specified range");
            }
            return Ok(rangeData);
        }


        [HttpPost("GetSingle")]
        public IActionResult GetSingle(
            [FromHeader(Name = "id"), Required] int id)
        {
            var singleData = _journalService.GetJournalById(id);
            if (singleData == null)
            {
                return BadRequest("Journal entry doesn't exist");
            }
            return Ok(singleData);
        }




    }
}
