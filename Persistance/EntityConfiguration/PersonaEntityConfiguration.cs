using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Infraestructura.Persistance.EntityConfiguration
{
    public class PersonaEntityConfiguration : IEntityTypeConfiguration<Persona>
    {
        public void Configure(EntityTypeBuilder<Persona> builder)
        {

            #region Configruacion basica.

            builder.HasKey(p => p.Id);

            #endregion


            #region Configruacion de propiedades.

            builder.Property(p => p.NombrePersona).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Apellido).IsRequired().HasMaxLength(200);
            builder.Property(p => p.Cedula).IsRequired().HasMaxLength(12);
            builder.Property(p => p.CreatedById).HasMaxLength(450);

            #endregion


            #region Relacion de Entidades.
            builder.HasMany(p => p.Cargos)
                   .WithOne(c => c.Persona)
                   .HasForeignKey(c => c.PersonaId)
                   .OnDelete(DeleteBehavior.Restrict);
            #endregion

        }
    }
}
