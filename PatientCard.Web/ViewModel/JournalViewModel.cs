using PatientCard.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientCard.Web.ViewModel
{
    public class JournalViewModel
    {
        public int Id { get; set; }
        public int PatientId { get; set; }
        public PatientViewModel Patient { get; set; }
        public int  DoctorId { get; set; }
        public DoctorViewModel Doctor { get; set; }
        public string Diagnosis { get; set; }
        public DateTime DateVisit { get; set; }
    }
}