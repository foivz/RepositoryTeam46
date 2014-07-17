<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudyTypes.aspx.cs" Inherits="Seds.Web.StudyTypes" %>
<%@ Register Src="~/UserControls/StudyTypesGrid.ascx" TagPrefix="seds" TagName="StudyTypesGrid" %>
<%@ Register Src="~/UserControls/DeleteConfirmation.ascx" TagPrefix="seds" TagName="DeleteConfirmation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="btn-toolbar">
        <button class="btn btn-primary" onclick="window.location.href = '<%= ResolveUrl("~/StudyTypeEdit.aspx") %>'; return false;"><i class="icon-plus"></i>Dodaj novog</button>
        <div class="btn-group">
        </div>
    </div>
    <div class="well">
        <seds:StudyTypesGrid runat="server" id="ucStudyTypes" />
    </div>
</asp:Content>
