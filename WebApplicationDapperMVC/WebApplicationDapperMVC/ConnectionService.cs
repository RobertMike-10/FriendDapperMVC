using System;

namespace WebApplicationDapperMVC
{

    public interface IConnectionService
    {
        string GetConnectionString();
    }
    public class ConnectionService: IConnectionService
    {
        private String _connectionString;
        public ConnectionService(string conn)
        {
            _connectionString = conn;
        }
        public string GetConnectionString()
        {
            return _connectionString;
        }
    }
}
