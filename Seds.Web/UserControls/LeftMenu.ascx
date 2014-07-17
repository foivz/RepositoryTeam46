<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="LeftMenu.ascx.cs" Inherits="Seds.Web.UserControls.LeftMenu" %>


<div class="sidebar-nav">
    <a href="#dashboard-menu" class="nav-header" data-toggle="collapse"><i class="icon-dashboard"></i>Stranice</a>
    <ul id="dashboard-menu" class="nav nav-list collapse<%= GetMenuSectionCssClass(Seds.Web.UserControls.LeftMenuSection.Page) %>">
        <li class="<%= GetMenuPageCssClass(Seds.Web.UserControls.LeftMenuPage.Search) %>">
            <asp:HyperLink runat="server" NavigateUrl="~/Search.aspx" meta:resourcekey="Search"></asp:HyperLink>
        </li>
        <li class="<%= GetMenuPageCssClass(Seds.Web.UserControls.LeftMenuPage.Students) %>">
            <asp:HyperLink runat="server" NavigateUrl="~/Students.aspx" meta:resourcekey="Students"></asp:HyperLink>
        <li class="<%= GetMenuPageCssClass(Seds.Web.UserControls.LeftMenuPage.Certificates) %>">
            <asp:HyperLink runat="server" NavigateUrl="~/Certificates.aspx" meta:resourcekey="Certificates"></asp:HyperLink>
        </li>

    </ul>
    <a href="#accounts-menu" class="nav-header" data-toggle="collapse"><i class="icon-briefcase"></i>Katalozi</a>
    <ul id="accounts-menu" class="nav nav-list collapse<%= GetMenuSectionCssClass(Seds.Web.UserControls.LeftMenuSection.Settings) %>">
        <li class="<%= GetMenuPageCssClass(Seds.Web.UserControls.LeftMenuPage.StudyTypes) %>">
            <asp:HyperLink runat="server" NavigateUrl="~/StudyTypes.aspx" meta:resourcekey="StudyTypes"></asp:HyperLink>
        </li>
        <li class="<%= GetMenuPageCssClass(Seds.Web.UserControls.LeftMenuPage.Studies) %>">
            <asp:HyperLink runat="server" NavigateUrl="~/Studies.aspx" meta:resourcekey="Studies"></asp:HyperLink>
        </li>
        <li class="<%= GetMenuPageCssClass(Seds.Web.UserControls.LeftMenuPage.OrganizationalUnits) %>">
            <asp:HyperLink runat="server" NavigateUrl="~/OrganizationalUnits.aspx" meta:resourcekey="OrganizationalUnits"></asp:HyperLink>
        </li>
        <li class="<%= GetMenuPageCssClass(Seds.Web.UserControls.LeftMenuPage.CourseGroups) %>">
            <asp:HyperLink runat="server" NavigateUrl="~/CourseGroups.aspx" meta:resourcekey="CourseGroups"></asp:HyperLink>
        </li>
        <li class="<%= GetMenuPageCssClass(Seds.Web.UserControls.LeftMenuPage.CertificateTypes) %>">
            <asp:HyperLink runat="server" NavigateUrl="~/CertificateTypes.aspx" meta:resourcekey="CertificateTypes"></asp:HyperLink>
        </li>
    </ul>
    <a href="#admin-menu" class="nav-header" data-toggle="collapse"><i class="icon-briefcase"></i>Administracija</a>
    <ul id="admin-menu" class="nav nav-list collapse<%= GetMenuSectionCssClass(Seds.Web.UserControls.LeftMenuSection.Administration) %>">
        <li class="<%= GetMenuPageCssClass(Seds.Web.UserControls.LeftMenuPage.Roles) %>">
            <asp:HyperLink runat="server" NavigateUrl="~/Roles.aspx" meta:resourcekey="Roles"></asp:HyperLink>
        </li>
        <li class="<%= GetMenuPageCssClass(Seds.Web.UserControls.LeftMenuPage.Users) %>">
            <asp:HyperLink runat="server" NavigateUrl="~/Users.aspx" meta:resourcekey="Users"></asp:HyperLink>
        </li>
    </ul>
    <a href="Helps.aspx" class="nav-header"><i class="icon-question-sign"></i>Pomoć</a>

    

</div>
