using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utiles;
using System.Data;

namespace materia_prima
{
    class materia_prima
    {


        public string articulo { get; set; }
        private string descripcion { get; set; }
        private DateTime fecha { get; set; }
        private string sscc { get; set; }
        private string loteq { get; set; }
        private string loteprov { get; set; }
        private DateTime fecha_rec { get; set; }
        private DateTime fecha_cad { get; set; }
        private bool fifo { get; set; }
        public string IDSSCC { get; set; }
        private string estado { get; set; }
        private DataTable datos_matricula { get; set; }
        private string cantidad { get; set; }
        private string unidad_medida { get; set; }
        private DataTable datos_stock { get; set; }
        private string _terminal { get; set; }
        private Quality con { get; set; }
        private bool exist { get; set; }
        public bool existe()
        {
           
            return exist;
        }
        public materia_prima(string matricula, string conex, string Terminal)
        {
            _terminal = Terminal;
            con = new Quality(conex);
            exist = false;
            string sql = @"SELECT        [STOCK PARTIDAS].Artículo as Articulo, ARTICULO.Descripción as Descripcion, SSCC.SSCC, CONVERT(varchar(14),SUBSTRING(CAST([STOCK PARTIDAS].[Año] AS VARCHAR),3,2)+'/'+ RIGHT('00' + CONVERT(varchar(3),[STOCK PARTIDAS].[Empresa]),2)+'/'+[STOCK PARTIDAS].[Serie]+'/'+ right('0000000' + CONVERT(varchar(8),[STOCK PARTIDAS].[Nº Partida]),6)) as 'LoteQ',RECEPCION_LIN.CampoAuxText4 AS [lote proveedor], CASE WHEN ARTICULO.[Control exist] = 'U' THEN [Unidades Actuales] ELSE round([Kg Actuales], 3) END AS Cantidad, 
                         CASE WHEN ARTICULO.[Control exist] = 'U' THEN 'Unidades' ELSE 'Mt/Lt/Kg/Uds' END AS Ud_Medida, 
                         convert(varchar,[STOCK PARTIDAS].[Fecha Creación], 103) AS[Fecha Recepcion], convert(varchar,[STOCK PARTIDAS].FechaCaducidad, 103) AS[Fecha Caducidad], Estado.Estado,IDSSCC

FROM            [STOCK PARTIDAS] INNER JOIN
                         SSCC_CON ON [STOCK PARTIDAS].IDSSCC = SSCC_CON.IdLote INNER JOIN
                         SSCC ON SSCC_CON.IdPadre = SSCC.Id INNER JOIN
                         ARTICULO ON [STOCK PARTIDAS].Artículo = ARTICULO.Artículo INNER JOIN
                         RECEPCION_LIN ON [STOCK PARTIDAS].Año = RECEPCION_LIN.Año AND [STOCK PARTIDAS].Empresa = RECEPCION_LIN.Empresa AND [STOCK PARTIDAS].Serie = RECEPCION_LIN.Serie AND 
                         [STOCK PARTIDAS].[Nº Partida] = RECEPCION_LIN.[Nº Albarán] AND [STOCK PARTIDAS].[Nº linea Partida] = RECEPCION_LIN.[Nº linea Albarán] INNER JOIN
                         Estado ON SSCC_CON.Estado = Estado.ID_Estado
where SSCC.SSCC='" + matricula + "'";
            datos_matricula = con.Sql_Datatable(sql);
            if (datos_matricula.Rows.Count > 0) { 
            foreach (DataRow row in datos_matricula.Rows)
            {
                articulo = row[0].ToString();
                descripcion = row[1].ToString();
                fecha = DateTime.Now;
                sscc = row[2].ToString();
                loteq = row[3].ToString();
                loteprov = row[4].ToString();
                cantidad = row[5].ToString();
                unidad_medida = row[6].ToString();
                fecha_rec = DateTime.ParseExact(row[7].ToString(), "dd/MM/yyyy", null);
                fecha_cad = DateTime.ParseExact(row[8].ToString(), "dd/MM/yyyy", null);
                estado = row[9].ToString();
                IDSSCC = row[10].ToString();

            }
            fifo = _fifo();
            if (datos_matricula.Rows.Count > 0)
            {
                exist = true;
            }
            }
            fifo = false;
        }

        public bool tiene_stock()
        {
            bool result = true;
            double cant = -1;
            double.TryParse(cantidad, out cant);
            if (cant <= 0)
            {
                result = false;
            }
            return result;
        }

