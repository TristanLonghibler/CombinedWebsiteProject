using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Models;

namespace DataLayer
{
    public class DataRepository : IDataRepository
    {
        private readonly IConfiguration _configuration;
        private protected readonly string sqlDataSource;

        public DataRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            sqlDataSource = _configuration.GetConnectionString("CentralDatabase");
        }

        public List<Customer> GetTop10Customers()
        {
            string query = "[ReactSite].[GetTop10Customers]";
            var list = new List<Customer> { };
            try
            {
                using(SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    SqlCommand cmd = new SqlCommand(query, myCon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(MapToCustomer(reader));
                    }

                    myCon.Close();
                }
            }
            catch(Exception e)
            {
                Console.WriteLine("Error while fetching customers" + e.ToString());
            }

            return list;
        }

        public List<Customer> GetTop10Customers(string title)
        {
            string query = "[ReactSite].[GetTop10Customers]";
            var list = new List<Customer> { };
            try
            {
                using (SqlConnection myCon = new SqlConnection(sqlDataSource))
                {
                    myCon.Open();
                    SqlCommand cmd = new SqlCommand(query, myCon);
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;

                    cmd.Parameters.AddWithValue("Title", title);

                    var reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        list.Add(MapToCustomer(reader));
                    }

                    myCon.Close();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("Error while fetching customers" + e.ToString());
            }

            return list;
        }

        private Customer MapToCustomer(SqlDataReader reader)
        {
            return new Customer()
            {
                CustomerId = (int)reader["CustomerId"],
                Title = reader["Title"].ToString(),
                FirstName = reader["FirstName"].ToString(),
                LastName = reader["LastName"].ToString()
            };
        }
    }
}
