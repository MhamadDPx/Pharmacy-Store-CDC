using Pharmacy_Store.ClassContainer;
using Pharmacy_Store.Properties;
using System;
using System.Windows.Forms;

namespace Pharmacy_Store.FormsContainer
{
    public partial class frm_ItemSearch : Form
    {
        Connection conn = new Connection();
        private string PrimaryKey = "item_id";
        private string TblName = "view_item_stock";
        private string SelectedColumns = "item_id,item_name,item_barcode,item_type,current_item_qty,item_pur_price,item_sell_price,item_note";

        public frm_ItemSearch()
        {
            InitializeComponent();
        }
        private void frm_Users_Load(object sender, EventArgs e)
        {
            RefreshDGV();
        }

        private void btnAdd_Click(object sender, EventArgs e)
        {
            using (frm_AddItem frm = new frm_AddItem(this))
                frm.ShowDialog();
        }
        private void btnClean_Click(object sender, EventArgs e)
        {
            txtSearch.ResetText();
            RefreshDGV();
        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            DGV.Columns.Clear();
            DGV.DataSource = conn.GetData($"SELECT {SelectedColumns} FROM {TblName} WHERE (item_name LIKE N'%{txtSearch.Text.Trim()}%' OR item_type LIKE N'%{txtSearch.Text.Trim()}%')");
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
                using (frm_AddItem frm = new frm_AddItem(this))
                {
                    frm.PrimaryID = DGV.CurrentRow.Cells[PrimaryKey].Value.ToString();
                    frm.ShowDialog();
                }
            }
        }

        public void RefreshDGV()
        {
            DGV.Columns.Clear();
            DGV.DataSource = conn.GetData($"SELECT {SelectedColumns} FROM {TblName} ORDER BY {PrimaryKey} DESC");
            DGV.ClearSelection();

            ResizeDGVHeader();
        }
        private void ResizeDGVHeader()
        {

            DGV.Columns.Add(new DataGridViewImageColumn() { Name = "btnEdit", HeaderText = "", Image = Resources.icons8_edit_row_24px, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });
            DGV.Columns.Add(new DataGridViewImageColumn() { Name = "btnDelete", HeaderText = "", Image = Resources.icons8_delete_row_24px, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });

            DGV.Columns["item_id"].HeaderText = "زنجیرە";
            DGV.Columns["item_name"].HeaderText = "ناوی دەرمان";
            DGV.Columns["item_barcode"].HeaderText = "بارکۆد";
            DGV.Columns["item_type"].HeaderText = "جۆری دەرمان";
            DGV.Columns["current_item_qty"].HeaderText = "دانەی کۆگا";
            DGV.Columns["item_pur_price"].HeaderText = "نرخی کڕین";
            DGV.Columns["item_sell_price"].HeaderText = "نرخی فرۆشتن";
            DGV.Columns["item_note"].HeaderText = "تێبینی";

            DGV.Columns["btnEdit"].Width = 40;
            DGV.Columns["btnDelete"].Width = 40;

        }

    }
}
