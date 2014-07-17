<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="CourseGroupEdit.aspx.cs" Inherits="Seds.Web.CourseGroupEdit" %>

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
                    <label>Naziv</label>
                    <asp:TextBox ID="txtTitle" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                    <label>Puni naziv</label>
                    <asp:TextBox ID="txtTitleName" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
