<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="MessageBox.ascx.cs" Inherits="Seds.Web.UserControls.MessageBox" %>
<div id="divMessageBoxSuccess" runat="server" style="display:none;" EnableViewState="false">    
	<asp:Label CssClass="MessageBoxLabel" ID="lblMessageBox" runat="server"></asp:Label>
</div>
<div id="divMessageBoxInfo" runat="server" style="display:none;" EnableViewState="false">
	<asp:Label CssClass="MessageBoxLabel" ID="lblMessageBoxInfo" runat="server"></asp:Label>
</div>
<div id="divMessageBoxError" runat="server" style="display:none;" EnableViewState="false">
	<asp:Label CssClass="MessageBoxLabel" ID="lblMessageBoxError" runat="server"></asp:Label>
</div>
