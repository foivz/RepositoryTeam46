﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CertificatesGrid.ascx.cs" Inherits="Seds.Web.UserControls.CertificatesGrid" %>
<asp:GridView
    ID="gvData" runat="server"
    CssClass="table"
    AutoGenerateColumns="False"
    AllowPaging="true"
    AllowSorting="True"
    PagerSettings-Position="Bottom"
    GridLines="None">
    <PagerStyle CssClass="pagination" />
    <HeaderStyle />

    <Columns>
        <asp:TemplateField
            SortExpression="Id"
            HeaderText="Id">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Id") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="CertificateNumber"
            HeaderText="Broj diplome">
            <ItemTemplate>
                <a href="/CertificateEdit.aspx?CertificateId=<%# DataBinder.Eval(Container.DataItem, "Id") %>">
                    <%# DataBinder.Eval(Container.DataItem, "CertificateNumber") %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="TypeId"
            HeaderText="Tip diplome">
            <ItemTemplate>
                <%#GetStudyTypeTitle (DataBinder.Eval(Container.DataItem, "TypeId") as int?) %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="GraduationDate"
            HeaderText="Datum diplomiranja">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "GraduationDate") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression=""
            HeaderText="">
            <ItemTemplate>
                <a href="/CertificateEdit.aspx?CertificateId=<%# DataBinder.Eval(Container.DataItem, "Id") %>"><i class="icon-pencil"></i></a>
                <asp:LinkButton ID="btnDelete"
                    runat="server"
                    OnClientClick="return confirm('Jeste li sigurni da želite brisati?');"
                    CommandName="delete"
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "Id") %>'><i class="icon-remove"></i>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
