<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="StudentEdit.aspx.cs" Inherits="Seds.Web.StudentEdit" %>

<%@ Register Src="~/UserControls/DatePicker.ascx" TagPrefix="seds" TagName="DatePicker" %>
<%@ Register Src="~/UserControls/StudentStudiesGrid.ascx" TagPrefix="seds" TagName="StudentStudiesGrid" %>
<%@ Register Src="~/UserControls/StudentCertificateGrid.ascx" TagPrefix="seds" TagName="StudentCertificateGrid" %>

<%@ Import Namespace="Seds.Web" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        $(function () {

            setTimeout(function () {

                $("#tab_<%= SelectedTab.ToString() %>").click();

                $("#TabContent").show();
            }, 15);

            $(".nav-tabs a").click(function () {
                $('#<%= hndSelectedTab.ClientID %>').val($(this).attr("id").replace("tab_", ""));
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="content" runat="server">
    <div class="btn-toolbar">
        <asp:LinkButton ID="btnSave" runat="server" CssClass="btn btn-primary"><i class="icon-save"></i>Spremi</asp:LinkButton>
        <div class="btn-group">
        </div>
    </div>
    <div class="well">
        <ul class="nav nav-tabs">
            <li class="active"><a id="tab_<%= StudentEditTab.Main.ToString() %>" href="#<%= StudentEditTab.Main.ToString() %>" data-toggle="tab">Podaci o studentu</a></li>
            <li><a id="tab_<%= StudentEditTab.Study.ToString() %>" href="#<%= StudentEditTab.Study.ToString() %>" data-toggle="tab">Upisani studiji</a></li>
            <li><a id="tab_<%= StudentEditTab.Certificates.ToString() %>" href="#<%= StudentEditTab.Certificates.ToString() %>" data-toggle="tab">Diplome</a></li>
        </ul>
        <asp:HiddenField ID="hndSelectedTab" Value="" runat="server" />
        <div id="TabContent" class="tab-content" style="display: none;">
            <div class="tab-pane active in" id="<%= StudentEditTab.Main.ToString() %>">
                <div id="tab">
                    <label>Ime</label>
                    <asp:TextBox ID="txtFirstName" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                    <label>Prezime</label>
                    <asp:TextBox ID="txtLastName" CssClass="input-xlarge" runat="server" Visible="true"></asp:TextBox>
                    <label>Djevojačko prezime</label>
                    <asp:TextBox ID="txtBirthLastName" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Spol</label>
                    <asp:DropDownList ID="ddlGender" CssClass="input-xlarge" runat="server">
                        <asp:ListItem Text="Muško" Value="1"></asp:ListItem>
                        <asp:ListItem Text="Žensko" Value="2"></asp:ListItem>
                    </asp:DropDownList>
                    <label>Datum rođenja</label>
                    <seds:DatePicker runat="server" ID="dpBirthDate" />
                    <label>JMBG</label>
                    <asp:TextBox ID="txtJMBG" MaxLength="13" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>OIB</label>
                    <asp:TextBox ID="txtOIB" MaxLength="11" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Matični broj studenta</label>
                    <asp:TextBox ID="txtUniqueNumber" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Prosjek grupe</label>
                    <asp:TextBox ID="txtAverageRating" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Prosjek grupe 1</label>
                    <asp:TextBox ID="txtAverageRating1" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Prosjek grupe 2</label>
                    <asp:TextBox ID="txtAverageRating2" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Provjera</label>
                    <asp:CheckBox ID="chkVerified" CssClass="input-xlarge" runat="server" />
                    <label>E-mail</label>
                    <asp:TextBox ID="txtEmail" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Telefon</label>
                    <asp:TextBox ID="txtPhoneNumber" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Slika</label>
                    <asp:TextBox ID="txtImagePath" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Bilješke</label>
                    <asp:TextBox ID="txtNote" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Grad</label>
                    <asp:TextBox ID="txtCity" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Adresa</label>
                    <asp:TextBox ID="txtAddress" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Obrisan</label>
                    <asp:CheckBox ID="chkIsDeleted" CssClass="input-xlarge" runat="server" />
                </div>
            </div>
            <div class="tab-pane fade" id="<%= StudentEditTab.Study.ToString() %>">
                <div id="tab2">
                    <seds:StudentStudiesGrid runat="server" ID="ucStudentStudiesGrid" />
                    <br />
                    <label>Studij</label>
                    <asp:DropDownList runat="server" ID="ddlStudies"></asp:DropDownList>
                    <label>Datum upisa</label>
                    <seds:DatePicker runat="server" ID="dpEnrollmentYear" />
                    <%--<br />
                    <asp:LinkButton ID="btnAddStudy" runat="server" CssClass="btn btn-primary"><i class="icon-save"></i>Dodajte</asp:LinkButton>--%>
                </div>
            </div>
            <div class="tab-pane fade" id="<%= StudentEditTab.Certificates.ToString() %>">
                <div id="tab3">
                    <seds:StudentCertificateGrid runat="server" ID="ucStudentCertificateGrid" />
                    <br />
                    <label>Broj diplome</label>
                    <asp:TextBox ID="txtCertificateNumber" CssClass="input-xlarge" runat="server"></asp:TextBox>
                    <label>Datum diplomiranja</label>
                    <seds:DatePicker runat="server" ID="dpGraduationDate" />
                    <label>Tip diplome</label>
                    <asp:DropDownList runat="server" ID="ddlCertificateType"></asp:DropDownList>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
