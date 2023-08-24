using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedFlow.Models
{
    public class Payments
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public int? prescription_id { get; set; }
        public int? price { get; set; }

        

        //Relations
        public Prescriptions prescriptions { get; set; }

    }
}
