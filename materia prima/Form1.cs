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
           
            materialSkinManager = MaterialSkinManager.Instance;
            materialSkinManager.AddFormToManage(this);
            materialSkinManager.Theme = MaterialSkinManager.Themes.DARK;
            //materialSkinManager.ColorScheme = new ColorScheme(Primary.BlueGrey800, Primary.BlueGrey900, Primary.BlueGrey500, Accent.LightBlue200, TextShade.WHITE);
            materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);
           

            config = ConfigurationManager.OpenExeConfiguration(ConfigurationUserLevel.None);
            conexion_q.ConnectionString = config.ConnectionStrings.ConnectionStrings["Cnn_Quality"].ConnectionString;
            terminal = config.AppSettings.Settings["ID"].Value;
            En_pantalla = new datos_materia_prima(terminal, conexion_q.ConnectionString);
            
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
        private bool es_numerico(string cadena) {
            bool result = true;
            
            foreach (char a in cadena)
            {
                if (!char.IsDigit(a))
                {
                    result = false;
                    break;
                }
            }
            return result;
        }
        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (textBox1.Text.Length > 10) {
                    textBox1.Text=textBox1.Text.Replace(" ", "");
                    if (es_numerico(textBox1.Text)){ 
                        materia_prima Materia = new materia_prima(textBox1.Text, conexion_q.ConnectionString, terminal);
                        DataTable a=new DataTable();
                        if (Materia.existe() == false)
                        {
                            MessageBox.Show("La Matricula no existe", "Atencion!!");
                        }
                       else if (Materia.tiene_stock() == false)
                        {
                            MessageBox.Show("La Matricula no tiene Stock" , "Atencion!!");
                        }
                       else if (Materia.ya_existe())
                        {
                            MessageBox.Show("La Matricula ya la has añadido", "Atencion!!");
                        }
                        else if (Materia.es_fifo())
                        {
                            a = Materia.Get_datos_matricula();
                            En_pantalla.Insertar(a);
                        }
                        else
                        {
                            Form_Leido popup = new Form_Leido(textBox1.Text, conexion_q.ConnectionString, terminal);
                            popup.getForm = this;
                            this.Hide();

                            if (popup.ShowDialog() == DialogResult.OK)
                            {
                                a = popup.Get_datos_matricula();
                                foreach (DataRow row in a.Rows)
                                {
                                    if (row[11].ToString() == "False")
                                    {
                                        label3.Text = "No Fifo";
                                    }
                                    else {
                                        label3.Text = "Fifo Ok";

                                    }
                                    
                                }
                                En_pantalla.Insertar(a);
                            }
                        }
                        materialSkinManager.ColorScheme = new ColorScheme(Primary.Green600, Primary.Green700, Primary.Green200, Accent.Red100, TextShade.WHITE);

                        Actualiza();
                        //rellenaListview(En_pantalla.Get_Datos());
                    }
                }
                //EjecutarFuncion();
            }
            

        }

        private void button1_Click(object sender, EventArgs e)
        {
           
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
            int termi = 0;
            int.TryParse(terminal, out termi);
            if (dataGridView1.RowCount > 0)
            {
                if (termi > 0)
                {
                    Form5 reporte = new Form5(termi);
                    reporte.getForm = this;
                    this.Hide();
                    if (DialogResult.OK == reporte.ShowDialog())
                    {
                        En_pantalla.Validar();
                        reporte.Dispose();
                        reporte = null;
                        Actualiza();
                    }
                }

            }else { MessageBox.Show("No hay matriculas que validad.", "Atencion!!"); }

           //tras.Intern_TraspasaStockPartidas(2016,1,"D", 152774,0, 366, 6, 100,100);
        }

        private void materialFlatButton1_Click(object sender, EventArgs e)
        {
            if (dataGridView1.RowCount > 0) { 
            int fila = this.dataGridView1.SelectedCells[0].RowIndex;
            string sscc = this.dataGridView1.Rows[fila].Cells[3].Value.ToString();
            string mensaje = "Deseas Borrar, la matricula:\r\n" + sscc;
            DialogResult boton = MessageBox.Show(mensaje, "Alerta", MessageBoxButtons.OKCancel, MessageBoxIcon.Warning, MessageBoxDefaultButton.Button2);
            if (boton == DialogResult.OK)
            {
                En_pantalla.Borrar(sscc);
                Actualiza();
            }
        }
            /*  else

              MessageBox.Show(sscc);*/

        }
    }
}
