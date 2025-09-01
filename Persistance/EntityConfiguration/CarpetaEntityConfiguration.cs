using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Infraestructura.Persistance.EntityConfiguration
{
    public class CarpetaEntityConfiguration : IEntityTypeConfiguration<Carpeta>
    {
        public void Configure(EntityTypeBuilder<Carpeta> builder)
        {
            #region "Configuracion de propiedades de la bd"
            builder.HasKey(c => c.Id);
            builder.Property(c => c.Nombre).IsRequired();
            #endregion;
        }
    }
}
