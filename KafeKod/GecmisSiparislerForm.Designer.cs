namespace KafeKod
{
    partial class GecmisSiparislerForm
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
            this.dgvSiparisler = new System.Windows.Forms.DataGridView();
            this.label1 = new System.Windows.Forms.Label();
            this.splitContainer1 = new System.Windows.Forms.SplitContainer();
            this.label2 = new System.Windows.Forms.Label();
            this.dgvSiparisDetay = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiparisler)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).BeginInit();
            this.splitContainer1.Panel1.SuspendLayout();
            this.splitContainer1.Panel2.SuspendLayout();
            this.splitContainer1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiparisDetay)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvSiparisler
            // 
            this.dgvSiparisler.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSiparisler.Location = new System.Drawing.Point(19, 26);
            this.dgvSiparisler.Name = "dgvSiparisler";
            this.dgvSiparisler.Size = new System.Drawing.Size(726, 181);
            this.dgvSiparisler.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 10);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(73, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "SİPARİŞLER ";
            // 
            // splitContainer1
            // 
            this.splitContainer1.Location = new System.Drawing.Point(21, 12);
            this.splitContainer1.Name = "splitContainer1";
            this.splitContainer1.Orientation = System.Windows.Forms.Orientation.Horizontal;
            // 
            // splitContainer1.Panel1
            // 
            this.splitContainer1.Panel1.Controls.Add(this.label1);
            this.splitContainer1.Panel1.Controls.Add(this.dgvSiparisler);
            // 
            // splitContainer1.Panel2
            // 
            this.splitContainer1.Panel2.Controls.Add(this.label2);
            this.splitContainer1.Panel2.Controls.Add(this.dgvSiparisDetay);
            this.splitContainer1.Size = new System.Drawing.Size(767, 471);
            this.splitContainer1.SplitterDistance = 235;
            this.splitContainer1.TabIndex = 4;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(16, 13);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(112, 13);
            this.label2.TabIndex = 5;
            this.label2.Text = "SİPARİŞ DETAYLARI";
            // 
            // dgvSiparisDetay
            // 
            this.dgvSiparisDetay.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvSiparisDetay.Location = new System.Drawing.Point(19, 29);
            this.dgvSiparisDetay.Name = "dgvSiparisDetay";
            this.dgvSiparisDetay.Size = new System.Drawing.Size(726, 182);
            this.dgvSiparisDetay.TabIndex = 4;
            // 
            // GecmisSiparislerForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(795, 496);
            this.Controls.Add(this.splitContainer1);
            this.Name = "GecmisSiparislerForm";
            this.Text = "GecmisSiparislerForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiparisler)).EndInit();
            this.splitContainer1.Panel1.ResumeLayout(false);
            this.splitContainer1.Panel1.PerformLayout();
            this.splitContainer1.Panel2.ResumeLayout(false);
            this.splitContainer1.Panel2.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer1)).EndInit();
            this.splitContainer1.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.dgvSiparisDetay)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.DataGridView dgvSiparisler;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.SplitContainer splitContainer1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.DataGridView dgvSiparisDetay;
    }
}