using System.ComponentModel.DataAnnotations;

namespace CuentasxPagarAPP_v2.Models.Validators
{
    public class ValidaCedulaRNCAttribute : ValidationAttribute
    {
        protected override ValidationResult IsValid(object value, ValidationContext validationContext)
        {
            string cedulaRNC = (string)value;
            bool esValido;

            // Verifica si es una cédula
            if (cedulaRNC.Length == 11)
            {
                esValido = ValidaCedula(cedulaRNC);
            }
            // Verifica si es un RNC
            else if (cedulaRNC.Length == 9)
            {
                esValido = EsUnRNCValido(cedulaRNC);
            }
            else
            {
                esValido = false;
            }

            if (!esValido)
            {
                return new ValidationResult(ErrorMessage);
            }

            return ValidationResult.Success;
        }

        private static bool ValidaCedula(string pCedula)
        {
            int vnTotal = 0;
            string vcCedula = pCedula.Replace("-", "");
            int pLongCed = vcCedula.Trim().Length;
            int[] digitoMult = new int[11] { 1, 2, 1, 2, 1, 2, 1, 2, 1, 2, 1 };
            if (pLongCed < 11 || pLongCed > 11)
                return false;
            for (int vDig = 1; vDig <= pLongCed; vDig++)
            {
                int vCalculo = Int32.Parse(vcCedula.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                if (vCalculo < 10)
                    vnTotal += vCalculo;
                else
                    vnTotal += Int32.Parse(vCalculo.ToString().Substring(0, 1)) + Int32.Parse(vCalculo.ToString().Substring(1, 1));
            }
            if (vnTotal % 10 == 0)
                return true;
            else
                return false;
        }

        private static bool EsUnRNCValido(string pRNC)
        {
            int vnTotal = 0;
            int[] digitoMult = new int[8] { 7, 9, 8, 6, 5, 4, 3, 2 };
            string vcRNC = pRNC.Replace("-", "").Replace(" ", "");
            string vDigito = vcRNC.Substring(8, 1);

            if (vcRNC.Length.Equals(9))
                if (!"145".Contains(vcRNC.Substring(0, 1)))
                    return false;

            for (int vDig = 1; vDig <= 8; vDig++)
            {
                int vCalculo = Int32.Parse(vcRNC.Substring(vDig - 1, 1)) * digitoMult[vDig - 1];
                vnTotal += vCalculo;
            }

            if (vnTotal % 11 == 0 && vDigito == "1" || vnTotal % 11 == 1 && vDigito == "1" ||
                (11 - (vnTotal % 11)).Equals(vDigito))
                return true;
            else
                return false;


        }
    }
}
