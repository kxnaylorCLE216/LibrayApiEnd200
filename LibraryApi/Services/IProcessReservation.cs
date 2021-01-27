using LibraryApi.Domain;
using System.Threading.Tasks;

namespace LibraryApi
{
    public interface IProcessReservation
    {
        Task ProcessReservation(Reservation reservation);
    }
}