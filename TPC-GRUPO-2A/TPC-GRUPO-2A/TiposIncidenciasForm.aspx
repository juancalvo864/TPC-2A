<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="TiposIncidenciasForm.aspx.cs" Inherits="TPC_GRUPO_2A.TipoIncidenciasForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
     <asp:Label ID="lblTituloPanel" runat="server" Text="Nuevo Tipo de Incidencia" CssClass="h5 mb-3 d-block" />
<div class="mb-3">
    <label class="form-label">Nombre</label>
    <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresá el nombre" />
</div>
<div class="mb-3">
    <label class="form-label">Descripción</label>
    <asp:TextBox ID="txtDescripcion" runat="server" CssClass="form-control" placeholder="Ingresá una descripción (opcional)" />
</div>
<div class="d-flex gap-2">
    <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success"/>
    <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary"/>
</div>
</asp:Content>
