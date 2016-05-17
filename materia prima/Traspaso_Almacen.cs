using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Utiles;
using System.Data;

namespace materia_prima
{
    
    class Traspaso_Almacen
    {
        public Traspaso_Almacen(string conex) {
            con = new Quality(conex);
        }

        private Quality con { get; set; }
        private int Intern_GetNumAlbar(int iEmpresa, int iAnio) {
            bool biniTrans;
            int lNumAlbar =-2;
            string ssql;
            ssql = "SELECT [Serie X] AS Valor FROM NUMERACIONES WHERE (Empresa=" + iEmpresa.ToString()  + ") AND (Año=" + iAnio.ToString()  + ")";
            biniTrans = int.TryParse( con.sql_string(ssql), out lNumAlbar);
            ssql = "UPDATE NUMERACIONES SET [Serie X]=[Serie X]+1 WHERE(Empresa = " + iEmpresa.ToString()  + ") AND(Año = " + iAnio.ToString()  + ")";
            con.sql_update(ssql);
            return lNumAlbar+1;
        }
        private int Intern_GeneraAlbCabe_X(int iAnio, int iEmpresa, string sSerie, int iAlmOrigen, int iAlmDestino) {
            int lNumAlbar = Intern_GetNumAlbar(iEmpresa, iAnio);
            string ssql = "";
            if (lNumAlbar > 0) {
                ssql = @"INSERT INTO ALBARAN_CABE ([Año],[Empresa],[Serie],[Nº Albarán],[Código Cliente],[Almacén Origen],[Almacén Destino]) " + "VALUES (" + iAnio.ToString() + "," + iEmpresa.ToString() + ",'X'," + lNumAlbar.ToString() + ",0," + iAlmOrigen.ToString() + "," + iAlmDestino.ToString() + ")";
                con.sql_update(ssql);
    
            return lNumAlbar;
        } else { 
                    //No hay numeracion definida para el albaran
           return -1;
        }

        }
        private bool SetContenidoUL(int idlote,int IdLoteAlmacen, string sSSCCLeido) {
            string sql = @"INSERT INTO [QC600].[dbo].[SSCCV] ([IdPadre]
      ,[SSCC]
      ,[Tipo]
      ,[Origen]
      ,[Cliente]
      ,[Fecha]
      ,[Usuario]) VALUES (0,'" + sSSCCLeido + @",'','FO',0,GETDATE(),'A-F')";
            con.sql_update(sql);
            sql = "SELECT IDENT_CURRENT('[QC600].[dbo].[SSCCV]')";
            string id_str = con.sql_string(sql);
            sql = "SELECT [Unidades]  FROM [QC600].[dbo].[ALBARAN_PARTIDA] where [IDSSCC]="+ idlote.ToString();
            string id_str2 = con.sql_string(sql);
            sql = "SELECT [Kgs]  FROM [QC600].[dbo].[ALBARAN_PARTIDA] where [IDSSCC]=" + idlote.ToString();
            string id_str3 = con.sql_string(sql);
            int idpadre = 0;
            int unidades = 0;
            float kgs = 0;
            int.TryParse(id_str, out idpadre);
            int.TryParse(id_str2, out unidades);
            float.TryParse(id_str2, out kgs);
            if (idpadre > 0)
            {
                sql = @"INSERT INTO [QC600].[dbo].[SSCCV_CON] ( [IdPadre]
      ,[IdLote]
      ,[IdLoteAlmacen]
      ,[UdActual]
      ,[KgActual]
      ,[Fecha]) VALUES ("+idpadre.ToString() + ","+ idlote.ToString()+","+ IdLoteAlmacen.ToString()+","+ unidades.ToString()+","+ kgs.ToString()+",GETDATE())";
                if (con.sql_update(sql))
                    return true;

                
            }

