using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Models.Reservations
{
    public class PostReservationRequest
    {
        [Required]
        public string For { get; set; }
        [Required]
        public string Books { get; set; } // "1,2,3,4"
    }

}
