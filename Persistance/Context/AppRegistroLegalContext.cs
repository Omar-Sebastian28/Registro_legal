using System.Reflection;
using Microsoft.EntityFrameworkCore;
using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Infraestructura.Persistance.Context
{

    public class AppRegistroLegalContext : DbContext
    {
        public AppRegistroLegalContext()
        {
        }

        public AppRegistroLegalContext(DbContextOptions<AppRegistroLegalContext> contextOptions) : base(contextOptions) { }

        public DbSet<Persona> Personas { get; set; }    

        public DbSet<Ilicito> Cargos { get; set; }        

        public DbSet<Medio> Medios { get; set; }

       public DbSet<Carpeta> Carpetas { get; set; }

        public DbSet<InfraccionCarpeta> InfraccionCarpetas { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        }
    }
}