            return false;
        }
        private bool Intern_GenAlbPart_X( int lNumAlbar, int iAnioAlb, int iEmpresaAlb , int lNumLinAlbar, string sSSCCLeido )
        {
            string ssql = "";
            ssql = @"INSERT INTO ALBARAN_PARTIDA (Año, Empresa, Serie, [Nº Albarán], [Nº linea Albarán], [Nº linea Albarán Partida], Artículo, [Tipo Lote], 
[Año Partida], [Empresa Partida], [Serie Partida], Partida, [Nº Individuo], Unidades,  
                         SSCCLeido,  Kgs) SELECT " + iAnioAlb.ToString() + "," + iEmpresaAlb.ToString() + @",'X'," + lNumAlbar.ToString() + "," + lNumLinAlbar.ToString() +  "," + lNumLinAlbar.ToString() +
                       ",[STOCK PARTIDAS].Artículo,[STOCK PARTIDAS].[Tipo Lote], [STOCK PARTIDAS].Año,[STOCK PARTIDAS].Empresa,[STOCK PARTIDAS].Serie,[STOCK PARTIDAS].[Nº Partida]," +
"[STOCK PARTIDAS].[Nº Individuo],[STOCK PARTIDAS].[Unidades Actuales],SSCC.SSCC, [STOCK PARTIDAS].[Kg Actuales]  from [STOCK PARTIDAS] inner join SSCC_CON on SSCC_CON.IdLote=[STOCK PARTIDAS].IDSSCC inner join SSCC on SSCC.Id=SSCC_CON.IdPadre" + @" where SSCC.SSCC = '" + sSSCCLeido + @"'";
            con.sql_update(ssql);
            string sql = "SELECT IDENT_CURRENT('[QC600].[dbo].[ALBARAN_PARTIDA]')";
            string id_str = con.sql_string(sql);
            int idLote = 0;
            sql = @"select [STOCK PARTIDAS].IDSSCC from [STOCK PARTIDAS] inner join SSCC_CON on SSCC_CON.IdLote=[STOCK PARTIDAS].IDSSCC inner join SSCC on SSCC.Id=SSCC_CON.IdPadre
where SSCC.SSCC = '" + sSSCCLeido + @"'";
            string id_str2 = con.sql_string(sql);
            int idlotealmacen = 0;
            int.TryParse(id_str, out idLote);
            int.TryParse(id_str2, out idlotealmacen);
            if (idLote > 0 && idlotealmacen>0)
            {
                if (SetContenidoUL(idLote, idlotealmacen, sSSCCLeido))
                {
                    return true;
                }
            }

           
                    return false;
        }
        private bool Intern_GeneraAlbLin_X(int lNumAlbar ,int iNumLinea ,int iAnioAlb, int iEmpresaAlb,int iAlmDestino,int Articulo, int UdActual, double KgActual,string sSSCCLeido)
        {
            string ssql = "";

            ssql = "INSERT INTO ALBARAN_LIN ([Año],[Empresa],[Serie],[Nº Albarán],[Nº linea Albarán],[Nº Pedido],[Artículo],[Cajas],[Almacén],[Cantidad])";
            ssql = ssql + " VALUES (";
            ssql = ssql + iAnioAlb + ",";
            ssql = ssql + iEmpresaAlb + ",";
            ssql = ssql + "'X',";
            ssql = ssql + lNumAlbar + ",";
            ssql = ssql + iNumLinea + ",";
            ssql = ssql + "0,";
            ssql = ssql + Articulo.ToString()  + ",";
            ssql = ssql + UdActual.ToString() + ",";
            ssql = ssql + iAlmDestino.ToString() + ",";
            ssql = ssql + KgActual.ToString();
            ssql = ssql + ")";
            con.sql_update(ssql);
           

            if (Intern_GenAlbPart_X(lNumAlbar, iAnioAlb, iEmpresaAlb, iNumLinea, sSSCCLeido))
            {
                return true;
            }
            return false;
        }
        private bool Actualiza_sscc_con(int idlote, int idlote_nuevo)
        {
            string sql = "SELECT [Id]  FROM [QC600].[dbo].[SSCC_CON] where [IdLote]="+ idlote.ToString();
            string dato = con.sql_string(sql);
            int idpadre = 0;
            int.TryParse(dato, out idpadre);
            if (idpadre != 0)
            {
                sql = "UPDATE [QC600].[dbo].[SSCC_CON] SET  [IdLote]="+ idlote_nuevo.ToString() + " WHERE [Id]=" + idpadre.ToString() ;
                con.sql_update(sql);
            }
            else return false;
            return true;
        }
       public long Intern_TraspasaStockPartidas(string sscc , int iAlmacenDestino, int uds, float kg) {
            string ssql = "";
             long lAuxIDSSCC = 0;
            long result = -1;
            ssql = @"select [STOCK PARTIDAS].* from [STOCK PARTIDAS] inner join SSCC_CON on SSCC_CON.IdLote=[STOCK PARTIDAS].IDSSCC inner join SSCC on SSCC.Id=SSCC_CON.IdPadre
where SSCC.SSCC = '"+sscc+"'";
            
            DataTable datos = con.Sql_Datatable(ssql);
            int unds_l_actual = 0;
            float kgs_l_actual = 0;
            int idssscc_actual = 0;
            int almacen_actual = 0;
            int iAnio = 0;
            int iEmpresa = 0;
            string sSerie = "";
            int partida = 0;
            int lin_partida = 0;
            int articulo = 0;

            if (datos.Rows.Count > 0)
            {
                foreach (DataRow row in datos.Rows)
                {
                    
                    int.TryParse(row["Unidades Actuales"].ToString(), out unds_l_actual);
                    float.TryParse(row["Kg Actuales"].ToString(), out kgs_l_actual);
                    int.TryParse(row["IDSSCC"].ToString(), out idssscc_actual);
                    int.TryParse(row["Almacén"].ToString(), out almacen_actual);
                    int.TryParse(row["Año"].ToString(), out iAnio);
                    int.TryParse(row["Empresa"].ToString(), out iEmpresa);
                    int.TryParse(row["Nº Partida"].ToString(), out partida);
                    int.TryParse(row["Nº linea Partida"].ToString(), out lin_partida);
                    int.TryParse(row["Artículo"].ToString(), out articulo);
                    sSerie = row["Serie"].ToString();
                }
            }
            if (almacen_actual!=0 && iAlmacenDestino != almacen_actual) {
                ssql = "SELECT * FROM [Stock Partidas] " +
        "WHERE ([Año] = " + iAnio.ToString() + " ) AND ([Empresa] = " + iEmpresa.ToString() + " ) AND " +
        "([Serie] = '" + sSerie.ToString() + "') AND ([Nº Partida] = " + partida.ToString() + ") AND " +
        "([Nº linea Partida] = " + lin_partida.ToString() + ") AND " +
        "([Artículo] = " + articulo.ToString() + " ) AND " +
        "([Almacén] = " + iAlmacenDestino + " )";
                datos = con.Sql_Datatable(ssql);
                if (datos.Rows.Count > 0)
                {
                    foreach (DataRow row in datos.Rows)
                    {
                        int unds_l_almacen = 0;
                        float kgs_l_almacen = 0;
                        int idssscc_almacen = 0;
                        int.TryParse(row["Unidades Actuales"].ToString(), out unds_l_almacen);
                        float.TryParse(row["Kg Actuales"].ToString(), out kgs_l_almacen);
                        int.TryParse(row["IDSSCC"].ToString(), out idssscc_almacen);
                        if (idssscc_almacen != 0 && idssscc_actual != 0 && idssscc_almacen != idssscc_actual)
                        {
                            if (Actualiza_sscc_con(idssscc_actual, idssscc_almacen))
                            {
                                ssql = "UPDATE [Stock Partidas] " +
                                "SET [Unidades Actuales] = [Unidades Actuales] + " + unds_l_actual.ToString() + "," +
                                "[Kg Actuales] = [Kg Actuales] + " + kgs_l_actual.ToString() + " " +
                                "WHERE (IDSSCC = " + idssscc_almacen.ToString() + ")";
                                if (con.sql_update(ssql))
                                {
                                    ssql = "UPDATE [Stock Partidas] " +
                               "SET [Unidades Actuales] = 0 ," +
                               "[Kg Actuales] = 0 " +
                               "WHERE (IDSSCC = " + idssscc_actual.ToString() + ")";
                                    con.sql_update(ssql);

                                }
                            }

                        }
                    }
                }
                else
                {
                    if (idssscc_actual != 0)
                    {
                        ssql = "INSERT INTO [Stock Partidas] " +
        "(Año,Empresa,Serie,[Tipo Lote],[Nº Partida],[Nº linea Partida], " +
        "Artículo,[Nº Individuo],Almacén,[Unidades Iniciales], " +
        "[Unidades Actuales],[Nº Individuo Proveedor],[Fecha Creación], " +
        "[Kg Iniciales],[Kg Actuales],[FechaCaducidad], LoteInterno) " +
        "SELECT Año,Empresa,Serie,[Tipo Lote],[Nº Partida],[Nº linea Partida], " +
        "Artículo,[Nº Individuo]," + iAlmacenDestino + ",[Unidades Iniciales],[Kg Actuales]," +
        "[Nº Individuo Proveedor],[Fecha Creación],[Kg Iniciales],[Unidades Actuales],[FechaCaducidad],LoteInterno " +
        "FROM [Stock Partidas] " +
        "WHERE (IDSSCC = " + idssscc_actual + ")";

                        if (con.sql_update(ssql)) { 
                        ssql = "UPDATE [Stock Partidas] " +
                        "SET [Unidades Actuales] = 0" +
                        "[Kg Actuales] = 0" +
                        "WHERE (IDSSCC = " + idssscc_actual + ")";
                            con.sql_update(ssql);

                            ssql = @"select [STOCK PARTIDAS].IDSSCC from [STOCK PARTIDAS] inner join SSCC_CON on SSCC_CON.IdLote=[STOCK PARTIDAS].IDSSCC inner join SSCC on SSCC.Id=SSCC_CON.IdPadre where SSCC.SSCC = '" + sscc + "'";
                            string dato = con.sql_string(ssql);
                            int idssscc_nuevo = 0;
                            int.TryParse(dato, out idssscc_nuevo);
                            if (idssscc_nuevo != 0) { 
                            Actualiza_sscc_con(idssscc_actual, idssscc_nuevo);
                            }
                            else
                            {
                                //ERROR
                            }

                        }
                    }

                }


            }
         

                return 0;
        }


        public bool TraspasoEntreAlmacenes_SSCC(int iAlmacenDestino, List <string> ssccs, bool bBorraTraspaso)
        {
            int unds_l_actual = 0;
            float kgs_l_actual = 0;
            int idssscc_actual = 0;
            int almacen_actual = 0;
            int iAnio = 0;
            int iEmpresa = 0;
            int articulo = 0;
            string sSerie = "";
            int albaran = 0;
            int contador = 0;
            int contador2 = 0;
            bool resultado = false;
            string  sql = "";
             foreach (string sscc_ in ssccs)
            {
                if (contador2 == 0)
                    sql = @"SELECT [STOCK PARTIDAS].Año, [STOCK PARTIDAS].Empresa, [STOCK PARTIDAS].Serie,[STOCK PARTIDAS].Almacén,[STOCK PARTIDAS].[Unidades Actuales],[STOCK PARTIDAS].[Kg Actuales],[STOCK PARTIDAS].IDSSCC,[STOCK PARTIDAS].Artículo,SSCC.SSCC from [STOCK PARTIDAS] inner join SSCC_CON on SSCC_CON.IdLote=[STOCK PARTIDAS].IDSSCC inner join SSCC on SSCC.Id=SSCC_CON.IdPadre where SSCC.SSCC = '" + sscc_ + "'";
                else
                    sql = sql + " OR SSCC.SSCC = '" + sscc_ + "'";

                contador2++;
            }
            DataTable datos = con.Sql_Datatable(sql);
            if (datos.Rows.Count > 0)
            {
                foreach (DataRow row in datos.Rows)
                {
                    int.TryParse(row["Unidades Actuales"].ToString(), out unds_l_actual);
                    float.TryParse(row["Kg Actuales"].ToString(), out kgs_l_actual);
                    int.TryParse(row["IDSSCC"].ToString(), out idssscc_actual);
                    int.TryParse(row["Almacén"].ToString(), out almacen_actual);
                    int.TryParse(row["Año"].ToString(), out iAnio);
                    int.TryParse(row["Empresa"].ToString(), out iEmpresa);
                    int.TryParse(row["Artículo"].ToString(), out articulo);
                    string sscc = row["SSCC"].ToString();
                    sSerie = row["Serie"].ToString();
                    if(iAlmacenDestino!= almacen_actual) { 
                        if (contador == 0) { 
                        albaran=Intern_GeneraAlbCabe_X(iAnio, iEmpresa, sSerie, almacen_actual, iAlmacenDestino);
                        }
                        contador++;
                        if (albaran > 0) {
                            Intern_TraspasaStockPartidas(sscc, iAlmacenDestino, unds_l_actual, kgs_l_actual);
                            if (Intern_GeneraAlbLin_X(albaran, contador, iAnio, iEmpresa, iAlmacenDestino, articulo, unds_l_actual, kgs_l_actual, sscc))
                                resultado= true;
                        }
                    }


                }
                return resultado;
            }
            
            return resultado;
        }

