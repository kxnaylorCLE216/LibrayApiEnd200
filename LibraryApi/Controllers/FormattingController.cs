using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class FormattingController : ControllerBase
    {
        // Step two: Describe that functionality abstractly
        private readonly IFormatNames _nameFormatter;

        public FormattingController(IFormatNames nameFormatter)
        {
            _nameFormatter = nameFormatter;
        }

        [HttpGet("formats/name/{first}/{last}")]
        public ActionResult FormatName(string first, string last)
        {
            // Step one - think about what you want.
            // WTCYWYH - Write the code you wish you had.
            string response = _nameFormatter.FormatName(first, last);
            return Ok(new { fullName = response });
        }
    }
}
