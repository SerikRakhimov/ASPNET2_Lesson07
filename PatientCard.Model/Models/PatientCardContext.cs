using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace PatientCard.Model.Models
{
    public class PatientCardContext:DbContext
    {
        public PatientCardContext():base("name=PatientDB")
        {
        }

        public DbSet<Patient> Patients { get; set; }
        public DbSet<Doctor> Doctors { get; set; }
        public DbSet<Journal> Journals { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Properties<DateTime>().Configure(x=>x.HasColumnType("datetime2"));
        }
    }
}