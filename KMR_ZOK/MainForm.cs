using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using static System.Math;

namespace KMR_ZOK
{
    public partial class MainForm : Form
    {
        List<double> r, u, v, Wr, Wu, Wv;
        List<double> rTemp, uTemp, vTemp, WrTemp, WuTemp, WvTemp;
        double[] lambda;
        List<List<double>> data;
        List<List<double>> dataTemp;
        double[] FIj, FIjTemp, FijDifferenc;
        double Fi;
        bool countinueShootingMethod = true;
        public MainForm()
        {
            InitializeComponent();
            r=new List<double>();
            u=new List<double>();
            v=new List<double>();
            Wr=new List<double>();
            Wu=new List<double>();
            Wv=new List<double>();

            rTemp = new List<double>();
            uTemp = new List<double>();
            vTemp = new List<double>();
            WrTemp = new List<double>();
            WuTemp = new List<double>();
            WvTemp = new List<double>();

            lambda = new double[3] { 3,1,12};
            data= new List<List<double>>();
            dataTemp= new List<List<double>>(); 
            FIj = new double[3];
            FIjTemp = new double[3];
            FijDifferenc = new double[3];
        }
        double TNorm;
        double r0;
        double mu;
        double m0;
        double P;
        double m;
        double alfa;
        double betta;
        int N = 10;
        double t0 = 0;
        double tau;
        int iteration;

        double delta = 0.5;
        double eps;

        private void btnCount_Click(object sender, EventArgs e)
        {
            Fi = 0;
            eps = Pow(10, -4);
            TNorm = 3.32;
            r0 = 1.496 * Math.Pow(10, 11);
            mu = 1.327 * Math.Pow(10, 20);
            m0 = Convert.ToDouble(numericM0.Value);
            P = 0.833 * Math.Pow(10, -3) * m0;
            m = 1.288 * Math.Pow(10, -3) * m0;
            alfa = r0 * r0 * P / mu * m0;
            betta = (m / P) * Math.Sqrt(mu / r0);

            tau = (TNorm - t0) / N;
            while (countinueShootingMethod)
            {
                ShootingMethod();
            }
        }

        private List<List<double>> InitListsOfData(double[] l, List<double> r, List<double> u, List<double> v, List<double> Wr, List<double> Wu, List<double> Wv)
        {
            List<List<double>> data = new List<List<double>>();
            r.Clear();
            u.Clear();
            v.Clear();
            Wr.Clear();
            Wu.Clear();
            Wv.Clear();

            r.Add(l[2]);
            u.Add(0);
            v.Add(1/Sqrt(l[2]));
            Wr.Add(1 + l[1] / 2 * Pow(Sqrt(l[2]), 3));
            Wu.Add(l[0]);
            Wv.Add(l[1]);

            data.Add(r);
            data.Add(u);
            data.Add(v);
            data.Add(Wr);
            data.Add(Wu);
            data.Add(Wv);
            return data;
        }

        public void ShootingMethod()
        {
            //for (int i = 0; i < lambda.Length; i++)
            //{
            //    lambda[i] = 1;
            //}
            List<List<double>> result = new List<List<double>>();
            do
            {

                data = InitListsOfData(lambda, r, u, v, Wr, Wu, Wv);
                data = MethodEilera(data);
                r = data[0];
                u = data[1];
                v = data[2];
                Wr = data[3];
                Wu = data[4];
                Wv = data[5];
                FIj = CheckNeviazka(lambda, r, u, v);

                foreach (var fi in FIj)
                {
                    Fi += Pow(fi, 2);
                }
                //if(Abs(0-Fi) < eps)
                //{
                lambda = Gradient();
                //}
            } while (Norma(FijDifferenc) < eps);
            countinueShootingMethod = false;
        }
        double t;
        public List<List<double>> MethodEilera(List<List<double>> lists)
        {   
            int j = 0;
            while (j <= N)
            {
                t = t0 + (N-j) * tau;
                lists[0].Add(r[j] - u[j]*tau);
                lists[1].Add(Pow(v[j],2) / r[j] - 1 / Pow(r[j],2) - (alfa / (1 - betta * t)) * (Wu[j] / Sqrt(Wv[j] * Wv[j] + Wu[j] * Wu[j])));
                lists[2].Add(-u[j] * v[j] / r[j] - (alfa / (1 - betta * t)) * (Wv[j] / Sqrt(Wv[j] * Wv[j] + Wu[j] * Wu[j])));
                lists[3].Add(-Wu[j] * (-Pow(v[j], 2) / Pow(r[j], 2) + 2 / Pow(r[j], 3)) - Wv[j] * u[j] * v[j] / Pow(r[j], 2));
                lists[4].Add(-Wr[j] + Wv[j] * v[j] / r[j]);
                lists[5].Add(-Wu[j] * 2 * v[j] / r[j] + Wv[j] * u[j] / r[j]);
                j++;
            }
       
            return lists;
        }

        private double[] CheckNeviazka(double[] l, List<double> r, List<double> u, List<double> v)
        {
            FIj[0] = r[r.Count-1] - l[0];
            FIj[1] = u[u.Count-1] - l[1];
            FIj[2] = v[v.Count - 1] - l[2];

            return FIj;
        }
        private double CheckNeviazkaTemp(double l, double ListElement)
        {
            return ListElement - l;
        }
        private double[] Gradient()
        {
            double[] lambdaTemp = new double[3];
            double norma = 0;
            double FiTemp = 0;
            Array.Copy(lambda, lambdaTemp, 3);
            for (int i = 0; i < lambda.Length; i++)
            {
                lambdaTemp[i] = lambda[i] + delta;
                dataTemp = InitListsOfData(lambdaTemp, rTemp, uTemp, vTemp, WrTemp, WuTemp, WvTemp);
                dataTemp = MethodEilera(dataTemp);
                //rTemp = dataTemp[0];
                //uTemp = dataTemp[1];
                //vTemp = dataTemp[2];
                //WrTemp = dataTemp[3];
                //WuTemp = dataTemp[4];
                //WvTemp = dataTemp[5];
                FIjTemp = CheckNeviazka(lambdaTemp, dataTemp[0], dataTemp[1], dataTemp[2]);
                foreach (var fi in FIjTemp)
                {
                    FiTemp += Pow(fi, 2);
                }

                FijDifferenc[i] = (Fi - FiTemp)/delta;
                lambdaTemp[i] = lambda[i];
            }

            for (int i = 0; i < lambda.Length; i++)
            {
                lambda[i] -= (FIj[i] / Pow(Norma(FijDifferenc), 2)) * FijDifferenc[i];
            }


                return lambda;
        }
        private double Norma(double[] FijDifferenc)
        {
            double n = 0;
            foreach(double f in FijDifferenc)
            {
                n += Pow(Abs(f), 2);
            }

            return Sqrt(n);
        }
    }
}
