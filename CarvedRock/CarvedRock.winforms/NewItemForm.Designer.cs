namespace CarvedRock
{
    partial class NewItemForm
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
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.txtItemDetail = new System.Windows.Forms.TextBox();
            this.txtItemText = new System.Windows.Forms.TextBox();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(34, 59);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(100, 25);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Text";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(39, 206);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(124, 25);
            this.label2.TabIndex = 1;
            this.label2.Text = "Item Details";
            // 
            // txtItemDetail
            // 
            this.txtItemDetail.AccessibleName = "ItemDetail";
            this.txtItemDetail.Location = new System.Drawing.Point(39, 264);
            this.txtItemDetail.Name = "txtItemDetail";
            this.txtItemDetail.Size = new System.Drawing.Size(687, 31);
            this.txtItemDetail.TabIndex = 2;
            // 
            // txtItemText
            // 
            this.txtItemText.AccessibleName = "ItemText";
            this.txtItemText.Location = new System.Drawing.Point(44, 109);
            this.txtItemText.Name = "txtItemText";
            this.txtItemText.Size = new System.Drawing.Size(682, 31);
            this.txtItemText.TabIndex = 1;
            // 
            // button1
            // 
            this.button1.AccessibleName = "Add";
            this.button1.Location = new System.Drawing.Point(306, 357);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(168, 61);
            this.button1.TabIndex = 4;
            this.button1.Text = "Add";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // NewItemForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.txtItemText);
            this.Controls.Add(this.txtItemDetail);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Name = "NewItemForm";
            this.Text = "NewItemForm";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox txtItemDetail;
        private System.Windows.Forms.TextBox txtItemText;
        private System.Windows.Forms.Button button1;
    }
}