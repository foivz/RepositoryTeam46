﻿<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="CourseGroupsGrid.ascx.cs" Inherits="Seds.Web.UserControls.CourseGroupsGrid" %>
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
            SortExpression="Title"
            HeaderText="Naziv">
            <ItemTemplate>               
                <a href="/CourseGroupEdit.aspx?CourseGroupId=<%# DataBinder.Eval(Container.DataItem, "Id") %>">
                     <%# DataBinder.Eval(Container.DataItem, "Title") %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="TitleName"
            HeaderText="Opis">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TitleName") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression=""
            HeaderText="">
            <ItemTemplate>
                <a href="/CourseGroupEdit.aspx?CourseGroupId=<%# DataBinder.Eval(Container.DataItem, "Id") %>"><i class="icon-pencil"></i></a>
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