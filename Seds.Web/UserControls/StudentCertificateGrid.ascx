<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentCertificateGrid.ascx.cs" Inherits="Seds.Web.UserControls.StudentCertificateGrid" %>
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
            SortExpression="Title"
            HeaderText="Naziv">
            <ItemTemplate>
                <%# DataBinder.Eval( Container.DataItem, "CertificateNumber") %>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField
            SortExpression="GraduationDate"
            HeaderText="Završetak studija">
            <ItemTemplate>
                <%#  ((DateTime)DataBinder.Eval(Container.DataItem, "GraduationDate")).ToString("dd.MM.yyyy") %>
            </ItemTemplate>
        </asp:TemplateField>

         <asp:TemplateField
            SortExpression="TypeId"
            HeaderText="Tip studija">
            <ItemTemplate>
                <%# GetTypeTitle((Seds.DataAccess.Certificate)Container.DataItem) %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression=""
            HeaderText="">
            <ItemTemplate>
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