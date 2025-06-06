using NPOI.HPSF;
using System;
using System.IO;
using System.Linq;
using System.Windows.Forms;


namespace MyAI.NetWork
{
    class Network
    {
        public Network()
        {


            input_layer = new InputLayer();
            hidden_layer1 = new HiddenLayer(76, 15,NeuronType.Hidden, nameof(hidden_layer1));
            hidden_layer2 = new HiddenLayer(31, 76,NeuronType.Hidden, nameof(hidden_layer2));
            output_layer = new OutputLayer(10, 31,NeuronType.Output, nameof(output_layer));
        }
    
    
        public InputLayer input_layer;
        public HiddenLayer hidden_layer1;
        public HiddenLayer hidden_layer2;
        public OutputLayer output_layer;
        public double[] netOut = new double[10];
        public double epochError;
        public double[] trainErrors=new double[1500];
        public Form3 trainForm = new Form3();
        public void Train(Network net)
        {
           
           
            int epoches = 100;
            for (int epoch = 0; epoch < epoches; epoch++)
            {
                net.input_layer.ShuffTrainset();
                epochError = 0;
                for (int i = 0; i < net.input_layer.Trainset_x.Length; ++i)
                {
                    ForwardPass(net, input_layer.ConvertStringToArray(net.input_layer.Trainset_x[i]));
                    double[] errors = new double[net.netOut.Length];
                    for (int err = 0; err < errors.Length; err++)
                    {
                        errors[err] = (err == Item2(net.input_layer.ConvertStringToArray(net.input_layer.Trainset_y[i]))) ? -(net.netOut[err] - 1.0d) : -net.netOut[err];
                    }
                    


                    double[] temp_gsums1 = net.output_layer.BackwardPass(errors);
                    double[] temp_gsums2 = net.hidden_layer2.BackwardPass(temp_gsums1);
                    net.hidden_layer1.BackwardPass(temp_gsums2);

                    epochError += crossEntropyLoss(net.input_layer.ConvertStringToArray(net.input_layer.Trainset_y[i]), net.netOut);
                }
                trainErrors[epoch]=epochError/input_layer.Trainset_x.Length;

                trainForm.draw.Invoke(epoch,trainErrors[epoch])/*(Draw) delegate{trainForm.drawChart(epoch, trainErrors[epoch]); }*/;
                Console.WriteLine(trainErrors[epoch]);

            }
            net.hidden_layer1.weightsUpdate(nameof(hidden_layer1));
            net.hidden_layer2.weightsUpdate(nameof(hidden_layer2));
            net.output_layer.weightsUpdate(nameof(output_layer));
            net.saveErrors(trainErrors);
            MessageBox.Show("Обучение завершенно!");
            trainForm.Show();
        }

        public void ForwardPass(Network net, double[] netInput)
        {
            net.hidden_layer1.Data = netInput;
            net.hidden_layer1.Recognize(net, net.hidden_layer2);
            net.hidden_layer2.Recognize(net, net.output_layer);
            net.output_layer.Recognize(net, null);
        }

        public string[] Test(Network net)
        {
            string[] result=new string[10];

            for (int i = 0; i < net.input_layer.Testset.Length; i++) {
                ForwardPass(net, net.input_layer.ConvertStringToArray(net.input_layer.Testset[i]));
                result[i] = net.netOut.ToList().IndexOf(net.netOut.Max()).ToString();
            }
            return result;
        }
        public double crossEntropyLoss(double[] y_true, double[] y_pred)
        {
            double sum = 0;
            for (int i=0;i<y_true.Length;i++) {

                sum += (-1)*(y_true[i] * Math.Log(y_pred[i]));

            }
            return sum;
        }
        public string ArStr(double[] array)
        {
            string result = "";
            for (int i = 0; i < array.Length; i++)
            {
                result += array[i].ToString() + " ";
            }
            return result;
        }
        public int Item2(double[] arr)
        {
            int res = 0;
            for(int i = 0; i < arr.Length; i++)
            {
                if (arr[i] == 1)
                {
                    res = i;
                }
            }
            return res;
        }


        public void saveErrors(double[] errors) {
            string filePath = "Sourses/errors1.txt";
            using (StreamWriter writer = new StreamWriter(filePath))
            {
                for(int i=0;i<errors.Length;i++) {
                    writer.WriteLine(errors[i]);
                }
            }

        }
        public double[] loadErrors(int graph)
        {
            string filePath;
            if (graph == 0) { 
                filePath = "Sourses/errors.txt"; 
            }
            else {
                filePath = "Sourses/errors1.txt";
            }
            string[] lines = File.ReadAllLines(filePath);
            double[] array = new double[lines.Length];

            for (int i = 0; i < lines.Length; i++)
            {
                array[i] = double.Parse(lines[i]);
            }

            return array;

        }

    }
}
