using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using MaterialSkin;
using MaterialSkin.Controls;
using System.Threading;
using System.Security.Permissions;

namespace materia_prima
{
    public partial class Report : MaterialForm
    { 
        public Report( int articulo,string sscc, int IDSSCC, int Terminal)
        {
            
           _articulo = articulo;
            _IDSSCC = IDSSCC;
            _Terminal = Terminal;
            _sscc = sscc;
           
            bw.WorkerReportsProgress = true;
            bw.WorkerSupportsCancellation = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork); 
            
            InitializeComponent();
         
        }
        private int _articulo { get; set; }
        private int _IDSSCC { get; set; }
        private int _Terminal { get; set; }
        private string _sscc { get; set; }
     
        private Thread t { get; set; }
       
        private MaterialForm myForm = new MaterialForm();
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
      
        private void Form4_Load(object sender, EventArgs e)
        {
            bw.RunWorkerAsync();
            //SPlashForm.Close();

            // TODO: This line of code loads data into the 'QC600DataSet.FIFO_MATERIA_PRIMA_STOCK' table. You can move, or remove it, as needed.
            this.FIFO_MATERIA_PRIMA_STOCKTableAdapter.Fill(this.QC600DataSet.FIFO_MATERIA_PRIMA_STOCK, _articulo, _IDSSCC, _Terminal);
            // TODO: This line of code loads data into the 'QC600DataSet.FIFO_MATERIA_PRIMA_MATRICULA' table. You can move, or remove it, as needed.
            this.FIFO_MATERIA_PRIMA_MATRICULATableAdapter.Fill(this.QC600DataSet.FIFO_MATERIA_PRIMA_MATRICULA, _sscc);
            //KillTheThread();
            
            this.reportViewer1.RefreshReport();
           
            bw.CancelAsync();
            bw.Dispose();
           
           
        }

        private void Report_FormClosed(object sender, FormClosedEventArgs e)
        {
            this.DialogResult = DialogResult.OK;
            myForm.Visible = true;
            
        }
    }
}
