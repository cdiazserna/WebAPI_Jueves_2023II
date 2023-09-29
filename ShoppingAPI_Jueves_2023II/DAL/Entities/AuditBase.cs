using System.ComponentModel.DataAnnotations;

namespace ShoppingAPI_Jueves_2023II.DAL.Entities
{
    public class AuditBase
    {
        [Key] //PK
        [Required] //Significa que este campo es Obligatorio
        public virtual Guid Id { get; set; } //Esta será el PK de todas las tablas

        public virtual DateTime? CreatedDate { get; set; } //para guardar todo registro nuevo con su date

        public virtual DateTime? ModifiedDate { get; set; } //para guardar todo registro que se modificó con su date
    }
}
