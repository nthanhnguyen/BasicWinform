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

namespace MyWindowsForms
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        User user = new User();

        private void MainForm_Load(object sender, EventArgs e)
        {
            fillGrid();
        }

        private void fillGrid()
        {
            // Thực hiện truy vấn SQL và lấy dữ liệu từ bảng Users
            SqlCommand sqlCommand = new SqlCommand("SELECT * FROM [user]");
            DataTable dataTable = user.GetAllUsers(sqlCommand);

            // Thêm cột số thứ tự vào DataTable
            dataTable.Columns.Add("Số Thứ Tự", typeof(int));
            int rowIndex = 1;
            foreach (DataRow row in dataTable.Rows)
            {
                row["Số Thứ Tự"] = rowIndex;
                rowIndex++;
            }

            // Thêm cột Disable là checkbox vào DataTable
            dataTable.Columns.Add("Không hiển thị", typeof(bool));
            foreach (DataRow row in dataTable.Rows)
            {
                object disabledValue = row["Disable"];
                if (disabledValue != DBNull.Value && int.TryParse(disabledValue.ToString(), out int disabledIntValue))
                {
                    row["Không hiển thị"] = disabledIntValue == 1;
                }
                else
                {
                    row["Không hiển thị"] = false; // Hoặc bạn có thể đặt giá trị mặc định khác nếu cần
                }
            }

            // Đặt DataGridView.DataSource bằng DataTable đã chỉnh sửa
            dataGridView1.DataSource = dataTable;
            dataGridView1.Columns["Số Thứ Tự"].DisplayIndex = 0;// Đặt lại tên hiển thị cho các cột
            dataGridView1.Columns["UserID"].HeaderText = "Mã nhân viên";
            dataGridView1.Columns["UserName"].HeaderText = "Tên nhân viên";
            dataGridView1.Columns["Password"].Visible = false;
            dataGridView1.Columns["Email"].HeaderText = "Email";
            dataGridView1.Columns["Tel"].HeaderText = "Số điện thoại";
            dataGridView1.Columns["Disable"].Visible = false;
            dataGridView1.Columns["Không hiển thị"].HeaderText = "Không hiển thị";
        }



        private void btnAdd_Click(object sender, EventArgs e)
        {
            EditUsers editUsers = new EditUsers();
            this.Hide();
            editUsers.Show();
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            fillGrid();
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
            string userId = dataGridView1.SelectedRows[0].Cells["UserID"].Value.ToString();
            DialogResult dg = MessageBox.Show("Bạn muốn xáo User này?", "Xóa", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
            if (dg == DialogResult.Yes)
            {
                try
                {
                    if (user.deleteUser(userId))
                    {
                        MessageBox.Show("Xóa thành công User", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Information);
                        fillGrid();
                    }
                    else
                    {
                        MessageBox.Show("Lỗi!", "Xóa", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        private void btnClose_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
