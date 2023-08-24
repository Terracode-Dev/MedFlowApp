using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedFlow.Models
{
    public class Appointmentq
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]


        public int id { get; set; }
        public DateTime date { get; set; }
        public string? status { get; set; }
        public int? patient_id { get; set; } //NOT A FOREIGN KEY
    }
}
