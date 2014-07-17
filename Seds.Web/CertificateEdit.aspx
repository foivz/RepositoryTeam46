<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CertificateEdit.aspx.cs" Inherits="Seds.Web.CertificateEdit" %>

<%@ Register Src="~/UserControls/DatePicker.ascx" TagPrefix="seds" TagName="DatePicker" %>
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
                    <label>Broj diplome</label>
                    <asp:TextBox ID="txtCertificateNumber" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                    <label>Vrsta diplome</label>
                    <asp:DropDownList ID="ddlTypeId" CssClass="input-xlarge" runat="server" Visible="true"></asp:DropDownList>
                    <label>Datum diplomiranja</label>
                    <seds:DatePicker runat="server" ID="dpGraduationDate" />
                </div>
            </div>
        </div>
    </div>
</asp:Content>
