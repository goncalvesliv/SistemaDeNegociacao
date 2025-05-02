namespace OrderUI
{
    partial class Form1
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
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
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            btnUploadCsv = new Button();
            dgvOrdens = new DataGridView();
            dgvNegocios = new DataGridView();
            openFileDialog1 = new OpenFileDialog();
            labelTitle = new Label();
            descText = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvOrdens).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvNegocios).BeginInit();
            SuspendLayout();
            // 
            // btnUploadCsv
            // 
            btnUploadCsv.FlatStyle = FlatStyle.Flat;
            btnUploadCsv.Location = new Point(430, 114);
            btnUploadCsv.Name = "btnUploadCsv";
            btnUploadCsv.Size = new Size(113, 28);
            btnUploadCsv.TabIndex = 0;
            btnUploadCsv.Text = "Upload CSV";
            btnUploadCsv.UseVisualStyleBackColor = true;
            btnUploadCsv.Click += btnUploadCsv_Click;

            // 
            // dgvOrdens
            // 
            dgvOrdens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdens.Location = new Point(142, 400);
            dgvOrdens.Name = "dgvOrdens";
            dgvOrdens.RowTemplate.Height = 25;
            dgvOrdens.Size = new Size(512, 120);
            dgvOrdens.TabIndex = 1;
            // 
            // dgvNegocios
            // 
            dgvNegocios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNegocios.Location = new Point(142, 201);
            dgvNegocios.Name = "dgvNegocios";
            dgvNegocios.RowTemplate.Height = 25;
            dgvNegocios.Size = new Size(512, 115);
            dgvNegocios.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitle.Location = new Point(211, 45);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(394, 29);
            labelTitle.TabIndex = 3;
            labelTitle.Text = "Sistema de Negociação de Ativos";
            // 
            // descText
            // 
            descText.AutoSize = true;
            descText.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point);
            descText.Location = new Point(224, 118);
            descText.Name = "descText";
            descText.Size = new Size(200, 18);
            descText.TabIndex = 4;
            descText.Text = "Carregue seu arquivo CSV ";
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 604);
            Controls.Add(descText);
            Controls.Add(labelTitle);
            Controls.Add(dgvNegocios);
            Controls.Add(dgvOrdens);
            Controls.Add(btnUploadCsv);
            Name = "Form1";
            Text = "Form1";
            ((System.ComponentModel.ISupportInitialize)dgvOrdens).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvNegocios).EndInit();
            this.Load += new System.EventHandler(this.Form1_Load);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button btnUploadCsv;
        private DataGridView dgvOrdens;
        private DataGridView dgvNegocios;
        private OpenFileDialog openFileDialog1;
        private Label labelTitle;
        private Label descText;
    }
}
