using Pharmacy_Store.ClassContainer;
using System;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Store.FormsContainer
{
    public partial class frm_AddItem : Form
    {
        Connection conn = new Connection();
        public string PrimaryID = "0";
        private string PrimaryKey = "item_id";
        private string TblName = "tbl_item";
        frm_ItemSearch FrmItemSearch;

        public frm_AddItem(frm_ItemSearch FrmItemSearch)
        {
            InitializeComponent();
            this.FrmItemSearch = FrmItemSearch;
        }
        private void frm_Load(object sender, EventArgs e)
        {
            if(PrimaryID!="0")
            {
                GetDataToText();
            }
        }

        private void btnSave_Click(object sender, EventArgs e)
        {
            if (CheckEmptyText())
                return;

            if (PrimaryID == "0")
            {
                conn.InsertData(TblName, $"N'{txtItemName.Text.Trim()}',N'{txtItemBarcode.Text.Trim()}',N'{txtItemType.Text.Trim()}',{txtPurchaseQty1.Text},{txtPurchasePrice.Text},{txtSellPrice.Text},N'{txtNote.Text.Trim()}',0");
            }
            else
            {
                conn.UpdatetData(TblName, $"item_name=N'{txtItemName.Text.Trim()}',item_type=N'{txtItemType.Text.Trim()}',item_barcode=N'{txtItemBarcode.Text.Trim()}',item_qty={txtPurchaseQty1.Text},item_pur_price={txtPurchasePrice.Text},item_sell_price={txtSellPrice.Text},item_note=N'{txtNote.Text.Trim()}'", $"{PrimaryKey}={PrimaryID}");
            }

            btnClean.PerformClick();
        }
        private void btnClean_Click(object sender, EventArgs e)
        {
            txtItemName.Text =  txtItemType.Text=  txtItemBarcode.Text = txtNote.Text = string.Empty;
            txtPurchaseQty1.Text = txtPurchasePrice.Text = txtSellPrice.Text = txtPurchaseQty2.Text = txtIncomeQty.Text = txtSellQty.Text = txtCurrentQty.Text = PrimaryID = "0";
            RefreshCMB();
            FrmItemSearch.RefreshDGV();
        }
        private void btnQty_Click(object sender, EventArgs e)
        {
            string Oparation = "";

            if (sender as Button == btnInc)
                Oparation = $"+";
            else if (sender as Button == btnDec)
                Oparation = $"-";

            conn.UpdatetData(TblName, $"item_qty=item_qty{Oparation}{txtIncomeQty.Text}", $"{PrimaryKey}={PrimaryID}");

            DataTable DT = conn.GetData($"SELECT * FROM view_item_stock WHERE {PrimaryKey}={PrimaryID}");
            txtPurchaseQty1.Text = txtPurchaseQty2.Text = DT.Rows[0]["item_qty"].ToString();
            txtSellQty.Text = DT.Rows[0]["total_sell_qty"].ToString();
            txtCurrentQty.Text = DT.Rows[0]["current_item_qty"].ToString();
            txtIncomeQty.Text = "0";
        }

        private void txtPrice_KeyPress(object sender, KeyPressEventArgs e)
        {
            if(!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void RefreshCMB()
        {
            txtItemType.DataSource = conn.GetData($"SELECT DISTINCT item_type FROM {TblName} WHERE archived=0");
            txtItemType.DisplayMember = "item_type";
            txtItemType.Text = string.Empty;
        }
        private void GetDataToText()
        {
            DataTable DT = conn.GetData($"SELECT * FROM view_item_stock WHERE {PrimaryKey}={PrimaryID}");

            txtItemName.Text = DT.Rows[0]["item_name"].ToString();
            txtItemType.Text = DT.Rows[0]["item_type"].ToString();
            txtItemBarcode.Text = DT.Rows[0]["item_barcode"].ToString();
            txtPurchaseQty1.Text = DT.Rows[0]["item_qty"].ToString();
            txtPurchasePrice.Text = DT.Rows[0]["item_pur_price"].ToString();
            txtSellPrice.Text = DT.Rows[0]["item_sell_price"].ToString();
            txtNote.Text = DT.Rows[0]["item_note"].ToString();
            txtPurchaseQty2.Text = DT.Rows[0]["item_qty"].ToString();
            txtSellQty.Text = DT.Rows[0]["total_sell_qty"].ToString();
            txtCurrentQty.Text = DT.Rows[0]["current_item_qty"].ToString();
        }
        private bool CheckEmptyText()
        {
            bool Check = false;

            if(string.IsNullOrWhiteSpace(txtItemType.Text.Trim()))
            {
                MessageBox.Show("تکایە سەرەتا جۆری دەمان داخڵبکە", "ئاگاداری", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Check = true;
            }
            else if (string.IsNullOrWhiteSpace(txtItemName.Text.Trim()))
            {
                MessageBox.Show("تکایە سەرەتا ناوی دەرمان داخڵبکە", "ئاگاداری", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Check = true;
            }
            else if (string.IsNullOrWhiteSpace(txtItemBarcode.Text.Trim()))
            {
                MessageBox.Show("تکایە سەرەتا بارکۆدی دەمان داخڵبکە", "ئاگاداری", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                Check = true;
            }


            return Check;
        }

    }
}
