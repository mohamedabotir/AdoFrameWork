using System.Data.SqlClient;
using System.Data;


namespace AdoFrameWork.Abstract.Services
{
    public interface Connection
    {
        public string QueryString { get; set; }
        public SqlConnection Connection { get; set; }
        Task<bool> BuildConnection(string connectionQuery);
        Task<bool> SubmitQueryScaler(string query);
        Task Insert();
    }
}