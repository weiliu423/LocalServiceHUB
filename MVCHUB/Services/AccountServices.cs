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
using MVCHUB.Interfaces;
using MVCHUB.Models;
using System.Globalization;

namespace MVCHUB.Services
{
    public class AccountServices : IAccountService
    {

        protected IMapper iMapper;
        protected DBContext _context;


        //Constructor
        public AccountServices()
        {       
            //MapConfig();
            _context = new DBContext();
        }

        //Get list of all clients
        public async Task<IEnumerable<string>> getAllUsers()
        {

            var names = await _context.Account.Select(g => g.userName).ToListAsync();
   
            if (names != null)
            {
                return names;
            }
            throw new Exception("No user found");
        }
        public async Task<IEnumerable<string>> getAllUsersSql()
        {

            string queryString = "select Username, Password from Account";
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
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
                        names.Add(reader["Username"].ToString()+":"+ reader["Password"].ToString());
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
            throw new Exception("No user found");
        }
        public async Task<IEnumerable<string>> getUserByName(String name)
        {

            string queryString = "select * from Account where Username = '"+name+"'";
            string connectionString = ConfigurationManager.AppSettings["ConnectionString"];
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
                        DateTime enteredDate = DateTime.Parse(reader["createDate"].ToString());
                        names.Add(reader["Username"].ToString() + ":" + enteredDate.ToString("dd-MM-yyyy") + ":" + reader["fullName"].ToString() + ":" + reader["Email"].ToString() + ":" + reader["PhoneNo"].ToString());
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
            throw new Exception("No user found");
        }
        public async Task<AccountModel> createNewAccount(AccountModel data)
        {
                Account Account = new Account();
                DateTime date = DateTime.Now;
                if (data != null)
                {
                Account.userName=data.UserName;
                Account.createDate = date;
                Account.expirationDate = date.AddYears(1); 
                Account.resourceKey = "";
                Account.Password = data.Password;
                Account.firstName = data.FirstName;
                Account.lastName = data.LastName;
                Account.fullName = data.FirstName+ " " + data.LastName;
                Account.Email = data.Email;
                Account.PhoneNo = data.PhoneNo;
                Account.isProvider = data.isProvider;

                data.FullName = data.FirstName + " " + data.LastName;
                var UserAccount = await _context.Account.FirstOrDefaultAsync(x => x.userName == data.UserName);

                    if (UserAccount != null)
                    {
                        throw new Exception("User with same username already exists");
                    }

                    _context.Account.Add(Account);

                    _context.Contexts.SaveChanges();
                    return data;
                }
                throw new Exception("No data entered");
           
        }
        public async Task<String> accountValidation(AccountModel data)
        {
            if (data != null)
            {
                
                var UserAccount = await _context.Account.FirstOrDefaultAsync(x => x.userName == data.UserName);

                if (UserAccount != null)
                {
                    var password = await _context.Account.FirstOrDefaultAsync(x => x.Password == data.Password);
                    if(password != null)
                    {
                        var fullName = await _context.Account.Where(x => x.userName == data.UserName).Select(x=> x.fullName).FirstOrDefaultAsync();
                        var isProvider = await _context.Account.Where(x => x.userName == data.UserName).Select(x => x.isProvider).FirstOrDefaultAsync();
                        return fullName + ":" + isProvider;
                    }
                    else
                    {
                        return null;
                    }
                }
                throw new Exception("User doesn't exist");



            }
            throw new Exception("No data entered");

        }
        public async Task<bool> DeleteAccount(int id)
        {

            string queryString = "Delete Account WHERE id = '" + id + "'";
            string connectionString = "Server=fyplab.database.windows.net;Database=LSHUB;User Id=wei;Password=Predator423;";
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlCommand command = new SqlCommand(queryString, connection);
                //command.Parameters.AddWithValue("@tPatSName", "Your-Parm-Value");
                connection.Open();
                SqlDataReader reader = command.ExecuteReader();

            }
            return true;


        }
   
    }
}