
namespace MyAI.NetWork
{
    class HiddenLayer : Layer
    {
        public HiddenLayer(int non, int nopn,NeuronType nt, string layerName) : base(non, nopn,nt, layerName) { }
        public override void Recognize(Network net, Layer nextLayer)
        {
            double[] hidden_out = new double[Neurons.Length];
            for (int i = 0; i < Neurons.Length; i++)
                hidden_out[i] = Neurons[i].Output;
            nextLayer.Data = hidden_out;
        }
        public override double[] BackwardPass(double[] gr_sums)
        {
            double[] gr_sum = new double[numofprevneurons];
            for (int i = 0; i < gr_sum.Length; i++)
            {
                double sum = 0;
                for (int k = 0; k < Neurons.Length; k++)
                    sum += Neurons[k].Weights[i] * Neurons[k].Derivative * gr_sums[k];
                gr_sum[i] = sum;
            }

            for (int i = 0; i < numofneurons; i++)
                for (int j = 0; j < numofprevneurons + 1; j++)
                {
                    double deltaw = (j == 0) ? (momentum * lastdeltaweights[i, 0] + learningrate * Neurons[i].Derivative * gr_sums[i]) : (momentum * lastdeltaweights[i, j] + learningrate * Neurons[i].Inputs[j - 1] * Neurons[i].Derivative * gr_sums[i]);
                    lastdeltaweights[i, j] = deltaw;
                    Neurons[i].Weights[j] += deltaw;
                }
            return gr_sum;
        }
    }
}