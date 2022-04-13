using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using graphics.Functions;

namespace graphics
{
    public class LayredNet
    {
        private NeuroLayer[] _layerList;
        private double error;
        public static double learningRate;
        

        //число слоёв, число нейронов на слое, -функции слоёв-
        public LayredNet(int size, int[] layerSize, double lr)
        {
           
            learningRate = lr;

            _layerList = new NeuroLayer[size];
            _layerList[0] = new NeuroLayer(layerSize[0], 1, 
                NeuronType.Input,new Sigmoid());
            

            for (int i = 1; i < size; i++)
            {
                if (i != (size - 1))
                {
                    _layerList[i] = new NeuroLayer(layerSize[i], layerSize[i - 1],
                        NeuronType.Hidden, new Sigmoid());
                }
                else
                {
                    _layerList[i] = new NeuroLayer(layerSize[i], layerSize[i - 1],
                        NeuronType.Output,new Sigmoid());
                }
            }
        }

        //public void SetFunction(IExcFunction fn1)
        //{
        //    function = fn1;
        //}

        public double Err(double ex, double outp)
        {
            return 0.5 * Math.Pow((outp - ex), 2);
        }

        public void SetFirstInput(double[] input)
        {
            _layerList[0].SetInputs(input);
        }

        //от входных к выходным
        public double[] Compute()
        {
            var output = _layerList[0].Compute();

            for (int i = 1; i < _layerList.Length; i++)
            {
                _layerList[i].SetInputs(output);
                output = _layerList[i].Compute();
            }

            return output;
        }

        //от выходных к входным
        public void BackPropagation(double expected, double output)
        {
            //работа с выходным нейроном 

            //на выходе массив из 1 элемента
            //double output = _layerList[_layerList.Length - 1].Compute()[0];

            //дэльта выходного слоя-нейрона
            _layerList[_layerList.Length - 1].Neurons[0].CalcDelta(expected);

            //вычисление дельт скрытых <-
            for (int j = _layerList.Length - 2; j > 0; j--)
            {
                var layer = _layerList[j];
                var previousLayer = _layerList[j + 1];

                layer.CalcDelta(previousLayer);
            }

            //обучение выходного нейрона
            //_layerList[_layerList.Length - 1].Learning();

            //обучение ->
            for (int j = 0; j <= _layerList.Length - 1; j++)
            {
                _layerList[j].Learning();
            }

            #region comments1

            //работа со скрытыми слоями
            //for (int j = _layerList.Length - 2; j >= 1; j--)
            //{
            //    var layer = _layerList[j];
            //    var previousLayer = _layerList[j + 1];

            //    layer.Learning(previousLayer);
            //}

            #endregion

        }
    }
}
