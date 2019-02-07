namespace materia_prima
{
    partial class Form5
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
            this.components = new System.ComponentModel.Container();
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource1 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.MATERIA_PRIMA_TERMINALBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.QC600DataSet1 = new QC600DataSet1();
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.MATERIA_PRIMA_TERMINALTableAdapter = new QC600DataSet1TableAdapters.MATERIA_PRIMA_TERMINALTableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.MATERIA_PRIMA_TERMINALBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QC600DataSet1)).BeginInit();
            this.SuspendLayout();
            // 
            // MATERIA_PRIMA_TERMINALBindingSource
            // 
            this.MATERIA_PRIMA_TERMINALBindingSource.DataMember = "MATERIA_PRIMA_TERMINAL";
            this.MATERIA_PRIMA_TERMINALBindingSource.DataSource = this.QC600DataSet1;
            // 
            // QC600DataSet1
            // 
            this.QC600DataSet1.DataSetName = "QC600DataSet1";
            this.QC600DataSet1.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.MATERIA_PRIMA_TERMINALBindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "materia_prima.Datos_Traspasados_formulacion.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(834, 423);
            this.reportViewer1.TabIndex = 0;
            // 
            // MATERIA_PRIMA_TERMINALTableAdapter
            // 
            this.MATERIA_PRIMA_TERMINALTableAdapter.ClearBeforeFill = true;
            // 
            // Form5
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 423);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Name = "Form5";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Form5_FormClosed);
            this.Load += new System.EventHandler(this.Form5_Load);
            ((System.ComponentModel.ISupportInitialize)(this.MATERIA_PRIMA_TERMINALBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QC600DataSet1)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource MATERIA_PRIMA_TERMINALBindingSource;
        private QC600DataSet1 QC600DataSet1;
        private QC600DataSet1TableAdapters.MATERIA_PRIMA_TERMINALTableAdapter MATERIA_PRIMA_TERMINALTableAdapter;
    }
}