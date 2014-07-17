<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CertificateTypeEdit.aspx.cs" Inherits="Seds.Web.CertificateTypeEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="btn-toolbar">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary"><i class="icon-save"></i>Spremi</asp:LinkButton>
        <div class="btn-group">
        </div>
    </div>
    <div class="well">
        <div id="TabContent" class="tab-content">
            <div class="tab-pane active in">
                <div id="tab">
                    <label>Tip dokumenta - diplome</label>
                    <asp:TextBox ID="txtTitle" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
