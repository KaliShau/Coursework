using Npgsql;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRMS
{
    public class DB
    {
        
        
        string vStrConnection = "Server=localhost; port=5432; User Id=postgres ;Password=postgres;database=prms;";
        NpgsqlConnection vCon;
        NpgsqlCommand vCmd;
        

        public void connection()
        {
            vCon = new NpgsqlConnection();
            vCon.ConnectionString = vStrConnection;
            
            if (vCon.State == System.Data.ConnectionState.Closed)
            {
                vCon.Open();
            }
        }

        public DataTable getData(string sql)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = sql;

            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }

        public DataTable login( string Login, string Pass)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = "SELECT * FROM \"User\" WHERE email = :login AND password = :pass";
            vCmd.Parameters.AddWithValue("login", Login);
            vCmd.Parameters.AddWithValue("pass", Pass);

            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }

        public DataTable register(string Login, string pass, string Name)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            dt = this.login(Login, pass);

            if (dt.Rows.Count > 0)
            {
                DataTable error = new DataTable();
                return error;
            }

            vCmd.CommandText = "INSERT INTO \"User\"(\"FIO\",email,password) VALUES(:name,:login,:pass);";
            vCmd.Parameters.AddWithValue("name", Name);
            vCmd.Parameters.AddWithValue("login", Login);
            vCmd.Parameters.AddWithValue("pass", pass);
            vCmd.ExecuteReader();


            dt = this.login(Login, pass);

            return dt;
        }

        public DataTable getSearchPatient(string keyword)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = "SELECT id as ID, created_at as \"Дата записи\", \"FIO\" as ФИО, record_type as \"Тип записи\", address as Адрес, content as Содержимое, \"userId\" as \"ID Врача\" FROM \"Patient\" \r\nWHERE CONCAT_WS(\"FIO\", record_type, address) LIKE :keyword";
            vCmd.Parameters.AddWithValue("keyword", '%' + keyword + '%');

            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }

        public DataTable getPatientId(int id)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = "SELECT id as ID, created_at as \"Дата записи\", \"FIO\" as \"ФИО\", record_type as \"Тип записи\", address as Адрес, content as Содержимое, \"userId\" \"ID Врача\" FROM \"Patient\" WHERE \"userId\" = :id";
            vCmd.Parameters.AddWithValue("id", id);

            NpgsqlDataReader dr = vCmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }

        public void updataPatient(int id, string fio, string record, string address, string content)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = "UPDATE \"Patient\" SET \"FIO\" = :fio, record_type = :record, address = :address, content = :content WHERE id = :id";
            vCmd.Parameters.AddWithValue("id", id);
            vCmd.Parameters.AddWithValue("fio", fio);
            vCmd.Parameters.AddWithValue("record", record);
            vCmd.Parameters.AddWithValue("address", address);
            vCmd.Parameters.AddWithValue("content", content);
            vCmd.ExecuteReader();
        }

        public void deletePatient(int id)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = "DELETE FROM \"Patient\" WHERE id = :id";
            vCmd.Parameters.AddWithValue("id", id);
            vCmd.ExecuteReader();
        }

        public void createPatient(int userId, string fio, string record, string address, string content)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = "INSERT INTO \"Patient\"(\"FIO\",address,record_type,content,\"userId\") VALUES(:fio,:address,:record,:content,:userId);";
            vCmd.Parameters.AddWithValue("userId", userId);
            vCmd.Parameters.AddWithValue("fio", fio);
            vCmd.Parameters.AddWithValue("record", record);
            vCmd.Parameters.AddWithValue("address", address);
            vCmd.Parameters.AddWithValue("content", content);
            vCmd.ExecuteReader();
            
        }

        public void updataUser(int id, string fio, string email, string password)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = "UPDATE \"User\" SET \"FIO\" = :fio, password = :pass, email = :email WHERE id = :id";
            vCmd.Parameters.AddWithValue("id", id);
            vCmd.Parameters.AddWithValue("fio", fio);
            vCmd.Parameters.AddWithValue("pass", password);
            vCmd.Parameters.AddWithValue("email", email);
            vCmd.ExecuteReader();
        }

        public void deleteUser(int id)
        {
            DataTable dt = new DataTable();
            connection();
            vCmd = new NpgsqlCommand();
            vCmd.Connection = vCon;
            vCmd.CommandText = "DELETE FROM \"User\" WHERE id = :id";
            vCmd.Parameters.AddWithValue("id", id);
            vCmd.ExecuteReader();
        }
    }
}
