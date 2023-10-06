using Pharmacy_Store.ClassContainer;
using Pharmacy_Store.Properties;
using System;
using System.Windows.Forms;

namespace Pharmacy_Store.FormsContainer
{
    public partial class frm_Expense : Form
    {
        Connection conn = new Connection();
        private string PrimaryID = "0";
        private string PrimaryKey = "exp_id";
        private string TblName = "tbl_expense";
        private string SelectedColumns = "exp_id,exp_type,exp_date,exp_price,exp_note";

        public frm_Expense()
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
                conn.InsertData(TblName, $"N'{txtExpType.Text.Trim()}','{txtExpDate.Text}',{txtExpPrice.Text},N'{txtExpNote.Text.Trim()}',0");
            }
            else
            {
                conn.UpdatetData(TblName, $"exp_type=N'{txtExpType.Text.Trim()}',exp_date='{txtExpDate.Text}',exp_price={txtExpPrice.Text},exp_note=N'{txtExpNote.Text.Trim()}'", $"{PrimaryKey}={PrimaryID}");
            }

            btnClean.PerformClick();
        }
        private void btnClean_Click(object sender, EventArgs e)
        {
            txtExpType.Text = txtExpNote.Text = txtSearch.Text = string.Empty;
            txtExpPrice.Text = PrimaryID = "0";
            txtExpDate.Text = DateTime.Now.ToString("d");
            RefreshDGV();
            RefreshCMB();
        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            DGV.Columns.Clear();
            DGV.DataSource = conn.GetData($"SELECT {SelectedColumns} FROM {TblName} WHERE (exp_type LIKE N'%{txtSearch.Text.Trim()}%' OR exp_note LIKE N'%{txtSearch.Text.Trim()}%') AND archived=0");
            DGV.ClearSelection();

            ResizeDGVHeader();
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
            DGV.DataSource = conn.GetData($"SELECT {SelectedColumns} FROM {TblName} WHERE archived=0");
            DGV.ClearSelection();

            ResizeDGVHeader();
        }
        private void ResizeDGVHeader()
        {

            DGV.Columns.Add(new DataGridViewImageColumn() { Name = "btnEdit", HeaderText = "", Image = Resources.icons8_edit_row_24px, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });
            DGV.Columns.Add(new DataGridViewImageColumn() { Name = "btnDelete", HeaderText = "", Image = Resources.icons8_delete_row_24px, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });

            DGV.Columns["exp_id"].HeaderText = "زنجیرە";
            DGV.Columns["exp_type"].HeaderText = "جۆری خەرجی";
            DGV.Columns["exp_date"].HeaderText = "بەروار";
            DGV.Columns["exp_price"].HeaderText = "بڕی خەرجی";
            DGV.Columns["exp_note"].HeaderText = "تێبینی";

            DGV.Columns["btnEdit"].Width = 40;
            DGV.Columns["btnDelete"].Width = 40;

        }
        private void RefreshCMB()
        {
            txtExpType.DataSource = conn.GetData($"SELECT DISTINCT exp_type FROM {TblName} WHERE archived=0");
            txtExpType.DisplayMember = "exp_type";
            txtExpType.Text = string.Empty;
        }
        private void GetDataToText()
        {
            PrimaryID = DGV.CurrentRow.Cells[PrimaryKey].Value.ToString();
            txtExpType.Text = DGV.CurrentRow.Cells["exp_type"].Value.ToString();
            txtExpDate.Text = DGV.CurrentRow.Cells["exp_date"].Value.ToString();
            txtExpPrice.Text = DGV.CurrentRow.Cells["exp_price"].Value.ToString();
            txtExpNote.Text = DGV.CurrentRow.Cells["exp_note"].Value.ToString();
        }
        private bool CheckEmptyText()
        {
            bool Check = false;

            if(string.IsNullOrWhiteSpace(txtExpType.Text.Trim()))
            {
                MessageBox.Show("تکایە سەرەتا جۆری خەرجی داخڵبکە", "ئاگاداری", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Check = true;
            }
            else if (string.IsNullOrWhiteSpace(txtExpPrice.Text.Trim()) || txtExpPrice.Text == "0")
            {
                MessageBox.Show("تکایە سەرەتا بڕی خەرجی داخڵبکە", "ئاگاداری", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Check = true;
            }

            return Check;
        }

        private void txtExpPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }
    }
}
