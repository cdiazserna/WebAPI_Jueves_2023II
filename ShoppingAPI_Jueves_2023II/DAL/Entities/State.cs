using System.ComponentModel.DataAnnotations;
using System.Xml.Linq;

namespace ShoppingAPI_Jueves_2023II.DAL.Entities
{
    public class State : AuditBase
    {
        [Display(Name = "Estado/Departamento")]
        [MaxLength(50, ErrorMessage = "El campo {0} debe tener máximo {1} caracteres")]
        [Required(ErrorMessage = "¡El campo {0} es obligatorio!")]
        public string Name { get; set; }

        [Display(Name = "País")]
        //Relación con Country
        public Country? Country { get; set; } //Este representa un OBJETO DE COUNTRY

        [Display(Name = "Id País")]
        public Guid CountryId { get; set; } //FK
    }
}
