using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Infraestructura.Persistance.EntityConfiguration
{
    public class UserEntityConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {

            #region Relaciones

            builder.ToTable("Usuarios");
            builder.HasKey(u => u.Id);

            #endregion
            

            #region Configuracion de propiedades.

            builder.Property(u => u.Nombre).IsRequired().HasMaxLength(200);
            builder.Property(u => u.Password).IsRequired().HasMaxLength(256);

            #endregion


            



        }
    }
}
