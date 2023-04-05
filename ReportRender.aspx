<%@ Page Language="C#" AutoEventWireup="true" CodeBehind="ReportRender.aspx.cs" Inherits="BAsset_3._0_Reports.ReportRender" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<!DOCTYPE html>

<html xmlns="http://www.w3.org/1999/xhtml">
<head runat="server">
    <title></title>
</head>
<body>
    <script type="text/javascript">
        function cargarBI(url) {
            $("#ocultar").hide('fast');
            $("#ReporteExterno").attr("src", url);
        }
    </script>
    <form id="form1" runat="server">
        <div>
            <asp:Label runat="server" ID="lbAux" />
        </div>
        <div>
            <asp:ScriptManager runat="server" ID="AdministradorScripts" EnablePageMethods="true" ScriptMode="Release"></asp:ScriptManager>
            <div id="ocultar">
                <rsweb:ReportViewer ID="ReportMasterBasset" runat="server" Width="100%" DocumentMapWidth="100%" AsyncRendering="false" Height="700px"></rsweb:ReportViewer>
            </div>
            <iframe id="ReporteExterno" src="" style="width: 100%; height: 900px; border: none;"></iframe>
        </div>
    </form>
</body>
</html>
