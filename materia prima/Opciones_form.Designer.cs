namespace materia_prima
{
    partial class Opciones_form
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.cbServer = new System.Windows.Forms.ComboBox();
            this.cbDataBase = new System.Windows.Forms.ComboBox();
            this.txtPassword = new System.Windows.Forms.TextBox();
            this.LbBase = new System.Windows.Forms.Label();
            this.lbClave = new System.Windows.Forms.Label();
            this.btnOK = new System.Windows.Forms.Button();
            this.lbUsuario = new System.Windows.Forms.Label();
            this.btnCancel = new System.Windows.Forms.Button();
            this.txtUser = new System.Windows.Forms.TextBox();
            this.GroupBox1 = new System.Windows.Forms.GroupBox();
            this.btnRefresh = new System.Windows.Forms.Button();
            this.GroupBox2 = new System.Windows.Forms.GroupBox();
            this.rbAuthenticationSql = new System.Windows.Forms.RadioButton();
            this.rbAuthenticationWin = new System.Windows.Forms.RadioButton();
            this.lbServidor = new System.Windows.Forms.Label();
            this.btnTest = new System.Windows.Forms.Button();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.ID_Text = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.GroupBox1.SuspendLayout();
            this.GroupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // cbServer
            // 
            this.cbServer.FormattingEnabled = true;
            this.cbServer.Location = new System.Drawing.Point(12, 25);
            this.cbServer.Name = "cbServer";
            this.cbServer.Size = new System.Drawing.Size(250, 21);
            this.cbServer.TabIndex = 8;
            // 
            // cbDataBase
            // 
            this.cbDataBase.FormattingEnabled = true;
            this.cbDataBase.Location = new System.Drawing.Point(49, 43);
            this.cbDataBase.Name = "cbDataBase";
            this.cbDataBase.Size = new System.Drawing.Size(235, 21);
            this.cbDataBase.TabIndex = 1;
            // 
            // txtPassword
            // 
            this.txtPassword.Location = new System.Drawing.Point(118, 94);
            this.txtPassword.Name = "txtPassword";
            this.txtPassword.PasswordChar = '*';
            this.txtPassword.Size = new System.Drawing.Size(189, 20);
            this.txtPassword.TabIndex = 5;
            // 
            // LbBase
            // 
            this.LbBase.AutoSize = true;
            this.LbBase.Location = new System.Drawing.Point(27, 27);
            this.LbBase.Name = "LbBase";
            this.LbBase.Size = new System.Drawing.Size(257, 13);
            this.LbBase.TabIndex = 0;
            this.LbBase.Text = "Selecciona o introduce un nombre de Base de Datos";
            // 
            // lbClave
            // 
            this.lbClave.AutoSize = true;
            this.lbClave.Location = new System.Drawing.Point(59, 97);
            this.lbClave.Name = "lbClave";
            this.lbClave.Size = new System.Drawing.Size(53, 13);
            this.lbClave.TabIndex = 4;
            this.lbClave.Text = "Password";
            // 
            // btnOK
            // 
            this.btnOK.Location = new System.Drawing.Point(12, 359);
            this.btnOK.Name = "btnOK";
            this.btnOK.Size = new System.Drawing.Size(75, 23);
            this.btnOK.TabIndex = 13;
            this.btnOK.Text = "OK";
            this.btnOK.UseVisualStyleBackColor = true;
            this.btnOK.Click += new System.EventHandler(this.btnOK_Click);
            // 
            // lbUsuario
            // 
            this.lbUsuario.AutoSize = true;
            this.lbUsuario.Location = new System.Drawing.Point(59, 70);
            this.lbUsuario.Name = "lbUsuario";
            this.lbUsuario.Size = new System.Drawing.Size(43, 13);
            this.lbUsuario.TabIndex = 3;
            this.lbUsuario.Text = "Usuario";
            // 
            // btnCancel
            // 
            this.btnCancel.Location = new System.Drawing.Point(93, 359);
            this.btnCancel.Name = "btnCancel";
            this.btnCancel.Size = new System.Drawing.Size(75, 23);
            this.btnCancel.TabIndex = 14;
            this.btnCancel.Text = "Cancel";
            this.btnCancel.UseVisualStyleBackColor = true;
            this.btnCancel.Click += new System.EventHandler(this.btnCancel_Click);
            // 
            // txtUser
            // 
            this.txtUser.Location = new System.Drawing.Point(118, 67);
            this.txtUser.Name = "txtUser";
            this.txtUser.Size = new System.Drawing.Size(189, 20);
            this.txtUser.TabIndex = 2;
            // 
            // GroupBox1
            // 
            this.GroupBox1.Controls.Add(this.cbDataBase);
            this.GroupBox1.Controls.Add(this.LbBase);
            this.GroupBox1.Location = new System.Drawing.Point(12, 182);
            this.GroupBox1.Name = "GroupBox1";
            this.GroupBox1.Size = new System.Drawing.Size(331, 90);
            this.GroupBox1.TabIndex = 12;
            this.GroupBox1.TabStop = false;
            this.GroupBox1.Text = "Conectar a Base de Datos";
            // 
            // btnRefresh
            // 
            this.btnRefresh.Location = new System.Drawing.Point(268, 23);
            this.btnRefresh.Name = "btnRefresh";
            this.btnRefresh.Size = new System.Drawing.Size(75, 23);
            this.btnRefresh.TabIndex = 11;
            this.btnRefresh.Text = "Refrescar";
            this.btnRefresh.UseVisualStyleBackColor = true;
            // 
            // GroupBox2
            // 
            this.GroupBox2.Controls.Add(this.txtPassword);
            this.GroupBox2.Controls.Add(this.lbClave);
            this.GroupBox2.Controls.Add(this.lbUsuario);
            this.GroupBox2.Controls.Add(this.txtUser);
            this.GroupBox2.Controls.Add(this.rbAuthenticationSql);
            this.GroupBox2.Controls.Add(this.rbAuthenticationWin);
            this.GroupBox2.Location = new System.Drawing.Point(12, 52);
            this.GroupBox2.Name = "GroupBox2";
            this.GroupBox2.Size = new System.Drawing.Size(331, 124);
            this.GroupBox2.TabIndex = 10;
            this.GroupBox2.TabStop = false;
            this.GroupBox2.Text = "Log on en  server";
            // 
            // rbAuthenticationSql
            // 
            this.rbAuthenticationSql.AutoSize = true;
            this.rbAuthenticationSql.Location = new System.Drawing.Point(7, 44);
            this.rbAuthenticationSql.Name = "rbAuthenticationSql";
            this.rbAuthenticationSql.Size = new System.Drawing.Size(172, 17);
            this.rbAuthenticationSql.TabIndex = 1;
            this.rbAuthenticationSql.Text = "Usar Autentificacion Sql Server";
            this.rbAuthenticationSql.UseVisualStyleBackColor = true;
            // 
            // rbAuthenticationWin
            // 
            this.rbAuthenticationWin.AutoSize = true;
            this.rbAuthenticationWin.Location = new System.Drawing.Point(7, 20);
            this.rbAuthenticationWin.Name = "rbAuthenticationWin";
            this.rbAuthenticationWin.Size = new System.Drawing.Size(167, 17);
            this.rbAuthenticationWin.TabIndex = 0;
            this.rbAuthenticationWin.TabStop = true;
            this.rbAuthenticationWin.Text = "Usar Autentificacion Windows";
            this.rbAuthenticationWin.UseVisualStyleBackColor = true;
            // 
            // lbServidor
            // 
            this.lbServidor.AutoSize = true;
            this.lbServidor.Location = new System.Drawing.Point(12, 9);
            this.lbServidor.Name = "lbServidor";
            this.lbServidor.Size = new System.Drawing.Size(106, 13);
            this.lbServidor.TabIndex = 9;
            this.lbServidor.Text = "Nombre del Servidor:";
            // 
            // btnTest
            // 
            this.btnTest.Location = new System.Drawing.Point(268, 359);
            this.btnTest.Name = "btnTest";
            this.btnTest.Size = new System.Drawing.Size(75, 23);
            this.btnTest.TabIndex = 15;
            this.btnTest.Text = "Test";
            this.btnTest.UseVisualStyleBackColor = true;
            this.btnTest.Click += new System.EventHandler(this.btnTest_Click);
            // 
            // groupBox3
            // 
            this.groupBox3.Controls.Add(this.ID_Text);
            this.groupBox3.Controls.Add(this.label1);
            this.groupBox3.Location = new System.Drawing.Point(12, 278);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(331, 66);
            this.groupBox3.TabIndex = 16;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Datos Puesto";
            // 
            // ID_Text
            // 
            this.ID_Text.Location = new System.Drawing.Point(123, 19);
            this.ID_Text.Name = "ID_Text";
            this.ID_Text.Size = new System.Drawing.Size(33, 20);
            this.ID_Text.TabIndex = 1;
            this.ID_Text.Text = "0";
            this.ID_Text.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(27, 27);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(90, 13);
            this.label1.TabIndex = 0;
            this.label1.Text = "Numero Terminal:";
            // 
            // Opciones_form
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(351, 393);
            this.Controls.Add(this.groupBox3);
            this.Controls.Add(this.cbServer);
            this.Controls.Add(this.btnOK);
            this.Controls.Add(this.btnCancel);
            this.Controls.Add(this.GroupBox1);
            this.Controls.Add(this.btnRefresh);
            this.Controls.Add(this.GroupBox2);
            this.Controls.Add(this.lbServidor);
            this.Controls.Add(this.btnTest);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.Name = "Opciones_form";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Sqldialog";
            this.Load += new System.EventHandler(this.SQLServerConnectionDialog_Load);
            this.GroupBox1.ResumeLayout(false);
            this.GroupBox1.PerformLayout();
            this.GroupBox2.ResumeLayout(false);
            this.GroupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.ComboBox cbServer;
        private System.Windows.Forms.ComboBox cbDataBase;
        private System.Windows.Forms.TextBox txtPassword;
        private System.Windows.Forms.Label LbBase;
        private System.Windows.Forms.Label lbClave;
        private System.Windows.Forms.Button btnOK;
        private System.Windows.Forms.Label lbUsuario;
        private System.Windows.Forms.Button btnCancel;
        private System.Windows.Forms.TextBox txtUser;
        private System.Windows.Forms.GroupBox GroupBox1;
        private System.Windows.Forms.Button btnRefresh;
        private System.Windows.Forms.GroupBox GroupBox2;
        private System.Windows.Forms.RadioButton rbAuthenticationSql;
        private System.Windows.Forms.RadioButton rbAuthenticationWin;
        private System.Windows.Forms.Label lbServidor;
        private System.Windows.Forms.Button btnTest;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.TextBox ID_Text;
        private System.Windows.Forms.Label label1;
    }
    }