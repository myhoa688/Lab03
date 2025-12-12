using System;
using System.Windows.Forms;

namespace Lab03_3
{
    partial class FrmMain
    {
        private System.ComponentModel.IContainer components = null;
        private MenuStrip menuStrip1;
        private ToolStripMenuItem chucNangToolStripMenuItem;
        private ToolStripMenuItem themMoiToolStripMenuItem;
        private ToolStripMenuItem thoatToolStripMenuItem;
        private ToolStrip toolStrip1;
        private ToolStripButton btnAddNew;
        private ToolStripLabel lblSearch;
        private ToolStripTextBox txtSearch;
        private DataGridView dgvStudents;

        // ✅ THÊM CÁC CONTROL MỚI
        private ComboBox cmbFilterFaculty;
        private Label lblTotal;
        private Label lblMale;
        private Label lblFemale;
        private FlowLayoutPanel filterPanel;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            this.menuStrip1 = new MenuStrip();
            this.chucNangToolStripMenuItem = new ToolStripMenuItem();
            this.themMoiToolStripMenuItem = new ToolStripMenuItem();
            this.thoatToolStripMenuItem = new ToolStripMenuItem();
            this.toolStrip1 = new ToolStrip();
            this.btnAddNew = new ToolStripButton();
            this.lblSearch = new ToolStripLabel();
            this.txtSearch = new ToolStripTextBox();
            this.dgvStudents = new DataGridView();

            // ✅ THÊM PANEL LỌC VÀ THỐNG KÊ
            this.filterPanel = new FlowLayoutPanel();
            this.cmbFilterFaculty = new ComboBox();
            this.lblTotal = new Label();
            this.lblMale = new Label();
            this.lblFemale = new Label();

            // MenuStrip
            this.menuStrip1.Items.AddRange(new ToolStripItem[] { this.chucNangToolStripMenuItem });
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(900, 24);

            this.chucNangToolStripMenuItem.Text = "Chức năng";
            this.chucNangToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[] {
                this.themMoiToolStripMenuItem, this.thoatToolStripMenuItem
            });

            this.themMoiToolStripMenuItem.Text = "Thêm mới";
            this.themMoiToolStripMenuItem.ShortcutKeys = System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.N;
            this.themMoiToolStripMenuItem.Click += new System.EventHandler(this.btnAddNew_Click);

            this.thoatToolStripMenuItem.Text = "Thoát";
            this.thoatToolStripMenuItem.Click += new System.EventHandler(this.thoatToolStripMenuItem_Click);

            // ToolStrip
            this.toolStrip1.Items.AddRange(new ToolStripItem[] { this.btnAddNew, this.lblSearch, this.txtSearch });
            this.toolStrip1.Location = new System.Drawing.Point(0, 24);
            this.toolStrip1.Name = "toolStrip1";
            this.toolStrip1.Size = new System.Drawing.Size(900, 25);

            this.btnAddNew.Text = "Thêm mới";
            this.btnAddNew.Click += new System.EventHandler(this.btnAddNew_Click);

            this.lblSearch.Text = "Tìm kiếm theo tên:";
            this.txtSearch.AutoSize = false;
            this.txtSearch.Width = 200;
            this.txtSearch.ToolTipText = "Nhập tên sinh viên để tìm";
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);

            // Filter Panel - ✅ ĐƯỢC THÊM VÀO GIỮA TOOLSTRIP VÀ DATAGRIDVIEW
            this.filterPanel.Dock = DockStyle.Top;
            this.filterPanel.Padding = new Padding(10, 5, 10, 5);
            this.filterPanel.BackColor = System.Drawing.SystemColors.Control;
            this.filterPanel.AutoSize = true;

            Label lblFilter = new Label { Text = "Lọc theo khoa:", AutoSize = true, Margin = new Padding(0, 0, 10, 0) };
            this.cmbFilterFaculty.DropDownStyle = ComboBoxStyle.DropDownList;
            this.cmbFilterFaculty.Width = 200;
            this.cmbFilterFaculty.SelectedIndexChanged += new EventHandler(cmbFilterFaculty_SelectedIndexChanged);

            this.lblTotal.Text = "Tổng: 0";
            this.lblTotal.AutoSize = true;
            this.lblTotal.Margin = new Padding(20, 0, 10, 0);

            this.lblMale.Text = "Nam: 0";
            this.lblMale.AutoSize = true;
            this.lblMale.Margin = new Padding(10, 0, 10, 0);

            this.lblFemale.Text = "Nữ: 0";
            this.lblFemale.AutoSize = true;
            this.lblFemale.Margin = new Padding(10, 0, 0, 0);

            this.filterPanel.Controls.AddRange(new Control[] {
                lblFilter, this.cmbFilterFaculty,
                this.lblTotal, this.lblMale, this.lblFemale
            });

            // DataGridView
            this.dgvStudents.AllowUserToAddRows = false;
            this.dgvStudents.AllowUserToDeleteRows = false;
            this.dgvStudents.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            this.dgvStudents.BackgroundColor = System.Drawing.SystemColors.Window;
            this.dgvStudents.Dock = DockStyle.Fill;
            this.dgvStudents.Location = new System.Drawing.Point(0, 74); // ✅ Chỉnh lại vị trí vì thêm panel
            this.dgvStudents.MultiSelect = false;
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.ReadOnly = true;
            this.dgvStudents.RowHeadersVisible = false;
            this.dgvStudents.SelectionMode = DataGridViewSelectionMode.FullRowSelect;
            this.dgvStudents.Size = new System.Drawing.Size(900, 426);
            this.dgvStudents.CellDoubleClick += new DataGridViewCellEventHandler(this.dgvStudents_CellDoubleClick);

            // FrmMain
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(900, 500);
            this.Controls.Add(this.dgvStudents);
            this.Controls.Add(this.filterPanel); // ✅ Thêm panel vào trước dgvStudents
            this.Controls.Add(this.toolStrip1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FrmMain";
            this.Text = "Quản Lý Sinh Viên";
            this.Load += new System.EventHandler(this.FrmMain_Load);
        }
    }
}