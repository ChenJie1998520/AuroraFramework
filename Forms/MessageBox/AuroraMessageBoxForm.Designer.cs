namespace AuroraFramework.Forms.MessageBox
{
    partial class AuroraMessageBoxForm
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
            this.tlpMain = new System.Windows.Forms.TableLayoutPanel();
            this.pnlBottom = new System.Windows.Forms.Panel();
            this.btnCancel = new Controls.AuroraButton();
            this.btnOK = new Controls.AuroraButton();
            this.pnlMain = new Controls.AuroraPanel();
            this.tlpContent = new System.Windows.Forms.TableLayoutPanel();
            this.lblMessage = new System.Windows.Forms.Label();
            this.pnlImage = new System.Windows.Forms.Panel();
            this.picImage = new System.Windows.Forms.PictureBox();
            this.tlpMain.SuspendLayout();
            this.pnlBottom.SuspendLayout();
            this.pnlMain.SuspendLayout();
            this.tlpContent.SuspendLayout();
            this.pnlImage.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.picImage)).BeginInit();
            this.SuspendLayout();

            // 
            // tlpMain
            // 
            this.tlpMain.BackColor = System.Drawing.Color.Transparent;
            this.tlpMain.ColumnCount = 1;
            this.tlpMain.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.Controls.Add(this.pnlBottom, 0, 1);
            this.tlpMain.Controls.Add(this.pnlMain, 0, 0);
            this.tlpMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpMain.Location = new System.Drawing.Point(1, 33);
            this.tlpMain.Name = "tlpMain";
            this.tlpMain.RowCount = 2;
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpMain.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 38F));
            this.tlpMain.Size = new System.Drawing.Size(423, 168);
            this.tlpMain.TabIndex = 0;
            // 
            // pnlBottom
            // 
            this.pnlBottom.Controls.Add(this.btnCancel);
            this.pnlBottom.Controls.Add(this.btnOK);
            this.pnlBottom.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlBottom.Location = new System.Drawing.Point(3, 133);
            this.pnlBottom.Name = "pnlBottom";
            this.pnlBottom.Size = new System.Drawing.Size(417, 32);
            this.pnlBottom.TabIndex = 1;
            // 
            // btnCancel
            // 
            this.btnCancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnCancel.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.btnCancel.Image = null;
            this.btnCancel.Location = new System.Drawing.Point(241, 1);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(100, 28);
            this.btnCancel.TabIndex = 1;
            this.btnCancel.Text = "取消";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.BtnCancel_Click);
            // 
            // btnOK
            // 
            this.btnOK.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btnOK.Image = null;
            this.btnOK.Location = new System.Drawing.Point(80, 1);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(100, 28);
            this.btnOK.TabIndex = 0;
            this.btnOK.Text = "确定";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.BtnOK_Click);
            // 
            // pnlMain
            // 
            this.pnlMain.BorderColor = System.Drawing.Color.LightGray;
            this.pnlMain.Controls.Add(this.tlpContent);
            this.pnlMain.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlMain.Location = new System.Drawing.Point(3, 3);
            this.pnlMain.Name = "pnlMain";
            this.pnlMain.RoundeStyle = AuroraFramework.Controls.AuroraRoundStyle.All;
            this.pnlMain.Size = new System.Drawing.Size(417, 124);
            this.pnlMain.TabIndex = 2;
            // 
            // tlpContent
            // 
            this.tlpContent.ColumnCount = 2;
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 92F));
            this.tlpContent.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tlpContent.Controls.Add(this.lblMessage, 1, 0);
            this.tlpContent.Controls.Add(this.pnlImage, 0, 1);
            this.tlpContent.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tlpContent.Location = new System.Drawing.Point(0, 0);
            this.tlpContent.Name = "tlpContent";
            this.tlpContent.RowCount = 3;
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 86F));
            this.tlpContent.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tlpContent.Size = new System.Drawing.Size(417, 124);
            this.tlpContent.TabIndex = 0;
            // 
            // lblMessage
            // 
            this.lblMessage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.lblMessage.Location = new System.Drawing.Point(97, 3);
            this.lblMessage.Margin = new System.Windows.Forms.Padding(5, 3, 3, 3);
            this.lblMessage.Name = "lblMessage";
            this.tlpContent.SetRowSpan(this.lblMessage, 3);
            this.lblMessage.Size = new System.Drawing.Size(317, 118);
            this.lblMessage.TabIndex = 3;
            this.lblMessage.Text = "Skin Message";
            this.lblMessage.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pnlImage
            // 
            this.pnlImage.Controls.Add(this.picImage);
            this.pnlImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pnlImage.Location = new System.Drawing.Point(3, 22);
            this.pnlImage.Name = "pnlImage";
            this.pnlImage.Padding = new System.Windows.Forms.Padding(5);
            this.pnlImage.Size = new System.Drawing.Size(86, 80);
            this.pnlImage.TabIndex = 2;
            // 
            // picImage
            // 
            this.picImage.Dock = System.Windows.Forms.DockStyle.Fill;
            this.picImage.Location = new System.Drawing.Point(5, 5);
            this.picImage.Name = "picImage";
            this.picImage.Padding = new System.Windows.Forms.Padding(12);
            this.picImage.Size = new System.Drawing.Size(76, 70);
            this.picImage.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.picImage.TabIndex = 2;
            this.picImage.TabStop = false;

            // 
            // AuroraMessageBoxForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackgroundColor.Factors = new float[0];
            this.BackgroundColor.FromColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.BackgroundColor.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Vertical;
            this.BackgroundColor.Positions = new float[0];
            this.BackgroundColor.ToColor = System.Drawing.SystemColors.GradientInactiveCaption;
            this.ClientSize = new System.Drawing.Size(436, 207);
            this.MinimumSize = new System.Drawing.Size(148, 32);
            this.Name = "AuroraMessageBoxForm";
            this.Text = "AuroraMessageBox";
            this.ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.TableLayoutPanel tlpMain;
        private System.Windows.Forms.Panel pnlBottom;
        private Controls.AuroraButton btnCancel;
        private Controls.AuroraButton btnOK;
        private Controls.AuroraPanel pnlMain;
        private System.Windows.Forms.TableLayoutPanel tlpContent;
        private System.Windows.Forms.Label lblMessage;
        private System.Windows.Forms.Panel pnlImage;
        private System.Windows.Forms.PictureBox picImage;
    }
}