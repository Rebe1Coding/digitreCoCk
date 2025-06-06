using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

using System.Windows.Forms.DataVisualization.Charting;


namespace MyAI
{
    public partial class Form2 : Form
    {
        public Form2(double[] trainError,int epoch)
        
        {

            InitializeComponent();
            _trainError = trainError;
            _epoch=epoch;
            series1 = new Series();
            series1.ChartType = SeriesChartType.FastLine;
            chart1.Series.Clear();
            chart1.Series.Add(series1);


        }
        public double[] _trainError;
        Series series1;
        public int _epoch;

        private void Form2_Load_1(object sender, EventArgs e)
        {
            timer1.Interval = 10;
            timer1.Start();
        }

        public int numofepoch = 0;
        private void Timer1_Tick(object sender, EventArgs e)
        {



            if (numofepoch == _epoch)
            {
                timer1.Stop();
                endShowTrain(numofepoch, _trainError[numofepoch - 1]);
                Close();

            }
            if (numofepoch < _epoch)
            {
                drawChart(numofepoch);
                numofepoch++;
            }


        }
        public  void drawChart(int i)
        {
            chart1.Titles.Clear();
            chart1.Titles.Add("Процесс обучения");

            chart1.ChartAreas[0].AxisX.Title = "Эпоха";
            chart1.ChartAreas[0].AxisY.Title = "Ошибка";
            chart1.Series[0].ChartType = SeriesChartType.Point;


            chart1.Series[0].Name = "Ошибка: " + _trainError[i].ToString() + "$\n" + "Эпоха: " + (i + 1).ToString();
            chart1.Series[0].Points.Add(_trainError[i]);



        }
        public void endShowTrain(int e,double err)
        {
            MessageBoxButtons btn=MessageBoxButtons.OK;

            MessageBox.Show($"Ошибка:{err}$\nКол-во эпох:{e}","Обучение закончено", btn);
            

        }

       
    }
}
