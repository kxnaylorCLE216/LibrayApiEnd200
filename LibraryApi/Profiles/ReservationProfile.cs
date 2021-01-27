using AutoMapper;
using LibraryApi.Domain;
using LibraryApi.Models.Reservations;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryApi.Profiles
{
    public class ReservationProfile : Profile
    {
        public ReservationProfile()
        {
            // PostReservationRequest->Reservation
            
            CreateMap<PostReservationRequest, Reservation>()
                .ForMember(dest => dest.Status, options => options.Ignore())
                .ForMember(dest => dest.Id, options => options.Ignore())
                .ForMember(dest => dest.CreatedAt, options => options.MapFrom(_ => DateTime.Now));

            CreateMap<Reservation, GetReservationDetailsResponse>();
        }
    }
}
