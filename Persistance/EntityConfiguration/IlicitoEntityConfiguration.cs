using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Infraestructura.Persistance.EntityConfiguration
{
    public class IlicitoEntityConfiguration : IEntityTypeConfiguration<Ilicito>
    {
        public void Configure(EntityTypeBuilder<Ilicito> builder)
        {
            #region Configuracion Basica.
            builder.ToTable("Ilicito");
            builder.HasKey(c => c.Id);
            #endregion

            #region Configuracion de las propiedades. 
            builder.Property(c => c.Tipo).IsRequired().HasMaxLength(200);
            builder.Property(c => c.Descripcion).IsRequired().HasMaxLength(800);
            #endregion

            #region COnfiguracion de relaciones.

            //La configuracion fue creada desde el mucho a uno.

            #endregion
        }
    }
}
