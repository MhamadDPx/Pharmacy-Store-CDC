using Pharmacy_Store.ClassContainer;
using Pharmacy_Store.CrystalReportContainer;
using Pharmacy_Store.Properties;
using System;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Store.FormsContainer
{
    public partial class frm_Sell : Form
    {
        Connection conn = new Connection();
        private string PrimaryInvoiceID = "0";
        private string PrimaryInvoiceKey = "sell_id";
        private string PrimaryItemKey = "seq";
        private string TblInvoiceName = "tbl_sell";
        private string TblItemName = "tbl_sell_item";
        private string SelectedColumns = "seq,(SELECT item_name FROM tbl_item WHERE item_id=item_id_fk) AS item_name,sell_qty,item_sell_price,ROUND(sell_qty*item_sell_price,2) AS item_total";

        public frm_Sell()
        {
            InitializeComponent();
        }
     
        private void btnNewInvoice_Click(object sender, EventArgs e)
        {
            txtBarcode.Text = string.Empty;
            txtInvoiceID.Text = PrimaryInvoiceID = txtSubTotal.Text = txtDiscount.Text = txtFinalTotal.Text = "0";
            txtInvoiceDate.Text = DateTime.Now.ToString("d");
            DGV.Columns.Clear();
        }
        private void txtBarcode_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.KeyCode == Keys.Enter)
            {
                if (CheckEmptyText())
                    return;

                if (PrimaryInvoiceID == "0")
                {
                    string MaxID = PrimaryInvoiceID = txtInvoiceID.Text = conn.GetData($"SELECT ISNULL(MAX({PrimaryInvoiceKey}),0)+1 FROM {TblInvoiceName}").Rows[0][0].ToString();
                    conn.InsertData(TblInvoiceName, $"{MaxID},'{txtInvoiceDate.Text}',{txtDiscount.Text},0",true,false);
                    conn.InsertData(TblItemName, $"{MaxID},(SELECT item_id FROM tbl_item WHERE item_barcode=N'{txtBarcode.Text.Trim()}'),1,(SELECT item_pur_price FROM tbl_item WHERE item_barcode=N'{txtBarcode.Text.Trim()}'),(SELECT item_sell_price FROM tbl_item WHERE item_barcode=N'{txtBarcode.Text.Trim()}'),0", true, false);
                }
                else
                {
                    if (conn.GetData($"SELECT * FROM {TblItemName} WHERE {PrimaryInvoiceKey}_fk={PrimaryInvoiceID} AND item_id_fk=(SELECT item_id FROM tbl_item WHERE item_barcode=N'{txtBarcode.Text.Trim()}')").Rows.Count <= 0)
                        conn.InsertData(TblItemName, $"{PrimaryInvoiceID},(SELECT item_id FROM tbl_item WHERE item_barcode=N'{txtBarcode.Text.Trim()}'),1,(SELECT item_pur_price FROM tbl_item WHERE item_barcode=N'{txtBarcode.Text.Trim()}'),(SELECT item_sell_price FROM tbl_item WHERE item_barcode=N'{txtBarcode.Text.Trim()}'),0", true, false);
                    else
                        conn.UpdatetData(TblItemName, "sell_qty=sell_qty+1",$"{PrimaryInvoiceKey}_fk={PrimaryInvoiceID} AND item_id_fk=(SELECT item_id FROM tbl_item WHERE item_barcode=N'{txtBarcode.Text.Trim()}')",false);
                }

                RefreshDGV();
            }
        }

        private void DGV_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if(DGV.Columns[e.ColumnIndex].Name == "btnDelete")
            {
                if (MessageBox.Show("دڵنیای لە ئەنجامدانی ئەم کردارە ؟", "سڕینەوە", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No)
                    return;

                conn.DeletetData(TblItemName, $"{PrimaryItemKey} = {DGV.CurrentRow.Cells[PrimaryItemKey].Value}");
                RefreshDGV();
            }
            
        }

        private void RefreshDGV()
        {
            txtBarcode.ResetText();
            DGV.Columns.Clear();
            DGV.DataSource = conn.GetData($"SELECT {SelectedColumns} FROM {TblItemName} WHERE {PrimaryInvoiceKey}_fk={PrimaryInvoiceID} AND archived=0");
            DGV.ClearSelection();

            ResizeDGVHeader();
            RefreshInvoiceTotal();
        }
        private void RefreshInvoiceTotal()
        {
            DataTable DT = conn.GetData($"SELECT sell_discount,sell_total_price,sell_final_price FROM view_sell_result WHERE {PrimaryInvoiceKey}={PrimaryInvoiceID}");

            txtSubTotal.Text = DT.Rows[0]["sell_total_price"].ToString();
            txtDiscount.Text = DT.Rows[0]["sell_discount"].ToString();
            txtFinalTotal.Text = DT.Rows[0]["sell_final_price"].ToString();
        }
        private void ResizeDGVHeader()
        {

            DGV.Columns.Add(new DataGridViewImageColumn() { Name = "btnDelete", HeaderText = "", Image = Resources.icons8_delete_row_24px, AutoSizeMode = DataGridViewAutoSizeColumnMode.None });

            DGV.Columns[PrimaryItemKey].Visible = false;

            DGV.Columns["item_name"].HeaderText = "ناوی کاڵا";
            DGV.Columns["sell_qty"].HeaderText = "دانە";
            DGV.Columns["item_sell_price"].HeaderText = "نرخ";
            DGV.Columns["item_total"].HeaderText = "کۆی گشتی";

            DGV.Columns["item_name"].ReadOnly = true;
            DGV.Columns["item_sell_price"].ReadOnly = true;
            DGV.Columns["item_total"].ReadOnly = true;

            DGV.Columns["btnDelete"].Width = 40;

        }
        private bool CheckEmptyText()
        {
            bool Check = false;

            if(string.IsNullOrWhiteSpace(txtBarcode.Text.Trim()))
            {
                MessageBox.Show("تکایە سەرەتا بارکۆدی کاڵا داخڵبکە", "ئاگاداری", MessageBoxButtons.OK, MessageBoxIcon.Warning);
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

        private void DGV_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if(DGV.Columns[e.ColumnIndex].Name == "sell_qty")
            {
                string Seq = DGV.Rows[e.RowIndex].Cells["seq"].Value.ToString();
                double.TryParse(DGV.Rows[e.RowIndex].Cells["sell_qty"].Value.ToString(), out double NewQty);
                double.TryParse(DGV.Rows[e.RowIndex].Cells["item_sell_price"].Value.ToString(), out double SellPrice);

                conn.UpdatetData(TblItemName, $"sell_qty={NewQty}", $"{PrimaryItemKey}={Seq}",false);

                DGV.Rows[e.RowIndex].Cells["item_total"].Value = (NewQty * SellPrice).ToString();
                RefreshInvoiceTotal();
            }
        }

        private void DGV_EditingControlShowing(object sender, DataGridViewEditingControlShowingEventArgs e)
        {
            if(e.Control is TextBox txtNewQty)
            {
                txtNewQty.PreviewKeyDown -= TxtNewQty_PreviewKeyDown;
                txtNewQty.PreviewKeyDown += TxtNewQty_PreviewKeyDown;

                txtNewQty.KeyPress -= TxtNewQty_KeyPress;
                txtNewQty.KeyPress += TxtNewQty_KeyPress;
            }
        }

        private void TxtNewQty_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsDigit(e.KeyChar) && !char.IsControl(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        private void TxtNewQty_PreviewKeyDown(object sender, PreviewKeyDownEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
                txtBarcode.Focus();
        }

        private void txtDiscount_KeyUp(object sender, KeyEventArgs e)
        {
            if(e.KeyCode != Keys.Enter)
            {
                conn.UpdatetData(TblInvoiceName, $"sell_discount={txtDiscount.Text}", $"{PrimaryInvoiceKey}={PrimaryInvoiceID}", false);
            }
            else
            {
                RefreshInvoiceTotal();
                txtBarcode.Focus();
            }
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (PrimaryInvoiceID == "0")
                return;

            using(frm_PrintPreview frm = new frm_PrintPreview())
            {
                crt_SellBill crt = new crt_SellBill();

                DataTable DT = conn.GetData($"SELECT * FROM view_sell_bill WHERE {PrimaryInvoiceKey}={PrimaryInvoiceID}");
                crt.SetDataSource(DT);

                frm.CRV.ReportSource = crt;

                frm.ShowDialog();
            }
        }
    }
}