        private bool _fifo()
        {
            bool result = true;
            string sql = @"SELECT      [STOCK PARTIDAS].Artículo as Articulo, ARTICULO.Descripción as Descripcion, SSCC.SSCC, CONVERT(varchar(14),SUBSTRING(CAST([STOCK PARTIDAS].[Año] AS VARCHAR),3,2)+'/'+ RIGHT('00' + CONVERT(varchar(3),[STOCK PARTIDAS].[Empresa]),2)+'/'+[STOCK PARTIDAS].[Serie]+'/'+ right('0000000' + CONVERT(varchar(8),[STOCK PARTIDAS].[Nº Partida]),6)) as 'Lote Q',RECEPCION_LIN.CampoAuxText4 AS [lote proveedor], CASE WHEN ARTICULO.[Control exist] = 'U' THEN [Unidades Actuales] ELSE round([Kg Actuales], 3) END AS Cantidad, 
                         CASE WHEN ARTICULO.[Control exist] = 'U' THEN 'Unidades' ELSE 'Mt/Lt/Kg/Uds' END AS 'Ud Medida', 
                         convert(varchar,[STOCK PARTIDAS].[Fecha Creación], 103) AS[Fecha Recepcion], convert(varchar,[STOCK PARTIDAS].FechaCaducidad, 103) AS[Fecha Caducidad], Estado.Estado
						
FROM[STOCK PARTIDAS] INNER JOIN
                         SSCC_CON ON[STOCK PARTIDAS].IDSSCC = SSCC_CON.IdLote INNER JOIN
                         SSCC ON SSCC_CON.IdPadre = SSCC.Id INNER JOIN
                         ARTICULO ON[STOCK PARTIDAS].Artículo = ARTICULO.Artículo INNER JOIN
                         RECEPCION_LIN ON[STOCK PARTIDAS].Año = RECEPCION_LIN.Año AND[STOCK PARTIDAS].Empresa = RECEPCION_LIN.Empresa AND[STOCK PARTIDAS].Serie = RECEPCION_LIN.Serie AND
                      [STOCK PARTIDAS].[Nº Partida] = RECEPCION_LIN.[Nº Albarán] AND[STOCK PARTIDAS].[Nº linea Partida] = RECEPCION_LIN.[Nº linea Albarán] INNER JOIN
                         Estado ON SSCC_CON.Estado = Estado.ID_Estado
WHERE[STOCK PARTIDAS].Artículo = " + articulo + @" and[STOCK PARTIDAS].FechaCaducidad > GETDATE() and((round([STOCK PARTIDAS].[Kg Actuales], 3) > 0) or([STOCK PARTIDAS].[Unidades Actuales] > 0)) and ([STOCK PARTIDAS].IDSSCC <> " + IDSSCC + @") 
and [STOCK PARTIDAS].IDSSCC  not in (select IDSSCC FROM DATOS_MATERIAS_PRIMAS where VALIDADO=0 and TERMINAL=" + _terminal + @")
order by[STOCK PARTIDAS].FechaCaducidad, [STOCK PARTIDAS].[Fecha Creación]
        desc ";

            datos_stock = con.Sql_Datatable(sql);
            if (datos_stock != null && datos_stock.Rows.Count > 0)
                foreach (DataRow row in datos_stock.Rows)
            {
                DateTime fecha_rec2 = DateTime.ParseExact(row[7].ToString(), "dd/MM/yyyy", null);
                DateTime fecha_cad2 = DateTime.ParseExact(row[8].ToString(), "dd/MM/yyyy", null);
                if (fecha_cad > fecha_cad2)
                {
                    result = false;
                    break;
                }
                else if (fecha_cad == fecha_cad2 & fecha_rec >= fecha_rec2)
                {
                    result = false;
                    break;
                }
                }
            else { result = false; }
            return result;
        }
        public bool ya_existe()
        {
            bool existe = true;
            int exist = -1;
            string sql = @"SELECT  count(*)  FROM[QC600].[dbo].[DATOS_MATERIAS_PRIMAS] where VALIDADO = 0 and SSCC = '" + sscc + @"'";
            int.TryParse( con.sql_string(sql), out exist);
            if (exist == 0) { existe = false; }
            return existe;
        }
    
        public bool es_fifo()
        {
            return fifo;
        }
        public DataTable Get_datos_matricula() {
            DataTable datos = new DataTable();
            datos.Columns.Add("Articulo", typeof(string));
            datos.Columns.Add("Descripcion", typeof(string));
            datos.Columns.Add("Fecha", typeof(DateTime));
            datos.Columns.Add("sscc", typeof(string));
            datos.Columns.Add("loteq", typeof(string));
            datos.Columns.Add("loteprov", typeof(string));
            datos.Columns.Add("Cantidad", typeof(string));
            datos.Columns.Add("Ud Medida", typeof(string));
            datos.Columns.Add("F. Recep", typeof(DateTime));
            datos.Columns.Add("F. Caducidad", typeof(DateTime));
            datos.Columns.Add("Estado", typeof(string));
            datos.Columns.Add("Fifo", typeof(bool));
            datos.Rows.Add(articulo, descripcion, fecha, sscc, loteq, loteprov,cantidad,unidad_medida, fecha_rec,fecha_cad, estado, fifo);

           

            return datos;
        }
        public DataTable Get_datos_stock()
        {
            return datos_stock;
        }

    }

    class datos_materia_prima
    {
        private DataTable _datos { get; set; }
        private string _conex { get; set; }
        private string _terminal { get; set; }
        private Quality con { get; set; }

