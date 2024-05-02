using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Net;
using System.Security.Principal;
using System.Threading;
using System.Web.UI;

namespace BAsset_3._0_Reports
{
    public partial class ReportRender : System.Web.UI.Page
    {
        string nombreReporte = "";
        string carpetaReporte = "B-Asset_4.0";
        protected void Page_Init(object sender, EventArgs e)
        {
            int idReporte = 0;
            string NombreReporteTitulo = String.Empty;
            idReporte = int.Parse(Request.QueryString["IdReporte"]);
            string cult = Request.QueryString["lan"] ?? "en-US";

            CultureInfo culture = CultureInfo.GetCultureInfo(cult);
            Thread.CurrentThread.CurrentCulture = culture;
            Thread.CurrentThread.CurrentUICulture = culture;
            var parameters = new List<ReportParameter>();

            switch (idReporte)
            {
                case 1:
                    nombreReporte = "/Ppto";
                    NombreReporteTitulo = Resources.multi.language.reportePresupuesto;
                    break;
                case 2:
                    nombreReporte = "/ActualvsPpto";
                    NombreReporteTitulo = Resources.multi.language.reporteActualvsPresupuesto;
                    break;
                case 3:
                    nombreReporte = "/ActualvsPptoxCuentas";
                    NombreReporteTitulo = Resources.multi.language.reporteActualPresupuestoporCuentas;
                    break;
                case 4:
                    nombreReporte = "/ActualvsPptovsForecast";
                    NombreReporteTitulo = Resources.multi.language.reporteActualPresupuestoForecast;
                    break;
                case 5:
                    nombreReporte = "";
                    NombreReporteTitulo = "";
                    break;
                case 6:
                    nombreReporte = "/Activos";
                    NombreReporteTitulo = Resources.multi.language.reporteActivos;
                    break;
                case 7:
                    nombreReporte = "/ActividadesEstrategicas";
                    NombreReporteTitulo = Resources.multi.language.reporteActividadesEstratégicas;
                    break;
                case 8:
                    nombreReporte = "/InterfazProgramacionActividadesEstrategicas";
                    NombreReporteTitulo = Resources.multi.language.reporteInterfazProgramaciónActividadesEstratégicas;
                    break;
                case 9:
                    nombreReporte = "/LibroMayor";
                    NombreReporteTitulo = Resources.multi.language.reporteLibroMayor;
                    break;
                case 10:
                    nombreReporte = "/OrdenesTrabajo";
                    NombreReporteTitulo = Resources.multi.language.reporteOrdenesdetrabajo;
                    break;
                case 11:
                    nombreReporte = "https://app.powerbi.com/view?r=eyJrIjoiNzA0ZWM1OTUtYjFlMC00MjEyLTg0Y2EtNmIzNmU1NzNmNjZhIiwidCI6ImVlOWVmOTJlLTBiNzctNGQwYi1iYmM1LWEyY2NkZjIyMjA5MyIsImMiOjR9";
                    NombreReporteTitulo = "B.I.";
                    break;
                case 12:
                    nombreReporte = "/CambioComponentes";
                    NombreReporteTitulo = Resources.multi.language.reporteCambioComponente;
                    break;
                case 13:
                    nombreReporte = "/ParadasActivos";
                    NombreReporteTitulo = Resources.multi.language.reporteParadasActivos;
                    break;
                case 14:
                    nombreReporte = "/DisponibilidadActual";
                    NombreReporteTitulo = Resources.multi.language.reporteDisponibilidadActual;
                    break;
                case 15:
                    nombreReporte = "/DisponibilidadProyectada";
                    NombreReporteTitulo = Resources.multi.language.reporteDisponibilidadProyectada;
                    break;
                case 16:
                    nombreReporte = "/ControlEstrategia";
                    NombreReporteTitulo = Resources.multi.language.reporteHorasPlanificadas;
                    break;
                case 17:
                    nombreReporte = "/ResumenCreacionRequerimiento";
                    string idRequest = Request.QueryString["idrequest"];
                    NombreReporteTitulo = Resources.multi.language.reporteResumenRequerimiento;
                    parameters.Add(new ReportParameter("IdRequerimiento", idRequest));
                    break;
            }

            if (!Page.IsPostBack)
            {
                //Title = "Reporte " + NombreReporteTitulo;
                Title = NombreReporteTitulo;
                // Set the processing mode for the ReportViewer to Remote
                if (idReporte != 11)
                {
                    ReportMasterBasset.ProcessingMode = ProcessingMode.Remote;
                    ServerReport serverReport = ReportMasterBasset.ServerReport;

                    // Set the report server URL and report path
                    serverReport.ReportServerUrl = new Uri(Properties.Settings.Default.repURI ?? "http://127.0.0.1/reportserver");
                    string rutareporte = "/" + carpetaReporte + (!String.IsNullOrEmpty(Resources.multi.language.carpetaReporte) ? "/" + Resources.multi.language.carpetaReporte : "") + nombreReporte + Resources.multi.language.extensionReporte;
                    serverReport.ReportPath = rutareporte;
                    serverReport.ReportServerCredentials = new MyReportServerCredentials();
                    ReportMasterBasset.ServerReport.SetParameters(parameters);
                    if (idReporte == 17) ReportMasterBasset.ShowParameterPrompts = false;
                    //ReportMasterBasset.SizeToReportContent = true;
                }
                else
                {
                    ScriptManager.RegisterStartupScript(this, typeof(Page), "externo", "cargarBI('" + nombreReporte + "')", true);
                }
            }
        }
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        //CREDENCIALES RRSS
        [Serializable]
        public sealed class MyReportServerCredentials : IReportServerCredentials
        {
            public WindowsIdentity ImpersonationUser
            {
                get
                {
                    // Use the default Windows user.  Credentials will be
                    // provided by the NetworkCredentials property.
                    return null;
                }
            }

            public ICredentials NetworkCredentials
            {
                get
                {
                    string userName = Properties.Settings.Default.repUserName ?? "Administrador";
                    string password = Properties.Settings.Default.repPassword ?? "Crea2015";
                    string domain = Properties.Settings.Default.repDomain ?? "";
                    return new NetworkCredential(userName, password);
                }
            }

            public bool GetFormsCredentials(out Cookie authCookie, out string userName, out string password, out string authority)
            {
                authCookie = null;
                userName = null;
                password = null;
                authority = null;
                // Not using form credentials
                return false;
            }
        }
    }
}