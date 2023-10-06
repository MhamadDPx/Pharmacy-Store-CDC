using Pharmacy_Store.ClassContainer;
using Pharmacy_Store.Properties;
using System;
using System.Windows.Forms;

namespace Pharmacy_Store.FormsContainer
{
    public partial class frm_Users : Form
    {
        Connection conn = new Connection();
        private string PrimaryID = "0";
        private string PrimaryKey = "user_id";
        private string TblName = "tbl_user";
        private string SelectedColumns = "user_id,fullname,username,pass";

        public frm_Users()
        {
            InitializeComponent();
        }
        private void frm_Users_Load(object sender, EventArgs e)
        {
            RefreshDGV();
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckEmptyText())
                return;

            if (PrimaryID == "0")
            {
                conn.InsertData(TblName, $"N'{txtFullName.Text.Trim()}',N'{txtUsername.Text.Trim()}',N'{txtPassword.Text.Trim()}',0", false, false);
            }
            else
            {
                conn.UpdatetData(TblName, $"fullname=N'{txtFullName.Text.Trim()}',username=N'{txtUsername.Text.Trim()}',pass=N'{txtPassword.Text.Trim()}'", $"{PrimaryKey}={PrimaryID}", false);
            }

            btnClean.PerformClick();
        }
        private void btnClean_Click(object sender, EventArgs e)
        {
            txtUsername.Text = txtFullName.Text = txtPassword.Text = txtSearch.Text = string.Empty;
            PrimaryID = "0";
            RefreshDGV();
        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            DGV.Columns.Clear();
            DGV.DataSource = conn.GetData($"SELECT {SelectedColumns} FROM {TblName} WHERE (fullname LIKE N'%{txtSearch.Text.Trim()}%' OR username LIKE N'%{txtSearch.Text.Trim()}%' OR pass LIKE N'%{txtSearch.Text.Trim()}%') AND archived=0 AND {PrimaryKey}<>1");
            DGV.ClearSelection();

            DGV.Columns.Add(new DataGridViewImageColumn() { Name = "btnEdit", HeaderText = "", Image = Resources.icons8_edit_row_24px, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });
            DGV.Columns.Add(new DataGridViewImageColumn() { Name = "btnDelete", HeaderText = "", Image = Resources.icons8_delete_row_24px, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });

            DGV.Columns["user_id"].HeaderText = "زنجیرە";
            DGV.Columns["fullname"].HeaderText = "ناوی تەواو";
            DGV.Columns["username"].HeaderText = "بەکارهێنەر";
            DGV.Columns["pass"].HeaderText = "وشەی تێپەڕ";

            DGV.Columns["btnEdit"].Width = 40;
            DGV.Columns["btnDelete"].Width = 40;
        }

        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(DGV.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                if (MessageBox.Show("دڵنیای لە ئەنجامدانی ئەم کردارە ؟", "سڕینەوە", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                conn.DeletetData(TblName, $"{PrimaryKey} = {DGV.CurrentRow.Cells[PrimaryKey].Value}");
                btnClean.PerformClick();
            }
            else if (DGV.Columns[e.ColumnIndex].Name == "btnEdit")
            {
                GetDataToText();
            }
        }

        private void RefreshDGV()
        {
            DGV.Columns.Clear();
            DGV.DataSource = conn.GetData($"SELECT {SelectedColumns} FROM {TblName} WHERE archived=0 AND {PrimaryKey}<>1");
            DGV.ClearSelection();

            DGV.Columns.Add(new DataGridViewImageColumn() { Name = "btnEdit", HeaderText = "", Image=Resources.icons8_edit_row_24px, AutoSizeMode=DataGridViewAutoSizeColumnMode.None});
            DGV.Columns.Add(new DataGridViewImageColumn() { Name = "btnDelete", HeaderText = "", Image = Resources.icons8_delete_row_24px, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });

            DGV.Columns["user_id"].HeaderText = "زنجیرە";
            DGV.Columns["fullname"].HeaderText = "ناوی تەواو";
            DGV.Columns["username"].HeaderText = "بەکارهێنەر";
            DGV.Columns["pass"].HeaderText = "وشەی تێپەڕ";

            DGV.Columns["btnEdit"].Width = 40;
            DGV.Columns["btnDelete"].Width = 40;
        }
        private void GetDataToText()
        {
            PrimaryID = DGV.CurrentRow.Cells[PrimaryKey].Value.ToString();
            txtFullName.Text = DGV.CurrentRow.Cells["fullname"].Value.ToString();
            txtUsername.Text = DGV.CurrentRow.Cells["username"].Value.ToString();
            txtPassword.Text = DGV.CurrentRow.Cells["pass"].Value.ToString();
        }
        private bool CheckEmptyText()
        {
            bool Check = false;

            if(string.IsNullOrWhiteSpace(txtFullName.Text.Trim()))
            {
                MessageBox.Show("تکایە سەرەتا ناوی تەواو داخڵبکە", "ئاگاداری", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Check = true;
            }
            else if (string.IsNullOrWhiteSpace(txtUsername.Text.Trim()))
            {
                MessageBox.Show("تکایە سەرەتا بەکارهێنەر داخڵبکە", "ئاگاداری", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Check = true;
            }
            else if (string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                MessageBox.Show("تکایە سەرەتا وشەی تێپەڕ داخڵبکە", "ئاگاداری", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Check = true;
            }

            return Check;
        }

    }
}
