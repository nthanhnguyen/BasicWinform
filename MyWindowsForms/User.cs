using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace MyWindowsForms
{
    class User
    {
        public string UserId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Email { get; set; }
        public string Tel { get; set; }
        public int Disable { get; set; }

        public User()   //Tạo student rỗng để chạy không lỗi với khởi tạo User để add vào List
        {

        }
        public User(string userid, string username, string password, string email, string tel, int disable)
        {
            this.UserId = userid;
            this.Username = username;
            this.Password = password;
            this.Email = email;
            this.Tel = tel;
            this.Disable = disable;
        }
        MyDB mydb = new MyDB();

        public DataTable GetAllUsers(SqlCommand command)
        {
            command.Connection = mydb.getConnection;
            SqlDataAdapter adapter = new SqlDataAdapter(command);
            DataTable table = new DataTable();
            adapter.Fill(table);
            return table;
        }

        public bool insertUser(string userid, string username, string password, string email, string tel, int disable)
        {
            SqlCommand command = new SqlCommand("INSERT INTO [user] (UserID, UserName, Password, Email, Tel, Disable)" +
                "VALUES (@id, @username, @password, @email, @tel, @disable) ", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = userid;
            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
            command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
            command.Parameters.Add("@tel", SqlDbType.NVarChar).Value = tel;
            command.Parameters.Add("@disable", SqlDbType.Int).Value = disable;


            mydb.openConnection();
            if ((command.ExecuteNonQuery() == 1))
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }

        public bool editUser(string userid, string username, string password, string email, string tel, int disable)
        {
            SqlCommand command = new SqlCommand("UPDATE user SET  UserName=@username, Password=@password, Email=@email, Tel=@tel, Disable=@disable WHERE UserID = @id", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.Int).Value = userid;
            command.Parameters.Add("@username", SqlDbType.VarChar).Value = username;
            command.Parameters.Add("@password", SqlDbType.VarChar).Value = password;
            command.Parameters.Add("@email", SqlDbType.DateTime).Value = email;
            command.Parameters.Add("@tel", SqlDbType.VarChar).Value = tel;
            command.Parameters.Add("@disable", SqlDbType.VarChar).Value = disable;

            mydb.openConnection();
            if ((command.ExecuteNonQuery() == 1))
            {
                mydb.closeConnection();
                return true;
            }
            else
            {
                mydb.closeConnection();
                return false;
            }
        }





    }
}
