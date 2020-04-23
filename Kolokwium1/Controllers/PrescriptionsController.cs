using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Kolokwium1.Dto;
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

        [HttpPost]
        public IActionResult PostNewPrescription(PresRequest request)
        {
            request = new PresRequest();

            request.IdPrescription = new Random().Next(4, 20000);
            using (SqlConnection con = new SqlConnection("Data Source=db-mssql;Initial Catalog=s19322;Integrated Security=True"))
            using (SqlCommand com = new SqlCommand())
            {
                com.Connection = con;
                con.Open();
                com.CommandText = "select IdPrescription from Prescription where IdPrescription=@IdPrescription";
                com.Parameters.AddWithValue("IdPrescription", request.IdPrescription);
                try
                {

                    var dr = com.ExecuteReader();
                    if (dr.Read())
                    {
                        return BadRequest("This Prescription no. already exists");
                    }
                    else
                    {

                        int IdPrescription = request.IdPrescription;
                        int IdDoctor = (int)dr["IdDoctor"];
                        DateTime Date = (DateTime)dr["Date"];
                        DateTime DueDate = (DateTime)dr["DueDate"];
                        int IdPatient = (int)dr["IdPatient"];
                        if (Date <= DueDate)
                        {
                            return BadRequest("DueDate is wrong. It should be elder ");
                        }

                        com.CommandText = "insert into Prescription values(@IdPrescription,@Date,@DueDate,@IdPatient,@IdDoctor)";
                        com.Parameters.AddWithValue("IdPrescription", IdPrescription);
                        com.Parameters.AddWithValue("Date", Date);
                        com.Parameters.AddWithValue("DueDate", DueDate);
                        com.Parameters.AddWithValue("IdPatient", IdPatient);
                        com.Parameters.AddWithValue("IdDoctor", IdDoctor);
                    }

                }catch(SqlException slqex)
                {
                    Console.WriteLine(slqex);
                }
                return Ok();
                }
        }
    }
}