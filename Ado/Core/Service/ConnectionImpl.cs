using AdoFrameWork.Abstract.Services;
using AdoFrameWork.Core.Builders;
using System.Data.SqlClient;

namespace AdoFrameWork.Core.Service
{
    public class ConnectionImpl : Connection
    {
        public string QueryString { get; set; }
        public SqlConnection Connection { get; set; }

        public ConnectionImpl()
        {
            QueryString = Environment.MachineName;
        }
        public ConnectionImpl(string query)
        {
            QueryString = query;
        }

        public Task<bool> BuildConnection(string connectionQuery)
        {
            QueryString = connectionQuery;
            try
            {

                Connection = new SqlConnection(connectionQuery);
                Connection.Open();
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                return Task.FromResult(false);
            }
            return Task.FromResult(true);

        }

        public Task<bool> SubmitQueryScaler(string query)
        {
            try
            {

                using (var Sqlcommand = new SqlCommand(query, Connection))
                {

                    var execute = (int)Sqlcommand.ExecuteScalar();
                    Console.WriteLine(execute);
                    return Task.FromResult(true);
                }
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                return Task.FromResult(false);
            }
        }

        public async Task<bool> Insert()
        {
            Console.WriteLine("Enter Query Parameter");

            var param = Console.ReadLine().Split(" ");
            Console.WriteLine("Enter TableName");
            var query = Console.ReadLine();
            Console.WriteLine("Enter Values");

            var values = Console.ReadLine().Split(" ");
            var result = await InsertParser(values, param, query);
            Console.WriteLine($"Insert Status:{result}");
           return result;

        }
        public async Task<bool> InsertParser(string[] values, string[] param, string table)
        {
            var queryBuilder = new InsertBuilder(Connection);
            var result = await queryBuilder.InsertInto(table, param)
             .Values(values);
            return result;
        }
    }
}
/*
Server=.;Database=master;Trusted_Connection=True;

*/