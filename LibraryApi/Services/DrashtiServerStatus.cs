using LibraryApi.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public class DrashtiServerStatus : IGetServerStatus
    {
        public Task<GetStatusResponse> GetCurrentStatus()
        {
            // this is all fake. When Drashti is done with her service, we'll actually call it.

            return Task.FromResult(new GetStatusResponse
            {
                Message = "All Good",
                LastChecked = DateTime.Now // todo - fix this!
            });
        }
    }
}
