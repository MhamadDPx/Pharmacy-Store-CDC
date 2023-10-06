using Pharmacy_Store.ClassContainer;
using System;
using System.Data;
using System.Windows.Forms;

namespace Pharmacy_Store.FormsContainer
{
    public partial class frm_Login : Form
    {
        Connection conn = new Connection();
        public frm_Login()
        {
            InitializeComponent();
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            if(string.IsNullOrWhiteSpace(txtUsername.Text.Trim()) || string.IsNullOrWhiteSpace(txtPassword.Text.Trim()))
            {
                MessageBox.Show("تکایە ناوی بەکارهێنەر و وشەی تێپەڕ داخڵبکە");
                return;
            }

            DataTable DT = conn.GetData($"SELECT user_id,username,fullname FROM tbl_user WHERE username='{txtUsername.Text.Trim()}' AND pass='{txtPassword.Text.Trim()}' AND archived=0;");

            if(DT.Rows.Count > 0)
            {
                UserInfo.UserID = DT.Rows[0]["user_id"].ToString();
                UserInfo.Username = DT.Rows[0]["username"].ToString();
                UserInfo.Fullname = DT.Rows[0]["fullname"].ToString();

                using(frm_Main frm = new frm_Main())
                {
                    this.Hide();
                    frm.ShowDialog();
                    this.Show();

                    txtUsername.Text = txtPassword.Text = string.Empty;

                    UserInfo.UserID = "0";
                    UserInfo.Username = UserInfo.Fullname = "";
                }
            }
            else
            {
                MessageBox.Show("بەکارهێنەر یاخود وشەی تێپەڕ هەڵەیە");
            }
        }

        private void lblClose_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }
    }
}
