using Domain.Entities;
using Domain.Repositories;
using MongoDB.Driver;

namespace DataAccess.MongoDB.Repositories
{
    public class PolizaRepository : MongoDBRepository<Poliza, string?>, IPolizaRepository
    {
        public override string COLLECTION_NAME => "Polizas";

        public PolizaRepository(IMongoDatabase mongoDatabase) 
            : base(mongoDatabase)
        {
        }
    }
}
