namespace BoBOtoparking
{
    partial class BobOtoParkMainForm
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
            GirenAracBtn = new Button();
            CikanAracBtn = new Button();
            label1 = new Label();
            GirenAracPlakaNo = new TextBox();
            CikanAracPlakaNo = new TextBox();
            label2 = new Label();
            label3 = new Label();
            label4 = new Label();
            OtoparkAlanCB = new ComboBox();
            label5 = new Label();
            label6 = new Label();
            OtoparkIstasyonCB = new ComboBox();
            OtoparkAracGrupCB = new ComboBox();
            label8 = new Label();
            SuspendLayout();
            // 
            // GirenAracBtn
            // 
            GirenAracBtn.Location = new Point(101, 313);
            GirenAracBtn.Name = "GirenAracBtn";
            GirenAracBtn.Size = new Size(193, 73);
            GirenAracBtn.TabIndex = 0;
            GirenAracBtn.Text = "Giren Araç Plaka Okundu";
            GirenAracBtn.UseVisualStyleBackColor = true;
            GirenAracBtn.Click += GirenAracBtn_Click;
            // 
            // CikanAracBtn
            // 
            CikanAracBtn.Location = new Point(447, 313);
            CikanAracBtn.Name = "CikanAracBtn";
            CikanAracBtn.Size = new Size(193, 73);
            CikanAracBtn.TabIndex = 1;
            CikanAracBtn.Text = "Çıkan Araç Plaka Okundu";
            CikanAracBtn.UseVisualStyleBackColor = true;
            CikanAracBtn.Click += CikanAracBtn_Click;
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label1.Location = new Point(217, 110);
            label1.Name = "label1";
            label1.Size = new Size(30, 20);
            label1.TabIndex = 2;
            label1.Text = "???";
            // 
            // GirenAracPlakaNo
            // 
            GirenAracPlakaNo.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            GirenAracPlakaNo.Location = new Point(131, 260);
            GirenAracPlakaNo.Name = "GirenAracPlakaNo";
            GirenAracPlakaNo.Size = new Size(133, 32);
            GirenAracPlakaNo.TabIndex = 3;
            // 
            // CikanAracPlakaNo
            // 
            CikanAracPlakaNo.Font = new Font("Segoe UI", 14F, FontStyle.Bold, GraphicsUnit.Point);
            CikanAracPlakaNo.Location = new Point(472, 260);
            CikanAracPlakaNo.Name = "CikanAracPlakaNo";
            CikanAracPlakaNo.Size = new Size(133, 32);
            CikanAracPlakaNo.TabIndex = 4;
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label2.Location = new Point(217, 136);
            label2.Name = "label2";
            label2.Size = new Size(30, 20);
            label2.TabIndex = 5;
            label2.Text = "???";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label3.Location = new Point(84, 110);
            label3.Name = "label3";
            label3.Size = new Size(117, 20);
            label3.TabIndex = 6;
            label3.Text = "Alan Kapasitesi :";
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label4.Location = new Point(32, 136);
            label4.Name = "label4";
            label4.Size = new Size(169, 20);
            label4.TabIndex = 7;
            label4.Text = "Otoparktaki Araç Sayısı :";
            // 
            // OtoparkAlanCB
            // 
            OtoparkAlanCB.FormattingEnabled = true;
            OtoparkAlanCB.Location = new Point(143, 24);
            OtoparkAlanCB.Name = "OtoparkAlanCB";
            OtoparkAlanCB.Size = new Size(151, 23);
            OtoparkAlanCB.TabIndex = 9;
            OtoparkAlanCB.TextChanged += OtoparkAlanCB_TextChanged;
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label5.Location = new Point(64, 27);
            label5.Name = "label5";
            label5.Size = new Size(73, 20);
            label5.TabIndex = 10;
            label5.Text = "Alan Seç :";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label6.Location = new Point(41, 60);
            label6.Name = "label6";
            label6.Size = new Size(96, 20);
            label6.TabIndex = 11;
            label6.Text = "İstasyon Seç :";
            // 
            // OtoparkIstasyonCB
            // 
            OtoparkIstasyonCB.FormattingEnabled = true;
            OtoparkIstasyonCB.Location = new Point(143, 60);
            OtoparkIstasyonCB.Name = "OtoparkIstasyonCB";
            OtoparkIstasyonCB.Size = new Size(151, 23);
            OtoparkIstasyonCB.TabIndex = 12;
            OtoparkIstasyonCB.TextChanged += OtoparkIstasyonCB_TextChanged;
            // 
            // OtoparkAracGrupCB
            // 
            OtoparkAracGrupCB.FormattingEnabled = true;
            OtoparkAracGrupCB.Items.AddRange(new object[] { "0", "1", "2", "3", "4", "5", "6" });
            OtoparkAracGrupCB.Location = new Point(143, 188);
            OtoparkAracGrupCB.Name = "OtoparkAracGrupCB";
            OtoparkAracGrupCB.Size = new Size(151, 23);
            OtoparkAracGrupCB.TabIndex = 15;
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 11F, FontStyle.Regular, GraphicsUnit.Point);
            label8.Location = new Point(20, 188);
            label8.Name = "label8";
            label8.Size = new Size(117, 20);
            label8.TabIndex = 14;
            label8.Text = "Araç Grubu Seç :";
            // 
            // BobOtoParkMainForm
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(800, 538);
            Controls.Add(OtoparkAracGrupCB);
            Controls.Add(label8);
            Controls.Add(OtoparkIstasyonCB);
            Controls.Add(label6);
            Controls.Add(label5);
            Controls.Add(OtoparkAlanCB);
            Controls.Add(label4);
            Controls.Add(label3);
            Controls.Add(label2);
            Controls.Add(CikanAracPlakaNo);
            Controls.Add(GirenAracPlakaNo);
            Controls.Add(label1);
            Controls.Add(CikanAracBtn);
            Controls.Add(GirenAracBtn);
            Name = "BobOtoParkMainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "BoB Otopark ";
            Load += BobOtoParkMainForm_Load;
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private Button GirenAracBtn;
        private Button CikanAracBtn;
        private Label label1;
        private TextBox GirenAracPlakaNo;
        private TextBox CikanAracPlakaNo;
        private Label label2;
        private Label label3;
        private Label label4;
        private ComboBox OtoparkAlanCB;
        private Label label5;
        private Label label6;
        private ComboBox OtoparkIstasyonCB;
        private ComboBox OtoparkAracGrupCB;
        private Label label8;
    }
}