using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using BUS;

namespace MyWindowsForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        //User user = new User();

        private void MainForm_Load(object sender, EventArgs e)
        {
            UserBUS.Instance.FillGrid(dataGridView1);
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditUsers editUsers = new EditUsers();
            this.Hide();
            editUsers.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            UserBUS.Instance.FillGrid(dataGridView1);
        }

        private void btnEdit_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có người dùng nào được chọn trong DataGridView chưa
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy thông tin người dùng được chọn
                string userId = dataGridView1.SelectedRows[0].Cells["UserID"].Value.ToString();
                string username = dataGridView1.SelectedRows[0].Cells["UserName"].Value.ToString();
                string password = dataGridView1.SelectedRows[0].Cells["Password"].Value.ToString();
                string email = dataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
                string tel = dataGridView1.SelectedRows[0].Cells["Tel"].Value.ToString();
                bool disable = (bool)dataGridView1.SelectedRows[0].Cells["Không hiển thị"].Value;

                // Tạo một instance của EditUsers và truyền thông tin người dùng
                EditUsers editUsers = new EditUsers(userId, username, password, email, tel, disable);
                this.Hide();
                editUsers.Show();

                // Ẩn nút "Nhập tiếp"
                editUsers.HideNextButton();

                // Khóa mã người dùng
                editUsers.LockUserId();
            }
        }

        private void btnView_Click(object sender, EventArgs e)
        {
            // Kiểm tra xem có người dùng nào được chọn trong DataGridView chưa
            if (dataGridView1.SelectedRows.Count > 0)
            {
                // Lấy thông tin người dùng được chọn
                string userId = dataGridView1.SelectedRows[0].Cells["UserID"].Value.ToString();
                string username = dataGridView1.SelectedRows[0].Cells["UserName"].Value.ToString();
                string password = dataGridView1.SelectedRows[0].Cells["Password"].Value.ToString();
                string email = dataGridView1.SelectedRows[0].Cells["Email"].Value.ToString();
                string tel = dataGridView1.SelectedRows[0].Cells["Tel"].Value.ToString();
                bool disable = (bool)dataGridView1.SelectedRows[0].Cells["Không hiển thị"].Value;

                // Tạo một instance của EditUsers và truyền thông tin người dùng
                EditUsers editUsers = new EditUsers(userId, username, password, email, tel, disable);
                this.Hide();
                editUsers.Show();

                // Ẩn nút "Nhập tiếp"
                editUsers.HideNextButton();

                // Ẩn nút "Lưu"
                editUsers.HideSaveButton();

                editUsers.LockAll();
            }
        }

        private void btnDelete_Click(object sender, EventArgs e)
        {
            DialogResult result = MessageBox.Show("Bạn có muốn xoá loại nhân viên này không", "Thông báo", MessageBoxButtons.OKCancel, MessageBoxIcon.Question);
            if (result == DialogResult.OK)
            {
                if (UserBUS.Instance.DeleteUser(dataGridView1))
                {
                    MessageBox.Show("Xoá thành công", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
                else
                    MessageBox.Show("Xoá thất bại, đã tồn tại nhân viên loại này", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Error);

            }
            UserBUS.Instance.FillGrid(dataGridView1);
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
