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
    public partial class FormMainfm : Form
    {
        public FormMainfm()
        {
            InitializeFrame();
           
        }
        public void InitializeFrame()
        {
            InitializeComponent();
            int count = this.Controls.Count * 2 + 2;
            float[] factor = new float[count];
            int i = 0;
            factor[i++] = Size.Width;
            factor[i++] = Size.Height;
            foreach (Control ctrl in this.Controls)
            {
                factor[i++] = ctrl.Location.X / (float)Size.Width;
                factor[i++] = ctrl.Location.Y / (float)Size.Height;
                ctrl.Tag = ctrl.Size;
            }
            Tag = factor;
        }



        private int percentValue = 0;
        private void backgroundWorker1_DoWork(object sender, DoWorkEventArgs e)
        {
            e.Result = ProcessProgress(backgroundWorker1, e);
        }

        private void backgroundWorker1_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            // bkWorker.ReportProgress 会调用到这里，此处可以进行自定义报告方式  
            this.progressBar1.Value = e.ProgressPercentage;
            int percent = (int)(e.ProgressPercentage / percentValue);
            this.label1.Text = "处理进度:" + Convert.ToString(percent) + "%";
        }

        private void backgroundWorker1_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            this.label1.Text = "处理完毕!";
        }
        private int ProcessProgress(object sender, DoWorkEventArgs e)
        {
            for (int i = 0; i <= 100; i++)
            {
                if (backgroundWorker1.CancellationPending)
                {
                    e.Cancel = true;
                    return -1;
                }
                else
                {
                    // 状态报告  
                    backgroundWorker1.ReportProgress(i);

                    // 等待，用于UI刷新界面，很重要  
                    System.Threading.Thread.Sleep(1);
                }
            }

            return -1;
        }
        
        private void FormMainfm_Resize(object sender, EventArgs e)
        {
           float[] scale = (float[])Tag;
            int i = 2;

            foreach (Control ctrl in this.Controls)
            {
                ctrl.Left = (int)(Size.Width * scale[i++]);
                ctrl.Top = (int)(Size.Height * scale[i++]);
                ctrl.Width = (int)(Size.Width / (float)scale[0] * ((Size)ctrl.Tag).Width);
                ctrl.Height = (int)(Size.Height / (float)scale[1] * ((Size)ctrl.Tag).Height);
                //每次使用的都是最初始的控件大小，保证准确无误。
            }
            
        }

        private void 规划设计参数ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DesigneParam Pd = new DesigneParam();
            Pd.Show();
        }

        private void 微灌工程规划ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IrrigationPlan IP = new IrrigationPlan();
            IP.Show();
        }

        private void 微灌工程设计ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            IrrigationDesinge ID = new IrrigationDesinge();
            ID.Show();
        }


    }
}
