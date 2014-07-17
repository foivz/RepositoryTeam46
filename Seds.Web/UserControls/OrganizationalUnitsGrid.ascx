<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="OrganizationalUnitsGrid.ascx.cs" Inherits="Seds.Web.UserControls.OrganizationalUnitsGrid" %>
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
                <a href="/OrganizationalUnitEdit.aspx?OrganizationalUnitId=<%# DataBinder.Eval(Container.DataItem, "Id") %>">
                <%# DataBinder.Eval(Container.DataItem, "Title") %>
                </!--a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField
            SortExpression="TitleName"
            HeaderText="Opis">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "TitleName") %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField
            SortExpression="IdIsvu"
            HeaderText="ISVU id">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "IdIsvu") %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>
        <asp:TemplateField
            SortExpression=""
            HeaderText="">
            <ItemTemplate>
                <a href="/OrganizationalUnitEdit.aspx?OrganizationalUnitId=<%# DataBinder.Eval(Container.DataItem, "Id") %>"><i class="icon-pencil"></i></a>
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
