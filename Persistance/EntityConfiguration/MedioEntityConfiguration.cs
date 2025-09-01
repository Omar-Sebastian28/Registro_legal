using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Infraestructura.Persistance.EntityConfiguration
{
    public class MedioEntityConfiguration : IEntityTypeConfiguration<Medio>
    {
        public void Configure(EntityTypeBuilder<Medio> builder)
        {

            #region Configuraciones basicas.
            builder.HasKey(m => m.Id);
            #endregion


            #region Configuraciones de propiedades.

            builder.Property(m => m.NombreMedio).IsRequired().HasMaxLength(100);
            builder.Property(m => m.Descripcion).IsRequired().HasMaxLength(600);

            #endregion


            #region Configuracion de Relaciones.

            builder.HasMany(m => m.Cargos)
                   .WithOne(c => c.Medio)
                   .HasForeignKey(c => c.MedioId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion
        }
    }
}
