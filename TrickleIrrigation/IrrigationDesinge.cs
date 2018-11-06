using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace TrickleIrrigation
{
    public partial class IrrigationDesinge : Form
    {
        public IrrigationDesinge()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Dictionary<string, double> param = new Dictionary<string, double>(20);
            double gm = 0, mma, mmam, mdj, mdm;
            double se = 0, sl = 0, sr = 0, st = 0, tl = 0;
            int ns = 0;
            int t = 0, tma = 0, td;
            foreach (Control txtb in this.panel1.Controls)
            {
                if (txtb is TextBox)
                {
                    string strTxt;
                    double txtValue;
                    strTxt = txtb.Text;
                    if (txtb.Name != txtGm.Name && txtb.Name != txtT.Name && txtb.Name != txtSe.Name && txtb.Name != txtSl.Name && txtb.Name != txtSr.Name && txtb.Name != txtSt.Name && txtb.Name != txtNs.Name)
                    {
                        if (double.TryParse(strTxt, out txtValue) == false)
                        {
                            MessageBox.Show("输入错误！请检查！");
                            return;
                        }
                        param.Add(txtb.Name, txtValue);
                    }
                    else if (strTxt != "" && txtb.Name == txtGm.Name)
                    {
                        if (double.TryParse(strTxt, out gm) == false)
                        {
                            MessageBox.Show("输入错误！请检查！");
                            return;
                        }

                    }
                    else if (strTxt != "" && txtb.Name == txtT.Name)
                    {
                        if (int.TryParse(strTxt, out t) == false)
                        {
                            MessageBox.Show("输入错误！请检查！");
                            return;
                        }
                    }
                    else if (strTxt != "" && txtb.Name == txtSe.Name)
                    {
                        if (double.TryParse(strTxt, out se) == false)
                        {
                            MessageBox.Show("输入错误！请检查！");
                            return;
                        }
                        if (double.TryParse(strTxt, out sl) == false)
                        {
                            MessageBox.Show("输入错误！请检查！");
                            return;
                        }
                    }
                    else if (strTxt != "" && txtb.Name == txtSr.Name)
                    {
                        if (double.TryParse(strTxt, out sr) == false)
                        {
                            MessageBox.Show("输入错误！请检查！");
                            return;
                        }
                        if (double.TryParse(strTxt, out st) == false)
                        {
                            MessageBox.Show("输入错误！请检查！");
                            return;
                        }
                        if (int.TryParse(strTxt, out ns) == false)
                        {
                            MessageBox.Show("输入错误！请检查！");
                            return;
                        }
                    }

                }
            }
            if (gm != 0)
            {
                mma = 0.001 * gm * param[txtZ.Name] * param[txtP.Name] * (param[txtQma.Name] - param[txtQmi.Name]);

            }
            else
            {
                mma = 0.001 * param[txtZ.Name] * param[txtP.Name] * (param[txtQma.Name] - param[txtQmi.Name]);
            }
            mmam = mma / param[txtYt.Name];
            if (t != 0)
            {
                tma = (int)Math.Ceiling(mma / param[txtETa.Name]);
                if (t > tma)
                {
                    MessageBox.Show("设计灌水周期超过最大灌水周期！！请从新设值！");
                }
                mdj = t * param[txtETa.Name];
                mdm = mdj / param[txtYt.Name];
                td = t;
                this.txtTd.Text = td.ToString();
            }
            else
            {
                tma = (int)Math.Ceiling(mma / param[txtETa.Name]);
                td = tma - 1;
                mdj = td * param[txtETa.Name];
                mdm = mdj / param[txtYt.Name];
                this.txtT.Text = td.ToString();
                this.txtTd.Text = td.ToString();
            }
            if (se != 0)
            {
                tl = mdm * se * sl / param[txtQd.Name];
            }
            else if (sr != 0)
            {
                tl = mdm * sr * st / ns / param[txtQd.Name];
            }
            this.txtMma.Text = Math.Round(mma, 3).ToString();
            this.txtMmam.Text = Math.Round(mmam, 3).ToString();
            this.txtMdj.Text = Math.Round(mdj, 3).ToString();
            this.txtMdm.Text = Math.Round(mdm, 3).ToString();
            this.txtTma.Text = tma.ToString();
            this.txtTl.Text = Math.Round(tl, 3).ToString();
        }

        private void txtSe_TextChanged(object sender, EventArgs e)
        {
            if (txtSe.Text != "")
            {
                this.txtSr.ReadOnly = true;
                this.txtSt.ReadOnly = true;
                this.txtNs.ReadOnly = true;
            }
            else
            {
                this.txtSr.ReadOnly = false;
                this.txtSt.ReadOnly = false;
                this.txtNs.ReadOnly = false;
            }
        }

        private void txtSl_TextChanged(object sender, EventArgs e)
        {
            if (txtSl.Text != "")
            {
                this.txtSr.ReadOnly = true;
                this.txtSt.ReadOnly = true;
                this.txtNs.ReadOnly = true;
            }
            else
            {
                this.txtSr.ReadOnly = false;
                this.txtSt.ReadOnly = false;
                this.txtNs.ReadOnly = false;
            }
        }

        private void txtSr_TextChanged(object sender, EventArgs e)
        {
            if (txtSr.Text != "")
            {
                this.txtSe.ReadOnly = true;
                this.txtSl.ReadOnly = true;
            }
            else
            {
                this.txtSe.ReadOnly = false;
                this.txtSl.ReadOnly = false;
            }
        }

        private void txtSt_TextChanged(object sender, EventArgs e)
        {
            if (txtSt.Text != "")
            {
                this.txtSe.ReadOnly = true;
                this.txtSl.ReadOnly = true;
            }
            else
            {
                this.txtSe.ReadOnly = false;
                this.txtSl.ReadOnly = false;
            }
        }

        private void txtNs_TextChanged(object sender, EventArgs e)
        {
            if (txtNs.Text != "")
            {
                this.txtSe.ReadOnly = true;
                this.txtSl.ReadOnly = true;
            }
            else
            {
                this.txtSe.ReadOnly = false;
                this.txtSl.ReadOnly = false;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            //Dictionary<string, double> param = new Dictionary<string, double>(20);
            //foreach (Control txtb in this.panel3.Controls)
            //{
            //    if (txtb is TextBox)
            //    {
            //        string strTxt;
            //        double txtValue;
            //        strTxt = txtb.Text;
            //        if (txtb.Name != txtNma.Name && txtb.Name != txtNd.Name)
            //        {
            //            if (double.TryParse(strTxt, out txtValue) == false)
            //            {
            //                MessageBox.Show("输入错误！请检查！");
            //                return;
            //            }
            //            param.Add(txtb.Name, txtValue);
            //        }
            //    }
            //}

            double qdx = 0, qd2 = 0, tl2 = 0;
            int nz = 0, tdm = 0, td2 = 0, nma = 0, nd = 0;
            string strNz, strTdm, strTd2, strQdx, strTl2, strQd2;
            strNz = txtNz.Text;
            strTdm = txtTdm.Text;
            strTd2 = txtTd2.Text;
            strQdx = txtQdx.Text;
            strTl2 = txtTl2.Text;
            strQd2 = txtQd2.Text;
            if (double.TryParse(strQdx, out qdx) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (double.TryParse(strQd2, out qd2) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (int.TryParse(strNz, out nz) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (int.TryParse(strTdm, out tdm) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (double.TryParse(strTl2, out tl2) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (int.TryParse(strTd2, out td2) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            nma = (int)Math.Ceiling(tdm * td2 / tl2);
            nd = (int)Math.Ceiling(nz * qdx / qd2);
            txtNma.Text = nma.ToString();
            txtNd.Text = nd.ToString();
        }

        double qdxx, qdmm, qdzz, qdzz2, qdgg;
        private void button3_Click(object sender, EventArgs e)
        {
            int n0 = 0;
            double qdx2 = 0, qd3;
            string strQdx2, strN0;
            strQdx2 = txtQdx2.Text;
            strN0 = txtN0.Text;
            if (double.TryParse(strQdx2, out qdx2) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (int.TryParse(strN0, out n0) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            qdxx = qdx2;
            qd3 = n0 * qdx2 / 1000;
            this.txtQd3.Text = qd3.ToString();

        }

        private void button4_Click(object sender, EventArgs e)
        {
            int n1 = 0;
            double qdm = 0;
            string strN1;
            strN1 = this.txtN1.Text;
            if (int.TryParse(strN1, out n1) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            qdm = n1 * qdxx;
            this.txtQdm.Text = qdm.ToString();
            qdmm = qdm;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            int n2 = 0;
            double qdz = 0;
            string strN2;
            strN2 = this.txtN2.Text;
            if (int.TryParse(strN2, out n2) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            qdz = n2 * qdmm;
            this.txtQdz.AppendText(qdz.ToString() + "\r\n");
            qdzz = qdz;
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int n3 = 0;
            double qdz2 = 0;
            string strN3;
            strN3 = this.txtN3.Text;
            if (int.TryParse(strN3, out n3) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            qdz2 = 2 * n3 * qdmm;
            this.txtQdz2.AppendText(qdz2.ToString() + "\r\n");
            qdzz2 = qdz2;
        }

        private void button7_Click(object sender, EventArgs e)
        {
            int n1 = this.txtQdz.Lines.Count();
            int n2 = this.txtQdz2.Lines.Count();
            double qdg = 0;
            if (n1 > 0)
            {
                for (int i = 0; i < n1 - 1; i++)
                {
                    double temp = Convert.ToDouble(this.txtQdz.Lines[i].ToString());
                    qdg += temp;

                }
            }
            if (n2 > 0)
            {
                for (int i = 0; i < n2 - 1; i++)
                {
                    double temp = Convert.ToDouble(this.txtQdz2.Lines[i]);
                    qdg += temp;

                }
            }
            qdgg = qdg;
            this.txtQdg.Text = qdg.ToString();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            string guancai = this.cbGc.SelectedItem.ToString();
            double m, b, f;
            double gdll = 0, gdnj = 0, gdcd = 0, stss = 0;
            double v, re;
            string strGdll, strGdnj, strGdcd;
            strGdll = txtGdll1.Text;
            strGdcd = txtGdcd1.Text;
            strGdnj = txtGdnj1.Text;
            if (double.TryParse(strGdll, out gdll) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (double.TryParse(strGdcd, out gdcd) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (double.TryParse(strGdnj, out gdnj) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            v = 4 * gdll / 1000.0 / (Math.PI * Math.Pow(gdnj / 1000.0, 2)) / 3600.0;
            re = v * gdnj / 1.007 * Math.Pow(10, 6);//20度雷诺数
            if (guancai == "硬塑料管")
            {
                f = 0.464;
                m = 1.77;
                b = 4.77;
            }
            else if (guancai == "微灌用聚乙烯管")
            {
                if (gdnj > 8)
                {
                    f = 0.505;
                    m = 1.75;
                    b = 4.69;
                }
                else if (gdnj > 0 && gdnj <= 8)
                {
                    if (re > 2320)
                    {
                        f = 0.595;
                        m = 1.69;
                        b = 4.69;
                    }
                    else
                    {
                        f = 1.75;
                        m = 1;
                        b = 4;
                    }
                }
                else
                {
                    f = 0.464;
                    m = 1.77;
                    b = 4.77;
                    MessageBox.Show("管径设置错误！可能是负值！");
                    return;
                }

            }
            else
            {
                f = 0.464;
                m = 1.77;
                b = 4.77;
            }
            stss = f * Math.Pow(gdll, m) * gdcd / Math.Pow(gdnj, b);
            txtStss1.Text = stss.ToString();




        }

        private void button9_Click(object sender, EventArgs e)
        {
            string guancai = this.cbGc.SelectedItem.ToString();
            double m, b, f;
            double gdll = 0, gdnj = 0, gdcd = 0, stss = 0, kkb = 1;
            int ckn = 0;
            double v, re;
            string strGdll, strGdnj, strGdcd, strKkb, strCkn;
            strGdll = txtGdll2.Text;
            strGdcd = txtGdcd2.Text;
            strGdnj = txtGdnj2.Text;
            strKkb = txtKkb.Text;
            strCkn = txtCkn1.Text;
            if (double.TryParse(strGdll, out gdll) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (double.TryParse(strGdcd, out gdcd) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (double.TryParse(strGdnj, out gdnj) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (double.TryParse(strKkb, out kkb) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (int.TryParse(strCkn, out ckn) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            v = 4 * gdll / 1000.0 / (Math.PI * Math.Pow(gdnj / 1000.0, 2)) / 3600.0;
            re = v * gdnj / 1.007 * Math.Pow(10, 6);//20度雷诺数
            if (guancai == "硬塑料管")
            {
                f = 0.464;
                m = 1.77;
                b = 4.77;
            }
            else if (guancai == "微灌用聚乙烯管")
            {
                if (gdnj > 8)
                {
                    f = 0.505;
                    m = 1.75;
                    b = 4.69;
                }
                else if (gdnj > 0 && gdnj <= 8)
                {
                    if (re > 2320)
                    {
                        f = 0.595;
                        m = 1.69;
                        b = 4.69;
                    }
                    else
                    {
                        f = 1.75;
                        m = 1;
                        b = 4;
                    }
                }
                else
                {
                    f = 0.464;
                    m = 1.77;
                    b = 4.77;
                    MessageBox.Show("管径设置错误！可能是负值！");
                    return;
                }

            }
            else
            {
                f = 0.464;
                m = 1.77;
                b = 4.77;
            }
            stss = f * Math.Pow(gdll, m) * gdcd / Math.Pow(gdnj, b);
            double df;//多口系数
            df = (ckn * (1.0 / (m + 1) + 0.5 / ckn + Math.Sqrt(m - 1) / 6 / Math.Pow(ckn, 2)) - 1 + kkb) / (ckn - 1 + kkb);
            stss *= df;
            txtStss2.Text = stss.ToString();

        }

        private void button10_Click(object sender, EventArgs e)
        {
            string guancai = this.cbGc.SelectedItem.ToString();
            double m, b, f;
            double  gdnj = 0, gdcd = 0, stss = 0;
            int ckn = 0;
            double v, re;
            string strGdll, strGdnj, strGdcd, strCkn;
            int num = this.txtGdll3.Lines.Count();
            double Lz = 0;
            double[] qz = new double[num];
            double stssz = 0;//总水头损失
            for (int i = 0; i < num; i++)
            {
                double temp1,temp2;
                strGdcd = txtGdcd3.Lines[i].ToString();
                strGdll = txtGdll3.Lines[i].ToString();
          
                if (double.TryParse(strGdcd, out temp1) == false)
                {
                    MessageBox.Show("输入错误！请检查！");
                    return;
                }
                if (double.TryParse(strGdll, out temp2) == false)
                {
                    MessageBox.Show("输入错误！请检查！");
                    return;
                }
                Lz += temp1;
                qz[i] = temp2;
            }
            for (int i = 0; i < num; i++)
            {
                //strGdll = txtGdll3.Lines[i].ToString();
                strGdcd = txtGdcd3.Lines[i].ToString();
                strGdnj = txtGdnj3.Lines[i].ToString();
                strCkn = txtCkn2.Lines[i].ToString();
                //if (double.TryParse(strGdll, out gdll) == false)
                //{
                //    MessageBox.Show("输入错误！请检查！");
                //    return;
                //}
                if (double.TryParse(strGdcd, out gdcd) == false)
                {
                    MessageBox.Show("输入错误！请检查！");
                    return;
                }
                if (double.TryParse(strGdnj, out gdnj) == false)
                {
                    MessageBox.Show("输入错误！请检查！");
                    return;
                }

                if (int.TryParse(strCkn, out ckn) == false)
                {
                    MessageBox.Show("输入错误！请检查！");
                    return;
                }
                v = 4 * qz[i] / 1000.0 / (Math.PI * Math.Pow(gdnj / 1000.0, 2)) / 3600.0;
                re = v * gdnj / 1.007 * Math.Pow(10, 6);//20度雷诺数
                if (guancai == "硬塑料管")
                {
                    f = 0.464;
                    m = 1.77;
                    b = 4.77;
                }
                else if (guancai == "微灌用聚乙烯管")
                {
                    if (gdnj > 8)
                    {
                        f = 0.505;
                        m = 1.75;
                        b = 4.69;
                    }
                    else if (gdnj > 0 && gdnj <= 8)
                    {
                        if (re > 2320)
                        {
                            f = 0.595;
                            m = 1.69;
                            b = 4.69;
                        }
                        else
                        {
                            f = 1.75;
                            m = 1;
                            b = 4;
                        }
                    }
                    else
                    {
                        f = 0.464;
                        m = 1.77;
                        b = 4.77;
                        MessageBox.Show("管径设置错误！可能是负值！");
                        return;
                    }

                }
                else
                {
                    f = 0.464;
                    m = 1.77;
                    b = 4.77;
                }
                double df;//多口系数
                df = 1.0 / (m + 1) * Math.Pow((ckn + 0.48) / ckn, m + 1);
                double f1 = 0,f2=0;//水头损失
                if(i<num -1)
                {
                    f1 = f * Math.Pow(qz[i], m) * Lz / Math.Pow(gdnj, b) * df;
                    f2 = f * Math.Pow(qz[i + 1], m) * (Lz - gdcd) / Math.Pow(gdnj, b) * df;
                    Lz = Lz - gdcd;
                    stss = f1 - f2;
                }
                else
                {
                    stss = f * Math.Pow(qz[i], m) * gdcd / Math.Pow(gdnj, b) * df;
                }
                
                stssz += stss;
                
            }





            this.txtStss3.Text = stssz.ToString();
        }
    }
}