        public datos_materia_prima( string terminal, string conex) {

            _conex = conex;
             _terminal= terminal;
            con = new Quality(_conex);
            string sql = @"SELECT  ID, ARTICULO, DESCRIPCION, FECHA, SSCC, LOTEQ, LOTEPROV, CANTIDAD, UD_MEDIDA, F_RECEP, F_CAD, ESTADO FROM DATOS_MATERIAS_PRIMAS where VALIDADO=0 and TERMINAL="+_terminal;
            _datos= con.Sql_Datatable(sql);

        }
        private void Actualiza() {
           
            string sql = @"SELECT   ARTICULO as Articulo , DESCRIPCION as Descripcion , FECHA as Fecha, SSCC as SSCC, LOTEQ as LoteQ , LOTEPROV as LoteProv, CANTIDAD as Cantidad, UD_MEDIDA as [Ud Medida], F_RECEP as [Fecha Recepcion], F_CAD as [Fecha Caducidad] , ESTADO as Estado, FIFO as Fifo FROM DATOS_MATERIAS_PRIMAS where VALIDADO=0 and TERMINAL=" + _terminal;
            _datos = con.Sql_Datatable(sql);
        }
        public bool Insertar( DataTable datos_materia)
        {
           
            string sql = "";
            bool resultado = false;
            foreach (DataRow row in datos_materia.Rows)
            {
               sql = @"select idlote from SSCC_CON inner join   SSCC ON SSCC_CON.IdPadre = SSCC.Id where SSCC.SSCC='" + row[3].ToString() + @"'";
               string idsscc= con.sql_string(sql);
                string fifo = "Si";
               
                if (row[11].ToString() == "False")
                {
                    fifo = "No";
                }
                sql = @"select coalesce (count(*),0) from DATOS_MATERIAS_PRIMAS where SSCC='" + row[3].ToString() + @"'";
                int existe = 0;
                string exist= con.sql_string(sql);
                int.TryParse(exist, out existe);
                if (existe == 0)
                {
                    sql = @"insert DATOS_MATERIAS_PRIMAS  ( ARTICULO, DESCRIPCION, FECHA, SSCC, LOTEQ, LOTEPROV, CANTIDAD, UD_MEDIDA, F_RECEP, F_CAD, ESTADO,FIFO, IDSSCC, TERMINAL, VALIDADO) 
values (" + row[0].ToString() + @",'" + row[1].ToString() + @"',getdate(),'" + row[3].ToString() + @"','" + row[4].ToString() + @"','" + row[5].ToString() + @"'," + row[6].ToString().Replace(',','.') + @",'" + row[7].ToString() + @"', convert(datetime,'" + row[8].ToString() + @"',103)" + @", convert(datetime,'" + row[9].ToString() + @"',103),'" + row[10].ToString().Replace(" ", "") + @"','" + fifo + @"'," + idsscc + @"," + _terminal + @",0)";
                    con.sql_update(sql);
                    resultado = true;
                }
                else
                {
                    resultado = false;
                }
            }
            return resultado;
        }
        static void OpenMicrosoftExcel(string f)
        {
            System.Diagnostics.ProcessStartInfo startInfo = new System.Diagnostics.ProcessStartInfo();
            startInfo.FileName = "EXCEL.EXE";
            startInfo.Arguments = "\"" + f + "\"";
            System.Diagnostics.Process.Start(startInfo);
        }
       
        public bool Validar_back() {
            Export excel = new Export();
            string nombre = "Materias_Primas_"+DateTime.Now.ToString().Replace(@"/","").Replace(@":","").Replace(@" ", "") + ".xlsx";
            string path = @"c:\temp\" + nombre;
            excel.ToExcelfile(path, _datos);

            OpenMicrosoftExcel(path);
            bool result = false;
            List<string> ssccs = new List<string>();
            foreach (DataRow row in _datos.AsEnumerable())
            {
                ssccs.Add(row["SSCC"].ToString());

            }
            Traspaso_Almacen tras = new Traspaso_Almacen(_conex);
             result = tras.TraspasoEntreAlmacenes_SSCC(14, ssccs, false);
            string sql = @"UPDATE DATOS_MATERIAS_PRIMAS SET VALIDADO=1 WHERE TERMINAL="+_terminal;
            con.sql_update(sql);
            OpenMicrosoftExcel(path);
            return result;
        }
        public bool Validar()
        {
          
            bool result = false;
            List<string> ssccs = new List<string>();
            foreach (DataRow row in _datos.AsEnumerable())
            {
                ssccs.Add(row["SSCC"].ToString());

            }
            Traspaso_Almacen tras = new Traspaso_Almacen(_conex);
            result = tras.TraspasoEntreAlmacenes_SSCC(14, ssccs, false);
            string sql = @"UPDATE DATOS_MATERIAS_PRIMAS SET VALIDADO=1 WHERE TERMINAL=" + _terminal;
            con.sql_update(sql);
           
            return result;
        }
        public bool Borrar(string sscc)
        {
           
            string sql = @"delete from DATOS_MATERIAS_PRIMAS where sscc='" + sscc + "'";
            
            return con.sql_update(sql); 
        }
        public DataTable Get_Datos()
        {
            Actualiza();
            return _datos;
        }
    }
}