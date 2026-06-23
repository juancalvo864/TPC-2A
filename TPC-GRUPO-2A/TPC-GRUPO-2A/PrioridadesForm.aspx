<%@ Page Title="" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="PrioridadesForm.aspx.cs" Inherits="TPC_GRUPO_2A.PrioridadesForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <h2 class="mb-4">Alta de Prioridad</h2>
    <asp:Panel ID="pnlFormulario" runat="server" CssClass="card p-3 mb-2">
    <div class="mb-2">
        <label class="form-label">Nombre</label>
        <asp:TextBox ID="txtNombre" runat="server" CssClass="form-control" placeholder="Ingresá el nombre" />
    </div>
    <div class="mb-2">
        <label class="form-label">Nivel</label>
        <asp:TextBox ID="txtNivel" runat="server" CssClass="form-control" placeholder="Ingresá el nivel" TextMode="Number" />
    </div>
     <hr />
    <div class="d-flex gap-2">
        <asp:Button ID="btnGuardar" runat="server" Text="Guardar" CssClass="btn btn-success" OnClick="btnGuardar_Click"/>
        <asp:Button ID="btnCancelar" runat="server" Text="Cancelar" CssClass="btn btn-secondary" OnClick="btnCancelar_Click"/>
    </div>
</asp:Panel>
</asp:Content>
