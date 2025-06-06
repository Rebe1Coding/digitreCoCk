using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

public delegate void Draw(int i, double err);
namespace MyAI
{
    public partial class Form3 : Form
    {
        public Form3()
        {
            InitializeComponent();
            draw = drawChart;
           
           
        }

        public Thread thread;
        public Draw draw;
        private  void drawChart(int i, double err)
        {
           
            chart1.Titles.Clear();
            chart1.Titles.Add("Процесс обучения");

            chart1.ChartAreas[0].AxisX.Title = "Эпоха";
            chart1.ChartAreas[0].AxisY.Title = "Ошибка";
            chart1.Series[0].ChartType = SeriesChartType.Point;


            /*chart1.Series[0].Name= "Ошибка: " + _trainError[i].ToString()+"$\n"+ "Эпоха: "+(i+1).ToString();
            chart1.Series[0].Points.Add(_trainError[i]);*/
            chart1.Series[0].Name = "Ошибка: " + err.ToString() + "$\n" + "Эпоха: " + (i + 1).ToString();
            chart1.Series[0].Points.Add(err);

           
        }

    }
}
