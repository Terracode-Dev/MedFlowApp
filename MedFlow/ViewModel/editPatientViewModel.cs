using MedFlow.Models;

namespace MedFlow.ViewModel
{
    public class editPatientViewModel
    {
        public Patient Patient { get; set; }
        public List<Prescriptions> prescriptions { get; set; }
    }
}
