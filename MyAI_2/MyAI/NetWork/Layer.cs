
using System;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using NPOI.SS.Util;
using NPOI.HSSF.Util;
using System.IO;



namespace MyAI.NetWork
{
    abstract class Layer
    {
        protected Layer(int non, int nopn,NeuronType nt, string layerName)
        {
            numofneurons = non;
            numofprevneurons = nopn;
            Neurons = new Neuron[non];
            double[,] Weights = weightInit(layerName);
            lastdeltaweights = Weights;
            for (int i = 0; i < non; ++i)
            {
                double[] temp_weights = new double[nopn + 1];
                for (int j = 0; j < nopn + 1; ++j)
                    temp_weights[j] = Weights[i, j];
                Neurons[i] = new Neuron(null, temp_weights, nt);
            }
        }
        protected int numofneurons;
        protected int numofprevneurons;
        protected const double learningrate = 0.005d;
        protected const double momentum = 0.03d;
        protected double[,] lastdeltaweights;
        Neuron[] _neurons;
        public Neuron[] Neurons { get => _neurons; set => _neurons = value; }
        public double[] Data
        {
            set
            {
                for (int i = 0; i < Neurons.Length; ++i)
                {
                    Neurons[i].Inputs = value;
                    Neurons[i].Activator(Neurons[i].Inputs, Neurons[i].Weights);
                }
            }
        }
        public double[,] weightInit(string nameLayer)
        {
            double[,] _weights = new double[numofneurons, numofprevneurons+1];
            Random rand = new Random();
            if (!File.Exists(Path.Combine("Sourses", "Weights", $"weights_{nameLayer}.xlsx")))
            {
                Directory.CreateDirectory(Path.Combine("Sourses", "Weights"));
                IWorkbook workbook= new XSSFWorkbook();
                ISheet sheet = workbook.CreateSheet("Лист1");
                IRow row;

                
                    using (FileStream fileStream = new FileStream(Path.Combine("Sourses", "Weights", $"weights_{nameLayer}.xlsx"), FileMode.Create))
                    {
                    for (int i = 0; i < numofneurons; i++)
                    {
                        row = sheet.CreateRow(i);
                        for (int j = 0; j < numofprevneurons + 1; j++)
                        {
                            _weights[i, j] = 2 * rand.NextDouble() - 1; ;//rand.NextDouble();
                            row.CreateCell(j).SetCellValue(_weights[i, j].ToString());

                        }
                    }


                    workbook.Write(fileStream, false);
                    }
                
                
            }
           // if (nm==NetWorkMode.Demo)
           else
            {
                IWorkbook workbook;
                using (FileStream fileStream = new FileStream(Path.Combine("Sourses", "Weights", $"weights_{nameLayer}.xlsx"), FileMode.Open, FileAccess.Read))
                {
                    workbook = new XSSFWorkbook(fileStream);
                }
                ISheet sheet = workbook.GetSheetAt(0);

                for (int i = 0; i < numofneurons; i++)
                {
                    for (int j = 0; j < numofprevneurons + 1; j++)
                    {
                        
                        _weights[i,j]=double.Parse(sheet.GetRow(i).GetCell(j).StringCellValue);

                    }
                }
            }

            
            return _weights;
        }
        public void weightsUpdate(string nameLayer)
        {

            IWorkbook workbook;
            using (FileStream fileStream = new FileStream(Path.Combine("Sourses", "Weights", $"weights_{nameLayer}.xlsx"), FileMode.Open, FileAccess.ReadWrite))
            {
                workbook = new XSSFWorkbook(fileStream);
            }

            ISheet sheet = workbook.GetSheetAt(0);
            
            
                for (int i = 0; i < numofneurons; i++)
                {
                    //row=sheet.GetRow(i);
                    for (int j = 0; j < numofprevneurons + 1; j++)
                    {
                        sheet.GetRow(i).GetCell(j).SetCellValue(Neurons[i].Weights[j].ToString());
                    }
                }
            using (FileStream fileStream = new FileStream(Path.Combine("Sourses", "Weights", $"weights_{nameLayer}.xlsx"), FileMode.Create))
            {
                workbook.Write(fileStream, false);
            }

        }
        abstract public void Recognize(Network net, Layer nextLayer);
        abstract public double[] BackwardPass(double[] gr_sums);
        
        
        
    }
}






