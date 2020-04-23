using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium1.Models;
using Microsoft.AspNetCore.Mvc;

namespace Kolokwium1.Controllers
{
    [ApiController]
    [Route("api/prescriptions")]

    public class PrescriptionsController : Controller
    {

        [HttpGet("{IdPrescription}")]
        public IActionResult getPrescripton(int IdPrescription)
        {
            var pres = new Perscription();

            string sqlConString = "Data Source=db-mssql;Initial Catalog=s19322;Integrated Security=True";
            using (SqlConnection con = new SqlConnection(sqlConString))
            using (SqlCommand comand = new SqlCommand())
            {


                //  return NotFound("Nie znaleziono studenta");
                comand.Connection = con;
                comand.CommandText = "select * from Prescription p inner join Prescription_Medicament pm on p.IdPrescription = pm.IdPrescription inner join Medicament m on m.IdMedicament = pm.IdMedicament where p.IdPrescription=@IdPrescription";


                comand.Parameters.AddWithValue("IdPrescription", IdPrescription);

                con.Open();
                SqlDataReader dr = comand.ExecuteReader();


                while (dr.Read())
                {
                    pres.IdPrescription = (int)dr["IdPrescription"];
                    pres.Date = (DateTime)dr["Date"];
                    pres.DueDate = (DateTime)dr["DueDate"];
                    pres.IdDoctor = (int)dr["IdDoctor"];
                    pres.IdPatient = (int)dr["IdPatient"];
                    var med = new Medicament()
                    {
                        Description = dr["Description"].ToString(),
                        Name = dr["Name"].ToString(),
                        IdMedicament = (int)dr["IdMedicament"],
                        Type = dr["Type"].ToString()
                    };
               //  pres.MedicineList.Add(med);??? nie dziala lista !!!!

                
            }
        }
            return Ok(pres);
        }
        

    }
}