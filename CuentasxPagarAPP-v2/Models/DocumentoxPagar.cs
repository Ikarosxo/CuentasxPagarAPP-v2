using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace CuentasxPagarAPP_v2.Models
{
    public class DocumentoxPagar
    {
        [Key]
        [Display(Name = "Número de Documento")]
        public int NumDocument { get; set; }

        [Required(ErrorMessage = "Ingrese el número de la factura a pagar")]
        [Display(Name = "Número de Factura a pagar")]
        public int NumFacturaPagar { get; set; }

        [Required]
        [Display(Name = "Fecha del Documento")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime FechaDocumento { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Ingrese el monto del documento por pagar")]
        [Column(TypeName = "decimal(10,2)")]
        [Range(0.01, int.MaxValue, ErrorMessage = "Favor ingresar un valor entero mayor a 0.01.")]
        [Display(Name = "Monto")]
        public decimal Monto { get; set; }

        [Required]
        [Display(Name = "Fecha de Registro")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}")]
        [DataType(DataType.Date)]
        public DateTime FechaRegistro { get; set; } = DateTime.UtcNow;

        [Required(ErrorMessage = "Seleccione un proveedor")]
        [Display(Name = "Proveedor")]
        public int IdProveedor { get; set; }

        [ForeignKey("IdProveedor")]
        public Proveedor Proveedor { get; set; }

        [Required(ErrorMessage = "Seleccione el estado del documento")]
        [Display(Name = "Estado del Documento")]
        public string? Estado { get; set; } //Pendiente o pagado
    }
}
