using Solution.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Solution.HelperClasses
{
    public class PatientService
    {
        private SqlConnection _conn;
        private void connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["solutionConn"].ToString();
            _conn = new SqlConnection(connectionString);
        }
        public bool AddPatient(Patient model)
        {
            connection();
            //use the insert query command
            SqlCommand cmd = new SqlCommand("INSERT INTO Patients(Firstname, Lastname, Email, PhoneNumber, DateOfBirth, GenerateID, DateCreated, DateUpdated)" +
            "VALUES(@fname, @lname, @email, @phone, @dob, @generateId, @created, @updated)", _conn);

            //assigning values to placeholders
            cmd.Parameters.AddWithValue("@fname", model.FirstName.Trim());
            cmd.Parameters.AddWithValue("@lname", model.LastName.Trim());
            cmd.Parameters.AddWithValue("@email", model.EmailAddress.Trim());
            cmd.Parameters.AddWithValue("@phone", model.PhoneNumber.Trim());
            cmd.Parameters.AddWithValue("@dob", model.DOB);
            cmd.Parameters.AddWithValue("@generateId", PatientID.CaseIDGeneration(3));
            cmd.Parameters.AddWithValue("@created", DateTime.Now);
            cmd.Parameters.AddWithValue("@updated", DateTime.Now);

            _conn.Open();
            //execute query and insert form values into database
            int i = cmd.ExecuteNonQuery();
            _conn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public List<Patient> GetAllPatients(string searchString)
        {
            connection();
            List<Patient> patients = new List<Patient>();
            SqlCommand cmd = null;
            string query = String.Empty;
            
            if (String.IsNullOrEmpty(searchString))
            {
                query = "SELECT * FROM Patients ORDER BY Lastname";
                cmd = new SqlCommand(query, _conn);
            }

            else
            {
                query = "SELECT * FROM Patients WHERE Firstname=@search OR Lastname=@search OR Email=@search OR GenerateID=@search ORDER BY Firstname";
                cmd = new SqlCommand(query, _conn);
                cmd.Parameters.AddWithValue("@search", searchString);
            }
            
            //create a sqldataadapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            _conn.Open();
            adapter.Fill(dt);
            _conn.Close();
            foreach (DataRow item in dt.Rows)
            {
                patients.Add(new Patient
                {
                    Id = Convert.ToInt32(item["Id"]),
                    FirstName = Convert.ToString(item["Firstname"]),
                    LastName = Convert.ToString(item["Lastname"]),
                    EmailAddress = Convert.ToString(item["Email"]),
                    PhoneNumber = Convert.ToString(item["PhoneNumber"]),
                    DOB = Convert.ToDateTime(item["DateOfBirth"]),
                    GenerateID = Convert.ToString(item["GenerateID"]),
                    CreatedAt = Convert.ToDateTime(item["DateCreated"]),
                    UpdatedAt = Convert.ToDateTime(item["DateUpdated"])

                });
            }
            return patients;
        }
        public Patient GetPatient(int id)
        {
            connection();
            Patient pt = new Patient();
            SqlCommand cmd = new SqlCommand("SELECT * FROM Patients WHERE Id = @id", _conn);
            cmd.Parameters.AddWithValue("@id", id);
            //create a sqldataadapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            _conn.Open();
            adapter.Fill(dt);
            _conn.Close();

            foreach (DataRow item in dt.Rows)
            {
                pt.Id = Convert.ToInt32(item["Id"]);
                pt.FirstName = Convert.ToString(item["Firstname"]);
                pt.LastName = Convert.ToString(item["Lastname"]);
                pt.EmailAddress = Convert.ToString(item["Email"]);
                pt.PhoneNumber = Convert.ToString(item["PhoneNumber"]);
                pt.DOB = Convert.ToDateTime(item["DateOfBirth"]);
                pt.GenerateID = Convert.ToString(item["GenerateID"]);
                pt.CreatedAt = Convert.ToDateTime(item["DateCreated"]);
                pt.UpdatedAt = Convert.ToDateTime(item["DateUpdated"]);
            }
            return pt;

        }
        public bool UpdatePatient(Patient model)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Update Patients SET " +
                "Firstname=@fname, Lastname=@lname, Email=@email, PhoneNumber=@phone, DateOfBirth=@dob, DateUpdated=@updated WHERE Id = @id", _conn);
            //assigning values to placeholders
            cmd.Parameters.AddWithValue("@id", model.Id);
            cmd.Parameters.AddWithValue("@fname", model.FirstName.Trim());
            cmd.Parameters.AddWithValue("@lname", model.LastName.Trim());
            cmd.Parameters.AddWithValue("@email", model.EmailAddress.Trim());
            cmd.Parameters.AddWithValue("@phone", model.PhoneNumber.Trim());
            cmd.Parameters.AddWithValue("@dob", model.DOB);
            cmd.Parameters.AddWithValue("@updated", DateTime.Now);

            _conn.Open();
            //execute query and insert form values into database
            int i = cmd.ExecuteNonQuery();
            _conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        /// <summary>
        /// should be able to delete all payments relating to this user also
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public bool DeletePatient(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Delete FROM Patients WHERE Id = @id", _conn);
            cmd.Parameters.AddWithValue("@id", id);

            _conn.Open();
            int i = cmd.ExecuteNonQuery();
            _conn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }

    }
}