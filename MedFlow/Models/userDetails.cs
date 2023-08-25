using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MedFlow.Models
{
    public class userDetails
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int id { get; set; }
        public string? username { get; set; }
        public string? password { get; set; }
        public int user_type { get; set; }

        //Realtioships
        public ICollection<Patient> patients { get; set; }
        public userType userType { get; set; }

    }
}
