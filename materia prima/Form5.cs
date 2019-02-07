using MaterialSkin.Controls;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Printing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace materia_prima
{
    public partial class Form5 : Form
    {
        private int _Terminal { get; set; }
        public Form5(int Terminal)
        {
            _Terminal = Terminal;
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            InitializeComponent();
            System.Drawing.Printing.PageSettings pg = new System.Drawing.Printing.PageSettings();
            pg.Margins.Top = 0;
            pg.Margins.Bottom = 0;
            pg.Margins.Left = 0;
            pg.Margins.Right = 0;

            pg.Landscape = true;
            
            reportViewer1.SetPageSettings(pg);
           // reportViewer1.Report.PageSettings.Landscape = true;
        }
        public MaterialForm getForm
        {
            get { return myForm; }

            set { myForm = value; }
        }

        BackgroundWorker bw = new BackgroundWorker();
        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            SplashScreen ss = new SplashScreen();
            ss.Show();

            while (!worker.CancellationPending) //just hangout and wait
            {
                Thread.Sleep(100);
            }

            if (worker.CancellationPending)
            {
                ss.Close();
                ss.Dispose();

                ss = null;
                e.Cancel = true;
            }

        }   

        private void Form5_Load(object sender, EventArgs e)
        {

            bw.RunWorkerAsync();
           
            // TODO: This line of code loads data into the 'QC600DataSet1.MATERIA_PRIMA_TERMINAL' table. You can move, or remove it, as needed.
            this.MATERIA_PRIMA_TERMINALTableAdapter.Fill(this.QC600DataSet1.MATERIA_PRIMA_TERMINAL, _Terminal);

            this.reportViewer1.RefreshReport();
            bw.CancelAsync();
            bw.Dispose();

        }
        private MaterialForm myForm = new MaterialForm();
        private void Form5_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            myForm.Visible = true;
        }
    }
}
