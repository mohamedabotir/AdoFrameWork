using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;

namespace AdoFrameWork.Core.Builders
{
    public class InsertBuilder
    {
        public string Query { get; set; }
        public string[] param { get; set; }
        public SqlConnection _connection { get; set; }
        public InsertBuilder(SqlConnection connection)
        {
            _connection = connection;
        }
        public InsertBuilder InsertInto(string table, string[] param)
        {
            this.param = param;
            Query += $"insert into {table} (";
            for (var i = 0; i < param.Length - 1; i++)
            {
                Query += $"{param[i]}" + ',';
            }
            Query += $"{param[param.Length - 1]}" + ") values(";

            for (var i = 0; i < param.Length - 1; i++)
            {
                Query += $"@{param[i]}" + ',';
            }
            Query += $"@{param[param.Length - 1]}" + ")";
            return this;
        }
        public Task<bool> Values(string[] values)
        {
            try
            {
                using (var SqlCommand = new SqlCommand(Query, _connection))
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
            return Task.FromResult(true);
        }
    }
}