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

namespace materia_prima
{
    public partial class Form_Leido : MaterialForm
    {
        private string sscc { get; set; }
        private string _terminal { get; set; }
        private materia_prima palet { get; set; }
        private DataTable datos_ { get; set; }
        private MaterialForm myForm = new MaterialForm();
        public MaterialForm getForm
        {
            get { return myForm; }

            set { myForm = value; }
        }
        private readonly MaterialSkinManager materialSkinManager;
        public Form_Leido(string matricula, string conex, string Terminal)
        {
            InitializeComponent();

            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
          
           // materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Red600, Primary.Red700, Primary.Red200, Accent.Green100, TextShade.WHITE);
            materialSkinManager.AddFormToManage(this);

            palet = new materia_prima(matricula, conex ,  Terminal);
            _terminal = Terminal;
            sscc = matricula;

            if (palet.es_fifo())
            {
                label1.Text = @"SSCC Leída: " + matricula + " cumple criterio Fifo";
            }
            else {
                label1.Text = @"SSCC Leída: " + matricula + " no cumple criterio Fifo";
            }
            datos_= palet.Get_datos_matricula();
            dataGridView1.DataSource = datos_;
            dataGridView2.DataSource = palet.Get_datos_stock();

        }
        public DataTable Get_datos_matricula()
        {
            
            return datos_;
        }
        internal void LoadSSCC(String sscc)
        {
            //ordersTableAdapter.FillByCustomerID(northwindDataSet.Orders, CustomerID);
        }

        private void Aceptar_Click(object sender, EventArgs e)
        {
            string mensaje = "¿Estas seguro que deseas aceptar el soporte no FIFO?";
            DialogResult boton = MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning,MessageBoxDefaultButton.Button2);
            if (boton == DialogResult.OK)
            {
                this.DialogResult = DialogResult.OK;
                this.Close();
              
            }
        }

        private void Rechazar_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        public void splash()
        {
            Application.Run(new SplashScreen());
        }
        
        
        private void Imprimir_Click(object sender, EventArgs e)
        {
            int art = 0;
            int idscc = 0;
            int terminal = 0;
            int.TryParse(palet.articulo, out art);
            int.TryParse(palet.IDSSCC, out idscc);
            int.TryParse(_terminal, out terminal);
            if (art > 0 && idscc>0 && terminal > 0) {

                //SplashScreen splash2 = new SplashScreen();
                /*Thread t = new Thread(new ThreadStart(splash));
                t.Start();*/
                  Report reporte = new Report(art, sscc, idscc, terminal);
                /*  Thread t = new Thread(new ThreadStart(splash));
                  reporte.t = t;
                  reporte.getForm = this;
                 // t.Start();*/
                reporte.getForm = this;
                //Application.Run(splas);
                this.Hide();
                if (DialogResult.OK== reporte.ShowDialog())
                {
                    reporte.Dispose();
                    reporte = null;
                }

                // splas.ShowDialog();


            }
        }

        private void Form_Leido_FormClosed(object sender, FormClosedEventArgs e)
        {
            myForm.Visible = true;
        }
    }
}
