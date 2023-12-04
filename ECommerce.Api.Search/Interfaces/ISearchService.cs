using System.Threading.Tasks;

namespace ECommerce.Api.Search.Interfaces
{
    public interface ISearchService
    {
        Task<(bool isSuccess, dynamic SearchResult)> SearchAsync(int customerId);
    }
}