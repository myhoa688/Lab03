using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace Lab03_3
{
    public partial class FrmMain : Form
    {
        private readonly List<Student> students = new List<Student>();

        // ✅ BIẾN LƯU TRẠNG THÁI LỌC
        private string _currentSearchKeyword = "";
        private string _currentFacultyFilter = "Tất cả";

        public FrmMain()
        {
            InitializeComponent();
        }

        private void FrmMain_Load(object sender, EventArgs e)
        {
            dgvStudents.Columns.Clear();
            dgvStudents.Columns.Add("STT", "Số TT");
            dgvStudents.Columns.Add("StudentID", "Mã Số SV");
            dgvStudents.Columns.Add("FullName", "Tên Sinh Viên");
            dgvStudents.Columns.Add("Gender", "Giới Tính");
            dgvStudents.Columns.Add("Faculty", "Khoa");
            dgvStudents.Columns.Add("AverageScore", "Điểm TB");

            LoadSampleStudents();
        }

        private void LoadSampleStudents()
        {
            students.Clear();
            students.Add(new Student { StudentID = "SV01", FullName = "Nguyễn Văn An", Gender = "Nam", Faculty = "Công nghệ thông tin", AverageScore = 7.5f });
            students.Add(new Student { StudentID = "SV02", FullName = "Trần Thị Bình", Gender = "Nữ", Faculty = "Ngôn ngữ Anh", AverageScore = 8.2f });
            students.Add(new Student { StudentID = "SV03", FullName = "Lê Văn Cường", Gender = "Nam", Faculty = "Quản trị kinh doanh", AverageScore = 6.8f });
            students.Add(new Student { StudentID = "SV04", FullName = "Phạm Thị Dung", Gender = "Nữ", Faculty = "Công nghệ thông tin", AverageScore = 9.0f });
            students.Add(new Student { StudentID = "SV05", FullName = "Hoàng Văn Em", Gender = "Nam", Faculty = "Ngôn ngữ Anh", AverageScore = 5.6f });
            students.Add(new Student { StudentID = "SV06", FullName = "Nguyễn Thị Hoa", Gender = "Nữ", Faculty = "Quản trị kinh doanh", AverageScore = 7.9f });
            students.Add(new Student { StudentID = "SV07", FullName = "Đặng Văn Khánh", Gender = "Nam", Faculty = "Công nghệ thông tin", AverageScore = 8.4f });
            students.Add(new Student { StudentID = "SV08", FullName = "Vũ Thị Lan", Gender = "Nữ", Faculty = "Ngôn ngữ Anh", AverageScore = 6.5f });
            students.Add(new Student { StudentID = "SV09", FullName = "Ngô Văn Minh", Gender = "Nam", Faculty = "Quản trị kinh doanh", AverageScore = 7.2f });
            students.Add(new Student { StudentID = "SV10", FullName = "Phan Thị Ngọc", Gender = "Nữ", Faculty = "Công nghệ thông tin", AverageScore = 8.7f });

            // ✅ LOAD DANH SÁCH KHOA VÀO COMBOBOX
            var faculties = students.Select(s => s.Faculty).Distinct().OrderBy(f => f).ToList();
            cmbFilterFaculty.Items.Clear();
            cmbFilterFaculty.Items.Add("Tất cả");
            cmbFilterFaculty.Items.AddRange(faculties.ToArray());
            cmbFilterFaculty.SelectedIndex = 0;

            RefreshGrid();
        }

        private void btnAddNew_Click(object sender, EventArgs e)
        {
            var frm = new FrmAddStudent();
            frm.OnAddStudent = AddOrUpdateStudent;
            frm.ShowDialog(this);
        }

        private void AddOrUpdateStudent(Student student)
        {
            if (student == null) return;

            var existing = students.FirstOrDefault(s => s.StudentID == student.StudentID);
            if (existing != null)
            {
                existing.FullName = student.FullName;
                existing.Gender = student.Gender;
                existing.Faculty = student.Faculty;
                existing.AverageScore = student.AverageScore;
                MessageBox.Show("Đã cập nhật thông tin sinh viên!", "Thông báo");
            }
            else
            {
                students.Add(student);
                MessageBox.Show("Thêm sinh viên thành công!", "Thông báo");
            }

            RefreshGrid();
        }

        private void RefreshGrid()
        {
            // Reset bộ lọc
            _currentSearchKeyword = "";
            _currentFacultyFilter = "Tất cả";
            txtSearch.Text = "";
            cmbFilterFaculty.SelectedIndex = 0;

            ApplyFilter(); // Gọi hàm lọc tổng hợp
        }

        private void ApplyFilter()
        {
            var filtered = students.AsEnumerable();

            // Lọc theo tên
            if (!string.IsNullOrEmpty(_currentSearchKeyword))
            {
                filtered = filtered.Where(s => s.FullName.ToLower().Contains(_currentSearchKeyword));
            }

            // Lọc theo khoa
            if (_currentFacultyFilter != "Tất cả")
            {
                filtered = filtered.Where(s => s.Faculty == _currentFacultyFilter);
            }

            var list = filtered.ToList();

            // Cập nhật DataGridView
            dgvStudents.Rows.Clear();
            int index = 1;
            foreach (var s in list)
            {
                dgvStudents.Rows.Add(index++, s.StudentID, s.FullName, s.Gender, s.Faculty, s.AverageScore);
            }

            // Cập nhật thống kê
            int total = list.Count;
            int male = list.Count(s => s.Gender == "Nam");
            int female = total - male;

            lblTotal.Text = $"Tổng: {total}";
            lblMale.Text = $"Nam: {male}";
            lblFemale.Text = $"Nữ: {female}";
        }

        private void dgvStudents_CellDoubleClick(object sender, DataGridViewCellEventArgs e)
        {
            if (e.RowIndex < 0) return;

            string id = dgvStudents.Rows[e.RowIndex].Cells["StudentID"].Value?.ToString();
            var student = students.FirstOrDefault(s => s.StudentID == id);
            if (student == null) return;

            var frm = new FrmAddStudent
            {
                EditingStudent = student
            };
            frm.OnAddStudent = AddOrUpdateStudent;
            frm.ShowDialog(this);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            _currentSearchKeyword = txtSearch.Text.Trim().ToLower();
            ApplyFilter();
        }

        private void cmbFilterFaculty_SelectedIndexChanged(object sender, EventArgs e)
        {
            _currentFacultyFilter = cmbFilterFaculty.SelectedItem?.ToString() ?? "Tất cả";
            ApplyFilter();
        }

        private void thoatToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Close();
        }
    }
}