using LibraryApi.Controllers;
using System.Threading.Tasks;

namespace LibraryApi.Services
{
    public interface IGetServerStatus
    {
        Task<GetStatusResponse> GetCurrentStatus();
    }
}