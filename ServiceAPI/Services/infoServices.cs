using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Reflection;
using AutoMapper;
using System.Data.Entity;
using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using ServiceAPI.Interfaces;
using ServiceAPI.Models;
using Newtonsoft.Json;

namespace ServiceAPI.Services
{
    public class infoServices : IinfoService
    {

        protected IMapper iMapper;
        protected DBContext _context;


        //Constructor
        public infoServices()
        {       
            //MapConfig();
            _context = new DBContext();
        }
        public async Task<IEnumerable<string>> getAllCategories()
        {

            String queryString = "select [CategoryName] from [dbo].[ServiceType]";
            string connectionString = "Server=fyplab.database.windows.net;Database=LSHUB;User Id=wei;Password=Predator423;";
            List<string> names = new List<string>();
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        names.Add(reader["CategoryName"].ToString());
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            if (names != null)
            {
                return names;
            }
            throw new Exception("No Category found");
        }

        public async Task<List<ServiceDataModel>> getAllServiceSql(string categoryName)
        {
            List<ServiceDataModel> serviceData = new List<ServiceDataModel>();
            //string queryString = "select [Name], [Description], [ImageLink], a.userName, s.CreateDate, a.[PhoneNo], a.[Email] from dbo.Services s join Account a on a.ID = s.LinkAccountId join ServiceType st on st.ServiceTypeID = s.TypeId where st.CategoryName = '" + categoryName + "'";
            string queryString = "getServicesByName ";
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
   
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@CategoryName", categoryName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                         serviceData = JsonConvert.DeserializeObject<List<ServiceDataModel>>(reader[0].ToString());
                       
                    //    services.Add(reader["Name"].ToString() + ":" + reader["Description"].ToString() + ":" + reader["ImageLink"].ToString() + ":" 
                    //        + reader["userName"].ToString() 
                    //        + ":" + reader["CreateDate"].ToString() + ":" + reader["PhoneNo"].ToString() + ":" + reader["Email"].ToString());
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            if (serviceData != null)
            {
                return serviceData;
            }
            return null;

            //throw new Exception("No user found");
        }
        
        public async Task<List<ServiceDataModel>> getAllServiceByAccount(string accountName)
        {
            List<ServiceDataModel> serviceData = new List<ServiceDataModel>();
            //string queryString = "select [Name], [Description], [ImageLink], a.userName, s.CreateDate, a.[PhoneNo], a.[Email] from dbo.Services s join Account a on a.ID = s.LinkAccountId join ServiceType st on st.ServiceTypeID = s.TypeId where st.CategoryName = '" + categoryName + "'";
            string queryString = "getServicesByAccount";
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = CommandType.StoredProcedure;
                command.Parameters.AddWithValue("@AccountName", accountName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        serviceData = JsonConvert.DeserializeObject<List<ServiceDataModel>>(reader[0].ToString());

                        //    services.Add(reader["Name"].ToString() + ":" + reader["Description"].ToString() + ":" + reader["ImageLink"].ToString() + ":" 
                        //        + reader["userName"].ToString() 
                        //        + ":" + reader["CreateDate"].ToString() + ":" + reader["PhoneNo"].ToString() + ":" + reader["Email"].ToString());
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            if (serviceData != null)
            {
                return serviceData;
            }
            return null;

            //throw new Exception("No user found");
        }

        public async Task<List<ServiceDataModel>> getAllServices()
        {
            List<ServiceDataModel> serviceData = new List<ServiceDataModel>();
            //string queryString = "select [Name], [Description], [ImageLink], a.userName, s.CreateDate, a.[PhoneNo], a.[Email] from dbo.Services s join Account a on a.ID = s.LinkAccountId join ServiceType st on st.ServiceTypeID = s.TypeId where st.CategoryName = '" + categoryName + "'";
            string queryString = "[getAllServices]";
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                command.CommandType = CommandType.StoredProcedure;
                //command.Parameters.AddWithValue("@CategoryName", categoryName);
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        serviceData = JsonConvert.DeserializeObject<List<ServiceDataModel>>(reader[0].ToString());

                        //    services.Add(reader["Name"].ToString() + ":" + reader["Description"].ToString() + ":" + reader["ImageLink"].ToString() + ":" 
                        //        + reader["userName"].ToString() 
                        //        + ":" + reader["CreateDate"].ToString() + ":" + reader["PhoneNo"].ToString() + ":" + reader["Email"].ToString());
                    }
                }
                finally
                {
                    // Always call Close when done reading.
                    reader.Close();
                }
            }
            if (serviceData != null)
            {
                return serviceData;
            }
            return null;

            //throw new Exception("No user found");
        }

        public async Task<bool> createService(infoModel data)
        {
            //Account Account = new Account();
            //DateTime date = DateTime.Now;
            var typeId = await _context.ServiceType.Where(n => n.CategoryName == data.TypeName).Select(x => x.ServiceTypeID).FirstOrDefaultAsync();
            string queryString1 = "Select ID from [dbo].[Account] where userName = '"+ data.AccountName + "'";
            int accountID = 0;
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString1, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        accountID = Convert.ToInt32(reader["ID"]);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            if (data != null)
            {
                string queryString = "  insert into [Services] values ('"+data.Name+"',"+ typeId + ", GETDATE(),"+ accountID +",'"+data.Description+"','" + data.ImageLink +"','" + data.ServiceLocation + "')";
               
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                    connection.Open();
                    command.ExecuteNonQuery();
                }
              
                return true;
            }
            throw new Exception("No data entered");

        }

        public async Task<bool> updateService(infoModel data)
        {
            //Account Account = new Account();
            //DateTime date = DateTime.Now;
            var typeId = await _context.ServiceType.Where(n => n.CategoryName == data.TypeName).Select(x => x.ServiceTypeID).FirstOrDefaultAsync();
            string queryString1 = "Select ID from [dbo].[Account] where userName = '" + data.AccountName + "'";
            int accountID = 0;
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString1, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();
                try
                {
                    while (reader.Read())
                    {
                        accountID = Convert.ToInt32(reader["ID"]);
                    }
                }
                finally
                {
                    reader.Close();
                }
            }
            if (data != null)
            {
                string queryString = " update[Services] set Name ='" + data.Name + "',TypeId =" + typeId + ", CreateDate = GETDATE(), LinkAccountId ="
                    + accountID + ",Description = '" + data.Description + "',ImageLink ='" + data.ImageLink 
                    + "',ServiceLocation ='" + data.ServiceLocation + "'  where ServiceID = " +  Convert.ToInt32(data.ServiceID);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            throw new Exception("No data entered");

        }

        public async Task<bool> deleteService(infoModel data)
        {
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
          
            if (data != null)
            {
                string queryString = " delete from [Serivces] where ServiceID = " + Convert.ToInt32(data.ServiceID);

                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            throw new Exception("No data entered");

        }
        public async Task<bool> addServiceType(ServiceType data)
        {
            //Account Account = new Account();
            //DateTime date = DateTime.Now;
            if (data != null)
            {

                string queryString = "  insert into [dbo].[ServiceType] values ('" + data.CategoryName + "')";
                string connectionString = "Server=fyplab.database.windows.net;Database=LSHUB;User Id=wei;Password=Predator423;";
                using (SqlConnection connection = new SqlConnection(connectionString))
                {
                    SqlCommand command = new SqlCommand(queryString, connection);
                    //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                    connection.Open();
                    command.ExecuteNonQuery();
                }

                return true;
            }
            throw new Exception("No data entered");

        }
    }
}