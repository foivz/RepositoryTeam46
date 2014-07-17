<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Certificates.aspx.cs" Inherits="Seds.Web.Certificates" %>
<%@ Register Src="~/UserControls/CertificatesGrid.ascx" TagPrefix="seds" TagName="CertificatesGrid" %>
<%@ Register Src="~/UserControls/DeleteConfirmation.ascx" TagPrefix="seds" TagName="DeleteConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="btn-toolbar">
        <button class="btn btn-primary" onclick="window.location.href = '<%= ResolveUrl("~/CertificateEdit.aspx") %>'; return false;"><i class="icon-plus"></i>Dodaj novog</button>
        <div class="btn-group">
        </div>
    </div>
    <div class="well">
        <seds:CertificatesGrid runat="server" id="ucCertificates" />
    </div>
</asp:Content>
