using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedFlow.Models
{
    public class Patient
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string name { get; set; }

        public string? birth_date { get; set; }

        public string? contact { get; set; }

        public string? address { get; set; }

        public int? added_by { get; set; }

        public string? gender { get; set; }

        

        //Relations
        public ICollection<Prescriptions> prescriptions { get; set; }
        public userDetails userDetails { get; set; }

    }
}
