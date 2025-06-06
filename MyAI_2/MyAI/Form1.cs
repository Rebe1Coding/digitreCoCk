using NPOI.SS.Formula.PTG;
using System;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace MyAI
{
    public partial class Form1 : Form
    {

        public Form1()
        {
            InitializeComponent();
        }

        NetWork.Network net = new NetWork.Network();

        private double[] _inputPixels = new double[15] { 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d, 0d };
        public double[] InputPixels { get => _inputPixels; }
        private void ChangeState(Button b, int index)
        {
            if (b.BackColor == Color.Black)
            {
                b.BackColor = Color.White;
                _inputPixels[index] = 0d;
            }
            else 
            {
                b.BackColor = Color.Black;
                _inputPixels[index] = 1d;
            }
        }
        private void buttons_Click(object sender, EventArgs e)
        {
            Button btn = (Button)sender;
            ChangeState(btn, btn.TabIndex);
        }

        private void button17_Click(object sender, EventArgs e)
        {
            net.ForwardPass(net, InputPixels);
            label3.Text=net.netOut.ToList().IndexOf(net.netOut.Max()).ToString(); 
           

        }
        
        private void button18_Click(object sender, EventArgs e)
        {
            Form f2 =
            new Form2(net.loadErrors((int)numericUpDown1.Value), net.loadErrors((int)numericUpDown1.Value).Length);
            f2.Show();

        }

        private void button19_Click(object sender, EventArgs e)
        {
            net.Train(net);
            
        }

        private void button20_Click(object sender, EventArgs e)
        {
            if (!File.Exists(Path.Combine("Sourses", "Weights"))) {
                net = null;
                foreach (var file in Directory.GetFiles(Path.Combine("Sourses", "Weights")))
                {
                    File.Delete(file);
                }
                Directory.Delete(Path.Combine("Sourses", "Weights"));
                net = new NetWork.Network();
                MessageBox.Show("Веса успешно удалены!");
            }
            
        }

        private void button21_Click(object sender, EventArgs e)
        {
            Form4 testForm = new Form4(net.Test(net));
            testForm.Show();

        }
    }
    
}
