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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            btnUploadCsv = new Button();
            dgvOrdens = new DataGridView();
            dgvNegocios = new DataGridView();
            openFileDialog1 = new OpenFileDialog();
            labelTitle = new Label();
            descText = new Label();
            Negócios = new Label();
            label1 = new Label();
            pictureBox1 = new PictureBox();
            ((System.ComponentModel.ISupportInitialize)dgvOrdens).BeginInit();
            ((System.ComponentModel.ISupportInitialize)dgvNegocios).BeginInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            SuspendLayout();
            // 
            // btnUploadCsv
            // 
            btnUploadCsv.FlatStyle = FlatStyle.Flat;
            btnUploadCsv.Location = new Point(111, 117);
            btnUploadCsv.Name = "btnUploadCsv";
            btnUploadCsv.Size = new Size(113, 28);
            btnUploadCsv.TabIndex = 0;
            btnUploadCsv.Text = "Upload CSV";
            btnUploadCsv.UseVisualStyleBackColor = true;
            btnUploadCsv.Click += btnUploadCsv_Click;
            // 
            // dgvOrdens
            // 
            dgvOrdens.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvOrdens.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvOrdens.Location = new Point(89, 410);
            dgvOrdens.Name = "dgvOrdens";
            dgvOrdens.RowHeadersVisible = false;
            dgvOrdens.RowTemplate.Height = 25;
            dgvOrdens.Size = new Size(411, 121);
            dgvOrdens.TabIndex = 1;
            // 
            // dgvNegocios
            // 
            dgvNegocios.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;
            dgvNegocios.AutoSizeRowsMode = DataGridViewAutoSizeRowsMode.AllCells;
            dgvNegocios.ColumnHeadersHeightSizeMode = DataGridViewColumnHeadersHeightSizeMode.AutoSize;
            dgvNegocios.Location = new Point(89, 231);
            dgvNegocios.Name = "dgvNegocios";
            dgvNegocios.RowHeadersVisible = false;
            dgvNegocios.RowTemplate.Height = 25;
            dgvNegocios.Size = new Size(409, 115);
            dgvNegocios.TabIndex = 2;
            // 
            // openFileDialog1
            // 
            openFileDialog1.FileName = "openFileDialog1";
            // 
            // labelTitle
            // 
            labelTitle.AutoSize = true;
            labelTitle.BackColor = Color.Transparent;
            labelTitle.Font = new Font("Arial", 18F, FontStyle.Bold, GraphicsUnit.Point);
            labelTitle.ForeColor = Color.Navy;
            labelTitle.Location = new Point(99, 33);
            labelTitle.Name = "labelTitle";
            labelTitle.Size = new Size(394, 29);
            labelTitle.TabIndex = 3;
            labelTitle.Text = "Sistema de Negociação de Ativos";
            // 
            // descText
            // 
            descText.AutoSize = true;
            descText.Font = new Font("Arial", 12F, FontStyle.Regular, GraphicsUnit.Point);
            descText.Location = new Point(230, 121);
            descText.Name = "descText";
            descText.Size = new Size(234, 18);
            descText.TabIndex = 4;
            descText.Text = "Carregue aqui seu arquivo CSV ";
            // 
            // Negócios
            // 
            Negócios.AutoSize = true;
            Negócios.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            Negócios.Location = new Point(89, 197);
            Negócios.Name = "Negócios";
            Negócios.Size = new Size(82, 19);
            Negócios.TabIndex = 5;
            Negócios.Text = "Negócios";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Arial", 12F, FontStyle.Bold, GraphicsUnit.Point);
            label1.Location = new Point(89, 376);
            label1.Name = "label1";
            label1.Size = new Size(169, 19);
            label1.TabIndex = 6;
            label1.Text = "Ordens Processadas";
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(18, 12);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(75, 62);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // Form1
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.White;
            ClientSize = new Size(589, 591);
            Controls.Add(pictureBox1);
            Controls.Add(label1);
            Controls.Add(Negócios);
            Controls.Add(descText);
            Controls.Add(labelTitle);
            Controls.Add(dgvNegocios);
            Controls.Add(dgvOrdens);
            Controls.Add(btnUploadCsv);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Name = "Form1";
            Load += Form1_Load;
            ((System.ComponentModel.ISupportInitialize)dgvOrdens).EndInit();
            ((System.ComponentModel.ISupportInitialize)dgvNegocios).EndInit();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
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
        private Label Negócios;
        private Label label1;
        private PictureBox pictureBox1;
    }
}
