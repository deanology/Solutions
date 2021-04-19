using Solution.Models;
using Solution.Models.ViewModel;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Solution.HelperClasses
{
    public class PaymentHandler
    {
        private SqlConnection _conn;
        private void connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["solutionConn"].ToString();
            _conn = new SqlConnection(connectionString);
        }
        public bool AddPayment(Payment model)
        {
            connection();
            //use the insert query command
            SqlCommand cmd = new SqlCommand("INSERT INTO Payments(Amount, PaymentDate, Balance,PatientID,PaymentTypeId, DateCreated, DateUpdated)" +
            "VALUES(@amount,@paymentDate, @balance, @patientId, @patientType,@dateCreated,@dateUpdated)", _conn);

            //assigning values to placeholders
            cmd.Parameters.AddWithValue("@amount", model.Amount);
            cmd.Parameters.AddWithValue("@paymentDate", model.PaymentDate);
            cmd.Parameters.AddWithValue("@balance", model.Balance); 
            cmd.Parameters.AddWithValue("@patientId", model.PatientId.Trim());
            cmd.Parameters.AddWithValue("@patientType", model.PaymentTypeId);
            cmd.Parameters.AddWithValue("@dateCreated", DateTime.Now);
            cmd.Parameters.AddWithValue("@dateUpdated", DateTime.Now);
            _conn.Open();
            //execute query and insert form values into database
            int i = cmd.ExecuteNonQuery();
            _conn.Close();

            if (i >= 1)
                return true;
            else
                return false;

        }
        /*public List<Payment> GetAllPayments(string searchString)
        {
            connection();
            List<Payment> payments = new List<Payment>();
            SqlCommand cmd = null;
            string query = String.Empty;

            if (String.IsNullOrEmpty(searchString))
            {
                query = "SELECT * FROM Payments";
                cmd = new SqlCommand(query, _conn);
            }

            else
            {
                *//*"  SELECT * FROM Payments INNER JOIN PaymentType ON Payments.PaymentTypeId = PaymentType.Id"*//*
                query = "SELECT * FROM Payments WHERE PatientID=@search ORDER BY Firstname";
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
                payments.Add(new Payment
                {
                    Id = Convert.ToInt32(item["Id"]),
                    Amount = Convert.ToDouble(item["Amount"]),
                    Balance = Convert.ToDouble(item["Balance"]),
                    PaymentDate = Convert.ToDateTime(item["PaymentDate"]),
                    PaymentTypeId = Convert.ToInt32(item["PaymentTypeId"]),
                    PatientId = Convert.ToString(item["PatientID"]),
                    CreatedAt = Convert.ToDateTime(item["DateCreated"]),
                    UpdatedAt = Convert.ToDateTime(item["DateUpdated"])

                });
            }
            return payments;
        }*/
        public Payment GetPayment(int id)
        {
            connection();
            Payment pt = new Payment();

            SqlCommand cmd = new SqlCommand("SELECT * FROM Payments WHERE Id = @id", _conn);
            /*SqlCommand cmd = new SqlCommand("SELECT * FROM Payments, PaymentType WHERE Payments.PaymentTypeId = PaymentType.Id", _conn);*/
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
                pt.Amount = Convert.ToDouble(item["Amount"]);
                pt.Balance = Convert.ToDouble(item["Balance"]);
                pt.PaymentDate = Convert.ToDateTime(item["PaymentDate"]);
                pt.PatientId = Convert.ToString(item["PatientID"]);
                pt.CreatedAt = Convert.ToDateTime(item["DateCreated"]);
                pt.UpdatedAt = Convert.ToDateTime(item["DateUpdated"]);
            }

            return pt;

        }
        public bool UpdatePayment(PaymentUpdate model)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Update Payments SET Amount=@amount, Balance=@balance,PaymentDate=@paymentDate,DateUpdated=@update WHERE Id = @id", _conn);
            //assigning values to placeholders
            cmd.Parameters.AddWithValue("@id", model.Id);
            cmd.Parameters.AddWithValue("@amount", model.Amount);
            cmd.Parameters.AddWithValue("@balance", model.Balance);
            cmd.Parameters.AddWithValue("@paymentDate", model.PaymentDate);
            cmd.Parameters.AddWithValue("@update", DateTime.Now);

            _conn.Open();
            //execute query and insert form values into database
            int i = cmd.ExecuteNonQuery();
            _conn.Close();
            if (i >= 1)
                return true;
            else
                return false;
        }
        public bool DeletePayment(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Delete FROM Payments WHERE Id = @id", _conn);
            cmd.Parameters.AddWithValue("@id", id);

            _conn.Open();
            int i = cmd.ExecuteNonQuery();
            _conn.Close();

            if (i >= 1)
                return true;
            else
                return false;
        }
        public List<PaymentViewModel> GetAllPayments2()
        {
            connection();
            List<PaymentViewModel> payments = new List<PaymentViewModel>();
            SqlCommand cmd = null;
            string query = String.Empty;
            query = "SELECT * FROM Payments INNER JOIN PaymentType ON Payments.PaymentTypeId = PaymentType.Id";
            cmd = new SqlCommand(query, _conn);

            //create a sqldataadapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            _conn.Open();
            adapter.Fill(dt);
            _conn.Close();
            foreach (DataRow item in dt.Rows)
            {
                payments.Add(new PaymentViewModel
                {
                    Id = Convert.ToInt32(item["Id"]),
                    Amount = Convert.ToDouble(item["Amount"]),
                    Balance = Convert.ToDouble(item["Balance"]),
                    PaymentDate = Convert.ToDateTime(item["PaymentDate"]),
                    PaymentType = Convert.ToString(item["Description"]),
                    PatientId = Convert.ToString(item["PatientID"]),
                    CreatedAt = Convert.ToDateTime(item["DateCreated"]),
                    UpdatedAt = Convert.ToDateTime(item["DateUpdated"])

                });;
            }
            return payments;
        }
    }
}