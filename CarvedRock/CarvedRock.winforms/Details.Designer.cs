namespace CarvedRock
{
    partial class Details
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
            this.lblItemText = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.lblItemDetail = new System.Windows.Forms.Label();
            this.button1 = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AccessibleName = "";
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(61, 53);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(136, 31);
            this.label1.TabIndex = 0;
            this.label1.Text = "Item Text";
            // 
            // lblItemText
            // 
            this.lblItemText.AccessibleName = "ItemText";
            this.lblItemText.AutoSize = true;
            this.lblItemText.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemText.Location = new System.Drawing.Point(61, 115);
            this.lblItemText.Name = "lblItemText";
            this.lblItemText.Size = new System.Drawing.Size(127, 31);
            this.lblItemText.TabIndex = 0;
            this.lblItemText.Text = "Item Text";
            // 
            // label3
            // 
            this.label3.AccessibleName = "";
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(61, 179);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(136, 31);
            this.label3.TabIndex = 0;
            this.label3.Text = "Item Text";
            // 
            // lblItemDetail
            // 
            this.lblItemDetail.AccessibleName = "ItemDetail";
            this.lblItemDetail.AutoSize = true;
            this.lblItemDetail.Font = new System.Drawing.Font("Microsoft Sans Serif", 10F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.lblItemDetail.Location = new System.Drawing.Point(61, 235);
            this.lblItemDetail.Name = "lblItemDetail";
            this.lblItemDetail.Size = new System.Drawing.Size(127, 31);
            this.lblItemDetail.TabIndex = 0;
            this.lblItemDetail.Text = "Item Text";
            // 
            // button1
            // 
            this.button1.AccessibleName = "Ok";
            this.button1.Location = new System.Drawing.Point(329, 358);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(135, 60);
            this.button1.TabIndex = 1;
            this.button1.Text = "Ok";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // Details
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(12F, 25F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.lblItemDetail);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.lblItemText);
            this.Controls.Add(this.label1);
            this.Name = "Details";
            this.Text = "Details";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label lblItemText;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblItemDetail;
        private System.Windows.Forms.Button button1;
    }
}