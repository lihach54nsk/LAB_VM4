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

        public int N = 0;

        private void DerivativeButton_Click(object sender, EventArgs e)
        {
            decimal[,] input = new decimal[2, N];

            for (int i = 0; i < 2; i++)
            {
                for (int j = 0; j < N; j++) input[i, j] = Convert.ToDecimal(dataGridView1[j, i].Value);
            }

            Graphics graphics = CreateGraphics();
            SolidBrush brush = new SolidBrush(Color.White);
            Pen pen = new Pen(Color.Black);
            PointF[] coords = new PointF[N];
            int k = 0;

            pen.Width = 1;
            graphics.FillRectangle(brush, new RectangleF(12, 150, 400, 300));

            foreach (var result in Derivative.FindDerivative(input))
            { coords[k].X = (float)result.x; coords[k].Y = (float)result.y; k++; }

            graphics.DrawCurve(pen, coords);
        }

        private void EnterButton_Click(object sender, EventArgs e)
        {
            N = Convert.ToInt32(NTextBox.Text);
            dataGridView1.ColumnCount = N;
            dataGridView1.RowCount = 2;
        }
    }
}