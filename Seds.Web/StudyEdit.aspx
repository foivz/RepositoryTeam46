<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudyEdit.aspx.cs" Inherits="Seds.Web.StudyEdit" %>
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
                    <label>Studijska grupa</label>
                    <asp:DropDownList ID="ddlCourseGroup" CssClass="input-xlarge" runat="server" Visible="true"></asp:DropDownList>
                    <label>Naziv studija</label>
                    <asp:TextBox ID="txtTitle" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                    <label>Trajanje</label>
                    <asp:TextBox ID="txtDuration" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>ECTS</label>
                    <asp:TextBox ID="txtEcts" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Tip studija</label>
                    <asp:DropDownList ID="ddlStudyType" CssClass="input-xlarge" runat="server" Visible="true"></asp:DropDownList>


                </div>
            </div>
        </div>
    </div>
</asp:Content>
