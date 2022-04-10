using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using graphics.Functions;
using System.IO;

namespace graphics
{
    public partial class Form1 : Form
    {
        
        //для графика
        double a, b, h;
        double x, y;
        //функция которую будем апроксимировать
        function1 functions = new function1();
        //сигмоида
        Sigmoid sg = new Sigmoid();

        //результат сети
        double[] output;
        //ожидание от сети
        double expected;

        //файл с датасетом
        //string input = 
        //сеть1e-7
        LayredNet pzdk = new LayredNet(3, new int[] { 3, 2, 1 }, 1e-12);
        int maxSet = System.IO.File.ReadAllLines(@"../../Storage/input.txt").Length;

        Storager s = new Storager();

        public Form1()
        {
            //эпохи
            for (int i = 0; i < 2; i++)
            {
                using (StreamReader sr = new StreamReader(@"../../Storage/input.txt"))
                {
                    //датасеты
                    for (int j = 0; j < maxSet;j++)
                    {
                        
                        //загрузка датасета в сеть
                        string[] data = s.Read(sr);
                        expected = Convert.ToDouble(data[0]);
                        pzdk.SetFirstInput(data[1].Split(' ').Select(double.Parse).ToArray());
                        
                        output = pzdk.Compute();
                        pzdk.BackPropagation(expected);
                    }
                    
                    sr.DiscardBufferedData();
                    sr.BaseStream.Seek(0, System.IO.SeekOrigin.Begin);
                }
            }
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            a = 0;
            b = 10;
            h = 0.1;
            x = a;

            this.chart1.Series[1].Points.Clear();
            while (x<=b)
            {
                y = functions.Execute(x);
                this.chart1.Series[1].Points.AddXY(x, y);
                x += h;
            }

            

            a = 0;
            b = 10;
            h = 1;
            x = a;

            this.chart1.Series[0].Points.Clear();
            

            while (x <= b)
            {
                pzdk.SetFirstInput(new double[] { x, x, x });
                y = pzdk.Compute()[0];
                this.chart1.Series[0].Points.AddXY(x, y);
                x += h;
            }
        }
    }
}
