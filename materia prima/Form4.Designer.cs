using MaterialSkin;
using MaterialSkin.Controls;
namespace materia_prima
{
    partial class Report
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
            Microsoft.Reporting.WinForms.ReportDataSource reportDataSource2 = new Microsoft.Reporting.WinForms.ReportDataSource();
            this.FIFO_MATERIA_PRIMA_STOCKBindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.QC600DataSet = new QC600DataSet();
            this.FIFO_MATERIA_PRIMA_MATRICULABindingSource = new System.Windows.Forms.BindingSource(this.components);
            this.reportViewer1 = new Microsoft.Reporting.WinForms.ReportViewer();
            this.FIFO_MATERIA_PRIMA_STOCKTableAdapter = new QC600DataSetTableAdapters.FIFO_MATERIA_PRIMA_STOCKTableAdapter();
            this.FIFO_MATERIA_PRIMA_MATRICULATableAdapter = new QC600DataSetTableAdapters.FIFO_MATERIA_PRIMA_MATRICULATableAdapter();
            ((System.ComponentModel.ISupportInitialize)(this.FIFO_MATERIA_PRIMA_STOCKBindingSource)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.QC600DataSet)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.FIFO_MATERIA_PRIMA_MATRICULABindingSource)).BeginInit();
            this.SuspendLayout();
            // 
            // FIFO_MATERIA_PRIMA_STOCKBindingSource
            // 
            this.FIFO_MATERIA_PRIMA_STOCKBindingSource.DataMember = "FIFO_MATERIA_PRIMA_STOCK";
            this.FIFO_MATERIA_PRIMA_STOCKBindingSource.DataSource = this.QC600DataSet;
            // 
            // QC600DataSet
            // 
            this.QC600DataSet.DataSetName = "QC600DataSet";
            this.QC600DataSet.SchemaSerializationMode = System.Data.SchemaSerializationMode.IncludeSchema;
            // 
            // FIFO_MATERIA_PRIMA_MATRICULABindingSource
            // 
            this.FIFO_MATERIA_PRIMA_MATRICULABindingSource.DataMember = "FIFO_MATERIA_PRIMA_MATRICULA";
            this.FIFO_MATERIA_PRIMA_MATRICULABindingSource.DataSource = this.QC600DataSet;
            // 
            // reportViewer1
            // 
            this.reportViewer1.Dock = System.Windows.Forms.DockStyle.Fill;
            reportDataSource1.Name = "DataSet1";
            reportDataSource1.Value = this.FIFO_MATERIA_PRIMA_STOCKBindingSource;
            reportDataSource2.Name = "DataSet2";
            reportDataSource2.Value = this.FIFO_MATERIA_PRIMA_MATRICULABindingSource;
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource1);
            this.reportViewer1.LocalReport.DataSources.Add(reportDataSource2);
            this.reportViewer1.LocalReport.ReportEmbeddedResource = "materia_prima.fifo_materia_prima.rdlc";
            this.reportViewer1.Location = new System.Drawing.Point(0, 0);
            this.reportViewer1.Name = "reportViewer1";
            this.reportViewer1.Size = new System.Drawing.Size(834, 423);
            this.reportViewer1.TabIndex = 0;
            // 
            // FIFO_MATERIA_PRIMA_STOCKTableAdapter
            // 
            this.FIFO_MATERIA_PRIMA_STOCKTableAdapter.ClearBeforeFill = true;
            // 
            // FIFO_MATERIA_PRIMA_MATRICULATableAdapter
            // 
            this.FIFO_MATERIA_PRIMA_MATRICULATableAdapter.ClearBeforeFill = true;
            // 
            // Report
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(834, 423);
            this.Controls.Add(this.reportViewer1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedToolWindow;
            this.Name = "Report";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.Text = "Report";
            this.FormClosed += new System.Windows.Forms.FormClosedEventHandler(this.Report_FormClosed);
            this.Load += new System.EventHandler(this.Form4_Load);
            ((System.ComponentModel.ISupportInitialize)(this.FIFO_MATERIA_PRIMA_STOCKBindingSource)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.QC600DataSet)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.FIFO_MATERIA_PRIMA_MATRICULABindingSource)).EndInit();
            this.ResumeLayout(false);

        }

        private Microsoft.Reporting.WinForms.ReportViewer reportViewer1;
        private System.Windows.Forms.BindingSource FIFO_MATERIA_PRIMA_STOCKBindingSource;
        private QC600DataSet QC600DataSet;
        private System.Windows.Forms.BindingSource FIFO_MATERIA_PRIMA_MATRICULABindingSource;
        private QC600DataSetTableAdapters.FIFO_MATERIA_PRIMA_STOCKTableAdapter FIFO_MATERIA_PRIMA_STOCKTableAdapter;
        private QC600DataSetTableAdapters.FIFO_MATERIA_PRIMA_MATRICULATableAdapter FIFO_MATERIA_PRIMA_MATRICULATableAdapter;

        #endregion

        //  private materia_prima.QC600DataSet QC600DataSet;
        //private materia_prima.QC600DataSetTableAdapters.FIFO_MATERIA_PRIMA_STOCKTableAdapter FIFO_MATERIA_PRIMA_STOCKTableAdapter;
    }
}