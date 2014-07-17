<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="UserEdit.aspx.cs" Inherits="Seds.Web.UserEdit" %>

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
                    <label>Korisničko ime</label>
                    <asp:TextBox ID="txtUserName" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                    <label>Lozinka</label>
                    <asp:TextBox ID="txtPassword" TextMode="Password" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                    <label>Ime</label>
                    <asp:TextBox ID="txtFirstName" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Prezime</label>
                    <asp:TextBox ID="txtLastName" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Email</label>
                    <asp:TextBox ID="txtEmail" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                    <label>Uloga</label>
                    <asp:DropDownList ID="ddlRole" CssClass="input-xlarge" runat="server" Visible="true"></asp:DropDownList>

                </div>
            </div>
        </div>
    </div>
</asp:Content>
