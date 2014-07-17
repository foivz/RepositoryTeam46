<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudentStudiesGrid.ascx.cs" Inherits="Seds.Web.UserControls.StudentStudiesGrid" %>
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
                <a href="/StudyEdit.aspx?StudyId=<%# DataBinder.Eval( Container.DataItem, "StudyId") %>">
                    <%#GetStudyTitle((Seds.DataAccess.Student_Studies)Container.DataItem) %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="EnrollmentYear"
            HeaderText="Godina upisa">
            <ItemTemplate>
                <%#  ((DateTime)DataBinder.Eval(Container.DataItem, "EnrollmentYear")).ToString("dd.MM.yyyy") %>
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
                    CommandArgument='<%# DataBinder.Eval(Container.DataItem, "StudyId") %>'><i class="icon-remove"></i>
                </asp:LinkButton>
            </ItemTemplate>
        </asp:TemplateField>
    </Columns>
</asp:GridView>
