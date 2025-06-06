using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace MyAI
{
    public partial class Form4 : Form
    {
        public Form4(string[] testResult)
        {
            InitializeComponent();
            _testResult = testResult;
        }
        string[] _testResult;

        private void Form4_Load(object sender, EventArgs e)
        {
            for(int i=0;i<10;i++)
            {
                label6.Text +=Environment.NewLine + i.ToString()+ Environment.NewLine;
                label1.Text+= Environment.NewLine+_testResult[i]+ Environment.NewLine;
            }
        }
    }
}
