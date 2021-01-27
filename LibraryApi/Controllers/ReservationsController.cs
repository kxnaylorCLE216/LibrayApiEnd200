using AutoMapper;
using AutoMapper.QueryableExtensions;
using LibraryApi.Domain;
using LibraryApi.Filters;
using LibraryApi.Models.Reservations;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Controllers
{
    public class ReservationsController : ControllerBase
    {

        private readonly LibraryDataContext _context;
        private readonly IMapper _mapper;
        private readonly MapperConfiguration _config;
        private readonly IProcessReservation _reservationProcessor;

        public ReservationsController(LibraryDataContext context, IMapper mapper, MapperConfiguration config, IProcessReservation reservationProcessor)
        {
            _context = context;
            _mapper = mapper;
            _config = config;
            _reservationProcessor = reservationProcessor;
        }

        // POST /reservations
        [HttpPost("/reservations")]
        [ResponseCache(Location = ResponseCacheLocation.Client, Duration = 5)]
        [ValidateModel]
        public async Task<ActionResult> CreateReservation([FromBody] PostReservationRequest request)
        {
            // Update the domain (POST is unsafe - it does work. What work will we do?)
            // -- Create and Process a new Reservation (in our synch model)
            // -- Save it to the database.

            var reservation = _mapper.Map<Reservation>(request);
            // Tell something else - somehow - to work on this outside the Request/Response cycle.

            reservation.Status = ReservationStatus.Pending;
            // the reservation has an Id of zero because it hasn't been saved yet.
            _context.Reservations.Add(reservation);
            await _context.SaveChangesAsync();
            // like "magic" it now has a non-zero id (it's database id)
            await _reservationProcessor.ProcessReservation(reservation);

            var response = _mapper.Map<GetReservationDetailsResponse>(reservation);

            return CreatedAtRoute("reservations#getareservation", new { id = response.Id }, response);
        }

        // GET /reservations/{id}
        [HttpGet("/reservations/{id:int}", Name = "reservations#getareservation")]
        public async Task<ActionResult<GetReservationDetailsResponse>> GetAReservation(int id)
        {
            var response = await _context.Reservations
                .ProjectTo<GetReservationDetailsResponse>(_config)
                .SingleOrDefaultAsync(r => r.Id == id);

            return this.Maybe(response); // I committed a sin here. See if you can find it!
        }
        // Async

        // When the BW Accepts a reservation
        [HttpPost("/reservations/accepted")]
        public async Task<ActionResult> ReservationAccepted([FromBody] GetReservationDetailsResponse request)
        {
            var reservation = await _context.Reservations
                .SingleOrDefaultAsync(r => r.Id == request.Id && r.Status == ReservationStatus.Pending);
            if (reservation == null)
            {
                return BadRequest("No pending Reservation with that Id");
            }
            else
            {
                reservation.Status = ReservationStatus.Accepted;
                await _context.SaveChangesAsync();
            }
            return Accepted(); // not really anything you need to send back. this is "fine - I did it"
        }

        // When a BW Rejects a reservation
        [HttpPost("/reservations/rejected")]
        public async Task<ActionResult> ReservationRejected([FromBody] GetReservationDetailsResponse request)
        {
            var reservation = await _context.Reservations
               .SingleOrDefaultAsync(r => r.Id == request.Id && r.Status == ReservationStatus.Pending);
            if (reservation == null)
            {
                return BadRequest("No pending Reservation with that Id");
            }
            else
            {
                reservation.Status = ReservationStatus.Rejected;
                await _context.SaveChangesAsync();
            }
            return Accepted(); // not really anything you need to send back. this is "fine - I did it"
        }

        [HttpGet("/reservations/pending")]
        public async Task<ActionResult> GetPendingReservations()
        {
            var reservations = await _context.Reservations
                .Where(res => res.Status == ReservationStatus.Pending)
                .ToListAsync();

            return Ok(new { data = reservations });
        }

        [HttpGet("/reservations/rejected")]
        public async Task<ActionResult> GetRejectedReservations()
        {
            var reservations = await _context.Reservations
                .Where(res => res.Status == ReservationStatus.Rejected)
                .ToListAsync();

            return Ok(new { data = reservations });
        }

        [HttpGet("/reservations/accepted")]
        public async Task<ActionResult> GetAcceptedReservations()
        {
            var reservations = await _context.Reservations
                .Where(res => res.Status == ReservationStatus.Accepted)
                .ToListAsync();

            return Ok(new { data = reservations });
        }
        [HttpGet("/reservations")]
        public async Task<ActionResult> GetAllReservations()
        {
            var reservations = await _context.Reservations.ToListAsync();

            return Ok(new { data = reservations });
        }

    }
}
