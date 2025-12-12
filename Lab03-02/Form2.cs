using System;
using System.Drawing;
using System.Windows.Forms;

namespace Lab03_02
{
    public partial class Form2 : Form
    {
        private string currentFilePath = null;

        public Form2()
        {
            InitializeComponent();
            LoadFonts();
            LoadSizes();
            SetDefaultFont();
        }

        private void LoadFonts()
        {
            foreach (FontFamily ff in FontFamily.Families)
            {
                cmbFont.Items.Add(ff.Name);
            }
        }

        private void LoadSizes()
        {
            int[] sizes = { 8, 9, 10, 11, 12, 14, 16, 18, 20, 22, 24, 26, 28, 36, 48, 72 };
            foreach (int size in sizes)
            {
                cmbSize.Items.Add(size.ToString());
            }
        }

        private void SetDefaultFont()
        {
            cmbFont.Text = "Tahoma";
            cmbSize.Text = "14";
            richTextBox1.Font = new Font("Tahoma", 14);
        }

        // === HỆ THỐNG ===
        private void mnuNew_Click(object sender, EventArgs e)
        {
            richTextBox1.Clear();
            currentFilePath = null;
            SetDefaultFont();
        }

        private void mnuOpen_Click(object sender, EventArgs e)
        {
            OpenFileDialog ofd = new OpenFileDialog();
            ofd.Filter = "Rich Text Format|*.rtf|Text Files|*.txt|All Files|*.*";
            if (ofd.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    currentFilePath = ofd.FileName;
                    string ext = System.IO.Path.GetExtension(currentFilePath).ToLower();
                    if (ext == ".rtf")
                        richTextBox1.LoadFile(currentFilePath, RichTextBoxStreamType.RichText);
                    else
                        richTextBox1.LoadFile(currentFilePath, RichTextBoxStreamType.PlainText);
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Lỗi khi mở file: " + ex.Message);
                }
            }
        }

        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (currentFilePath == null)
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.Filter = "Rich Text Format|*.rtf";
                sfd.DefaultExt = "rtf";
                if (sfd.ShowDialog() != DialogResult.OK) return;
                currentFilePath = sfd.FileName;
            }

            try
            {
                richTextBox1.SaveFile(currentFilePath, RichTextBoxStreamType.RichText);
                MessageBox.Show("Lưu file thành công!", "Thông báo", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Lỗi khi lưu file: " + ex.Message);
            }
        }

        private void mnuExit_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        // === ĐỊNH DẠNG FONT ===
        private void mnuFont_Click(object sender, EventArgs e)
        {
            FontDialog dlg = new FontDialog();
            dlg.ShowColor = true;
            dlg.ShowEffects = true;
            if (dlg.ShowDialog() != DialogResult.Cancel)
            {
                richTextBox1.SelectionFont = dlg.Font;
                richTextBox1.SelectionColor = dlg.Color;
                cmbFont.Text = dlg.Font.FontFamily.Name;
                cmbSize.Text = dlg.Font.Size.ToString();
            }
        }

        // === TOOLSTRIP: NEW/OPEN/SAVE ===
        private void tsbNew_Click(object sender, EventArgs e) => mnuNew_Click(sender, e);
        private void tsbOpen_Click(object sender, EventArgs e) => mnuOpen_Click(sender, e);
        private void tsbSave_Click(object sender, EventArgs e) => mnuSave_Click(sender, e);

        // === ĐỊNH DẠNG CHỮ ===
        private void tsbBold_Click(object sender, EventArgs e)
        {
            ToggleStyle(FontStyle.Bold);
        }

        private void tsbItalic_Click(object sender, EventArgs e)
        {
            ToggleStyle(FontStyle.Italic);
        }

        private void tsbUnderline_Click(object sender, EventArgs e)
        {
            ToggleStyle(FontStyle.Underline);
        }

        private void ToggleStyle(FontStyle style)
        {
            if (richTextBox1.SelectionFont != null)
            {
                Font f = richTextBox1.SelectionFont;
                richTextBox1.SelectionFont = new Font(f, f.Style ^ style);
            }
        }

        // === CĂN LỀ ===
        private void tsbLeft_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Left;
        }

        private void tsbCenter_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Center;
        }

        private void tsbRight_Click(object sender, EventArgs e)
        {
            richTextBox1.SelectionAlignment = HorizontalAlignment.Right;
        }

        // === COMBOBOX FONT/SIZE ===
        private void cmbFont_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null && cmbFont.SelectedItem != null)
            {
                string name = cmbFont.SelectedItem.ToString();
                float size = richTextBox1.SelectionFont.Size;
                FontStyle style = richTextBox1.SelectionFont.Style;
                richTextBox1.SelectionFont = new Font(name, size, style);
            }
        }

        private void cmbSize_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (richTextBox1.SelectionFont != null && cmbSize.SelectedItem != null)
            {
                string sizeStr = cmbSize.SelectedItem.ToString();
                if (float.TryParse(sizeStr, out float size))
                {
                    string name = richTextBox1.SelectionFont.FontFamily.Name;
                    FontStyle style = richTextBox1.SelectionFont.Style;
                    richTextBox1.SelectionFont = new Font(name, size, style);
                }
            }
        }
    }
}