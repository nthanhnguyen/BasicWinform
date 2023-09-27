﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DTO;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.StartPanel;
using static System.Net.Mime.MediaTypeNames;

namespace DAL
{
    /// <summary>
    /// Đại diện cho một lớp truy cập dữ liệu để quản lý dữ liệu người dùng.
    /// </summary>
    public class UserDAL
    {
        #region Constructor
        private static UserDAL instance;

        /// <summary>
        /// Hàm tạo riêng tư để đảm bảo mô hình singleton.
        /// </summary>
        private UserDAL() { }
        #endregion


        #region Properties
        /// <summary>
        /// Lấy thể hiện của lớp UserDAL.
        /// </summary>
        public static UserDAL Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDAL();
                }
                return instance;
            }
        }
        #endregion

        #region Public Methods
        /// <summary>
        /// Truy xuất tất cả người dùng từ cơ sở dữ liệu.
        /// </summary>
        /// <returns>Một DataTable chứa dữ liệu người dùng.</returns>
        public DataTable GetAllUsers()
        {
            string query = "SELECT * FROM [user]";
            return DataProvider.Instance.ExecuteQuery(query);
        }

        /// <summary>
        /// Xóa người dùng theo ID.
        /// </summary>
        /// <param name="userid">ID người dùng cần xóa.</param>
        /// <returns>Trả về true nếu xóa thành công; ngược lại, trả về false.</returns>
        public bool deleteUser(string userid)
        {
            string query = "DELETE FROM [user] WHERE UserID = @id";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { userid }) > 0;
        }

        /// <summary>
        /// Chèn người dùng mới vào cơ sở dữ liệu.
        /// </summary>
        /// <param name="userid">ID người dùng.</param>
        /// <param name="username">Tên người dùng.</param>
        /// <param name="password">Mật khẩu.</param>
        /// <param name="email">Địa chỉ email.</param>
        /// <param name="tel">Số điện thoại.</param>
        /// <param name="disable">Trạng thái vô hiệu hóa.</param>
        /// <returns>Trả về true nếu chèn thành công; ngược lại, trả về false.</returns>
        public bool insertUser(string userid, string username, string password, string email, string tel, int disable)
        {
            string query = "INSERT INTO [user] (UserID, UserName, Password, Email, Tel, Disable)" +
                "VALUES (@id, @username, @password, @email, @tel, @disable)";
            SqlCommand command = new SqlCommand();
            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = userid;
            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
            command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
            command.Parameters.Add("@tel", SqlDbType.NVarChar).Value = tel;
            command.Parameters.Add("@disable", SqlDbType.Int).Value = disable;
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { userid, username, password, email, tel, disable }) > 0;
        }

        /// <summary>
        /// Sửa thông tin người dùng.
        /// </summary>
        /// <param name="userid">ID người dùng.</param>
        /// <param name="username">Tên người dùng mới.</param>
        /// <param name="password">Mật khẩu mới.</param>
        /// <param name="email">Địa chỉ email mới.</param>
        /// <param name="tel">Số điện thoại mới.</param>
        /// <param name="disable">Trạng thái vô hiệu hóa mới.</param>
        /// <returns>Trả về true nếu sửa thông tin thành công; ngược lại, trả về false.</returns>
        public bool editUser(string userid, string username, string password, string email, string tel, int disable)
        {
            string query = "UPDATE [user] SET  UserName = @username, Password = @password, Email = @email, Tel = @tel, Disable = @disable WHERE UserID = @id";
            SqlCommand command = new SqlCommand();
            command.Parameters.Add("@id", SqlDbType.NVarChar).Value = userid;
            command.Parameters.Add("@username", SqlDbType.NVarChar).Value = username;
            command.Parameters.Add("@password", SqlDbType.NVarChar).Value = password;
            command.Parameters.Add("@email", SqlDbType.NVarChar).Value = email;
            command.Parameters.Add("@tel", SqlDbType.NVarChar).Value = tel;
            command.Parameters.Add("@disable", SqlDbType.Int).Value = disable;
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { userid, username, password, email, tel, disable }) > 0;
        }

        /// <summary>
        /// Kiểm tra sự tồn tại của người dùng theo ID.
        /// </summary>
        /// <param name="userid">ID người dùng cần kiểm tra.</param>
        /// <returns>Trả về true nếu người dùng tồn tại; ngược lại, trả về false.</returns>
        public bool CheckUserExists(string userid)
        {
            string query = "EXECUTE [dbo].[CheckUserExists] UserID = @userid";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { userid }) > 0;
        }

        /// <summary>
        /// Kiểm tra tính hợp lệ của địa chỉ email.
        /// </summary>
        /// <param name="email">Địa chỉ email cần kiểm tra.</param>
        /// <returns>Trả về true nếu địa chỉ email hợp lệ; ngược lại, trả về false.</returns>
        public bool CheckEmail(string email)
        {
            string query = "EXECUTE [dbo].[CheckValidEmail] Email = @email";
            return DataProvider.Instance.ExecuteNoneQuery(query, new object[] { email }) > 0;
        }
        #endregion

    }

}

