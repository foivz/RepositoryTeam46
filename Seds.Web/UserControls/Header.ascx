<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="Header.ascx.cs" Inherits="Seds.Web.UserControls.Header" %>
<%@ Register Src="~/UserControls/LoginMenu.ascx" TagPrefix="seds" TagName="LoginMenu" %>

<div class="navbar-inner">    
    <ul class="nav pull-right">
        <li id="fat-menu" class="dropdown">
            <seds:LoginMenu runat="server" ID="LoginMenu" />
        </li>
    </ul>
    <a class="brand" href="<%= ResolveUrl("~/Home.aspx") %>"><span class="first">SEDS ::</span> <span class="second">Sustav za evidenciju diplomiranih studenata v1.0</span></a>
</div>