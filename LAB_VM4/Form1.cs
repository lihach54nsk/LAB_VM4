using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

namespace LAB_VM4
{
    public partial class Form1 : Form
    {
        public Series series = new Series();

        public Form1()
        {
            InitializeComponent();
        }

        int N = 0;
        
        public void DerivativeButton_Click(object sender, EventArgs e)
        {
            decimal[,] input = new decimal[2, N];            

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < N; j++) input[i, j] = Convert.ToDecimal(dataGridView1[j, i].Value);
            }

            foreach (var result in Derivative.FindDerivative(input))
            {
                chart1.Series[0].Points.AddXY(result.x, result.y);
            }
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            N = Convert.ToInt32(NTextBox.Text);
            dataGridView1.ColumnCount = N;
            dataGridView1.RowCount = 2;
        }
    }
}