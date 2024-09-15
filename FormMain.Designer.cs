namespace WinFormsAppImageEditor
{
    partial class FormMain
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
            toolStrip1 = new ToolStrip();
            loadToolStripButton = new ToolStripButton();
            saveToolStripButton = new ToolStripButton();
            toolStripSeparator1 = new ToolStripSeparator();
            справкаToolStripButton1 = new ToolStripButton();
            pictureBox = new PictureBox();
            groupBox = new GroupBox();
            bttnShading = new Button();
            toolStrip1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).BeginInit();
            groupBox.SuspendLayout();
            SuspendLayout();
            // 
            // toolStrip1
            // 
            toolStrip1.ImageScalingSize = new Size(20, 20);
            toolStrip1.Items.AddRange(new ToolStripItem[] { loadToolStripButton, saveToolStripButton, toolStripSeparator1, справкаToolStripButton1 });
            toolStrip1.Location = new Point(0, 0);
            toolStrip1.Name = "toolStrip1";
            toolStrip1.Size = new Size(914, 27);
            toolStrip1.TabIndex = 0;
            toolStrip1.Text = "toolStrip1";
            // 
            // loadToolStripButton
            // 
            loadToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            loadToolStripButton.Image = Properties.Resources._8664907_folder_open_document_icon;
            loadToolStripButton.ImageTransparentColor = Color.Magenta;
            loadToolStripButton.Name = "loadToolStripButton";
            loadToolStripButton.Size = new Size(29, 24);
            loadToolStripButton.Text = "&Открыть";
            loadToolStripButton.Click += loadToolStripButton_Click;
            // 
            // saveToolStripButton
            // 
            saveToolStripButton.DisplayStyle = ToolStripItemDisplayStyle.Image;
            saveToolStripButton.Image = Properties.Resources._9054919_bx_save_icon;
            saveToolStripButton.ImageTransparentColor = Color.Magenta;
            saveToolStripButton.Name = "saveToolStripButton";
            saveToolStripButton.Size = new Size(29, 24);
            saveToolStripButton.Text = "&Сохранить";
            saveToolStripButton.Click += saveToolStripButton_Click;
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new Size(6, 27);
            // 
            // справкаToolStripButton1
            // 
            справкаToolStripButton1.DisplayStyle = ToolStripItemDisplayStyle.Image;
            справкаToolStripButton1.Image = Properties.Resources._8207898_about_info_information_help_ui_icon;
            справкаToolStripButton1.ImageTransparentColor = Color.Magenta;
            справкаToolStripButton1.Name = "справкаToolStripButton1";
            справкаToolStripButton1.Size = new Size(29, 24);
            справкаToolStripButton1.Text = "С&правка";
            // 
            // pictureBox
            // 
            pictureBox.Location = new Point(14, 37);
            pictureBox.Margin = new Padding(3, 4, 3, 4);
            pictureBox.Name = "pictureBox";
            pictureBox.Size = new Size(286, 333);
            pictureBox.SizeMode = PictureBoxSizeMode.StretchImage;
            pictureBox.TabIndex = 1;
            pictureBox.TabStop = false;
            pictureBox.Click += pictureBox_Click;
            // 
            // groupBox
            // 
            groupBox.Controls.Add(bttnShading);
            groupBox.Location = new Point(597, 37);
            groupBox.Margin = new Padding(3, 4, 3, 4);
            groupBox.Name = "groupBox";
            groupBox.Padding = new Padding(3, 4, 3, 4);
            groupBox.Size = new Size(304, 333);
            groupBox.TabIndex = 2;
            groupBox.TabStop = false;
            groupBox.Text = "Функции обработки";
            // 
            // bttnShading
            // 
            bttnShading.Location = new Point(7, 29);
            bttnShading.Margin = new Padding(3, 4, 3, 4);
            bttnShading.Name = "bttnShading";
            bttnShading.Size = new Size(290, 40);
            bttnShading.TabIndex = 0;
            bttnShading.Text = "Расстушёвка";
            bttnShading.UseVisualStyleBackColor = true;
            bttnShading.Click += bttnShading_Click;
            // 
            // FormMain
            // 
            AutoScaleDimensions = new SizeF(8F, 20F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(914, 423);
            Controls.Add(groupBox);
            Controls.Add(pictureBox);
            Controls.Add(toolStrip1);
            Margin = new Padding(3, 4, 3, 4);
            Name = "FormMain";
            Text = "Main Form";
            toolStrip1.ResumeLayout(false);
            toolStrip1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox).EndInit();
            groupBox.ResumeLayout(false);
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ToolStrip toolStrip1;
        private PictureBox pictureBox;
        private GroupBox groupBox;
        private Button bttnShading;
        private ToolStripButton loadToolStripButton;
        private ToolStripButton saveToolStripButton;
        private ToolStripSeparator toolStripSeparator1;
        private ToolStripButton справкаToolStripButton1;
    }
}
