
namespace MyAI.NetWork
{

    class OutputLayer : Layer
    {
        public OutputLayer(int non, int nopn, NeuronType nt, string layerName) : base(non, nopn,nt, layerName) { }
        public override void Recognize(Network net, Layer nextLayer)
        {
            double e_sum = 0;
            for (int i = 0; i < Neurons.Length; i++)
                e_sum += Neurons[i].Output;
            for (int i = 0; i < Neurons.Length; i++)
            {
                net.netOut[i] = Neurons[i].Output / e_sum;
                
            }

        }
        public override double[] BackwardPass(double[] errors)
        {
            double[] gr_sum = new double[numofprevneurons + 1];
            for (int i = 0; i < numofprevneurons + 1; i++)
            {
                double sum = 0;
                for (int j = 0; j < Neurons.Length; j++)
                    sum += Neurons[j].Weights[i] * errors[j];
                gr_sum[i] = sum;
            }
            for (int i = 0; i < numofneurons; i++)
                for (int j = 0; j < numofprevneurons + 1; j++)
                {
                    double deltaw = (j == 0) ? (momentum * lastdeltaweights[i, 0] + learningrate * errors[i]) : (momentum * lastdeltaweights[i, j] + learningrate * Neurons[i].Inputs[j - 1] * errors[i]);
                    lastdeltaweights[i, j] = deltaw;
                    Neurons[i].Weights[j] += deltaw;
                }
            return gr_sum;
        }
    }
}