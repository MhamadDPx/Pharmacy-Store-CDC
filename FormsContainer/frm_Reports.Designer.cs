
namespace Pharmacy_Store.FormsContainer
{
    partial class frm_Reports
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.tabMain = new System.Windows.Forms.TabControl();
            this.tabSell = new System.Windows.Forms.TabPage();
            this.tabItem = new System.Windows.Forms.TabPage();
            this.tabExpireItem = new System.Windows.Forms.TabPage();
            this.uc_SellListReport1 = new Pharmacy_Store.FormsContainer.uc_SellListReport();
            this.tabMain.SuspendLayout();
            this.tabSell.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabMain
            // 
            this.tabMain.Controls.Add(this.tabSell);
            this.tabMain.Controls.Add(this.tabItem);
            this.tabMain.Controls.Add(this.tabExpireItem);
            this.tabMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabMain.Location = new System.Drawing.Point(0, 0);
            this.tabMain.Name = "tabMain";
            this.tabMain.Padding = new System.Drawing.Point(15, 12);
            this.tabMain.RightToLeft = System.Windows.Forms.RightToLeft.Yes;
            this.tabMain.RightToLeftLayout = true;
            this.tabMain.SelectedIndex = 0;
            this.tabMain.Size = new System.Drawing.Size(1120, 630);
            this.tabMain.TabIndex = 0;
            // 
            // tabSell
            // 
            this.tabSell.BackColor = System.Drawing.Color.White;
            this.tabSell.Controls.Add(this.uc_SellListReport1);
            this.tabSell.Location = new System.Drawing.Point(4, 60);
            this.tabSell.Name = "tabSell";
            this.tabSell.Padding = new System.Windows.Forms.Padding(3);
            this.tabSell.Size = new System.Drawing.Size(1112, 566);
            this.tabSell.TabIndex = 0;
            this.tabSell.Text = "لیستی فرۆشتن";
            // 
            // tabItem
            // 
            this.tabItem.BackColor = System.Drawing.Color.White;
            this.tabItem.Location = new System.Drawing.Point(4, 60);
            this.tabItem.Name = "tabItem";
            this.tabItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabItem.Size = new System.Drawing.Size(1112, 566);
            this.tabItem.TabIndex = 1;
            this.tabItem.Text = "دەرمانە ناسێندراوەکان";
            // 
            // tabExpireItem
            // 
            this.tabExpireItem.BackColor = System.Drawing.Color.White;
            this.tabExpireItem.Location = new System.Drawing.Point(4, 60);
            this.tabExpireItem.Name = "tabExpireItem";
            this.tabExpireItem.Padding = new System.Windows.Forms.Padding(3);
            this.tabExpireItem.Size = new System.Drawing.Size(1112, 566);
            this.tabExpireItem.TabIndex = 2;
            this.tabExpireItem.Text = "دەرمانی بەسەرچوو";
            // 
            // uc_SellListReport1
            // 
            this.uc_SellListReport1.BackColor = System.Drawing.Color.White;
            this.uc_SellListReport1.Dock = System.Windows.Forms.DockStyle.Fill;
            this.uc_SellListReport1.Font = new System.Drawing.Font("Rabar_013", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.uc_SellListReport1.Location = new System.Drawing.Point(3, 3);
            this.uc_SellListReport1.Name = "uc_SellListReport1";
            this.uc_SellListReport1.Size = new System.Drawing.Size(1106, 560);
            this.uc_SellListReport1.TabIndex = 0;
            // 
            // frm_Reports
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(120F, 120F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Dpi;
            this.AutoSize = true;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(1120, 630);
            this.Controls.Add(this.tabMain);
            this.DoubleBuffered = true;
            this.Font = new System.Drawing.Font("Rabar_013", 12F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Name = "frm_Reports";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ڕاپۆرتەکان";
            this.tabMain.ResumeLayout(false);
            this.tabSell.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl tabMain;
        private System.Windows.Forms.TabPage tabSell;
        private System.Windows.Forms.TabPage tabItem;
        private System.Windows.Forms.TabPage tabExpireItem;
        private uc_SellListReport uc_SellListReport1;
    }
}