using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedFlow.Models
{
    public class prescriptionq
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]

        public int id { get; set; }

        public int prescription_id { get; set; }
        public DateTime date { get; set; }
        public string? filepath { get; set; }
        public int? token { get; set; } //doctor inserts from appoinment id
        public int? patient_id { get; set; } //NOT A FOREIGN KEY MANUAL INSERTION
    }
}
