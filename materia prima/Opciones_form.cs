using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Data.Sql;
using System.Configuration;

namespace materia_prima
{
    public partial class Opciones_form : Form
    {
        public Opciones_form(SqlConnectionStringBuilder con,string id)
        {
            InitializeComponent();
            conn = con;
            ID_Text.Text = id;
        }


        SqlConnectionStringBuilder conn = new SqlConnectionStringBuilder();
        public string RootPath = "";
        public string _id = "";
        public string connection_string = "";

        private void btnOK_Click(object sender, EventArgs e)
        {
            ContructConnection();
            connection_string = conn.ConnectionString;
            _id = ID_Text.Text;
            this.Close();
            this.DialogResult = DialogResult.OK;
        }

        private void SQLServerConnectionDialog_Load(object sender, EventArgs e)
        {
            cbServer.Text = conn.DataSource;
            cbDataBase.Text = conn.InitialCatalog;

            if (conn.IntegratedSecurity == false)
            {
                txtUser.Enabled = true;
                txtPassword.Enabled = true;
                rbAuthenticationWin.Checked = false;
                rbAuthenticationSql.Checked = true;
                txtUser.Text = conn.UserID;
                txtPassword.Text = conn.Password;
            }
            else
            {
                rbAuthenticationWin.Checked = true;
                //txtUser.Enabled == false;
                //  txtPassword.Enabled == false;
            }
        }
        void ContructConnection()
        {
            conn.DataSource = cbServer.Text;
            conn.IntegratedSecurity = true;
            conn.UserID = "";
            conn.Password = "";
            conn.InitialCatalog = "";

            if (rbAuthenticationSql.Checked)
            {
                conn.IntegratedSecurity = false;
                conn.UserID = txtUser.Text;
                conn.Password = txtPassword.Text;
            }
            if (cbDataBase.Text != "")
                conn.InitialCatalog = cbDataBase.Text;


        }
        private void SqlInstances()
        {
            Cursor.Current = Cursors.WaitCursor;
            try
            {
                cbServer.Items.Clear();

                DataTable sqlSources = SqlDataSourceEnumerator.Instance.GetDataSources();

                foreach (DataRow datarow in sqlSources.Rows)
                {

                    string datasource = datarow["ServerName"].ToString();
                    if (datarow["InstanceName"] != DBNull.Value)
                    {
                        datasource += String.Format("{0}", datarow["InstanceName"]);
                    }

                    cbServer.Items.Add(datasource);
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            Cursor.Current = Cursors.Default;
        }
        private void SqlDatabaseNames()
        {
            ContructConnection();
            Cursor.Current = Cursors.WaitCursor;
            String connString;
            List<string> databaseNames = new List<string>();
            connString = conn.ConnectionString;
            cbDataBase.Items.Clear();
            try
            {
                SqlConnection cn = new SqlConnection(connString);
                cn.Open();
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = cn;
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.CommandText = "sp_databases";

                SqlDataReader myReader = cmd.ExecuteReader();
                while (myReader.Read())
                {
                    cbDataBase.Items.Add(myReader.GetString(0));
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
            Cursor.Current = Cursors.Default;

        }
        private void TestDB()
        {
            ContructConnection();
            try
            {
                SqlConnection objConn = new SqlConnection(conn.ConnectionString);
                objConn.Open();
                objConn.Close();
                MessageBox.Show("Conexion realizada con exito!");
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void btnRefresh_Click(object sender, EventArgs e)
        {
            SqlInstances();
        }

        private void cbServer_DropDown(object sender, EventArgs e)
        {
            if (cbServer.Items.Count == 0)
                SqlInstances();

        }

        private void cbDataBase_DropDown(object sender, EventArgs e)
        {
            SqlDatabaseNames();
        }

        private void rbAuthenticationWin_CheckedChanged(object sender, EventArgs e)
        {
            txtUser.Enabled = false;
            txtPassword.Enabled = false;
        }

        private void rbAuthenticationSql_CheckedChanged(object sender, EventArgs e)
        {
            txtUser.Enabled = true;
            txtPassword.Enabled = true;
        }


        public string ConnectionString
        {
            get
            {
                return conn.ConnectionString;
            }
            set
            {
                conn.ConnectionString = value;
            }
        }
        private void btnTest_Click(object sender, EventArgs e)
        {
            TestDB();
        }

        private void btnCancel_Click(object sender, EventArgs e)
        {
            this.Close();
            this.DialogResult = DialogResult.Cancel;
        }
    }
}
