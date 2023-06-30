using Domain.Entities;
using Domain.Repositories;
using MongoDB.Driver;

namespace DataAccess.MongoDB.Repositories
{
    public class UsuarioRepository : MongoDBRepository<Usuario, string?>, IUsuarioRepository
    {
        public override string COLLECTION_NAME => "Usuarios";

        public UsuarioRepository(IMongoDatabase mongoDatabase) : base(mongoDatabase)
        {
        }
    }
}
