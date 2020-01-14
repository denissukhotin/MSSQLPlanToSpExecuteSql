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

namespace MSSQLPlanToSpExecuteSql
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private async Task<string> ReadFile(FileStream stream)
        {
            string result = "";

            var buffer = new char[12];

            using (var reader = new StreamReader(stream))
            {
                if (reader.Read(buffer, 0, buffer.Length) > 0)
                {
                    var startingStr = new string(buffer);
                    if (startingStr == "<ShowPlanXML")
                    {
                        result = startingStr;
                        result += await reader.ReadToEndAsync();
                    }
                    else
                    {
                        result = "Wrong file format!";
                    }
                }                           
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
            List<Statement> results = StatementExtractor.ConvertPlanToStatementList(xmlStr);

            SpExecuteSqlText.Text = "";
            DirectSqlText.Text = "";

            foreach (var res in results)
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
    }
}
