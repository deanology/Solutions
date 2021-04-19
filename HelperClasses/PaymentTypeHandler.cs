using Solution.Interfaces;
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
    public class PaymentTypeHandler
    {
        private SqlConnection _conn;
        private void connection()
        {
            string connectionString = ConfigurationManager.ConnectionStrings["solutionConn"].ToString();
            _conn = new SqlConnection(connectionString);
        }

        //******************** ADD NEW PAYMENT TYPE ********************//
        public bool AddPaymentType(PaymentType model)
        {
            connection();
            //use the insert query command
            SqlCommand cmd = new SqlCommand("INSERT INTO PaymentType(Type, Description, DateCreated, DateUpdated)" +
            "VALUES(@type, @description, @dateCreated, @dateUpdated)", _conn);

            //assigning values to placeholders
            cmd.Parameters.AddWithValue("@type", model.Type.Trim());
            cmd.Parameters.AddWithValue("@description", model.Description.Trim());
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
        public List<PaymentType> GetAllPaymentTypes()
        {
            connection();
            List<PaymentType> paymentTypesList = new List<PaymentType>();
            SqlCommand cmd = new SqlCommand("SELECT * FROM PaymentType", _conn);

            //vreate a sqldataadapter
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            _conn.Open();
            adapter.Fill(dt);
            _conn.Close();
            foreach (DataRow item in dt.Rows)
            {
                paymentTypesList.Add(new PaymentType
                {
                    Id = Convert.ToInt32(item["Id"]),
                    Type = Convert.ToString(item["Type"]),
                    Description = Convert.ToString(item["Description"]),
                    CreatedAt = Convert.ToDateTime(item["DateCreated"]),
                    UpdatedAt = Convert.ToDateTime(item["DateUpdated"])
                });
            }
            return paymentTypesList;
        }
        public PaymentType GetPaymentType(int id)
        {
            connection();
            PaymentType pt = new PaymentType();
            
            SqlCommand cmd = new SqlCommand("SELECT * FROM PaymentType WHERE Id = @id", _conn);
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
                pt.Type = Convert.ToString(item["Type"]);
                pt.Description = Convert.ToString(item["Description"]);
                pt.CreatedAt = Convert.ToDateTime(item["DateCreated"]);
                pt.UpdatedAt = Convert.ToDateTime(item["DateUpdated"]);
            }

            return pt;
            
        }
        public bool UpdatePaymentType(PaymentType model)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Update PaymentType SET Type=@type, Description=@desc, DateUpdated=@update WHERE Id = @id", _conn);
            //assigning values to placeholders
            cmd.Parameters.AddWithValue("@id", model.Id);
            cmd.Parameters.AddWithValue("@type", model.Type.Trim());
            cmd.Parameters.AddWithValue("@desc", model.Description.Trim());
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
        public bool DeletePaymentType(int id)
        {
            connection();
            SqlCommand cmd = new SqlCommand("Delete FROM PaymentType WHERE Id = @id", _conn);
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