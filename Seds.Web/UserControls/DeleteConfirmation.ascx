<%@ Control Language="C#" AutoEventWireup="true" CodeBehind="DeleteConfirmation.ascx.cs" Inherits="Seds.Web.UserControls.DeleteConfirmation" %>
<div class="modal small hide fade" id="<%= ClientID %>" tabindex="-1" role="dialog" aria-labelledby="<%= ClientID %>_Label" aria-hidden="true">
    <div class="modal-header">
        <button type="button" class="close" data-dismiss="modal" aria-hidden="true">×</button>
        <h3 id="myModalLabel">Potvrda brisanja</h3>
    </div>
    <div class="modal-body">

        <p class="error-text"><i class="icon-warning-sign modal-icon"></i><%= Message %></p>
    </div>
    <div class="modal-footer">
        <button class="btn" data-dismiss="modal" aria-hidden="true">Odustani</button>
        <button class="btn btn-danger" data-dismiss="modal">Obriši</button>
    </div>
</div>