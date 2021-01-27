using System.Threading.Tasks;

namespace LibraryApi
{
    public interface IBookCommands
    {
        Task RemoveBookFromInventory(int id);
    }
}