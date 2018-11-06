using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;
using MathNet.Numerics.Statistics;
using excel=Microsoft.Office.Interop.Excel;
using extool=Microsoft.Office.Tools.Excel;

namespace TrickleIrrigation
{
    public partial class DesigneParam : Form
    {
        public DesigneParam()
        {
            InitializeComponent();
        }

        public double CalcEven(double[] data)
        {
            return data.Average();
        }
        public double[] CalcKi(double even, double[] data)
        {
            double[] result = new double[data.Length];
            for (int i = 0; i < data.Length; i++)
            {
                result[i] = data[i] / even;
            }
            return result;
        }
        public double[] PaiXu(double[] data)//冒泡排序降序
        {
            // double[] result = new double[data.Length];
            for (int i = 0; i < data.Length - 1; i++)
            {
                for (int j = 0; j < data.Length - i - 1; j++)
                {
                    if (data[j] < data[j + 1])
                    {
                        double empty = data[j];
                        data[j] = data[j + 1];
                        data[j + 1] = empty;
                    }
                }
            }
            return data;
        }
        public double[] Xi;
        public double[] P;
        public int N = 0;

        public void CalcRain()
        {

            int num = this.dataGridView1.RowCount;
            N = num;
            double[] x = new double[num - 1];
            if (num > 1)
            {
                for (int i = 0; i < this.dataGridView1.RowCount - 1; i++)
                {
                    string rain = this.dataGridView1.Rows[i].Cells[1].Value.ToString();
                    if (double.TryParse(rain, out x[i]) == false)
                    {
                        MessageBox.Show("数据错误！请检查！");
                        return;
                    }
                    this.dataGridView1.Rows[i].Cells[2].Value = (i + 1);
                }
                double[] xi = PaiXu(x);
                Xi = xi;
                double evenXi = CalcEven(x);
                double[] ki = CalcKi(evenXi, xi);
                double[] ki1 = new double[x.Length];//ki-1
                double[] ki2 = new double[x.Length];//(ki-1)2
                double[] p = new double[x.Length];//p
                for (int i = 0; i < x.Length; i++)
                {
                    ki1[i] = ki[i] - 1;
                    ki2[i] = Math.Pow(ki1[i], 2);
                    p[i] = (i + 1.0) / (x.Length + 1.0) * 100;
                    this.dataGridView1.Rows[i].Cells[3].Value = xi[i];
                    this.dataGridView1.Rows[i].Cells[4].Value = ki[i];
                    this.dataGridView1.Rows[i].Cells[5].Value = ki1[i];
                    this.dataGridView1.Rows[i].Cells[6].Value = ki2[i];
                    this.dataGridView1.Rows[i].Cells[7].Value = p[i];
                }
                P = p;
                this.textBox1.Text = evenXi.ToString();
                double sumKi2 = ki2.Sum();
                double cv = Math.Sqrt(sumKi2 / ki2.Length);
                this.textBox2.Text = cv.ToString();



            }
            else
            {
                MessageBox.Show("数据太少，请增加数据！");
            }

        }


        public void CalcKp(double pp, double cv, double cs)
        {
            ////excel.Workbook wb = new excel.Workbook();
            ////wb.Worksheets.Add();
            //excel.Worksheet ws = new excel.Worksheet();
            //ws.get_Range 
            //ws.get_Range("B2").Value = pp;
            //ws.get_Range("B3").set_Value(cv );
            //ws.get_Range("B4").set_Value(cs );
            //ws.get_Range("B5").set_Value(@"=GAMMAINV(1-B2/100,4/B4^2,1)");
            ////excel.Range rang1 = ws.get_Range("B2");
            ////excel.Range rang2 = ws.get_Range("B3");
            ////excel.Range rang3 = ws.get_Range("B4");
            ////excel.Range rang4 = ws.get_Range("B5");
            ////rang1.set_Value(pp);
            ////rang2.set_Value(cv);
            ////rang3.set_Value(cs);
            ////rang4.set_Value(@"=GAMMAINV(1-B2/100,4/B4^2,1)");
            //double tp = (double)ws.get_Range("B5").Value;
            //double fi = cs / 2 * tp - 2 / cs;
            //double kp = fi * cv + 1;
            //return kp;
            MessageBox.Show("本功能待增加！");

        }
        //public double Gammaynv(double probability, double alpha, double beta)
        //{


