<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Search.aspx.cs" Inherits="Seds.Web.Search" %>
<%@ Register Src="~/UserControls/SearchGrid.ascx" TagPrefix="seds" TagName="SearchGrid" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="btn-toolbar">
        <asp:LinkButton ID="btnSearch" runat="server" CssClass="btn btn-primary"><i class="icon-save"></i>Traži</asp:LinkButton>
        <div class="btn-group">
        </div>
    </div>
    <div class="well">
        <div id="TabContent" class="tab-content">
            <div class="tab-pane active in">
                <div id="tab">
                    <label>Upišite traženi pojam (ime, prezime, oib, jmbg,jmbag)</label>
                    <asp:TextBox ID="txtPharse" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                </div>
            </div>
        </div>
        <seds:SearchGrid runat="server" id="ucStudents" HideIfNoItems="true"/>
    </div>
</asp:Content>
