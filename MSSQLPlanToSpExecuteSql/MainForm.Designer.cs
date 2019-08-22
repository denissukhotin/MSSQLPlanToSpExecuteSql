namespace MSSQLPlanToSpExecuteSql
{
    partial class MainForm
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
            this.GenerateSqlButton = new System.Windows.Forms.Button();
            this.PlanLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.SpExecuteSqlText = new System.Windows.Forms.RichTextBox();
            this.PlanXMLText = new System.Windows.Forms.RichTextBox();
            this.tableLayoutPanel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // GenerateSqlButton
            // 
            this.GenerateSqlButton.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.GenerateSqlButton.Location = new System.Drawing.Point(509, 24);
            this.GenerateSqlButton.Name = "GenerateSqlButton";
            this.GenerateSqlButton.Size = new System.Drawing.Size(54, 459);
            this.GenerateSqlButton.TabIndex = 2;
            this.GenerateSqlButton.Text = ">>";
            this.GenerateSqlButton.UseVisualStyleBackColor = true;
            this.GenerateSqlButton.Click += new System.EventHandler(this.GenerateSqlButton_Click);
            // 
            // PlanLabel
            // 
            this.PlanLabel.AutoSize = true;
            this.PlanLabel.Location = new System.Drawing.Point(3, 0);
            this.PlanLabel.Name = "PlanLabel";
            this.PlanLabel.Size = new System.Drawing.Size(212, 17);
            this.PlanLabel.TabIndex = 5;
            this.PlanLabel.Text = "Paste SQL query plan XML here";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(569, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(171, 17);
            this.label1.TabIndex = 6;
            this.label1.Text = "Grab your statement here";
            // 
            // tableLayoutPanel1
            // 
            this.tableLayoutPanel1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel1.ColumnCount = 3;
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 60F));
            this.tableLayoutPanel1.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel1.Controls.Add(this.PlanLabel, 0, 0);
            this.tableLayoutPanel1.Controls.Add(this.label1, 2, 0);
            this.tableLayoutPanel1.Controls.Add(this.GenerateSqlButton, 1, 1);
            this.tableLayoutPanel1.Controls.Add(this.SpExecuteSqlText, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.PlanXMLText, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.67902F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1073, 486);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // SpExecuteSqlText
            // 
            this.SpExecuteSqlText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SpExecuteSqlText.Location = new System.Drawing.Point(569, 24);
            this.SpExecuteSqlText.Name = "SpExecuteSqlText";
            this.SpExecuteSqlText.Size = new System.Drawing.Size(501, 459);
            this.SpExecuteSqlText.TabIndex = 7;
            this.SpExecuteSqlText.Text = "";
            // 
            // PlanXMLText
            // 
            this.PlanXMLText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlanXMLText.Location = new System.Drawing.Point(3, 24);
            this.PlanXMLText.Name = "PlanXMLText";
            this.PlanXMLText.Size = new System.Drawing.Size(500, 459);
            this.PlanXMLText.TabIndex = 8;
            this.PlanXMLText.Text = "";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 489);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Plan XML to sp_executesql";
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button GenerateSqlButton;
        private System.Windows.Forms.Label PlanLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.RichTextBox SpExecuteSqlText;
        private System.Windows.Forms.RichTextBox PlanXMLText;
    }
}

