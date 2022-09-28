using AdoFrameWork.Abstract.Services;
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

        public async Task Insert()
        {
            Console.WriteLine("Enter Query Parameter");

            var param = Console.ReadLine().Split(" ");
            Console.WriteLine("Enter TableName");
            var query = Console.ReadLine();
            Console.WriteLine("Enter Values");

            var values = Console.ReadLine().Split(" ");
            var result = await InsertParser(values, param, query);
            if (result)
                Console.WriteLine("Inserted");

        }
        private Task<bool> InsertParser(string[] values, string[] param, string table)
        {
            var query = $"insert into {table} (";
            for (var i = 0; i < param.Length - 1; i++)
            {
                query += $"{param[i]}" + ',';
            }
            query += $"{param[param.Length - 1]}" + ") values(";
            for (var i = 0; i < param.Length - 1; i++)
            {
                query += $"@{param[i]}" + ',';
            }
            query += $"@{param[param.Length - 1]}" + ")";
            try
            {
                using (var SqlCommand = new SqlCommand(query, Connection))
                {
                    for (var i = 0; i < values.Length; i++)
                    {
                        SqlCommand.Parameters.Add(new SqlParameter($"@{param[i]}", values[i]));
                        Console.WriteLine(values[i]);
                    }
                    var execute = (int)SqlCommand.ExecuteNonQuery();
                    Console.WriteLine(execute);
                }
            }
            catch (Exception ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(ex);
                return Task.FromResult(false);
            }
            Console.WriteLine(query);
            //insert into products ()
            return Task.FromResult(true);
        }
    }
}
/*
Server=DESKTOP-R0GTE5O;Database=SportsStore;Trusted_Connection=True;

*/