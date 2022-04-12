using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphics.Functions;

namespace graphics
{
    public class Neuron
    {
        public double bias;
        public double[] weights;
        public double[] input;

        public NeuronType type;

        public double delta;
        public double _output;

        private Func<double, double> _Activate;
        private Func<double, double> _Derivative;

        //функция нейрона, размер входных связей, тип нейрона
        public Neuron(IExcFunction func, int size, NeuronType nt)
        {            
            _Derivative = func.Derivative;
            _Activate = func.Execute;

            type = nt;
            input = new double[size];
            weights = new double[size];

            RandomizeWeightsAndBias(size);
        }

        private void RandomizeWeightsAndBias(int size)
        {
            if (type != NeuronType.Input)
            {
                Random rnd = new Random();
                for (int i = 0; i < size; i++)
                {
                    weights[i] = rnd.NextDouble() + 1;
                }
                //bias = rnd.NextDouble() + 1;
                bias = 0;
            }
            else
            {
                weights = new double[] { 1.0 };
            }
        }
        
        //умножение синопса соответствующего нейрона на дельту нейрона
        public double CalcDeltaOnWeights(int i)
        {
            return weights[i] * delta;
        }

       //дельта для текущего нейрона
        public void CalcDelta(double x)
        {
            if(type == NeuronType.Output)
            {
                //дельта выходного слоя: первый множитель - ошибка сети
                delta = (x - _output) * _Derivative(_output);
                //CalcErr(x);
            }
            else
            {
                //дельта скрытого слоя
                delta = x * _Derivative(_output);
            }            
        }

      

        public void SetInput(double[] input)
        {
            this.input = input;
        }

        public double Activate()
        {
            var res = Summator();
            _output = _Activate(res);
            return _output;
        }

        public double Summator()
        {
            double sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                sum += input[i] * weights[i];
            }

            sum += bias;
            return sum;
        }

        public void Learn(/*double err, double expected,*/  double learningRate)
        {            
            //изменения весов нейрона
            for (int i = 0; i < weights.Length; i++)
            {
                double oldWeight = weights[i];
                var inp = input[i];
                double dab = learningRate * inp * delta;
                weights[i] = oldWeight + dab;
            }

        }
    }
}
