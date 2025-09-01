namespace RegistroLegal.Core.Aplications.ViewModel.VmMedio
{
    public class BasicMedioViewModel
    {
        public int Id { get; set; }

        public required string NombreMedio { get; set; }

        public required string Descripcion { get; set; }

        public string? LinkReferencia { get; set; }

        public int? Publicaciones { get; set; }

        //Para saber que usuario lo creo.
        public string? CreatedById { get; set; }


        public string? Foto { get; set; }
    }
}
