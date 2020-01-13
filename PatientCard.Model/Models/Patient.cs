using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PatientCard.Model.Models
{
    public class Patient
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Iin { get; set; }
    }    
}