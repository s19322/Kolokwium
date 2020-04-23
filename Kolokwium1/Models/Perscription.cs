﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.Models
{
    public class Perscription
    {
        public int IdPrescription { get; set; }
        public DateTime Date { get; set; }
    
        public DateTime DueDate { get; set; }

        public int IdPatient { get; set; }

        public int IdDoctor { get; set; }

        public List<Medicament> MedicineList { get; set; }
    
    }
}
