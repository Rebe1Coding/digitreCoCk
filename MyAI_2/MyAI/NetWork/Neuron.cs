using MyAI.NetWork;
using System;
using System.Drawing;
using static System.Math;

namespace MyAI.NetWork
{
    class Neuron
    {
        public Neuron(double[] inputs, double[] weights, NeuronType type)
        {
            _type = type;
            _weights = weights;
            _inputs = inputs;
        }
        private NeuronType _type;
        private double[] _weights;
        private double[] _inputs;
        private double _output;
        private double _derivative;
       
        public double[] Weights { get => _weights; set => _weights = value; }
        public double[] Inputs { get => _inputs; set => _inputs = value; }
        public double Output { get => _output; }
        public double Derivative { get => _derivative; }
        public void Activator(double[] i, double[] w)
        {
            double sum = w[0];// смещение b
            for (int l = 0; l < i.Length; ++l)
                sum += i[l] * w[l + 1];
            switch (_type)
            {
                case NeuronType.Hidden:
                    _output = Tanh(sum);
                    _derivative = TanhDerivative(sum);
                    break;
                case NeuronType.Output:
                    _output = Exp(sum);
                    break;
            }
        }
        public static double Tanh(double x)
        {
            return Math.Tanh(x);
        }

        // Производная гиперболического тангенса
        public static double TanhDerivative(double x)
        {
            double tanhX = Tanh(x);
            return 1 - tanhX * tanhX;
        }

    }
}




/*public static double TanhDerivative(double x)
{
    double tanhX = Tanh(x);
    return 1 - tanhX * tanhX;
}
static double Sigmoid(double x)
{
    return 1.0d / (1.0d + Exp(-x));
}
static double SigmoidDerivative(double x)
{
    double sigmoidValue = Sigmoid(x);
    return sigmoidValue * (1.0d - sigmoidValue);
}
       
    }
*/