using RegistroLegal.Core.Domain.Entity;

namespace RegistroLegal.Core.Aplications.ViewModel.VmCarpeta
{
    public class BasicCarpetaViewModel
    {
        public int Id { get; set; }
        public required string Nombre { get; set; }

        public string? CreatedById { get; set; }
        public int Elementos { get; set; }  
    }
}
