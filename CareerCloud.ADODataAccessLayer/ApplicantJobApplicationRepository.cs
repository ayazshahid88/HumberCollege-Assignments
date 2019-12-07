using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;
using CareerCloud.DataAccessLayer;
using CareerCloud.Pocos;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Linq.Expressions;
using Microsoft.Extensions.Configuration;

namespace CareerCloud.ADODataAccessLayer
{

    public class ApplicantJobApplicationRepository : IDataRepository<ApplicantJobApplicationPoco>
    {
        private readonly string _connStr;
        public ApplicantJobApplicationRepository()
        {
            var config = new ConfigurationBuilder();
            var path = Path.Combine(Directory.GetCurrentDirectory(), "appsettings.json");
            config.AddJsonFile(path, false);
            var root = config.Build();
            _connStr = root.GetSection("ConnectionStrings").GetSection("DataConnection").Value;
        }

        public void Add(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                foreach (var poco in items)
                {
                    SqlCommand comm = new SqlCommand();
                    comm.Connection = connection;
                    comm.CommandText = @"INSERT INTO [dbo].[Applicant_Job_Applications]
                                       ([Id]
                                       ,[Applicant]
                                       ,[Job]
                                       ,[Application_Date])
                                 VALUES
                                       (@Id
                                       ,@Applicant
                                       ,@Job
                                       ,@Application_Date)";

                    comm.Parameters.AddWithValue("@Id", poco.Id);
                    comm.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    comm.Parameters.AddWithValue("@Job", poco.Job);
                    comm.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);

                    connection.Open();
                    int rowEffected = comm.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void CallStoredProc(string name, params Tuple<string, string>[] parameters)
        {
            throw new NotImplementedException();
        }

        public IList<ApplicantJobApplicationPoco> GetAll(params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                cmd.CommandText = @"SELECT [Id]
                                  ,[Applicant]
                                  ,[Job]
                                  ,[Application_Date]
                                  ,[Time_Stamp]
                              FROM [dbo].[Applicant_Job_Applications]";

                connection.Open();
                var reader = cmd.ExecuteReader();
                ApplicantJobApplicationPoco[] pocos = new ApplicantJobApplicationPoco[500];

                int index = 0;

                while (reader.Read())
                {
                    ApplicantJobApplicationPoco poco = new ApplicantJobApplicationPoco();
                    poco.Id = reader.GetGuid(0);
                    poco.Applicant = Guid.Parse(reader["Applicant"].ToString());
                    poco.Job = Guid.Parse(reader[2].ToString());
                    poco.ApplicationDate = reader.GetDateTime(3);
                    poco.TimeStamp = (byte[])reader[4];

                    pocos[index] = poco;
                    index++;
                }
                connection.Close();
                return pocos.Where(a => a != null).ToList();
            }
        }

        public IList<ApplicantJobApplicationPoco> GetList(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            throw new NotImplementedException();
        }

        public ApplicantJobApplicationPoco GetSingle(Expression<Func<ApplicantJobApplicationPoco, bool>> where, params Expression<Func<ApplicantJobApplicationPoco, object>>[] navigationProperties)
        {
            IQueryable<ApplicantJobApplicationPoco> pocos = GetAll().AsQueryable();
            return pocos.Where(where).FirstOrDefault();
        }

        public void Remove(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;

                foreach (var poco in items)
                {
                    cmd.CommandText = @"DELETE FROM [dbo].[Applicant_Job_Applications]
                                      WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);

                    connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }

        public void Update(params ApplicantJobApplicationPoco[] items)
        {
            using (SqlConnection connection = new SqlConnection(_connStr))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = connection;
                foreach (var poco in items)
                {
                    cmd.CommandText = @"UPDATE [dbo].[Applicant_Job_Applications]
                                       SET 
                                           [Applicant] = @Applicant
                                          ,[Job] = @Job
                                          ,[Application_Date] = @Application_Date
                                        WHERE [Id] = @Id";

                    cmd.Parameters.AddWithValue("@Id", poco.Id);
                    cmd.Parameters.AddWithValue("@Applicant", poco.Applicant);
                    cmd.Parameters.AddWithValue("@Application_Date", poco.ApplicationDate);
                    cmd.Parameters.AddWithValue("@Job", poco.Job);

                    connection.Open();
                    int rowEffected = cmd.ExecuteNonQuery();
                    connection.Close();
                }
            }
        }
    }
}
