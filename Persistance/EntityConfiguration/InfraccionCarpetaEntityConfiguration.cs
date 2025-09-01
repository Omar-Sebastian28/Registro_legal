using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Infraestructura.Persistance.EntityConfiguration
{
    public class InfraccionCarpetaEntityConfiguration : IEntityTypeConfiguration<InfraccionCarpeta>
    {
        public void Configure(EntityTypeBuilder<InfraccionCarpeta> builder)
        {

            builder.HasKey(ic => ic.Id); //Clave compuesta.


            #region Relaciones de bd entre la carpeta y el ilicito.


            builder.HasOne(ic => ic.Infraccion)
                   .WithMany(i => i.CarpetaInfracciones)
                   .HasForeignKey(ic => ic.InfraccionId);

            builder.HasOne(ic => ic.Carpeta)
                   .WithMany(c => c.CarpetaInfracciones)
                   .HasForeignKey(ic => ic.CarpetaId)
                   .OnDelete(DeleteBehavior.Cascade);
            #endregion

        }
    }
}
