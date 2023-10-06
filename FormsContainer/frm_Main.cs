using Pharmacy_Store.ClassContainer;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Pharmacy_Store.FormsContainer
{
    public partial class frm_Main : Form
    {

        public frm_Main()
        {
            InitializeComponent();
        }

        private void btnLogout_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void frm_Main_Load(object sender, EventArgs e)
        {
            lblUser.Text = "بەکار‌هێنەر : " + UserInfo.Fullname;
        }

        private void btnCreateUser_Click(object sender, EventArgs e)
        {
            using(frm_Users frm = new frm_Users())
                frm.ShowDialog();
        }

        private void btnExpense_Click(object sender, EventArgs e)
        {
            using (frm_Expense frm = new frm_Expense())
                frm.ShowDialog();
        }

        private void btnAddItem_Click(object sender, EventArgs e)
        {
            using (frm_ItemSearch frm = new frm_ItemSearch())
                frm.ShowDialog();
        }

        private void btnSelIItem_Click(object sender, EventArgs e)
        {
            using (frm_Sell frm = new frm_Sell())
                frm.ShowDialog();
        }

        private void btnReport_Click(object sender, EventArgs e)
        {
            using (frm_Reports frm = new frm_Reports())
                frm.ShowDialog();
        }
    }
}
