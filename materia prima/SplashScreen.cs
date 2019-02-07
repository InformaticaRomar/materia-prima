using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace materia_prima
{
    public partial class SplashScreen : Form
    {
        const int DEFAULT_TIME = 1000;

        Thread t;
        public SplashScreen()
        {
            InitializeComponent();
            Control.CheckForIllegalCrossThreadCalls = false;
          //  InitializeComponent();
           // this.Shown += new EventHandler(fSplashScreen_Shown);
           // this.FormClosing += new FormClosingEventHandler(Form1_FormClosing);
        }
        void fSplashScreen_Shown(object sender, EventArgs e)
        {
           // initvalues();
            /*t = new System.Threading.Thread(initApplication);
            t.IsBackground = true;
            t.SetApartmentState(ApartmentState.STA);
            t.Start();*/
        }


        void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
           /* if (t != null && t.IsAlive)
            {
                t.Abort();
                t = null;
            }*/
        }
      
        public void initApplication()
        {
            Thread.Sleep(DEFAULT_TIME);
            this.Invoke((MethodInvoker)(() => setMessage("Buscando Matriculas...")));
            Thread.Sleep(DEFAULT_TIME);
            this.Invoke((MethodInvoker)(() => setMessage("Procesando Procesando...")));
            Thread.Sleep(DEFAULT_TIME);
            this.Invoke((MethodInvoker)(() => setMessage("...")));
            Thread.Sleep(DEFAULT_TIME);
            this.Invoke((MethodInvoker)(() => setMessage("..::Procesando::..")));
            Thread.Sleep(DEFAULT_TIME);
        }
        public void setMessage(string msg)
        {
            //label2.Text = msg;
        }
    }
 }
