<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="StudiesGrid.ascx.cs" Inherits="Seds.Web.UserControls.StudiesGrid" %>
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
            SortExpression="CourseGroupId"
            HeaderText="Grupa kolegija">
            <ItemTemplate>
                <%#GetCourseGroupTitle(DataBinder.Eval(Container.DataItem, "CourseGroupId") as int?) %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="Title"
            HeaderText="Naziv">
            <ItemTemplate>
                <a href="/StudyEdit.aspx?StudyId=<%# DataBinder.Eval( Container.DataItem, "Id") %>">
                    <%# DataBinder.Eval(Container.DataItem, "Title") %>
                </a>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="Duration"
            HeaderText="Trajanje">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "Duration") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="ECTS"
            HeaderText="ECTS">
            <ItemTemplate>
                <%# DataBinder.Eval(Container.DataItem, "ECTS") %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression="TypeId"
            HeaderText="Tip">
            <ItemTemplate>
                <%#GetStudyTypeTitle (DataBinder.Eval(Container.DataItem, "TypeId") as int?) %>
            </ItemTemplate>
        </asp:TemplateField>

        <asp:TemplateField
            SortExpression=""
            HeaderText="">
            <ItemTemplate>
                <a href="/StudyEdit.aspx?StudyId=<%# DataBinder.Eval( Container.DataItem, "Id") %>"><i class="icon-pencil"></i></a>
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
