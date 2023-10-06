using Pharmacy_Store.ClassContainer;
using Pharmacy_Store.CrystalReportContainer;
using System;
using System.Windows.Forms;

namespace Pharmacy_Store.FormsContainer
{
    public partial class uc_SellListReport : UserControl
    {

        Connection conn = new Connection();
        private string PrimaryKey = "sell_id";
        private string TblName = "view_sell_result";
        private string SelectedColumns = "sell_id,sell_date,sell_total_price,sell_discount,sell_final_price";

        public uc_SellListReport()
        {
            InitializeComponent();
        }

        private void frm_Load(object sender, EventArgs e)
        {
            RefreshDGV();
        }

        private void btnPrint_Click(object sender, EventArgs e)
        {
            if (DGV.Rows.Count == 0)
                return;

            using (frm_PrintPreview frm = new frm_PrintPreview())
            {
                crt_SellInvoiceList crt = new crt_SellInvoiceList();

                crt.SetDataSource(DGV.DataSource);

                frm.CRV.ReportSource = crt;

                frm.ShowDialog();
            }
        }
        private void btnClean_Click(object sender, EventArgs e)
        {
            txtSearch.ResetText();
            RefreshDGV();
        }
        private void txtSearch_KeyUp(object sender, KeyEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtSearch.Text.Trim()))
            {
                RefreshDGV();
                return;
            }

            DGV.Columns.Clear();
            DGV.DataSource = conn.GetData($"SELECT {SelectedColumns} FROM {TblName} WHERE sell_id={txtSearch.Text.Trim()}");
            DGV.ClearSelection();

            ResizeDGVHeader();
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
            DGV.Columns["sell_id"].HeaderText = "ژ.پسووڵە";
            DGV.Columns["sell_date"].HeaderText = "بەروار";
            DGV.Columns["sell_total_price"].HeaderText = "کۆی پسووڵە";
            DGV.Columns["sell_discount"].HeaderText = "داشکاندن";
            DGV.Columns["sell_final_price"].HeaderText = "کۆ داوی داشکاندن";
        }

    }
}
