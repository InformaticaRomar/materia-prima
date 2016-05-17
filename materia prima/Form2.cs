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

namespace materia_prima
{
    public partial class Form_Leido : MaterialForm
    {
        private string sscc { get; set; }
        private materia_prima palet { get; set; }
        private DataTable datos_ { get; set; }
        public Form_Leido(string matricula, string conex, string Terminal, MaterialSkinManager materialSkinManager)
        {
            InitializeComponent();
            //materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);

            palet = new materia_prima(matricula, conex ,  Terminal);
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
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void Rechazar_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
