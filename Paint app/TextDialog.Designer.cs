using System;

namespace Paint {
  partial class TextDialog {
    /// <summary>
    /// Required designer variable.
    /// </summary>
    private System.ComponentModel.IContainer components = null;

    /// <summary>
    /// Clean up any resources being used.
    /// </summary>
    /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
    protected override void Dispose(bool disposing) {
      if (disposing && (components != null)) {
        components.Dispose();
      }
      base.Dispose(disposing);
    }

    #region Windows Form Designer generated code

    /// <summary>
    /// Required method for Designer support - do not modify
    /// the contents of this method with the code editor.
    /// </summary>
    private void InitializeComponent() {
            this.label1 = new System.Windows.Forms.Label();
            this.textBox = new System.Windows.Forms.TextBox();
            this.fontBtn = new System.Windows.Forms.Button();
            this.cancelBtn = new System.Windows.Forms.Button();
            this.drawBtn = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(16, 11);
            this.label1.Margin = new System.Windows.Forms.Padding(4, 0, 4, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(39, 17);
            this.label1.TabIndex = 0;
            this.label1.Text = "Text:";
            // 
            // textBox
            // 
            this.textBox.Location = new System.Drawing.Point(16, 31);
            this.textBox.Margin = new System.Windows.Forms.Padding(4);
            this.textBox.Multiline = true;
            this.textBox.Name = "textBox";
            this.textBox.ScrollBars = System.Windows.Forms.ScrollBars.Both;
            this.textBox.Size = new System.Drawing.Size(488, 79);
            this.textBox.TabIndex = 1;
            this.textBox.TextChanged += new System.EventHandler(this.textBox_TextChanged);
            // 
            // fontBtn
            // 
            this.fontBtn.Location = new System.Drawing.Point(16, 118);
            this.fontBtn.Margin = new System.Windows.Forms.Padding(4);
            this.fontBtn.Name = "fontBtn";
            this.fontBtn.Size = new System.Drawing.Size(100, 28);
            this.fontBtn.TabIndex = 2;
            this.fontBtn.Text = "&Font...";
            this.fontBtn.UseVisualStyleBackColor = true;
            this.fontBtn.Click += new System.EventHandler(this.fontBtn_Click);
            // 
            // cancelBtn
            // 
            this.cancelBtn.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this.cancelBtn.Location = new System.Drawing.Point(405, 118);
            this.cancelBtn.Margin = new System.Windows.Forms.Padding(4);
            this.cancelBtn.Name = "cancelBtn";
            this.cancelBtn.Size = new System.Drawing.Size(100, 28);
            this.cancelBtn.TabIndex = 3;
            this.cancelBtn.Text = "&Cancel";
            this.cancelBtn.UseVisualStyleBackColor = true;
            // 
            // drawBtn
            // 
            this.drawBtn.DialogResult = System.Windows.Forms.DialogResult.OK;
            this.drawBtn.Location = new System.Drawing.Point(297, 118);
            this.drawBtn.Margin = new System.Windows.Forms.Padding(4);
            this.drawBtn.Name = "drawBtn";
            this.drawBtn.Size = new System.Drawing.Size(100, 28);
            this.drawBtn.TabIndex = 4;
            this.drawBtn.Text = "&Draw";
            this.drawBtn.UseVisualStyleBackColor = true;
            // 
            // TextDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(521, 161);
            this.Controls.Add(this.drawBtn);
            this.Controls.Add(this.cancelBtn);
            this.Controls.Add(this.fontBtn);
            this.Controls.Add(this.textBox);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Margin = new System.Windows.Forms.Padding(4);
            this.Name = "TextDialog";
            this.Text = "Text";
            this.Load += new System.EventHandler(this.TextDialog_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

    }

        private void TextDialog_Load(object sender, EventArgs e)
        {
            throw new NotImplementedException();
        }

        #endregion

        private System.Windows.Forms.Label label1;
    private System.Windows.Forms.TextBox textBox;
    private System.Windows.Forms.Button fontBtn;
    private System.Windows.Forms.Button cancelBtn;
    private System.Windows.Forms.Button drawBtn;
  }
}