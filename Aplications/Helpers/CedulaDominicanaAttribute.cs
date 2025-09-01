namespace RegistroLegal.Core.Aplications.Helpers
{
    using System.ComponentModel.DataAnnotations;

    public class CedulaDominicanaAttribute : ValidationAttribute
    {
        public CedulaDominicanaAttribute()
        {
            ErrorMessage = "La cédula ingresada no es válida.";
        }

        public override bool IsValid(object? value)
        {
            if (value == null) return false;

            var cedula = value.ToString()?.Replace("-", "").Trim();
            if (string.IsNullOrEmpty(cedula) || cedula.Length != 11 || !cedula.All(char.IsDigit))
                return false;

            // Validación de checksum (algoritmo oficial de la junta central electoral).
            int[] multiplicadores = { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2 };
            int suma = 0;

            for (int i = 0; i < 10; i++)
            {
                int producto = int.Parse(cedula[i].ToString()) * multiplicadores[i];
                suma += producto < 10 ? producto : producto - 9;
            }

            int digitoVerificador = int.Parse(cedula[10].ToString());
            return (suma + digitoVerificador) % 10 == 0;
        }
    }
}
