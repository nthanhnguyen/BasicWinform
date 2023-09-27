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
using BUS;

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

        public void HideSaveButton()
        {
            btnSave.Enabled = false;
        }

        public EditUsers()
        {
            InitializeComponent();
        }

        public void LockAll()
        {
            txtId.Enabled = false;
            txtName.Enabled = false;
            txtPassword.Enabled = false;
            txtConfirmPassword.Enabled = false;
            txtEmail.Enabled = false;
            txtTel.Enabled = false;
        }


        private void btnSave_Click(object sender, EventArgs e)
        {
            string UserId = txtId.Text;
            string Username = txtName.Text;
            string Password = txtPassword.Text;
            string ConfirmPassword = txtConfirmPassword.Text;

            if (ConfirmPassword != Password)
            {
                MessageBox.Show("Xác nhận mật khẩu không đúng!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return; // Không thực hiện thêm nếu xác nhận mật khẩu không đúng
            }

            string Email = txtEmail.Text;
            string Tel = txtTel.Text;

            int Disable = 0;
            if (checkDisplay.Checked)
            {
                Disable = 1;
            }


            if (UserBUS.Instance.CheckEmpty(UserId, Username))
            {
                try
                {
                    if (UserBUS.Instance.CheckEmail(Email))
                    {
                        MessageBox.Show("Email người dùng không hợp lệ!", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return;
                    }
                    else if (isUserIdLocked)
                    {
                        if (UserBUS.Instance.EditUser(UserId, Username, Password, Email, Tel, Disable))
                        {
                            MessageBox.Show("Đã sửa thông tin thành công", "Sửa thông tin", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        }
                    }
                    else if (UserBUS.Instance.CheckUserExists(UserId))
                    {
                        MessageBox.Show("Mã người dùng đã tồn tại. Vui lòng chọn mã người dùng khác.", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                        return; // Không thực hiện thêm hoặc sửa nếu mã người dùng đã tồn tại
                    }
                    else if (UserBUS.Instance.InsertUser(UserId, Username, Password, Email, Tel, Disable))
                    {
                        MessageBox.Show("Đã thêm người dùng thành công", "Thêm người dùng", MessageBoxButtons.OK, MessageBoxIcon.Information);

                        // Sau khi lưu thành công, làm cho nút btnSave trở thành mờ đi
                        btnSave.Enabled = false;

                        // Hiện nút "Nhập tiếp"
                        btnNext.Enabled = true;
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message, "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("Không được để trống Mã nhân viên hay Tên nhân viên.", "Lỗi!", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            MainForm mainform = new MainForm();
            this.Close();
            mainform.Show();
            
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
