namespace RazorPay
{
    partial class Form1
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(Form1));
            this.pic_bg = new System.Windows.Forms.PictureBox();
            this.pic_logo = new System.Windows.Forms.PictureBox();
            this.lbl_name = new DrakeUI.Framework.DrakeUILabel();
            this.lbl_selectfile = new DrakeUI.Framework.DrakeUILabel();
            this.txt_browse = new DrakeUI.Framework.DrakeUITextBox();
            this.btn_Browse = new DrakeUI.Framework.DrakeUIButtonIcon();
            this.btn_save = new DrakeUI.Framework.DrakeUIButtonIcon();
            ((System.ComponentModel.ISupportInitialize)(this.pic_bg)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).BeginInit();
            this.SuspendLayout();
            // 
            // pic_bg
            // 
            this.pic_bg.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pic_bg.Image = ((System.Drawing.Image)(resources.GetObject("pic_bg.Image")));
            this.pic_bg.Location = new System.Drawing.Point(0, 0);
            this.pic_bg.Name = "pic_bg";
            this.pic_bg.Size = new System.Drawing.Size(788, 459);
            this.pic_bg.SizeMode = System.Windows.Forms.PictureBoxSizeMode.StretchImage;
            this.pic_bg.TabIndex = 0;
            this.pic_bg.TabStop = false;
            // 
            // pic_logo
            // 
            this.pic_logo.Image = ((System.Drawing.Image)(resources.GetObject("pic_logo.Image")));
            this.pic_logo.Location = new System.Drawing.Point(48, 40);
            this.pic_logo.Name = "pic_logo";
            this.pic_logo.Size = new System.Drawing.Size(126, 52);
            this.pic_logo.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pic_logo.TabIndex = 1;
            this.pic_logo.TabStop = false;
            // 
            // lbl_name
            // 
            this.lbl_name.Font = new System.Drawing.Font("Century Gothic", 8.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_name.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(60)))), ((int)(((byte)(76)))));
            this.lbl_name.Location = new System.Drawing.Point(77, 95);
            this.lbl_name.Name = "lbl_name";
            this.lbl_name.Size = new System.Drawing.Size(60, 15);
            this.lbl_name.TabIndex = 2;
            this.lbl_name.Text = "RazorPay";
            this.lbl_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // lbl_selectfile
            // 
            this.lbl_selectfile.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.lbl_selectfile.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lbl_selectfile.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(15)))), ((int)(((byte)(60)))), ((int)(((byte)(76)))));
            this.lbl_selectfile.Location = new System.Drawing.Point(125, 183);
            this.lbl_selectfile.Name = "lbl_selectfile";
            this.lbl_selectfile.Size = new System.Drawing.Size(133, 23);
            this.lbl_selectfile.TabIndex = 3;
            this.lbl_selectfile.Text = "Select Excel File";
            this.lbl_selectfile.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // txt_browse
            // 
            this.txt_browse.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.txt_browse.Cursor = System.Windows.Forms.Cursors.IBeam;
            this.txt_browse.FillColor = System.Drawing.Color.White;
            this.txt_browse.Font = new System.Drawing.Font("Segoe UI", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.txt_browse.Location = new System.Drawing.Point(129, 211);
            this.txt_browse.Margin = new System.Windows.Forms.Padding(4, 5, 4, 5);
            this.txt_browse.Maximum = 2147483647D;
            this.txt_browse.Minimum = -2147483648D;
            this.txt_browse.Name = "txt_browse";
            this.txt_browse.Padding = new System.Windows.Forms.Padding(5);
            this.txt_browse.RectColor = System.Drawing.Color.White;
            this.txt_browse.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.txt_browse.Size = new System.Drawing.Size(316, 27);
            this.txt_browse.Style = DrakeUI.Framework.UIStyle.Custom;
            this.txt_browse.TabIndex = 4;
            // 
            // btn_Browse
            // 
            this.btn_Browse.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_Browse.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_Browse.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(90)))), ((int)(((byte)(106)))));
            this.btn_Browse.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_Browse.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_Browse.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_Browse.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Browse.Location = new System.Drawing.Point(456, 210);
            this.btn_Browse.Name = "btn_Browse";
            this.btn_Browse.Radius = 0;
            this.btn_Browse.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(90)))), ((int)(((byte)(106)))));
            this.btn_Browse.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btn_Browse.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_Browse.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_Browse.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_Browse.Size = new System.Drawing.Size(126, 28);
            this.btn_Browse.Style = DrakeUI.Framework.UIStyle.Custom;
            this.btn_Browse.Symbol = 61564;
            this.btn_Browse.TabIndex = 5;
            this.btn_Browse.Text = "Browse File";
            this.btn_Browse.TipsFont = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_Browse.Click += new System.EventHandler(this.btn_Browse_Click_1);
            // 
            // btn_save
            // 
            this.btn_save.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.btn_save.Cursor = System.Windows.Forms.Cursors.Hand;
            this.btn_save.FillColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(90)))), ((int)(((byte)(106)))));
            this.btn_save.FillHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_save.FillPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_save.FillSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_save.Font = new System.Drawing.Font("Century Gothic", 11.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Location = new System.Drawing.Point(253, 287);
            this.btn_save.Name = "btn_save";
            this.btn_save.Radius = 0;
            this.btn_save.RectColor = System.Drawing.Color.FromArgb(((int)(((byte)(14)))), ((int)(((byte)(90)))), ((int)(((byte)(106)))));
            this.btn_save.RectDisableColor = System.Drawing.Color.FromArgb(((int)(((byte)(227)))), ((int)(((byte)(242)))), ((int)(((byte)(253)))));
            this.btn_save.RectHoverColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_save.RectPressColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_save.RectSelectedColor = System.Drawing.Color.FromArgb(((int)(((byte)(30)))), ((int)(((byte)(136)))), ((int)(((byte)(229)))));
            this.btn_save.Size = new System.Drawing.Size(213, 33);
            this.btn_save.Style = DrakeUI.Framework.UIStyle.Custom;
            this.btn_save.Symbol = 57439;
            this.btn_save.TabIndex = 6;
            this.btn_save.Text = "Save File";
            this.btn_save.TipsFont = new System.Drawing.Font("Century Gothic", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.btn_save.Click += new System.EventHandler(this.btn_save_Click);
            // 
            // Form1
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(788, 459);
            this.Controls.Add(this.btn_save);
            this.Controls.Add(this.btn_Browse);
            this.Controls.Add(this.txt_browse);
            this.Controls.Add(this.lbl_selectfile);
            this.Controls.Add(this.lbl_name);
            this.Controls.Add(this.pic_logo);
            this.Controls.Add(this.pic_bg);
            this.Name = "Form1";
            this.Opacity = 0.98D;
            this.Text = "RazorPay";
            this.Load += new System.EventHandler(this.Form1_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pic_bg)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pic_logo)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.PictureBox pic_bg;
        private System.Windows.Forms.PictureBox pic_logo;
        private DrakeUI.Framework.DrakeUILabel lbl_name;
        private DrakeUI.Framework.DrakeUILabel lbl_selectfile;
        private DrakeUI.Framework.DrakeUITextBox txt_browse;
        private DrakeUI.Framework.DrakeUIButtonIcon btn_Browse;
        private DrakeUI.Framework.DrakeUIButtonIcon btn_save;
    }
}

