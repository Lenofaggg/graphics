using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphics.Functions;

namespace graphics
{
    public class NeuroLayer
    {
        public Neuron[] Neurons;
        NeuronType NeuronsType;
        public IExcFunction function;


        //число нейронов, число входных параметров на каждый нейрон, тип нейронов слоя
        public NeuroLayer(int size, int inputSize, NeuronType nt, IExcFunction fn)
        {
            Neurons = new Neuron[size];
            function = fn;
            NeuronsType = nt;
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i] = new Neuron(function, inputSize, NeuronsType);
            }

        }
        public void SetInputs(double[] input)
        {
            //если нейрон входной то инпут 1 если любой другой то инпуты - массив
            if (NeuronsType == NeuronType.Input)
            {
                for (int i = 0; i < Neurons.Length; i++)
                {
                    Neurons[i].SetInput(new double[] { input[i] });
                }
            }
            else
            {
                foreach (var neuron in Neurons)
                {
                    neuron.SetInput(input);
                }
            }
        }

        public double CalcDeltaOnWeightsFromNeurons( int indexNextNeuron)
        {
            double sum = 0;
            //суммирование для дельты
            //веса нейронов предыдущего слоя зависимых от текущего нейрона
            for (int i=0; i< Neurons.Length;i++)
            {
                sum += Neurons[i].CalcDeltaOnWeights(indexNextNeuron);
            }
            return sum;
        }

        public double[] Compute()
        {
            return Neurons
                    .Select(neuron => neuron.Activate())
                    .ToArray();
        }


        public void CalcDelta(NeuroLayer previousLayer)
        {
            //приходимые значения на 1 нейрон, т.к. на все нейроны приходят одинаковые значения
            double[] expected = previousLayer.Neurons
                                .Select(neuron => neuron.input).First().ToArray();

            //делать дельты 
            //перебор всех нейронов(получение дельт) = кол - во синопсов любого из нейронов следующего слоя
            //  , тк все со всеми связанны
            for (int i = 0; i < expected.Length; i++)
            {
                Neurons[i].CalcDelta(previousLayer.CalcDeltaOnWeightsFromNeurons(i));
            }


            #region comments2
            //Neurons[Neurons.Length-1].CalcDelta(previousLayer.CalcDeltaOnWeightsFromNeurons(Neurons.Length - 1));
            //Neurons[Neurons.Length - 1].Learn(LayredNet.learningRate);

            //перебор всех нейронов(получение дельт) = кол - во синопсов любого из нейронов следующего слоя
            //  , тк все со всеми связанны
            //for (int i = 0; i < expected.Length; i++)
            //{

            //    Neurons[i].CalcDelta(previousLayer.CalcDeltaOnWeightsFromNeurons(i));

            //    Neurons[i].Learn( LayredNet.learningRate);
            //}


            #endregion
        }

        public void Learning()
        {
            //учить учить учить
            for (int i = 0; i < Neurons.Length; i++)
            {
                Neurons[i].Learn(LayredNet.learningRate);
            }
        }


    }
}
