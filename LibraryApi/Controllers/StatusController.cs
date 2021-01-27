using LibraryApi.Models.Employees;
using LibraryApi.Services;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class StatusController : ControllerBase
    {
        private readonly IGetServerStatus _serverStatus;

        public StatusController(IGetServerStatus serverStatus)
        {
            _serverStatus = serverStatus;
        }



        // GET /status
        [HttpGet("status")]
        public async Task<ActionResult<GetStatusResponse>> GetTheStatus()
        {
            // WTCYWYH - Write the Code You Wish You Had.
            GetStatusResponse response = await  _serverStatus.GetCurrentStatus();

           
            return Ok(response);
        }

        // GET /sayhi/Putintane
        [HttpGet("sayhi/{name}")]
        public ActionResult SayHello(string name)
        {
            return Ok($"Hello, {name}!");
        }

        [HttpGet("orders/{year:int}/{month:int:range(1,12)}/{day:int}", Name ="status#getordersfor")]
        public ActionResult GetOrdersFor(int year, int month, int day)
        {
            return Ok($"Getting orders for {year}/{month}/{day}");
        }

        // Query Strings
        [HttpGet("employees")]
        public ActionResult GetEmployees([FromQuery] string department = "All")
        {
            return Ok($"Showing employees in department {department}");

        }

        [HttpGet("whoami")]
        public ActionResult WhoAmi([FromHeader(Name ="User-Agent")]string userAgent)
        {
            return Ok($"I see you are running {userAgent}");
        }

        [HttpPost("employees")]
        public ActionResult HireSomeone([FromBody] PostEmployeeRequest request)
        {
            return Ok($"Hiring {request.Name} in {request.Department} for {request.StartingSalary:c}"); // TODO: Go through the whole pattern tomorrow when we add a book to our library.
        }
    }


    // Method Description (Request | Response) Requests to the server, REsponses come from the server
    public class GetStatusResponse
    {
        public string Message { get; set; }
        public DateTime LastChecked { get; set; }
    }
}
