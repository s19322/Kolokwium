using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Kolokwium1.Dto
{
    public class PresRequest
    {
        
        public int IdPrescription { get; set; }
        [Required]
       
        public DateTime Date { get; set; }
        [Required]
        
        public DateTime DueDate { get; set; }
        [Required]
        public int IdPatient { get; set; }
        [Required]
        public int IdDoctor { get; set; }
    }
}