/*

        int TraspasoEntreAlmacenes_SSCC(int iAlmacenDestino, List<string> oCollCont, List<string> oCollContGrp, List<string> oCollULs, int iAnioAlb, int iEmpresaAlb, string sSerieAlb, int iAlmacenOrigen,  bool bBorraTraspaso)
        {
            // 19/12/05 - SAS - Gestion del traspaso de soportes entre almacenes
            // Warning!!! Optional parameters not supported
            // Warning!!! Optional parameters not supported
            // Parametros:
            // Almacen destino del traspaso
            // Coleccion con el contenido de todos los soportes a traspasar
            // Coleccion con el contenido AGRUPADO de todos los soportes a traspasar
            // Coleccion con cada uno de los soportes a traspasar (Key = SSCC, Value = ID)
            // Anio del albaran
            // Empresa del albaran
            // Serie del albaran (X)
            // Almacen origen de los soportes
            // Flag indicador de si es un nuevo albaran o borrado del mismo
            // TODO: On Error GoTo Warning!!!: The statement is not translatable 
            bool biniTrans;
            clsSSCCAlmacen loUL = new clsSSCCAlmacen();
            clsSSCCVentas loULV = new clsSSCCVentas();
            clsSSCC_ULCon loCon;
            // Coleccion de contenido
            Collection loCollCont;
            // Coleccion de contenidos agrupada
            Collection loCollGrpAux;
            Collection loCollContGrp;
            // Coleccion con referencia a los objetos copiados a ventas
            Collection loCollDefV = new Collection();
            // Identificador
            long lId;
            // Objeto InfoLote
            clsSSCC_InfoLote loInfoLote;
            long lNumAlbar;
            int iNumLinea;
            long laux;
            // Objeto TraspAlm
            clsSSCC_TraspAlm loULTrasp;
            clsSSCC_TraspAlm loULTrasp_Aux;
            // Objeto ULCon ambito almacen
            clsSSCC_ULCon loConAux;
            TraspasoEntreAlmacenes_SSCC = true;
            if (!(oCollContGrp == null))
            {
                if ((oCollContGrp.Count > 0))
                {
                    // 20/06/07 - JDPC - Colocamos este codigo fuera del bucle de contenidos
                    // Copio los soportes a ventas - Solo si no estoy borrando un traspaso ya realizado
                    if (!bBorraTraspaso)
                    {
                        foreach (loULTrasp in oCollULs)
                        {
                            // Armo una coleccion con la coleccion de objetos copiados a ventas
                            loULTrasp_Aux = new clsSSCC_TraspAlm();
                            // Copio el soporte y recibo la coleccion
                            loULTrasp_Aux.CollCont = loULV.CopiaUL(loULTrasp.Id);
                            loULTrasp_Aux.Id = loULTrasp.Id;
                            loULTrasp_Aux.SSCC = loULTrasp.SSCC;
                            // Agrego objeto a la coleccion
                            loCollDefV.Add;
                            loULTrasp_Aux;
                            Format(loULTrasp_Aux.Id);
                            loULTrasp_Aux = null;
                        }

                    }
                    else
                    {
                        // Elimino los soportes de ventas
                        foreach (loULTrasp in oCollULs)
                        {
                            loULV.BorrarUL(loULTrasp.Id, true);
                        }

                    }

                    // Genero cabecera de ALBARAN_X - Solo si no estoy borrando un traspaso ya realizado
                    if (!bBorraTraspaso)
                    {
                        lNumAlbar = Intern_GeneraAlbCabe_X(iAnioAlb, iEmpresaAlb, sSerieAlb, iAlmacenOrigen, iAlmacenDestino);
                        if ((lNumAlbar < 0))
                        {
                            // 'Error en la generacion de la cabecera del albaran
                            // MsgBox "ERROR TraspasoEntreAlmacenes_SSCC: " & Err.Description
                            // TraspasoEntreAlmacenes_SSCC = False
                            // Exit Function
                            // Error
                            Err.Raise;
                            (vbObjectError + 1002);
                            "Error al generar cabecera de albar�n";
                        }

                    }

                    // Numerador de lineas
                    iNumLinea = 0;
                    foreach (loCon in oCollContGrp)
                    {
                        // Obtiene informacion de lotes
                        loInfoLote = loUL.GetInfoLoteContenido(loCon);
                        // Actualiza el stock del almacen y articulo
                        // 20/06/07 - JDPC - Gestion de stock. si articulo tiene control por lotes no modificamos almacen Articulo
                        // Se estima que todos los articulos TRASPASADO  tiene control por lotoes ya que se utilizan matriculas
                        // If Not Intern_TraspasaAlmacenArticulo(loInfoLote, loCon, iAlmacenDestino) Then
                        // 
                        //     GoTo TrataERR
                        // 
                        // End If
                        // 20/06/07 - JDPC - Colocamos este codigo fuera del bucle de contenidos
                        // Evitar error en la transaccio
                        // 'Copio los soportes a ventas - Solo si no estoy borrando un traspaso ya realizado
                        // If Not (bBorraTraspaso) Then
                        //     For Each loULTrasp In oCollULs
                        // 
                        //         'Armo una coleccion con la coleccion de objetos copiados a ventas
                        //         Set loULTrasp_Aux = New clsSSCC_TraspAlm
                        // 
                        //         'Copio el soporte y recibo la coleccion
                        //         Set loULTrasp_Aux.CollCont = loULV.CopiaUL(loULTrasp.Id)
                        //         loULTrasp_Aux.Id = loULTrasp.Id
                        //         loULTrasp_Aux.SSCC = loULTrasp.SSCC
                        // 
                        //         'Agrego objeto a la coleccion
                        //         loCollDefV.Add loULTrasp_Aux, Format(loULTrasp_Aux.Id)
                        //         Set loULTrasp_Aux = Nothing
                        // 
                        //     Next loULTrasp
                        // Else
                        //     'Elimino los soportes de ventas
                        //     For Each loULTrasp In oCollULs
                        // 
                        //         Call loULV.BorrarUL(loULTrasp.Id, True)
                        // 
                        //     Next loULTrasp
                        // End If
                        // Actualiza el stock partidas y el contenido de dichas partidas - Retorno IdLote actualizado
                        laux = Intern_TraspasaStockPartidas(loInfoLote, loCon, iAlmacenDestino, oCollCont);
                        if ((laux < 0))
                        {
                            Err.Raise;
                            (vbObjectError + 1002);
                            "Error al generar stock partida";
                        }

                        // Alta de las lineas de albaran - Solo si no estoy borrando un traspaso ya realizado
                        if (!bBorraTraspaso)
                        {
                            // Ciclo por cada uno de los elementos de la coleccion de soportes a traspasar
                            foreach (loULTrasp in oCollULs)
                            {
                                // Agrupo el contenido del soporte
                                loCollGrpAux = loUL.GroupContenido(loULTrasp.CollCont);
                                foreach (loConAux in loCollGrpAux)
                                {
                                    // Solo trato aquellos que pertenecen al mismo lote
                                    if ((loConAux.IdLote == loInfoLote.IdLote))
                                    {
                                        iNumLinea = (iNumLinea + 1);
                                        // Obtengo de la coleccion de venta, solo aquel objeto que coincide con la matricula
                                        loULTrasp_Aux = loCollDefV[loULTrasp.Id.ToString()];
                                        // 20/06/07 - JDPC - Agregado retorno errorneo
                                        // 
                                        if (!Intern_GeneraAlbLin_X(lNumAlbar, iNumLinea, iAnioAlb, iEmpresaAlb, iAlmacenDestino, loInfoLote, loConAux, loULTrasp.CollCont, loULTrasp_Aux.CollCont, laux, loULTrasp.SSCC))
                                        {
                                            // Error
                                            Err.Raise;
                                            (vbObjectError + 1002);
                                            "Error al generar lineas de albar�n";
                                        }

                                    }

                                }

                            }

                        }

                    }

                }
                else
                {
                    // La coleccion de contenido no tiene elementos
                    TraspasoEntreAlmacenes_SSCC = false;
                }

            }
            else
            {
                // La coleccion no tiene elementos, no hay contenido
                TraspasoEntreAlmacenes_SSCC = false;
            }

            LiberaObjetos:
            // TODO: On Error Resume Next Warning!!!: The statement is not translatable 
            loCollDefV = null;
            loCollGrpAux = null;
            loConAux = null;
            loULTrasp = null;
            loCon = null;
            loUL = null;
            loULV = null;
            loULTrasp_Aux = null;
            // TODO: Exit Function: Warning!!! Need to return the value
            return;
            TrataERR:
            MsgBox;
            ("ERROR TraspasoEntreAlmacenes_SSCC: " + Err.Description);
            Err.Clear;
            // TODO: On Error GoTo 0 Warning!!!: The statement is not translatable 
            TraspasoEntreAlmacenes_SSCC = false;
            LiberaObjetos;
        }*/
    }
}
