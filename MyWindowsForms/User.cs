using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ListView;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;

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
            SqlCommand command = new SqlCommand("UPDATE [user] SET  UserName=@username, Password=@password, Email=@email, Tel=@tel, Disable=@disable WHERE UserID = @id", mydb.getConnection);
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


        public bool checkUserExists(string UserId)
        {
            SqlCommand command = new SqlCommand("CheckUserExists", mydb.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            // Thêm tham số đầu vào cho stored procedure
            command.Parameters.Add(new SqlParameter("@UserID", SqlDbType.NVarChar, 50));
            command.Parameters["@UserID"].Value = UserId;
            mydb.openConnection();

            try
            {
                command.ExecuteNonQuery();
                // Nếu không có lỗi, mã người dùng hợp lệ
                return false;
            }
            catch (SqlException ex)
            {
                // Xử lý lỗi trả về từ stored procedure
                return true;
            }
            finally
            {
                mydb.closeConnection();
            }
        }

        public bool checkEmpty(string UserId, string UserName) 
        {
            if (UserId.Trim() == ""
               || UserName.Trim() == "")
            {
                return false;
            }
            else 
                return true;
        }

        public bool checkUserEmail(string Email)
        {
            SqlCommand command = new SqlCommand("CheckValidEmail", mydb.getConnection);
            command.CommandType = CommandType.StoredProcedure;
            // Thêm tham số đầu vào cho stored procedure
            command.Parameters.Add(new SqlParameter("@Email", SqlDbType.NVarChar, 50));
            command.Parameters["@Email"].Value = Email;
            mydb.openConnection();
            try
            {
                command.ExecuteNonQuery();
                return false;
            }
            catch (SqlException ex)
            {
                return true;
            }
            finally
            {
                mydb.closeConnection();
            }
        }


        public bool deleteUser(string userid)
        {
            SqlCommand command = new SqlCommand("DELETE FROM [user] WHERE UserID = @id", mydb.getConnection);
            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = userid;

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
