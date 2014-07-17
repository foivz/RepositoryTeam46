namespace Seds.WinForms
{
    partial class MasterForm
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
            this.dgvStudents = new System.Windows.Forms.DataGridView();
            this.btnStudents = new System.Windows.Forms.Button();
            this.btnAddNewStudy = new System.Windows.Forms.Button();
            this.btnRemove = new System.Windows.Forms.Button();
            this.btnCertificateType = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).BeginInit();
            this.SuspendLayout();
            // 
            // dgvStudents
            // 
            this.dgvStudents.ColumnHeadersHeightSizeMode = System.Windows.Forms.DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            this.dgvStudents.Location = new System.Drawing.Point(12, 47);
            this.dgvStudents.Name = "dgvStudents";
            this.dgvStudents.RowTemplate.Height = 24;
            this.dgvStudents.Size = new System.Drawing.Size(982, 204);
            this.dgvStudents.TabIndex = 0;
            // 
            // btnStudents
            // 
            this.btnStudents.Location = new System.Drawing.Point(12, 280);
            this.btnStudents.Name = "btnStudents";
            this.btnStudents.Size = new System.Drawing.Size(75, 23);
            this.btnStudents.TabIndex = 1;
            this.btnStudents.Text = "Dohvati";
            this.btnStudents.UseVisualStyleBackColor = true;
            this.btnStudents.Click += new System.EventHandler(this.button1_Click);
            // 
            // btnAddNewStudy
            // 
            this.btnAddNewStudy.Location = new System.Drawing.Point(123, 280);
            this.btnAddNewStudy.Name = "btnAddNewStudy";
            this.btnAddNewStudy.Size = new System.Drawing.Size(75, 23);
            this.btnAddNewStudy.TabIndex = 2;
            this.btnAddNewStudy.Text = "Dodaj studij";
            this.btnAddNewStudy.UseVisualStyleBackColor = true;
            this.btnAddNewStudy.Click += new System.EventHandler(this.btnAddNewStudy_Click);
            // 
            // btnRemove
            // 
            this.btnRemove.Location = new System.Drawing.Point(243, 280);
            this.btnRemove.Name = "btnRemove";
            this.btnRemove.Size = new System.Drawing.Size(75, 23);
            this.btnRemove.TabIndex = 3;
            this.btnRemove.Text = "Obriši";
            this.btnRemove.UseVisualStyleBackColor = true;
            this.btnRemove.Click += new System.EventHandler(this.btnRemove_Click);
            // 
            // btnCertificateType
            // 
            this.btnCertificateType.Location = new System.Drawing.Point(13, 463);
            this.btnCertificateType.Name = "btnCertificateType";
            this.btnCertificateType.Size = new System.Drawing.Size(145, 23);
            this.btnCertificateType.TabIndex = 4;
            this.btnCertificateType.Text = "Tip diplome";
            this.btnCertificateType.UseVisualStyleBackColor = true;
            this.btnCertificateType.Click += new System.EventHandler(this.btnCertificateType_Click);
            // 
            // MasterForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1060, 722);
            this.Controls.Add(this.btnCertificateType);
            this.Controls.Add(this.btnRemove);
            this.Controls.Add(this.btnAddNewStudy);
            this.Controls.Add(this.btnStudents);
            this.Controls.Add(this.dgvStudents);
            this.Name = "MasterForm";
            this.Text = "MasterForm";
            ((System.ComponentModel.ISupportInitialize)(this.dgvStudents)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.DataGridView dgvStudents;
        private System.Windows.Forms.Button btnStudents;
        private System.Windows.Forms.Button btnAddNewStudy;
        private System.Windows.Forms.Button btnRemove;
        private System.Windows.Forms.Button btnCertificateType;

    }
}