using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientCard.Model.Models
{
    public class Journal
    {
        public int Id { get; set; }
        public virtual Patient Patient { get; set; }
        public virtual Doctor Doctor { get; set; }
        public string Diagnosis { get; set; }
        public DateTime DateVisit { get; set; }
    }
}