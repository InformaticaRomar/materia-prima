using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Configuration;
using System.Data.SqlClient;
using MaterialSkin;
using MaterialSkin.Controls;

namespace materia_prima
{
    public partial class Form1 : MaterialForm
    {

        private readonly MaterialSkinManager materialSkinManager;
        public Form1()
        {
            InitializeComponent();
            //dataGridView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            //dataGridView1.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
            // MaterialSkin.Controls.MaterialSingleLineTextField
            //  MaterialSkin.Controls.MaterialListView

            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conexion_q.ConnectionString = config.ConnectionStrings.ConnectionStrings["Cnn_Quality"].ConnectionString;
            terminal = config.AppSettings.Settings["ID"].Value;
            En_pantalla = new datos_materia_prima(terminal, conexion_q.ConnectionString);
            //DataTable datos= En_pantalla.Get_Datos();
            //rellenaListview(En_pantalla.Get_Datos());
            dataGridView1.DataSource = En_pantalla.Get_Datos();
            //dataGridView1=
        }

        private void rellenaListview(DataTable dt) {
            int fc = dt.Columns.Count;

         /*   dataGridView1.Columns.Clear();
            dataGridView1.Items.Clear();
            foreach (DataColumn column in dt.Columns)
            {
                dataGridView1.Columns.Add(column.ColumnName);
            }
            foreach (DataRow row in dt.Rows)
            {
                string[] subitems = new string[fc];

                object[] o = row.ItemArray;
                for (int i = 0; i < fc; i++)
                {
                    subitems[i] = o[i].ToString();
                }
                ListViewItem item = new ListViewItem(subitems);
                dataGridView1.Items.Add(item);
            }
            // dataGridView1.View = View.List;
            // autoResizeColumns(dataGridView1);
            ResizeListViewColumns(dataGridView1);*/
        }
        datos_materia_prima En_pantalla { get; set; }
        Configuration config { get; set; }
        private SqlConnectionStringBuilder conexion_q = new SqlConnectionStringBuilder();
        private string terminal { get; set; }
        private void Actualiza() { dataGridView1.DataSource = En_pantalla.Get_Datos(); }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Length > 10) { 
                Form_Leido popup = new Form_Leido(textBox1.Text, conexion_q.ConnectionString,terminal, materialSkinManager);
                
                if (popup.ShowDialog() == DialogResult.OK) {
                    DataTable a = popup.Get_datos_matricula();
                    foreach (DataRow row in a.Rows) {
                        if (row[11].ToString() == "False")
                        {
                            label3.Text = "No Fifo";
                        }
                        else { label3.Text = "Fifo Ok"; }
                    }
                    En_pantalla.Insertar(a);
                    Actualiza();
                    //rellenaListview(En_pantalla.Get_Datos());
                }
                    //EjecutarFuncion();
                }
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            //Opciones_form opiones = new Opciones_form();
           // opiones.ShowDialog();
          
          // SqlConnectionStringBuilder conexion_q = new SqlConnectionStringBuilder();
            conexion_q.ConnectionString=config.ConnectionStrings.ConnectionStrings["Cnn_Quality"].ConnectionString;
            terminal = config.AppSettings.Settings["ID"].Value;
            Opciones_form opciones = new Opciones_form(conexion_q, terminal);
            if (opciones.ShowDialog() == DialogResult.OK)
            {
                config.ConnectionStrings.ConnectionStrings["Cnn_Quality"].ConnectionString= opciones.connection_string;
                conexion_q.ConnectionString= opciones.connection_string;
                config.AppSettings.Settings["ID"].Value = opciones._id;
                terminal= opciones._id;
                config.Save(ConfigurationSaveMode.Modified);
            }

        }
        private Dictionary<string, int[]> listViewHeaderWidths = new Dictionary<string, int[]>();
        private void ResizeListViewColumns(MaterialListView lv)
        {
            int[] headerWidths = listViewHeaderWidths.ContainsKey(lv.Name) ? listViewHeaderWidths[lv.Name] : null;

            lv.BeginUpdate();

            if (headerWidths == null)
            {
                lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.HeaderSize);

                headerWidths = new int[lv.Columns.Count];

                for (int i = 0; i < lv.Columns.Count; i++)
                {
                    headerWidths[i] = lv.Columns[i].Width;
                }

                listViewHeaderWidths.Add(lv.Name, headerWidths);
            }

            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);

            for (int j = 0; j < lv.Columns.Count; j++)
            {
                lv.Columns[j].Width = Math.Max(lv.Columns[j].Width, headerWidths[j]);
            }

            lv.EndUpdate();
        }
        public static void autoResizeColumns(MaterialListView lv)
        {
            lv.AutoResizeColumns(ColumnHeaderAutoResizeStyle.ColumnContent);
            MaterialListView.ColumnHeaderCollection cc = lv.Columns;
            for (int i = 0; i < cc.Count; i++)
            {
                int colWidth = TextRenderer.MeasureText(cc[i].Text, lv.Font).Width + 5;
                if (colWidth > cc[i].Width)
                {
                    cc[i].Width = colWidth;
                }
            }
            lv.View = View.Details;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button2_Click(object sender, EventArgs e)
        {
            // Traspaso_Almacen tras = new Traspaso_Almacen(conexion_q.ConnectionString);
            if (En_pantalla.Validar()) {
                MessageBox.Show("Datos Guardados Correctamente", "Atencion");
                Actualiza();
            }
            /*string[] ssccs = { "684370000003511884", "684370000003315642" };
           bool result =tras.TraspasoEntreAlmacenes_SSCC(14, ssccs, false);*/
            int a=0;
           //tras.Intern_TraspasaStockPartidas(2016,1,"D", 152774,0, 366, 6, 100,100);
        }
    }
}
