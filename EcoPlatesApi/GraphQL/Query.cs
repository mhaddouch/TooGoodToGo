using Core.Domain;
using Core.DomainServices;

namespace EcoPlatesApi.GraphQL
{
    public class Query
    {


        public IQueryable<Package> GetPackages([Service] IPackageRepository packageRepository) =>
       packageRepository.Query();
    }
}
