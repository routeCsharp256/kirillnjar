using OzonEdu.MerchApi.Domain.Models;

namespace OzonEdu.MerchApi.Domain.Contracts
{
    public interface IRepository<TEntity> where TEntity : Entity
    {
    }
}