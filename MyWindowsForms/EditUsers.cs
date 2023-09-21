using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Windows.Forms.AxHost;
using System.Xml.Linq;
using System.Data.SqlClient;

namespace MyWindowsForms
{
    public partial class EditUsers : Form
    {

        private bool isUserIdLocked = false;

        public EditUsers(string userId, string username, string password, string email, string tel, bool disable)
        {
            InitializeComponent();

            // Hiển thị thông tin người dùng
            txtId.Text = userId;
            txtName.Text = username;
            txtPassword.Text = password;
            txtConfirmPassword.Text = password;
            txtEmail.Text = email;
            txtTel.Text = tel;
            checkDisplay.Checked = disable;

            // Khóa mã người dùng
            LockUserId();
        }

        public void LockUserId()
        {
            isUserIdLocked = true;
            txtId.Enabled = false;
        }

        public void HideNextButton()
        {
            btnNext.Enabled = false;
        }

        public EditUsers()
        {
            InitializeComponent();
        }


        private void btnSave_Click(object sender, EventArgs e)
        {

            User user = new User();
            string UserId = txtId.Text;
            string Username = txtName.Text;
            string Password = txtPassword.Text;
            string ConfirmPassword = txtConfirmPassword.Text;

            if (ConfirmPassword != Password)
            {
                MessageBox.Show("Confirm password is incorrect!", "Sign Up error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Không thực hiện thêm nếu xác nhận mật khẩu không đúng
            }

            string Email = txtEmail.Text;
            string Tel = txtTel.Text;

            int Disable = 0;
            if (checkDisplay.Checked)
            {
                Disable = 1;
            }

            // Thực hiện thêm User
            if (user.insertUser(UserId, Username, Password, Email, Tel, Disable))
            {
                MessageBox.Show("New User Added", "Add User", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Sau khi lưu thành công, làm cho nút btnSave trở thành mờ đi
                btnSave.Enabled = false;

                // Hiện nút "Nhập tiếp"
                btnNext.Enabled = true;

            }


            if (isUserIdLocked)
            {
/*                UserId = txtId.Text;
                Username = txtName.Text;
                Password = txtPassword.Text;
                ConfirmPassword = txtConfirmPassword.Text;
                if (ConfirmPassword != Password)
                {
                    MessageBox.Show("Confirm password is incorrect!", "Sign Up error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return; // Không thực hiện thêm nếu xác nhận mật khẩu không đúng
                }
                Email = txtEmail.Text;
                Tel = txtTel.Text;
                Disable = 0;
                if (checkDisplay.Checked)
                {
                    Disable = 1;
                }*/
                // Thực hiện thêm User
                if (user.editUser(UserId, Username, Password, Email, Tel, Disable))
                {
                    MessageBox.Show("Edit succesfully", "Edit User", MessageBoxButtons.OK, MessageBoxIcon.Information);

                }

            }


        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void btnNext_Click(object sender, EventArgs e)
        {
            // Hiện nút "Lưu" và làm cho nút "Nhập tiếp" mờ đi
            btnSave.Enabled = true;
            btnNext.Enabled = false;

            // Xóa dữ liệu trong các TextBox
            txtId.Text = "";
            txtName.Text = "";
            txtPassword.Text = "";
            txtConfirmPassword.Text = "";
            txtEmail.Text = "";
            txtTel.Text = "";
            checkDisplay.Checked = false;
        }

        private void EditUsers_Load(object sender, EventArgs e)
        {
            btnSave.Enabled = true;
            btnNext.Enabled = false;
        }
    }
}
