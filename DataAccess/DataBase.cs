using System;
using MySql.Data.MySqlClient;
namespace DataAccess
{
    public class DataBase
    {
        private  MySqlConnection _connection;

        public DataBase(string server, string userId, string password, string database)
        {
            MySqlConnectionStringBuilder builder = new MySqlConnectionStringBuilder();
            builder.Server = server;
            builder.UserID = userId;
            builder.Database = database;
            builder.SslMode = MySqlSslMode.None;
            
            _connection = new MySqlConnection(builder.ToString());
        }

        internal void Command(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, _connection);
            if (OpenConnection())
                cmd.ExecuteNonQuery();
            CloseConnection();
        }

        internal MySqlDataReader CommandQuery(string query)
        {
            MySqlCommand cmd = new MySqlCommand(query, _connection);

            return OpenConnection() ? cmd.ExecuteReader() : null;
        }
        
        // ReSharper disable once MemberCanBePrivate.Global
        internal bool OpenConnection()
        {
            try
            {
                _connection.Open();
                Console.WriteLine("Conexão aberta");
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erro ao abrir conexão");
                Console.WriteLine(e);
                return false;
            }
        }

        internal bool CloseConnection()
        {
            try
            {
                _connection.Close();
                Console.WriteLine("Conexão fechada");
                return true;
            }
            catch (MySqlException e)
            {
                Console.WriteLine("Erro ao fechar conexão");
                Console.WriteLine(e);
                return false;
            }
        }
    }
}