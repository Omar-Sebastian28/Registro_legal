using RegistroLegal.Core.Aplications.ViewModel.VmIlicito;
namespace RegistroLegal.Core.Aplications.ViewModel.VmInfraccionCarpeta
{
    public class BasicInfraccionCarpetaViewModel
    {
        public int Id { get; set; } // PK        
        public int CarpetaId { get; set; }
        public int InfraccionId { get; set; }
        public BasicIlicitoViewModel? Infraccion { get; set; }

        public DateTime FechaAgregado { get; set; }
    }
}
