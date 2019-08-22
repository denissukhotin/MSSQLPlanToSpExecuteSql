using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace MSSQLPlanToSpExecuteSql
{
    public partial class MainForm : Form
    {
        public MainForm()
        {
            InitializeComponent();
        }

        private void GenerateSqlButton_Click(object sender, EventArgs e)
        {
            string xmlStr = PlanXMLText.Text;

            if (string.IsNullOrEmpty(xmlStr))
            {
                return;
            }

            List<string> results = StatementExtractor.ConvertPlanToStatementList(xmlStr);

            SpExecuteSqlText.Text = "";

            foreach(var res in results)
            {
                if (!string.IsNullOrEmpty(SpExecuteSqlText.Text))
                {
                    SpExecuteSqlText.Text += Environment.NewLine + Environment.NewLine;
                }
                SpExecuteSqlText.Text += res;
            }
        }
    }
}
