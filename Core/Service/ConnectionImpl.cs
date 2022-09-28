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
            try
            {
                Connection = new SqlConnection(connectionQuery);
            }
            catch (SqlException ex)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.WriteLine(ex.Errors.ToString());
                return Task.FromResult(false);
            }
            return Task.FromResult(true);

        }
    }
}