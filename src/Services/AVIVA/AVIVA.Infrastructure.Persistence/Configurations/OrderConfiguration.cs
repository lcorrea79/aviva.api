using AVIVA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using Newtonsoft.Json;
using System.Collections.Generic;

namespace AVIVA.Infrastructure.Persistence.Configurations
{
    internal class OrderConfiguration : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            // Configuración de la clave primaria
            builder.HasKey(o => o.OrderId);

            // Configuración para las propiedades de tipo Dictionary<string, object> (OtherData y ControlData)
            builder.Property(o => o.OtherData)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),  // Serializa a JSON (string)
                    v => JsonConvert.DeserializeObject<Dictionary<string, object>>(v));  // Deserializa desde JSON

            builder.Property(o => o.ControlData)
                .HasConversion(
                    v => JsonConvert.SerializeObject(v),  // Serializa a JSON (string)
                    v => JsonConvert.DeserializeObject<Dictionary<string, object>>(v));  // Deserializa desde JSON        


        }
    }
}
