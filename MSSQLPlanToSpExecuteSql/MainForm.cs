using MSSQLPlanToSpExecuteSql.Contracts;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml;

namespace MSSQLPlanToSpExecuteSql
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
            PlanXMLText.DragEnter += MainForm_DragEnter;
            PlanXMLText.DragDrop += MainForm_DragDrop;
        }

        private async Task<string> ReadFile(FileStream stream)
        {
            string result = "";

            using (var reader = new StreamReader(stream))
            {
                result = await reader.ReadToEndAsync();
            }
            return result;
        }

        private async void LoadPlanXMLFromFile(string fileName)
        {
            using (var fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read))
            {
                PlanXMLText.Text = await ReadFile(fileStream);                
            }
        }

        private void GenerateSqlButton_Click(object sender, EventArgs e)
        {
            string xmlStr = PlanXMLText.Text;

            if (string.IsNullOrEmpty(xmlStr))
            {
                return;
            }
            SpExecuteSqlText.Text = "";
            DirectSqlText.Text = "";

            try
            {
                foreach (var res in StatementExtractor.ConvertPlanToStatements(xmlStr))
                {
                    if (!string.IsNullOrEmpty(SpExecuteSqlText.Text))
                    {
                        SpExecuteSqlText.Text += Environment.NewLine + Environment.NewLine;
                    }
                    SpExecuteSqlText.Text += res.SpExecSql;

                    if (!string.IsNullOrEmpty(DirectSqlText.Text))
                    {
                        DirectSqlText.Text += Environment.NewLine + Environment.NewLine;
                    }
                    DirectSqlText.Text += res.DirectSql;
                }
            }
            catch (XmlException)
            {
                SpExecuteSqlText.Text = "Wrong showplan XML format!";
                DirectSqlText.Text = "Wrong showplan XML format!";
            }
        }

        void CopyContext(object sender, EventArgs e)
        {
            var tsItem = (ToolStripMenuItem)sender;
            var cms = (ContextMenuStrip)tsItem.Owner;            

            if (cms.SourceControl is RichTextBox)
            {
                var senderRTB = (RichTextBox)cms.SourceControl;                

                switch (senderRTB.Name)
                {
                    case "SpExecuteSqlText":
                        SpExecuteSqlText.Copy();
                        break;

                    case "DirectSqlText":
                        Clipboard.Clear();
                        DirectSqlText.Copy();                        
                        break;
                }
            }
        }

        void CopySpExecSql(object sender, EventArgs e)
        {
            Clipboard.SetText(SpExecuteSqlText.Text);
        }
        void CopyDirectSql(object sender, EventArgs e)
        {
            Clipboard.SetText(DirectSqlText.Text);
        }

        private void MainForm_Load(object sender, EventArgs e)
        {
            OpenFileDialog.InitialDirectory = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
        }

        private void Browse_Click(object sender, EventArgs e)
        {
            var dialogResult = OpenFileDialog.ShowDialog();

            if (dialogResult == DialogResult.OK)
            {
                LoadPlanXMLFromFile(OpenFileDialog.FileName);
            }
        }

        private void MainForm_DragEnter(object sender, DragEventArgs e)
        {
            if (e.Data.GetDataPresent(DataFormats.FileDrop))
            {
                e.Effect = DragDropEffects.Copy;
            }
        }

        private void MainForm_DragDrop(object sender, DragEventArgs e)
        {
            var files = (string[])e.Data.GetData(DataFormats.FileDrop);
            if (files.Length >= 1)
            {
                LoadPlanXMLFromFile(files[0]);
            }
        }
    }
}
