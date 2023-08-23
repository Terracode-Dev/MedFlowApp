using Microsoft.EntityFrameworkCore;
namespace MedFlow.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) 
        { 
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<userDetails>(e =>
            {
                e.HasOne(d => d.userType).WithMany(x => x.userDetails).HasForeignKey(d => d.user_type);
            });

            modelBuilder.Entity<Patient>(e =>
            {
                e.HasOne(x => x.userDetails).WithMany(y => y.patients).HasForeignKey(x => x.added_by);
            });

            modelBuilder.Entity<Prescriptions>(e =>
            {
                e.HasOne(x => x.patient).WithMany(y => y.prescriptions).HasForeignKey(x => x.patient_id);
            });

           
            modelBuilder.Entity<Payments>(e =>
            {
                e.HasOne(x => x.prescriptions).WithOne(y => y.payments).HasForeignKey<Payments>(x => x.prescription_id);
            });

           




        }


        public DbSet<medStock> medStocks { get; set; }
        public DbSet<paidState> paidState { get; set; }

        public DbSet<Patient> patients { get; set; }
        
        public DbSet<Payments> payment { get; set; }
        public DbSet<Prescriptions> prescriptions { get; set; }

        public DbSet<userDetails> userdetails { get; set; }

        public DbSet<userType> usertypes { get; set; }

        public DbSet<Appointmentq> appointmentq { get; set; }

        public DbSet<prescriptionq> prescriptionq { get; set; }




    }
}
