<%@ Page Title="Clientes" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Clientes.aspx.cs" Inherits="TPC_GRUPO_2A.Clientes" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <main>
        <h2>Página de Clientes</h2>
        <asp:GridView ID="dgvClientes" runat="server" CssClass="table"></asp:GridView>
    </main>
</asp:Content>