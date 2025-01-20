using AVIVA.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace AVIVA.Infrastructure.Persistence.Configurations
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            // Configurar la clave primaria
            builder.HasKey(p => p.Id);

            // Configurar las propiedades con restricciones, si es necesario
            builder.Property(p => p.Name)
                .IsRequired() // Asegurarse de que el nombre sea obligatorio
                .HasMaxLength(200); // Limitar el tamaño del nombre

            builder.Property(p => p.Details)
                .IsRequired(false) // Si no es obligatorio
                .HasMaxLength(500); // Limitar el tamaño de los detalles

            builder.Property(p => p.UnitPrice)
                .IsRequired() // El precio debe ser obligatorio
                .HasColumnType("decimal(18,2)"); // Definir el tipo de dato y la precisión del precio

            builder.Property(p => p.Status)
                .IsRequired(); // El estado es obligatorio (asumimos que es un booleano)

            // Si necesitas configurar otros aspectos, puedes hacerlo aquí.
        }
    }
}