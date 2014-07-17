namespace Seds.WinForms
{
    partial class CertificateTypeForm
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
            this.label2 = new System.Windows.Forms.Label();
            this.textBox1 = new System.Windows.Forms.TextBox();
            this.btnSaveCertificateType = new System.Windows.Forms.Button();
            this.dgvCertificateType = new System.Windows.Forms.DataGridView();
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificateType)).BeginInit();
            this.SuspendLayout();
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 25);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(123, 17);
            this.label2.TabIndex = 1;
            this.label2.Text = "Naziv tipa diplome";
            // 
            // textBox1
            // 
            this.textBox1.Location = new System.Drawing.Point(153, 22);
            this.textBox1.Multiline = true;
            this.textBox1.Name = "textBox1";
            this.textBox1.Size = new System.Drawing.Size(274, 61);
            this.textBox1.TabIndex = 2;
            // 
            // btnSaveCertificateType
            // 
            this.btnSaveCertificateType.Location = new System.Drawing.Point(352, 100);
            this.btnSaveCertificateType.Name = "btnSaveCertificateType";
            this.btnSaveCertificateType.Size = new System.Drawing.Size(75, 33);
            this.btnSaveCertificateType.TabIndex = 3;
            this.btnSaveCertificateType.Text = "Spremi";
            this.btnSaveCertificateType.UseVisualStyleBackColor = true;
            this.btnSaveCertificateType.Click += new System.EventHandler(this.btnSaveCertificateType_Click);
            // 
            // dgvCertificateType
            // 
            this.dgvCertificateType.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvCertificateType.Location = new System.Drawing.Point(12, 173);
            this.dgvCertificateType.Name = "dgvCertificateType";
            this.dgvCertificateType.RowTemplate.Height = 24;
            this.dgvCertificateType.Size = new System.Drawing.Size(420, 155);
            this.dgvCertificateType.TabIndex = 4;
            // 
            // CertificateTypeForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 352);
            this.Controls.Add(this.dgvCertificateType);
            this.Controls.Add(this.btnSaveCertificateType);
            this.Controls.Add(this.textBox1);
            this.Controls.Add(this.label2);
            this.Name = "CertificateTypeForm";
            this.Text = "CertificateTypeForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvCertificateType)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox1;
        private System.Windows.Forms.Button btnSaveCertificateType;
        private System.Windows.Forms.DataGridView dgvCertificateType;
    }
}