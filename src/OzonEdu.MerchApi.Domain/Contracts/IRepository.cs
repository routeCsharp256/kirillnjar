using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.Contracts
{
    public interface IRepository<TAggregationRoot> where TAggregationRoot : IAggregationRoot
    {
    }
}