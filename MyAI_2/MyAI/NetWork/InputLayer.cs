using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.IO;
using MyAI.Properties;
using System.Data.SqlTypes;

namespace MyAI.NetWork
{
    class InputLayer
    {

        public InputLayer()
        {
            

           _trainset_x = ReadSet("datasetLearn_x.txt");
           _trainset_y = ReadSet("datasetLearn_y.txt");
           _testset = ReadSet("datasetTest.txt");

        }
        string[] _trainset_x;
        string[] _trainset_y;
        string[] _testset;
        public string[] Trainset_x { get => _trainset_x; }
        public string[] Trainset_y { get => _trainset_y; }
        public string[] Testset { get => _testset; }
        public double[] ConvertStringToArray(string input)
        {
            return input
                .Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries) // Разделяем строку по пробелам
                .Select(double.Parse) // Преобразуем каждую строку в целое число
                .ToArray(); // Преобразуем результат в массив
        }
        public string[] ReadSet(string filePath)
        {
            
            
            string[] lines = File.ReadAllLines(filePath);
            int[] numbers = new int[lines.Length];
            for (int i = 0; i < lines.Length; i++) {

                lines[i] = lines[i].Trim();
            }

            return lines;

        }
        public void ShuffTrainset()
        {
            Random random = new Random();
            for (int n = Trainset_x.Length - 1; n >= 1; --n)
            {
                int j = random.Next(n+1);
                string temp1 = Trainset_x[n];
                string temp2 = Trainset_y[n];
                Trainset_x[n] = Trainset_x[j];
                Trainset_y[n] = Trainset_y[j];
                Trainset_x[j] = temp1;
                Trainset_y[j] = temp2;
            }
            

        }


        static int GetRand(int n)
        {
            Random random = new Random();
            return random.Next(0, n + 1); // Случайное число от 0 до n включительно
        }
    }
}
