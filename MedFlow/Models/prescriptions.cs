using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedFlow.Models
{
    public class Prescriptions
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }

        public DateTime date { get; set; }

        public string? filepath { get; set; }

        public int? patient_id { get; set; }
        public int? payment_id { get; set; }

        public int? token { get; set; }

       

        public string? billfilepath { get; set; }


        //Realtions
        public Patient patient { get; set; }
        public Payments payments { get; set; }
        

    }
}
