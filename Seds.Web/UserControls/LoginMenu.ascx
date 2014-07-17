<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LoginMenu.ascx.cs" Inherits="Seds.Web.UserControls.LoginMenu" %>
<asp:PlaceHolder ID="phUserStatus" runat="server">
    <a data-toggle="dropdown" class="dropdown-toggle" role="button" href="#">
        <i class="icon-user"></i>
        <asp:Literal ID="hlProfileInfo" runat="server"></asp:Literal>
        &nbsp;
        <asp:Image ID="imgProfile" CssClass="ProfileImage" runat="server" Visible="false" Width="27" Height="27" />
        <i class="icon-caret-down"></i>
    </a>
    <ul class="dropdown-menu">
        <%--<li><a href="<%= UserProfile != null ? UserProfile.SsoUser.Url : "#" %>" tabindex="-1">Uredi profil (<%= UserProfile.SsoUser.Provider %>)</a></li>--%>
        <li class="divider"></li>
        <li><a href="#" class="visible-phone" tabindex="-1">Settings</a></li>
        <li class="divider visible-phone"></li>
        <li>
            <asp:HyperLink ID="hlLogout" NavigateUrl="~/Logout.aspx" TabIndex="-1" CssClass="logout" runat="server" Text="Odjavi se" Visible="true"></asp:HyperLink>
        </li>
    </ul>
</asp:PlaceHolder>
<asp:PlaceHolder ID="phLoginLinks" runat="server" Visible="false">Prijava: 
    <asp:Repeater ID="rptLoginLinks" runat="server">
        <ItemTemplate>
            <asp:HyperLink ID="hlLogin" runat="server"></asp:HyperLink>
            <asp:Literal ID="ltDelimiter" runat="server"></asp:Literal>
        </ItemTemplate>
    </asp:Repeater>
</asp:PlaceHolder>


