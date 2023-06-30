using MongoDB.Bson.Serialization.Attributes;

namespace Domain.Entities
{
    public class Poliza : IEntity<string?>
    {
        [BsonId]
        [BsonRepresentation(MongoDB.Bson.BsonType.ObjectId)]
        public string? Id { get; set; }
        public string NumeroPoliza { get; set; } = "";
        public string NombreCliente { get; set; } = "";
        public string IdentificacionCliente { get; set; } = "";
        public DateTime? FechaNacimientoCliente { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public DateTime FechaPoliza { get; set; }
        public decimal ValorMaximoPoliza { get; set; }
        public string NombrePlanPoliza { get; set; } = "";
        public string CiudadCliente { get; set; } = "";
        public string DireccionResidencia { get; set; } = "";
        public string Placa { get; set; } = "";
        public string Modelo { get; set; } = "";
        public bool TieneInspeccion { get; set; }
    }
}
