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
            this.components = new System.ComponentModel.Container();
            this.GenerateSqlButton = new System.Windows.Forms.Button();
            this.PlanLabel = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.tableLayoutPanel1 = new System.Windows.Forms.TableLayoutPanel();
            this.tableLayoutPanel2 = new System.Windows.Forms.TableLayoutPanel();
            this.DirectSqlText = new System.Windows.Forms.RichTextBox();
            this.contextMenuStrip1 = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.copyToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.SpExecuteSqlText = new System.Windows.Forms.RichTextBox();
            this.SpExecSqlCopy = new System.Windows.Forms.Button();
            this.DirectSqlCopy = new System.Windows.Forms.Button();
            this.tableLayoutPanel3 = new System.Windows.Forms.TableLayoutPanel();
            this.PlanXMLText = new System.Windows.Forms.RichTextBox();
            this.Browse = new System.Windows.Forms.Button();
            this.OpenFileDialog = new System.Windows.Forms.OpenFileDialog();
            this.tableLayoutPanel1.SuspendLayout();
            this.tableLayoutPanel2.SuspendLayout();
            this.contextMenuStrip1.SuspendLayout();
            this.tableLayoutPanel3.SuspendLayout();
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
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel2, 2, 1);
            this.tableLayoutPanel1.Controls.Add(this.tableLayoutPanel3, 0, 1);
            this.tableLayoutPanel1.Location = new System.Drawing.Point(3, 2);
            this.tableLayoutPanel1.Name = "tableLayoutPanel1";
            this.tableLayoutPanel1.RowCount = 2;
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 21F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 95.67902F));
            this.tableLayoutPanel1.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Absolute, 20F));
            this.tableLayoutPanel1.Size = new System.Drawing.Size(1073, 486);
            this.tableLayoutPanel1.TabIndex = 7;
            // 
            // tableLayoutPanel2
            // 
            this.tableLayoutPanel2.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel2.ColumnCount = 2;
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel2.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Absolute, 75F));
            this.tableLayoutPanel2.Controls.Add(this.DirectSqlText, 0, 1);
            this.tableLayoutPanel2.Controls.Add(this.SpExecuteSqlText, 0, 0);
            this.tableLayoutPanel2.Controls.Add(this.SpExecSqlCopy, 1, 0);
            this.tableLayoutPanel2.Controls.Add(this.DirectSqlCopy, 1, 1);
            this.tableLayoutPanel2.Location = new System.Drawing.Point(569, 24);
            this.tableLayoutPanel2.Name = "tableLayoutPanel2";
            this.tableLayoutPanel2.RowCount = 2;
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 50F));
            this.tableLayoutPanel2.Size = new System.Drawing.Size(501, 459);
            this.tableLayoutPanel2.TabIndex = 9;
            // 
            // DirectSqlText
            // 
            this.DirectSqlText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectSqlText.ContextMenuStrip = this.contextMenuStrip1;
            this.DirectSqlText.Location = new System.Drawing.Point(3, 232);
            this.DirectSqlText.Name = "DirectSqlText";
            this.DirectSqlText.ReadOnly = true;
            this.DirectSqlText.Size = new System.Drawing.Size(420, 224);
            this.DirectSqlText.TabIndex = 9;
            this.DirectSqlText.Text = "";
            // 
            // contextMenuStrip1
            // 
            this.contextMenuStrip1.ImageScalingSize = new System.Drawing.Size(20, 20);
            this.contextMenuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.copyToolStripMenuItem});
            this.contextMenuStrip1.Name = "contextMenuStrip1";
            this.contextMenuStrip1.Size = new System.Drawing.Size(113, 28);
            // 
            // copyToolStripMenuItem
            // 
            this.copyToolStripMenuItem.Name = "copyToolStripMenuItem";
            this.copyToolStripMenuItem.Size = new System.Drawing.Size(112, 24);
            this.copyToolStripMenuItem.Text = "Copy";
            this.copyToolStripMenuItem.Click += new System.EventHandler(this.CopyContext);
            // 
            // SpExecuteSqlText
            // 
            this.SpExecuteSqlText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.SpExecuteSqlText.ContextMenuStrip = this.contextMenuStrip1;
            this.SpExecuteSqlText.Location = new System.Drawing.Point(3, 3);
            this.SpExecuteSqlText.Name = "SpExecuteSqlText";
            this.SpExecuteSqlText.ReadOnly = true;
            this.SpExecuteSqlText.Size = new System.Drawing.Size(420, 223);
            this.SpExecuteSqlText.TabIndex = 8;
            this.SpExecuteSqlText.Text = "";
            // 
            // SpExecSqlCopy
            // 
            this.SpExecSqlCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.SpExecSqlCopy.Location = new System.Drawing.Point(429, 3);
            this.SpExecSqlCopy.Name = "SpExecSqlCopy";
            this.SpExecSqlCopy.Size = new System.Drawing.Size(69, 40);
            this.SpExecSqlCopy.TabIndex = 10;
            this.SpExecSqlCopy.Text = "Copy";
            this.SpExecSqlCopy.UseVisualStyleBackColor = true;
            this.SpExecSqlCopy.Click += new System.EventHandler(this.CopySpExecSql);
            // 
            // DirectSqlCopy
            // 
            this.DirectSqlCopy.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.DirectSqlCopy.Location = new System.Drawing.Point(429, 232);
            this.DirectSqlCopy.Name = "DirectSqlCopy";
            this.DirectSqlCopy.Size = new System.Drawing.Size(69, 40);
            this.DirectSqlCopy.TabIndex = 11;
            this.DirectSqlCopy.Text = "Copy";
            this.DirectSqlCopy.UseVisualStyleBackColor = true;
            this.DirectSqlCopy.Click += new System.EventHandler(this.CopyDirectSql);
            // 
            // tableLayoutPanel3
            // 
            this.tableLayoutPanel3.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tableLayoutPanel3.ColumnCount = 1;
            this.tableLayoutPanel3.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 100F));
            this.tableLayoutPanel3.Controls.Add(this.PlanXMLText, 0, 0);
            this.tableLayoutPanel3.Controls.Add(this.Browse, 0, 1);
            this.tableLayoutPanel3.Location = new System.Drawing.Point(3, 24);
            this.tableLayoutPanel3.Name = "tableLayoutPanel3";
            this.tableLayoutPanel3.RowCount = 2;
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 93.24619F));
            this.tableLayoutPanel3.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 6.753813F));
            this.tableLayoutPanel3.Size = new System.Drawing.Size(500, 459);
            this.tableLayoutPanel3.TabIndex = 10;
            // 
            // PlanXMLText
            // 
            this.PlanXMLText.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.PlanXMLText.Location = new System.Drawing.Point(3, 3);
            this.PlanXMLText.Name = "PlanXMLText";
            this.PlanXMLText.Size = new System.Drawing.Size(494, 421);
            this.PlanXMLText.TabIndex = 9;
            this.PlanXMLText.Text = "";
            // 
            // Browse
            // 
            this.Browse.Location = new System.Drawing.Point(3, 430);
            this.Browse.Name = "Browse";
            this.Browse.Size = new System.Drawing.Size(69, 26);
            this.Browse.TabIndex = 10;
            this.Browse.Text = "Browse";
            this.Browse.UseVisualStyleBackColor = true;
            this.Browse.Click += new System.EventHandler(this.Browse_Click);
            // 
            // OpenFileDialog
            // 
            this.OpenFileDialog.RestoreDirectory = true;
            this.OpenFileDialog.Title = "Select showplan file...";
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(1079, 489);
            this.Controls.Add(this.tableLayoutPanel1);
            this.Name = "MainForm";
            this.Text = "Plan XML to sp_executesql";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.tableLayoutPanel1.ResumeLayout(false);
            this.tableLayoutPanel1.PerformLayout();
            this.tableLayoutPanel2.ResumeLayout(false);
            this.contextMenuStrip1.ResumeLayout(false);
            this.tableLayoutPanel3.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.Button GenerateSqlButton;
        private System.Windows.Forms.Label PlanLabel;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel1;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel2;
        private System.Windows.Forms.RichTextBox SpExecuteSqlText;
        private System.Windows.Forms.RichTextBox DirectSqlText;
        private System.Windows.Forms.ContextMenuStrip contextMenuStrip1;
        private System.Windows.Forms.ToolStripMenuItem copyToolStripMenuItem;
        private System.Windows.Forms.Button SpExecSqlCopy;
        private System.Windows.Forms.Button DirectSqlCopy;
        private System.Windows.Forms.TableLayoutPanel tableLayoutPanel3;
        private System.Windows.Forms.RichTextBox PlanXMLText;
        private System.Windows.Forms.Button Browse;
        private System.Windows.Forms.OpenFileDialog OpenFileDialog;
    }
}

