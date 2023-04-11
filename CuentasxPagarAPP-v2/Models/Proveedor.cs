using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;
using CuentasxPagarAPP_v2.Models.Validators;

namespace CuentasxPagarAPP_v2.Models
{
    public class Proveedor
    {
        [Key]
        public int IdProveedor { get; set; }

        [RegularExpression(@"^[A-Z]+[a-zA-Z\s]*$")]
        [Required(ErrorMessage = "Ingrese el nombre del proveedor")]
        public string Nombre { get; set; }

        [Required(ErrorMessage = "Ingrese si es persona física o jurídica")]
        [Display(Name = "Tipo de Persona")]
        public string TipoPersona { get; set; } //si fisica o juridica

        [ValidaCedulaRNC(ErrorMessage = "Ingrese la cédula o RNC del proveedor")]
        [Display(Name = "Cédula / RNC")]
        [StringLength(11)]
        public string CedulaRNC { get; set; }

        [Required(ErrorMessage = "Ingrese el balance que se le debe al proveedor")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Favor ingresar un valor entero mayor a 0.01.")]
        [Display(Name = "Balance")]
        public decimal Balance { get; set; }

        [Required(ErrorMessage = "Seleccione el estado del proveedor")]
        public string Estado { get; set; }

        //public ICollection<DocumentoxPagar> DocumentosxPagar { get; set; }
    }
}
