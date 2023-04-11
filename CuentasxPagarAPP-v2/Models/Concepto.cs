using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CuentasxPagarAPP_v2.Models
{
    public class Concepto
    {
        [Key]
        public int IdConcepto { get; set; }

        [Required(ErrorMessage = "Ingrese la descripción concepto")]
        [Display(Name = "Descripción")]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Seleccione el estado del proveedor")]
        public string Estado { get; set; }
    }
}
