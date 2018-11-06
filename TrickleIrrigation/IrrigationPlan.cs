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
    public partial class IrrigationPlan : Form
    {
        public IrrigationPlan()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int lines = this.txtM.Lines.Count();
            double[] m = new double[lines];
            double[] bz = new double[lines];
            double a = 0, yt = 0.8, mc, wm;
            string strA, strYT, strM, strBZ;
            strA = this.txtA.Text;
            strYT = this.txtYT.Text;
            if (double.TryParse(strA, out a) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }
            if (double.TryParse(strYT, out yt) == false)
            {
                MessageBox.Show("输入错误！请检查！");
                return;
            }


            if (lines > 1)
            {
                for (int i = 0; i < lines; i++)
                {
                    strM = this.txtM.Lines[i].ToString();
                    if (double.TryParse(strM, out m[i]) == false)
                    {
                        MessageBox.Show("输入错误！请检查！");
                        return;
                    }
                    strBZ = this.txtBZ.Lines[i].ToString();
                    if (double.TryParse(strBZ, out bz[i]) == false)
                    {
                        MessageBox.Show("输入错误！请检查！");
                        return;
                    }
                }
            }
            else
            {
                strM = this.txtM.Lines[0].ToString();
                if (double.TryParse(strM, out m[0]) == false)
                {
                    MessageBox.Show("输入错误！请检查！");
                    return;
                }
                bz[0] = 1;
            }
            double[] mci = new double[lines];
            for (int i = 0; i < lines; i++)
            {
                mci[i] = bz[i] * m[i];
            }
            mc = mci.Sum();
            wm = mc * a / yt;
            this.txtWm.AppendText(wm.ToString() + "\r\n");
            double t = 0, td = 0, qv;
            double []q=new double[lines];           
            string strT, strTd;
            strT = this.txtT.Text;
            strTd = this.txtTd.Text;
            if(strT !=""&&strTd !="")
            {
                if (double.TryParse(strT , out t) == false)
                {
                    MessageBox.Show("输入错误！请检查！");
                    return;
                }
                if (double.TryParse(strTd, out td) == false)
                {
                    MessageBox.Show("输入错误！请检查！");
                    return;
                }
                for(int i=0;i<lines ;i++)
                {
                    //q[i] = bz[i] * m[i] / (3.6 * t * td);
                    q[i] = bz[i] * m[i] / (0.36 * t * td);
                    this.txtQ.AppendText(Math.Round(q[i], 2).ToString() + ",");
                }
                this.txtQ.AppendText("Sum="+Math .Round( q.Sum (),2).ToString ()+"\r\n");
                qv = wm / t / td;
                this.txtQv.AppendText(qv.ToString() + "\r\n");
            }

        }

        private void txtM_TextChanged(object sender, EventArgs e)
        {
            if(this .txtM.Lines.Count ()>1)
            {
                txtBZ.ReadOnly = false;
            }
            else
            {
                txtBZ.ReadOnly = true;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.txtWm.Clear();
            this.txtQ.Clear();
            this.txtQv.Clear();
        }
    }
}
