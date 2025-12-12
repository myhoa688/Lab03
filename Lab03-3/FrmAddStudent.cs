// FrmAddStudent.cs
using System;
using System.Windows.Forms;

namespace Lab03_3
{
    public partial class FrmAddStudent : Form
    {
        public delegate void AddStudentDelegate(Student student);
        public AddStudentDelegate OnAddStudent;
        public Student EditingStudent { get; set; } = null;

        public FrmAddStudent()
        {
            InitializeComponent(); // ✅ OK – vì InitializeComponent() tồn tại trong .Designer.cs
        }

        private void FrmAddStudent_Load(object sender, EventArgs e)
        {
            cmbFaculty.Items.Clear();
            cmbFaculty.Items.AddRange(new string[] {
                "Công nghệ thông tin",
                "Ngôn ngữ Anh",
                "Quản trị kinh doanh"
            });

            if (EditingState != null)
            {
                txtID.Text = EditingState.StudentID;
                txtName.Text = EditingState.FullName;
                txtScore.Text = EditingState.AverageScore.ToString();
                cmbFaculty.SelectedItem = EditingState.Faculty;
                if (EditingState.Gender == "Nam")
                    rdbMale.Checked = true;
                else
                    rdbFemale.Checked = true;

                txtID.Enabled = true; //  cho sửa MSSV khi cập nhật
                btnAdd.Text = "Cập nhật";
            }
        }

        private Student EditingState => EditingStudent;

        private void btnAdd_Click(object sender, EventArgs e)
        {
            string id = txtID.Text.Trim();
            string name = txtName.Text.Trim();
            string faculty = cmbFaculty.SelectedItem?.ToString();
            string gender = rdbMale.Checked ? "Nam" : "Nữ";

            if (string.IsNullOrEmpty(id) || string.IsNullOrEmpty(name))
            {
                MessageBox.Show("Mã số SV và Tên là bắt buộc!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (string.IsNullOrEmpty(faculty))
            {
                MessageBox.Show("Vui lòng chọn Khoa!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }
            if (!double.TryParse(txtScore.Text, out double score) || score < 0 || score > 10)
            {
                MessageBox.Show("Điểm TB phải từ 0 đến 10!", "Lỗi", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return;
            }

            var student = new Student
            {
                StudentID = id,
                FullName = name,
                Gender = gender,
                Faculty = faculty,
                AverageScore = (float)score
            };

            OnAddStudent?.Invoke(student);
            this.Close();
        }

        private void btnExit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}