        //}
        private void button1_Click(object sender, EventArgs e)
        {
            CalcRain();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                List<double> txData = P.ToList<double>();
                List<double> tyData = Xi.ToList<double>();
                this.chart1.Series.Clear();
                this.chart1.Series.Add(new Series("降水频率"));
                this.chart1.Series[0].XValueType = ChartValueType.Double;
                this.chart1.Series[0].YValueType = ChartValueType.Double;
                this.chart1.Series[0].Label = "#VAL";
                this.chart1.Series[0].ToolTip = "#VALX频率\r#VAL";
                this.chart1.Series[0].ChartType = SeriesChartType.Point;
                this.chart1.Series[0].Points.DataBindXY(txData, tyData);
            }
            catch (Exception ex)
            {
                MessageBox.Show("没有计算数据！");
                MessageBox.Show(ex.Message);
            }


        }

        private void button3_Click(object sender, EventArgs e)
        {
            CalcKp(99, 0.1, 0.2);
        }

        private void button4_Click(object sender, EventArgs e)
        {
            double kc, kp, ep, eTc;
            string strKc, strKp, strEp;
            strKc = this.txtKc.Text;
            strKp = this.txtKp.Text;
            strEp = this.txtEp.Text;
            if (double.TryParse(strKc, out kc) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            if (double.TryParse(strKp, out kp) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            if (double.TryParse(strEp, out ep) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            eTc = kc * kp * ep;
            this.txtETc.Text = eTc.ToString();
        }
        //private double tryParse(string value)
        //{
        //    double result;
        //    if(double .TryParse (value,out result)==false)
        //    {
        //        MessageBox.Show("填写错误！！");
        //        return 0;
        //    }
        //    else
        //    {
        //        return result;
        //    }
        //}
        private void button5_Click(object sender, EventArgs e)
        {
            double c = 0, w = 0, rf = 0, n = 0, eN = 0, ra = 0, ed = 0, ea = 0, eTk = 0, u1 = 0, u2 = 0, z = 0, rn = 0, kc2 = 0, eT0 = 0, eTc2;
            string strC, strW, strRf, strN, strEN, strRa, strEd, strEa, strETk, strU1, strU2, strZ, strRn, strKc2, strET0;
            strC = this.txtC.Text;
            strW = this.txtW.Text;
            strRf = this.txtRf.Text;
            strN = this.txtN.Text;
            strEN = this.txtEN.Text;
            strRa = this.txtRa.Text;
            strEd = this.txtEd.Text;
            strEa = this.txtEa.Text;
            strETk = this.txtETk.Text;
            strU1 = this.txtU1.Text;
            strU2 = this.txtU2.Text;
            strZ = this.txtZ.Text;
            strRn = this.txtRn.Text;
            strKc2 = this.txtKc2.Text;
            strET0 = this.txtET0.Text;
            //strETc2 = this.txtETc2.Text;
            if (double.TryParse(strKc2, out kc2) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            if (strET0 == "")
            {
                if (double.TryParse(strW, out w) == false)
                {
                    MessageBox.Show("填写错误！！");
                    return;
                }

                if (double.TryParse(strC, out c) == false)
                {
                    MessageBox.Show("填写错误！！");
                    return;
                }

                if (double.TryParse(strEa, out ea) == false)
                {
                    MessageBox.Show("填写错误！！");
                    return;
                }
                if (strRn == "")
                {
                    if (double.TryParse(strRf, out rf) == false)
                    {
                        MessageBox.Show("填写错误！！");
                        return;
                    }
                    if (double.TryParse(strN, out n) == false)
                    {
                        MessageBox.Show("填写错误！！");
                        return;
                    }
                    if (double.TryParse(strEN, out eN) == false)
                    {
                        MessageBox.Show("填写错误！！");
                        return;
                    }
                    if (double.TryParse(strEd, out ed) == false)
                    {
                        MessageBox.Show("填写错误！！");
                        return;
                    }
                    if (double.TryParse(strRa, out ra) == false)
                    {
                        MessageBox.Show("填写错误！！");
                        return;
                    }
                    if (double.TryParse(strETk, out eTk) == false)
                    {
                        MessageBox.Show("填写错误！！");
                        return;
                    }
                    if (eTk < 250)
                    {
                        eTk += 273.14;
                    }

                }
                else
                {
                    if (double.TryParse(strRn, out rn) == false)
                    {
                        MessageBox.Show("填写错误！！");
                        return;
                    }
                }

                if (strU2 == "")
                {
                    if (double.TryParse(strU1, out u1) == false)
                    {
                        MessageBox.Show("填写错误！！");
                        return;
                    }
                    if (double.TryParse(strZ, out z) == false)
                    {
                        MessageBox.Show("填写错误！！");
                        return;
                    }
                }
                else
                {
                    if (double.TryParse(strU2, out u2) == false)
                    {
                        MessageBox.Show("填写错误！！");
                        return;
                    }
                }
                double sigama = 2e-9;
                if (rn == 0)
                {
                    double rns = (1 - rf) * (0.25 + 0.5 * n / eN) * ra;
                    double rni = sigama * Math.Pow(eTk, 4) * (0.34 - 0.044 * Math.Sqrt(ed)) * (0.1 + 0.9 * n / eN);
                    rn = rns - rni;
                }
                if (u2 == 0)
                {
                    u2 = u1 * Math.Pow(2 / z, 0.2);
                }
                double fu = 0.27 * (1 + u2 / 100);
                eT0 = c * w * rn + c * (1 - w) * fu * (ea - ed);
                eTc2 = eT0 * kc2;

                this.txtU2.Text = u2.ToString();
                this.txtRn.Text = rn.ToString();
                this.txtET0.Text = eT0.ToString();
                this.txtETc2.Text = eTc2.ToString();
            }
            else
            {
                if (double.TryParse(strET0, out eT0) == false)
                {
                    MessageBox.Show("填写错误！！");
                    return;
                }
                eTc2 = eT0 * kc2;
            }


        }

        private void txtRn_TextChanged(object sender, EventArgs e)
        {
            if (txtRn.Text != "")
            {
                txtRf.ReadOnly = true;
                txtN.ReadOnly = true;
                txtEN.ReadOnly = true;
                txtEd.ReadOnly = true;
                txtETk.ReadOnly = true;
                txtRa.ReadOnly = true;
            }
            else
            {
                txtRf.ReadOnly = false;
                txtN.ReadOnly = false;
                txtEN.ReadOnly = false;
                txtEd.ReadOnly = false;
                txtETk.ReadOnly = false;
                txtRa.ReadOnly = false;
            }

        }

        private void txtU2_TextChanged(object sender, EventArgs e)
        {
            if (txtU2.Text != "")
            {
                txtU1.ReadOnly = true;
                txtZ.ReadOnly = true;
            }
            else
            {
                txtU1.ReadOnly = false;
                txtZ.ReadOnly = false;
            }
        }

        private void button6_Click(object sender, EventArgs e)
        {
            double gc = 0, eTa = 0, eTc1 = 0, eTc2 = 0;
            string strGc, strETc1, strETc2;
            strGc = this.txtGc.Text;
            if (double.TryParse(strGc, out gc) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            if (this.txtETc.Text != "" || this.txtETc2.Text != "")
            {
                strETc1 = this.txtETc.Text;
                strETc2 = this.txtETc2.Text;
                double.TryParse(strETc1, out eTc1);
                double.TryParse(strETc2, out eTc2);
                double kr = gc / 0.85;
                if (eTc1 != 0)
                {
                    eTa = kr * eTc1;
                    this.txtETa.Text = eTa.ToString();
                }
                else
                {
                    eTa = kr * eTc2;
                    this.txtETa.Text = eTa.ToString();
                }
            }

        }

        private void txtET0_TextChanged(object sender, EventArgs e)
        {
            if (txtET0.Text != "")
            {
                txtRf.ReadOnly = true;
                txtN.ReadOnly = true;
                txtEN.ReadOnly = true;
                txtEd.ReadOnly = true;
                txtETk.ReadOnly = true;
                txtRa.ReadOnly = true;
                txtC.ReadOnly = true;
                txtW.ReadOnly = true;
                txtRn.ReadOnly = true;
                txtEa.ReadOnly = true;
                txtU1.ReadOnly = true;
                txtU2.ReadOnly = true;
                txtZ.ReadOnly = true;

            }
            else
            {
                txtRf.ReadOnly = false;
                txtN.ReadOnly = false;
                txtEN.ReadOnly = false;
                txtEd.ReadOnly = false;
                txtETk.ReadOnly = false;
                txtRa.ReadOnly = false;
                txtC.ReadOnly = false;
                txtW.ReadOnly = false;
                txtRn.ReadOnly = false;
                txtEa.ReadOnly = false;
                txtU1.ReadOnly = false;
                txtU2.ReadOnly = false;
                txtZ.ReadOnly = false;
            }
        }

        private void button7_Click(object sender, EventArgs e)
        {
            double se = 0, sl = 0, dw = 0, p = 0;
            string strSe, strSl, strDw;
            strSe = this.txtSe.Text;
            strSl = this.txtSl.Text;
            strDw = this.txtDw.Text;

            if (double.TryParse(strSe, out se) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            if (double.TryParse(strSl, out sl) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            if (double.TryParse(strDw, out dw) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            p = 0.785 * Math.Pow(dw, 2) / se / sl;
            this.txtP.Text = p.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            double se2 = 0, slz = 0, slk = 0, dw2 = 0, p1 = 0, p2 = 0, p = 0;
            string strSe2, strSlz, strSlk, strDw2;
            strSe2 = this.txtSe2.Text;
            strSlz = this.txtSlz.Text;
            strSlk = this.txtSlk.Text;
            strDw2 = this.txtDw2.Text;

            if (double.TryParse(strSe2, out se2) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            if (double.TryParse(strSlz, out slz) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            if (double.TryParse(strSlk, out slk) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            if (double.TryParse(strDw2, out dw2) == false)
            {
                MessageBox.Show("填写错误！！");
                return;
            }
            p1 = 0.785 * Math.Pow(dw2, 2) / se2 / slz;
            p2 = 0.785 * Math.Pow(dw2, 2) / se2 / slk;
            p = (p1 * slz + p2 * slk) / (slz + slk);
            this.txtp2.Text = p.ToString();
        }

        private void button9_Click(object sender, EventArgs e)
        {
            int num = this.dataGridView2.RowCount;

            double[] q = new double[num - 1];
            double[] eq = new double[num - 1];
            if (num > 1)
            {
                for (int i = 0; i < num - 1; i++)
                {
                    string qi = this.dataGridView2.Rows[i].Cells[1].Value.ToString();
                    if (double.TryParse(qi, out q[i]) == false)
                    {
                        MessageBox.Show("数据错误！请检查！");
                        return;
                    }
                    this.dataGridView2.Rows[i].Cells[0].Value = (i + 1);
                }

                double evenq = CalcEven(q);//流量平均值
                for (int j = 0; j < q.Length; j++)
                {
                    eq[j] = Math.Abs(q[j] - evenq);
                }
                double evq = eq.Sum() / num;//灌水器流量平均偏差
                double cu = 1 - evq / evenq;
                this.txtCu.Text = cu.ToString();
                this.txtEq.Text = evq.ToString();

            }
            else
            {
                MessageBox.Show("数据太少，请增加数据！");
            }

        }

        private void button10_Click(object sender, EventArgs e)
        {
            int num = this.dataGridView3.RowCount;

            double[] q = new double[num - 1];
            double cv = 0, lx = 0, kd = 0, eu, qmin=0, hmin, dh, ha;
            int n1 = 0;
            string strCv, strLx, strKd, strN1, strQmin;
            strCv = this.txtCv.Text;
            strLx = this.txtLx.Text;
            strKd = this.txtKd.Text;
            strN1 = this.txtN1.Text;
            strQmin = this.txtQmin.Text;
            if (double.TryParse(strLx, out lx) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            if (double.TryParse(strKd, out kd) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            if (int.TryParse(strN1, out n1) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            if (double.TryParse(strQmin, out qmin) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            if (strCv == "")
            {
                if (num > 1)
                {
                    for (int i = 0; i < num - 1; i++)
                    {
                        string qi = this.dataGridView3.Rows[i].Cells[1].Value.ToString();
                        if (double.TryParse(qi, out q[i]) == false)
                        {
                            MessageBox.Show("数据错误！请检查！");
                            return;
                        }
                        this.dataGridView3.Rows[i].Cells[0].Value = (i + 1);
                    }

                    double evenq = CalcEven(q);//流量平均值
                    cv = MathNet.Numerics.Statistics.ArrayStatistics.StandardDeviation(q);
                    eu = (1 - 1.27 * cv / Math.Sqrt(n1)) * qmin / evenq;
                    hmin = Math.Pow(qmin / kd, 1.0 / lx);
                    ha = Math.Pow(evenq / kd, 1.0 / lx);
                    dh = 2.5 * (ha - hmin);
                    this.txtCv.Text = cv.ToString();
                    this.txtEu.Text = eu.ToString();
                    this.txtHmin.Text = hmin.ToString();
                    this.txtDh.Text = dh.ToString();
                }
                else
                {
                    MessageBox.Show("数据太少，请增加数据！");
                }
            }
            else
            {
                if (double.TryParse(strCv, out cv) == false)
                {
                    MessageBox.Show("数据错误！请检查！");
                    return;
                }
                if (num > 1)
                {
                    for (int i = 0; i < num - 1; i++)
                    {
                        string qi = this.dataGridView3.Rows[i].Cells[1].Value.ToString();
                        if (double.TryParse(qi, out q[i]) == false)
                        {
                            MessageBox.Show("数据错误！请检查！");
                            return;
                        }
                        this.dataGridView3.Rows[i].Cells[0].Value = (i + 1);
                    }

                    double evenq = CalcEven(q);//流量平均值
                   
                    eu = (1 - 1.27 * cv / Math.Sqrt(n1)) * qmin / evenq;
                    hmin = Math.Pow(qmin / kd, 1.0 / lx);
                    ha = Math.Pow(evenq / kd, 1.0 / lx);
                    dh = 2.5 * (ha - hmin);
                    //this.txtCv.Text = cv.ToString();
                    this.txtEu.Text = eu.ToString();
                    this.txtHmin.Text = hmin.ToString();
                    this.txtDh.Text = dh.ToString();
                }
                else
                {
                    MessageBox.Show("数据太少，请增加数据！");
                }

            }


        }

        private void button11_Click(object sender, EventArgs e)
        {
            double lx2 = 0, qmi = 0, qma = 0, qd = 0, hd = 0, kd2 = 0, hmi = 0, hma = 0, qv, hv;
            string strLx2, strQmi, strQma, strQd,strHd, strKd2, strHmi, strHma;
            strLx2 = this.txtLx2.Text;
            strQmi = this.txtQmi.Text;
            strQma = this.txtQma.Text;
            strQd = this.txtQd.Text;
            strHd = this.txtHd.Text;
            strKd2 = this.txtKd2.Text;
            strHmi = this.txtHmi.Text;
            strHma = this.txtHma.Text;
            
            if (double.TryParse(strQmi, out qmi ) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            if (double.TryParse(strQma, out qma) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            if (double.TryParse(strQd, out qd) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            
            if(strKd2 =="")
            {
               
                
                if(strLx2 =="")
                {
                    if (double.TryParse(strHmi, out hmi) == false)
                    {
                        MessageBox.Show("数据错误！请检查！");
                        return;
                    }
                    if (double.TryParse(strHma, out hma) == false)
                    {
                        MessageBox.Show("数据错误！请检查！");
                        return;
                    }
                    if (double.TryParse(strHd, out hd) == false)
                    {
                        MessageBox.Show("数据错误！请检查！");
                        return;
                    }
                    qv = (qma - qmi) / qd;
                    hv = (hma - hmi) / hd;
                    this.txtQv.Text = qv.ToString();
                    this.txtHv.Text = hv.ToString();
                }
                else
                {
                    if (double.TryParse(strLx2, out lx2) == false)
                    {
                        MessageBox.Show("数据错误！请检查！");
                        return;
                    }
                    qv = (qma - qmi) / qd;
                    hv = qv / lx2 * (1 + 0.15 * (1 - lx2) / lx2 * qv);
                    this.txtQv.Text = qv.ToString();
                    this.txtHv.Text = hv.ToString();
                }
                
            }
            else
            {
                if (double.TryParse(strKd2, out kd2) == false)
                {
                    MessageBox.Show("数据错误！请检查！");
                    return;
                }
                if (double.TryParse(strLx2, out lx2) == false)
                {
                    MessageBox.Show("数据错误！请检查！");
                    return;
                }
                hmi = Math.Pow(qmi / kd2, 1 / lx2);
                hma = Math.Pow(qma / kd2, 1 / lx2);
                hd = Math.Pow(qd / kd2, 1 / lx2);
                qv = (qma - qmi) / qd;
                hv = (hma - hmi) / hd;
                this.txtHmi.Text = hmi.ToString();
                this.txtHma.Text = hma.ToString();
                this.txtHd.Text = hd.ToString();
                this.txtQv.Text = qv.ToString();
                this.txtHv.Text = hv.ToString();
            }

        }


        private void txtKd2_TextChanged(object sender, EventArgs e)
        {
            if(txtKd2 .Text !="")
            {
                txtHmi.ReadOnly = true;
                txtHma.ReadOnly = true;
                txtHd.ReadOnly = true;
            }
            else
            {
                txtHmi.ReadOnly = false ;
                txtHma.ReadOnly = false;
                txtHd.ReadOnly = false;
            }
        }

        private void button12_Click(object sender, EventArgs e)
        {
            double irg = 0, irn = 0,yiTa;
            string strIRg, strIRn;
            strIRg = this.txtIRg.Text;
            strIRn = this.txtIRn.Text;
            if (double.TryParse(strIRg, out irg ) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            if (double.TryParse(strIRn, out irn) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            yiTa = irn / irg;
            this.txtYT.Text = yiTa.ToString();
        }

        private void button13_Click(object sender, EventArgs e)
        {
            double irg2 = 0, irn2 = 0,lr=0, yiTa2;
            string strIRg2, strIRn2,strLr;
            strIRg2 = this.txtIRg2.Text;
            strIRn2 = this.txtIRn2.Text;
            strLr =this .txtLr .Text ;
            if (double.TryParse(strIRg2, out irg2) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            if (double.TryParse(strIRn2, out irn2) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            if (double.TryParse(strLr, out lr) == false)
            {
                MessageBox.Show("数据错误！请检查！");
                return;
            }
            yiTa2 = irn2 / (irg2-lr);
            this.txtYT2.Text = yiTa2.ToString();
        }



    }
}
