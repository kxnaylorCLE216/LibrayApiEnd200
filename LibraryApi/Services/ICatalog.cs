using System.Threading.Tasks;

namespace LibraryApi
{
    public interface ICatalog
    {
        Task<CatalogModel> GetTheCatalog();
    }
}