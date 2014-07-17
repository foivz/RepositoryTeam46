<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Users.aspx.cs" Inherits="Seds.Web.Users" %>
<%@ Register Src="~/UserControls/UsersGrid.ascx" TagPrefix="seds" TagName="UsersGrid" %>
<%@ Register Src="~/UserControls/DeleteConfirmation.ascx" TagPrefix="seds" TagName="DeleteConfirmation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="btn-toolbar">
        <button class="btn btn-primary" onclick="window.location.href = '<%= ResolveUrl("~/UserEdit.aspx") %>'; return false;"><i class="icon-plus"></i>Dodaj novog</button>
        <div class="btn-group">
        </div>
    </div>
    <div class="well">
        <seds:UsersGrid runat="server" id="ucUsers" />
    </div>
</asp:Content>
