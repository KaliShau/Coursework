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
        
        
        string StrConnection = "Server=localhost; port=5432; User Id=postgres ;Password=postgres;database=prms;";
        NpgsqlConnection Con;
        NpgsqlCommand Cmd;
        

        public void connection()
        {
            Con = new NpgsqlConnection();
            Con.ConnectionString = StrConnection;
            
            if (Con.State == System.Data.ConnectionState.Closed)
            {
                Con.Open();
            }
        }

        public DataTable getData(string sql)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = sql;

            NpgsqlDataReader dr = Cmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }

        public DataTable login( string Login, string Pass)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "SELECT * FROM \"User\" WHERE email = :login AND password = :pass";
            Cmd.Parameters.AddWithValue("login", Login);
            Cmd.Parameters.AddWithValue("pass", Pass);

            NpgsqlDataReader dr = Cmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }

        public DataTable getCurrentDepartament(int departmentId)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "SELECT * FROM \"Department\" WHERE id = :departmentId";
            Cmd.Parameters.AddWithValue("departmentId", departmentId);

            NpgsqlDataReader dr = Cmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }

        public DataTable register(string Login, string pass, string Name, int departamentId)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            dt = this.login(Login, pass);

            if (dt.Rows.Count > 0)
            {
                DataTable error = new DataTable();
                return error;
            }

            Cmd.CommandText = "INSERT INTO \"User\"(\"FIO\",email,password,\"departmentId\") VALUES(:name,:login,:pass, :departamentId);";
            Cmd.Parameters.AddWithValue("name", Name);
            Cmd.Parameters.AddWithValue("login", Login);
            Cmd.Parameters.AddWithValue("pass", pass);
            Cmd.Parameters.AddWithValue("departamentId", departamentId);
            Cmd.ExecuteReader();


            dt = this.login(Login, pass);

            return dt;
        }

        public DataTable getSearchPatient(string keyword)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "SELECT id as ID, created_at as \"Дата записи\", \"FIO\" as ФИО, record_type as \"Тип записи\", address as Адрес, content as Содержимое, \"userId\" as \"ID Врача\" FROM \"Patient\" \r\nWHERE CONCAT_WS(\"FIO\", record_type, address) LIKE :keyword";
            Cmd.Parameters.AddWithValue("keyword", '%' + keyword + '%');

            NpgsqlDataReader dr = Cmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }

        public DataTable getDepartamenthPatient(int departamentId)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "SELECT id as ID, created_at as \"Дата записи\", \"FIO\" as ФИО, record_type as \"Тип записи\", address as Адрес, content as Содержимое, \"userId\" as \"ID Врача\" FROM \"Patient\" \r\nWHERE \"departmentId\" = :departamentId ";
            Cmd.Parameters.AddWithValue("departamentId", departamentId);

            NpgsqlDataReader dr = Cmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }

        public DataTable getPatientId(int id)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "SELECT id as ID, created_at as \"Дата записи\", \"FIO\" as \"ФИО\", record_type as \"Тип записи\", address as Адрес, content as Содержимое, \"userId\" \"ID Врача\" FROM \"Patient\" WHERE \"userId\" = :id";
            Cmd.Parameters.AddWithValue("id", id);

            NpgsqlDataReader dr = Cmd.ExecuteReader();
            dt.Load(dr);
            return dt;
        }

        public void updataPatient(int id, string fio, string record, string address, string content)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "UPDATE \"Patient\" SET \"FIO\" = :fio, record_type = :record, address = :address, content = :content WHERE id = :id";
            Cmd.Parameters.AddWithValue("id", id);
            Cmd.Parameters.AddWithValue("fio", fio);
            Cmd.Parameters.AddWithValue("record", record);
            Cmd.Parameters.AddWithValue("address", address);
            Cmd.Parameters.AddWithValue("content", content);
            Cmd.ExecuteReader();
        }

        public void deletePatient(int id)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "DELETE FROM \"Patient\" WHERE id = :id";
            Cmd.Parameters.AddWithValue("id", id);
            Cmd.ExecuteReader();
        }

        public void createPatient(int userId, string fio, string record, string address, string content, int departmentId)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "INSERT INTO \"Patient\"(\"FIO\",address,record_type,content,\"userId\", \"departmentId\") VALUES(:fio,:address,:record,:content,:userId,:departmentId);";
            Cmd.Parameters.AddWithValue("userId", userId);
            Cmd.Parameters.AddWithValue("fio", fio);
            Cmd.Parameters.AddWithValue("record", record);
            Cmd.Parameters.AddWithValue("address", address);
            Cmd.Parameters.AddWithValue("content", content);
            Cmd.Parameters.AddWithValue("departmentId", departmentId);
            Cmd.ExecuteReader();
            
        }

        public void createDepartament(string name)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "INSERT INTO \"Department\"(\"name\") VALUES(:name)";
            Cmd.Parameters.AddWithValue("name", name);
            Cmd.ExecuteReader();

        }

        public void updateDepartament(string name, int id)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "UPDATE \"Department\" SET \"name\" = :name WHERE id = :id";
            Cmd.Parameters.AddWithValue("name", name);
            Cmd.Parameters.AddWithValue("id", id);
            Cmd.ExecuteReader();

        }

        public void deleteDepartament(int id)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "DELETE FROM \"Department\" WHERE \"id\"= :id";
            Cmd.Parameters.AddWithValue("id", id);
            Cmd.ExecuteReader();
        }

        public void updataUser(int id, string fio, string email, string password)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "UPDATE \"User\" SET \"FIO\" = :fio, password = :pass, email = :email WHERE id = :id";
            Cmd.Parameters.AddWithValue("id", id);
            Cmd.Parameters.AddWithValue("fio", fio);
            Cmd.Parameters.AddWithValue("pass", password);
            Cmd.Parameters.AddWithValue("email", email);
            Cmd.ExecuteReader();
        }

        public void deleteUser(int id)
        {
            DataTable dt = new DataTable();
            connection();
            Cmd = new NpgsqlCommand();
            Cmd.Connection = Con;
            Cmd.CommandText = "DELETE FROM \"User\" WHERE id = :id";
            Cmd.Parameters.AddWithValue("id", id);
            Cmd.ExecuteReader();
        }
    }
}
