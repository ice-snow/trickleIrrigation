namespace TrickleIrrigation
{
    partial class FormMainfm
    {
        /// <summary>
        /// 必需的设计器变量。
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 清理所有正在使用的资源。
        /// </summary>
        /// <param name="disposing">如果应释放托管资源，为 true；否则为 false。</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows 窗体设计器生成的代码

        /// <summary>
        /// 设计器支持所需的方法 - 不要
        /// 使用代码编辑器修改此方法的内容。
        /// </summary>
        private void InitializeComponent()
        {
            this.backgroundWorker1 = new System.ComponentModel.BackgroundWorker();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label1 = new System.Windows.Forms.Label();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.规划设计参数ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.微灌工程规划ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.微灌工程设计ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.btnCalc = new System.Windows.Forms.Button();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // backgroundWorker1
            // 
            this.backgroundWorker1.WorkerReportsProgress = true;
            this.backgroundWorker1.WorkerSupportsCancellation = true;
            this.backgroundWorker1.DoWork += new System.ComponentModel.DoWorkEventHandler(this.backgroundWorker1_DoWork);
            this.backgroundWorker1.ProgressChanged += new System.ComponentModel.ProgressChangedEventHandler(this.backgroundWorker1_ProgressChanged);
            this.backgroundWorker1.RunWorkerCompleted += new System.ComponentModel.RunWorkerCompletedEventHandler(this.backgroundWorker1_RunWorkerCompleted);
            // 
            // progressBar1
            // 
            this.progressBar1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.progressBar1.Location = new System.Drawing.Point(410, -54);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(132, 23);
            this.progressBar1.TabIndex = 0;
            // 
            // label1
            // 
            this.label1.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(339, -47);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 1;
            this.label1.Text = "运行进度：";
            // 
            // menuStrip1
            // 
            this.menuStrip1.GripStyle = System.Windows.Forms.ToolStripGripStyle.Visible;
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.规划设计参数ToolStripMenuItem,
            this.微灌工程规划ToolStripMenuItem,
            this.微灌工程设计ToolStripMenuItem});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(554, 25);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // 规划设计参数ToolStripMenuItem
            // 
            this.规划设计参数ToolStripMenuItem.Name = "规划设计参数ToolStripMenuItem";
            this.规划设计参数ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.规划设计参数ToolStripMenuItem.Text = "规划设计参数";
            this.规划设计参数ToolStripMenuItem.Click += new System.EventHandler(this.规划设计参数ToolStripMenuItem_Click);
            // 
            // 微灌工程规划ToolStripMenuItem
            // 
            this.微灌工程规划ToolStripMenuItem.Name = "微灌工程规划ToolStripMenuItem";
            this.微灌工程规划ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.微灌工程规划ToolStripMenuItem.Text = "微灌工程规划";
            this.微灌工程规划ToolStripMenuItem.Click += new System.EventHandler(this.微灌工程规划ToolStripMenuItem_Click);
            // 
            // 微灌工程设计ToolStripMenuItem
            // 
            this.微灌工程设计ToolStripMenuItem.Name = "微灌工程设计ToolStripMenuItem";
            this.微灌工程设计ToolStripMenuItem.Size = new System.Drawing.Size(92, 21);
            this.微灌工程设计ToolStripMenuItem.Text = "微灌工程设计";
            this.微灌工程设计ToolStripMenuItem.Click += new System.EventHandler(this.微灌工程设计ToolStripMenuItem_Click);
            // 
            // btnCalc
            // 
            this.btnCalc.Location = new System.Drawing.Point(12, 240);
            this.btnCalc.Name = "btnCalc";
            this.btnCalc.Size = new System.Drawing.Size(110, 30);
            this.btnCalc.TabIndex = 3;
            this.btnCalc.Text = "计算器";
            this.btnCalc.UseVisualStyleBackColor = true;
            // 
            // FormMainfm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(554, 317);
            this.Controls.Add(this.btnCalc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.menuStrip1);
            this.MainMenuStrip = this.menuStrip1;
            this.Name = "FormMainfm";
            this.Text = "微灌计算系统";
            this.Resize += new System.EventHandler(this.FormMainfm_Resize);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.ComponentModel.BackgroundWorker backgroundWorker1;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem 规划设计参数ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 微灌工程规划ToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem 微灌工程设计ToolStripMenuItem;
        private System.Windows.Forms.Button btnCalc;
    }
}

